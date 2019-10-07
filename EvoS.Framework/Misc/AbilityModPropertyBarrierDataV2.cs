using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("AbilityModPropertyBarrierDataV2")]
    public class AbilityModPropertyBarrierDataV2 : ISerializedItem
    {
        public ModOp operation;
        public BarrierModData barrierModData;

        public AbilityModPropertyBarrierDataV2()
        {
        }

        public AbilityModPropertyBarrierDataV2(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            operation = (ModOp) stream.ReadInt32(); 
            
            barrierModData = new BarrierModData(assetFile, stream);
        }

        public override string ToString()
        {
            return $"{nameof(AbilityModPropertyBarrierDataV2)}>(" +
                   $"{nameof(operation)}: {operation}, " +
                   $"{nameof(barrierModData)}: {barrierModData}, " +
                   ")";
        }

        public enum ModOp
        {
            Ignore,
            UseMods,
        }
    }
}
