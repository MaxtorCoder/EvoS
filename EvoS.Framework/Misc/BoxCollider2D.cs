using System.Numerics;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;

namespace EvoS.Framework.Misc
{
    public class BoxCollider2D : ISerializedItem
    {
        public SerializedComponent SerializedGameObject;
        public bool Enabled;
        public float Density;
        public SerializedComponent Material;
        public bool IsTrigger;
        public bool UsedByEffector;
        public Vector2 Offset;
        public Vector2 Size;

        public BoxCollider2D()
        {
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            stream.AlignTo();
            SerializedGameObject = new SerializedComponent(assetFile, stream);
            Enabled = stream.ReadBoolean();
            stream.AlignTo();
            Density = stream.ReadSingle();
            Material = new SerializedComponent(assetFile, stream);
            IsTrigger = stream.ReadBoolean();
            UsedByEffector = stream.ReadBoolean();
            stream.AlignTo();
            Offset = stream.ReadVector2();
            Size = stream.ReadVector2();
        }

        public override string ToString()
        {
            return $"{nameof(BoxCollider2D)}(" +
//                   $"{nameof(SerializedGameObject)}: {SerializedGameObject}, " +
                   $"{nameof(Enabled)}: {Enabled}, " +
                   $"{nameof(Density)}: {Density}, " +
                   $"{nameof(Material)}: {Material}, " +
                   $"{nameof(IsTrigger)}: {IsTrigger}, " +
                   $"{nameof(UsedByEffector)}: {UsedByEffector}, " +
                   $"{nameof(Offset)}: {Offset}, " +
                   $"{nameof(Size)}: {Size}" +
                   ")";
        }
    }
}
