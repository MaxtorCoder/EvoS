using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Assets.Serialized.Behaviours
{
    [SerializedMonoBehaviour("Passive")]
    public class SerializedPassive : ISerializedItem
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

        public virtual void DeserializeAsset(AssetFile assetFile, StreamReader stream)
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
