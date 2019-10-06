using System;
using System.Collections.Generic;

namespace EvoS.Framework.Assets.Serialized.Behaviours
{
    [SerializedMonoBehaviour("Passive_StickAndMove")]
    public class SerializedPassive_StickAndMove : SerializedPassive
    {
        public int DamageToAdvanceCooldown;
//        public List<Ability> AbilitiesToAdvanceCooldown;
        public List<SerializedComponent> AbilitiesToAdvanceCooldown;

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            base.DeserializeAsset(assetFile, stream);

            DamageToAdvanceCooldown = stream.ReadInt32();
            AbilitiesToAdvanceCooldown = new SerializedVector<SerializedComponent>(assetFile, stream);
        }

        public override string ToString()
        {
            return $"{nameof(SerializedPassive_StickAndMove)}(" +
                   $"{nameof(DamageToAdvanceCooldown)}: {DamageToAdvanceCooldown}, " +
                   $"{nameof(AbilitiesToAdvanceCooldown)}: {AbilitiesToAdvanceCooldown}" +
                   ")";
        }
    }
}
