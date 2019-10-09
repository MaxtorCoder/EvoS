using System.Numerics;
using Newtonsoft.Json;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;

namespace EvoS.Framework.Network.Unity
{
    public class Transform : Component, ISerializedItem
    {
        public SerializedComponent serializedGameObject;
        [JsonIgnore] public Vector3 position = Vector3.Zero;
        public Vector3 localPosition = Vector3.Zero;
        [JsonIgnore] public Quaternion rotation = Quaternion.Identity;
        [JsonIgnore] public Quaternion localRotation { get; set; } = Quaternion.Identity;
        [JsonIgnore] public Vector3 localScale { get; set; } = Vector3.One;
        [JsonIgnore] public SerializedVector<Transform> children { get; set; }
        [JsonIgnore] public Transform father { get; set; }

        public Transform()
        {
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            stream.AlignTo();
            serializedGameObject = new SerializedComponent(assetFile, stream);
            localRotation = stream.ReadQuaternion();
            localPosition = stream.ReadVector3();
            localScale = stream.ReadVector3();
            children = new SerializedVector<Transform>(assetFile, stream);
            father = (Transform) new SerializedComponent(assetFile, stream).LoadValue();
        }

        public override string ToString()
        {
            return $"{nameof(Transform)}(" +
//                   $"{nameof(serializedGameObject)}: {serializedGameObject}, " +
                   $"{nameof(localPosition)}: {localPosition}, " +
                   $"{nameof(localRotation)}: {localRotation}, " +
                   $"{nameof(localScale)}: {localScale}, " +
                   $"{nameof(children)}: {children.Count} entries, " +
                   $"{nameof(father)}: {(father != null ? "Exists" : "null")}" +
                   ")";
        }
    }
}
