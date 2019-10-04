using System;
using System.Collections.Generic;
using System.IO;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;

namespace EvoS.Framework.Assets
{
    public class AssetFile : IDisposable
    {
        private StreamReader _stream;
        public SerializedFileHeader Header { get; set; }
        public SerializedFileMetadata Metadata { get; set; }
        private List<AssetFile> _fileMap = new List<AssetFile>();

        private static Dictionary<int, Type> _unityTypeMap = new Dictionary<int, Type>
        {
            {1, typeof(SerializedGameObject)},
            {2, typeof(SerializedComponent)},
            {114, typeof(SerializedMonoBehaviour)},
            {115, typeof(SerializedMonoScript)},
        };
        private static Dictionary<string, Type> _scriptTypeMap = new Dictionary<string, Type>
        {
            {"NetworkIdentity", typeof(SerializedNetworkIdentity)},
        };

        public AssetFile(string filePath)
        {
            _fileMap.Add(this);

            _stream = new StreamReader(filePath);
            Header = new SerializedFileHeader(_stream);
            Metadata = new SerializedFileMetadata(_stream);

            LoadExternalReferences(filePath);
        }

        public IEnumerable<ObjectInfoEntry> GetObjectInfosByType(TypeEntry typeEntry)
        {
            foreach (var objInfo in Metadata.ObjectInfoTable.Entries)
            {
                if (objInfo.TypeId == typeEntry.Index)
                {
                    yield return objInfo;
                }
            }
        }

        private void LoadExternalReferences(string filePath)
        {
            foreach (var externalReference in Metadata.ExternalReferencesTable.Entries)
            {
                var fileName = externalReference.FileName;
                if (fileName.StartsWith("library/"))
                {
                    fileName = "resources/" + fileName.Substring(8);
                }

                var path = Path.Join(Path.GetDirectoryName(filePath), fileName);

                _fileMap.Add(new AssetFile(path));
            }
        }

        public void Dispose()
        {
            _stream?.Dispose();
        }

        public TypeEntry FindTypeById(int typeId)
        {
            foreach (var typeEntry in Metadata.TypeTree.BaseClasses)
            {
                if (typeEntry.TypeId == typeId)
                {
                    return typeEntry;
                }
            }

            return null;
        }
        
        public SerializedMonoChildBase ReadMonoScriptChild(SerializedMonoScript script)
        {
            if (!_scriptTypeMap.ContainsKey(script.ClassName))
            {
                return null;
            }

            var child = (SerializedMonoChildBase) Activator.CreateInstance(_scriptTypeMap[script.ClassName]);
            child.Deserialize(this, _stream);

            return child;
        }

        public ISerializedItem ReadObject(SerializedComponent objInfo)
        {
            return ReadObject(objInfo.PathId, objInfo.FileId);
        }

        public ISerializedItem ReadObject(ObjectInfoEntry objInfo, int fileId = 0)
        {
            return ReadObject(objInfo.PathId, fileId);
        }

        public ISerializedItem ReadObject(long pathId, int fileId = 0, bool restorePos = true)
        {
            if (pathId == 0)
            {
                if (fileId != 0)
                {
                    throw new ArgumentException($"pathId was 0, but fileId was {fileId}");
                }
                return null;
            }
            
            var assetFile = _fileMap[fileId];
            var savedPos = assetFile._stream.Position;

            var objInfo = assetFile.Metadata.ObjectInfoTable.EntryMap[pathId];

            var objType = assetFile.Metadata.TypeTree.BaseClasses[objInfo.TypeId];

            assetFile._stream.Position = assetFile.Header.DataOffset + objInfo.ByteStart;
            var endPos = assetFile.Header.DataOffset + objInfo.ByteStart + objInfo.ByteSize;
            if (_unityTypeMap.ContainsKey(objType.TypeId))
            {
                var obj = (ISerializedItem) Activator.CreateInstance(_unityTypeMap[objType.TypeId]);
                obj.Deserialize(assetFile, assetFile._stream);

                if (assetFile._stream.Position < endPos)
                {
//                    Log.Print(LogType.Warning,
//                        $"Didn't fully read {obj.GetType().Name}, {assetFile._stream.Position}/{endPos}");
                }
                else if (assetFile._stream.Position > endPos)
                {
                    throw new IndexOutOfRangeException(
                        $"Read past the end of {obj.GetType().Name}, {assetFile._stream.Position}/{endPos}");
                }

                assetFile._stream.Position = savedPos;
                return obj;
            }
            else
            {
//                Log.Print(LogType.Warning, $"No type mapping for {objType} {objInfo}");
            }

            if (restorePos)
            {
                assetFile._stream.Position = savedPos;
            }

            return null;
        }
    }
}
