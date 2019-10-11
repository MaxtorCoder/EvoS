using System.Numerics;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    public class BoxCollider : Component
    {
        public SerializedComponent SerializedGameObject;
        public SerializedComponent Material;
        public bool IsTrigger;
        public bool Enabled;
        public Vector3 Size;
        public Vector3 Center;

        public BoxCollider()
        {
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            stream.AlignTo();
            SerializedGameObject = new SerializedComponent(assetFile, stream);
            Material = new SerializedComponent(assetFile, stream);
            IsTrigger = stream.ReadBoolean();
            Enabled = stream.ReadBoolean();
            stream.AlignTo();
            Size = stream.ReadVector3();
            Center = stream.ReadVector3();
        }

        public override string ToString()
        {
            return $"{nameof(BoxCollider)}(" +
//                   $"{nameof(SerializedGameObject)}: {SerializedGameObject}, " +
                   $"{nameof(Material)}: {Material}, " +
                   $"{nameof(IsTrigger)}: {IsTrigger}, " +
                   $"{nameof(Enabled)}: {Enabled}, " +
                   $"{nameof(Size)}: {Size}, " +
                   $"{nameof(Center)}: {Center}" +
                   ")";
        }
    }
}
