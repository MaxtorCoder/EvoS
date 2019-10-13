using System.Collections.Generic;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;

namespace EvoS.Framework.Misc
{
    [SerializedMonoBehaviour("Passive_StickAndMove")]
    public class PassiveStickAndMove : Passive
    {
        public int DamageToAdvanceCooldown;
//        public List<Ability> AbilitiesToAdvanceCooldown;
        public List<SerializedMonoBehaviour> AbilitiesToAdvanceCooldown;

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            base.DeserializeAsset(assetFile, stream);

            DamageToAdvanceCooldown = stream.ReadInt32();
            AbilitiesToAdvanceCooldown = new SerializedVector<SerializedMonoBehaviour>(assetFile, stream);
        }

        public override string ToString()
        {
            return $"{nameof(PassiveStickAndMove)}(" +
                   $"{nameof(DamageToAdvanceCooldown)}: {DamageToAdvanceCooldown}, " +
                   $"{nameof(AbilitiesToAdvanceCooldown)}: {AbilitiesToAdvanceCooldown}" +
                   ")";
        }
    }
}
