using EvoS.Framework.Misc;
using EvoS.Framework.Network.Unity;
using Newtonsoft.Json;

namespace EvoS.Framework.Assets.Serialized
{
    public class SerializedMonoScript : ISerializedItem
    {
        public string Name { get; set; }
        public int ExecutionOrder { get; set; }
        [JsonIgnore]
        public NetworkHash128 PropertiesHash { get; set; }
        public string ClassName { get; set; }
        public string Namespace { get; set; }
        public string AssemblyName { get; set; }
        public bool IsEditorScript { get; set; }
        public string QualifiedName => $"{Namespace}.{ClassName}";

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            Name = stream.ReadString32();
            ExecutionOrder = stream.ReadInt32();
            PropertiesHash = stream.ReadNetworkHash128();
            ClassName = stream.ReadString32();
            Namespace = stream.ReadString32();
            AssemblyName = stream.ReadString32();
            IsEditorScript = stream.ReadBoolean();
        }

        public override string ToString()
        {
            return $"{nameof(SerializedMonoScript)}(" +
                   (!Name.Equals(ClassName) ? $"{nameof(Name)}: {Name}, " : "") +
                   $"{nameof(ExecutionOrder)}: {ExecutionOrder}, " +
                   $"{nameof(PropertiesHash)}: {PropertiesHash}, " +
                   $"{nameof(AssemblyName)}: {AssemblyName}, " +
                   (!Namespace.IsNullOrEmpty() ? $"{nameof(Namespace)}: {Namespace}, " : "") +
                   $"{nameof(ClassName)}: {ClassName}, " +
                   $"{nameof(IsEditorScript)}: {IsEditorScript}" +
                   ")";
        }
    }
}
