using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Assets.Serialized.Behaviours
{
    [SerializedMonoBehaviour("PassiveData")]
    public class PassiveData : MonoBehaviour
    {
        public SerializedVector<SerializedComponent> Passives { get; set; }
        public string ToolTip { get; set; }
        public string ToolTipTitle { get; set; }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            Passives = new SerializedVector<SerializedComponent>();
            Passives.DeserializeAsset(assetFile, stream);
            ToolTip = stream.ReadString32();
            ToolTipTitle = stream.ReadString32();
        }

        public override string ToString()
        {
            return $"{nameof(PassiveData)}(" +
                   $"{nameof(Passives)}: {Passives}, " +
                   $"{nameof(ToolTip)}: {ToolTip}, " +
                   $"{nameof(ToolTipTitle)}: {ToolTipTitle}" +
                   ")";
        }
    }
}
