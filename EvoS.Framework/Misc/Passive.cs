using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    [SerializedMonoBehaviour("Passive")]
    public class Passive : MonoBehaviour
    {
        public string PassiveName { get; set; }
        public SerializedComponent SequencePrefab { get; set; }
//        public SerializedVector<AbilityStatMod> PassiveStatMods { get; set; }
        public SerializedVector<SerializedComponent> PassiveStatMods { get; set; }
        public SerializedVector<StatusType> PassiveStatusChanges { get; set; }
//        public SerializedVector<EmptyTurnData> OnEmptyTurn { get; set; }
        public SerializedVector<SerializedComponent> OnEmptyTurn { get; set; }
//        public SerializedVector<PassiveEventData> EventResponses { get; set; }
        public SerializedVector<SerializedComponent> EventResponses { get; set; }
//        public SerializedVector<StandardActorEffectData> EffectsOnEveryTurn { get; set; }
        public SerializedVector<SerializedComponent> EffectsOnEveryTurn { get; set; }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            PassiveName = stream.ReadString32();
            SequencePrefab = new SerializedComponent();
            SequencePrefab.DeserializeAsset(assetFile, stream);
//            PassiveStatMods = new SerializedVector<AbilityStatMod>();
            PassiveStatMods = new SerializedVector<SerializedComponent>();
            PassiveStatMods.DeserializeAsset(assetFile, stream);
            PassiveStatusChanges = new SerializedVector<StatusType>();
            PassiveStatusChanges.DeserializeAsset(assetFile, stream);
//            OnEmptyTurn = new SerializedVector<EmptyTurnData>();
            OnEmptyTurn = new SerializedVector<SerializedComponent>();
            OnEmptyTurn.DeserializeAsset(assetFile, stream);
//            EventResponses = new SerializedVector<PassiveEventData>();
            EventResponses = new SerializedVector<SerializedComponent>();
            EventResponses.DeserializeAsset(assetFile, stream);
//            EffectsOnEveryTurn = new SerializedVector<StandardActorEffectData>();
            EffectsOnEveryTurn = new SerializedVector<SerializedComponent>();
            EffectsOnEveryTurn.DeserializeAsset(assetFile, stream);
        }
    }
}
