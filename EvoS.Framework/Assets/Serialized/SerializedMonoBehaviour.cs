using System;

namespace EvoS.Framework.Assets.Serialized
{
    public class SerializedMonoBehaviour : ISerializedItem
    {
        public SerializedComponent GameObject { get; set; }
        public bool Enabled { get; set; }
        public SerializedMonoScript Script { get; set; }
        [Obsolete("This is probably not what you want, see Script.ClassName")]
        public string Name { get; set; }
        public SerializedMonoChildBase Child { get; set; }

        public void Deserialize(AssetFile assetFile, StreamReader stream)
        {
            GameObject = new SerializedComponent();
            GameObject.Deserialize(assetFile, stream);
            Enabled = stream.ReadBoolean();
            stream.AlignTo();
            var script = new SerializedComponent();
            script.Deserialize(assetFile, stream);
            Name = stream.ReadString32();

            Script = (SerializedMonoScript) assetFile.ReadObject(script);
            if (Script != null)
            {
                Child = assetFile.ReadMonoScriptChild(Script);
            }
        }

        public override string ToString()
        {
            return $"{nameof(SerializedMonoBehaviour)}(" +
//                   $"{nameof(GameObject)}: {GameObject}, " +
//                   $"{nameof(Enabled)}: {Enabled}, " +
//                   $"{nameof(Name)}: {Name}, " +
                   (Child == null ? $"{nameof(Script)}: {Script}" : "") +
                   (Child != null ? $"{nameof(Child)}: {Child}" : "") +
                   ")";
        }
    }
}
