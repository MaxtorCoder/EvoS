using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.NetworkBehaviours
{
    [SerializedMonoBehaviour("AbilityData")]
    public class SerializedAbilityData : NetworkBehaviour
    {
        public bool IgnoreForLocalization { get; set; }
        public SerializedComponent Ability0 { get; set; }
        public string SpritePath0 { get; set; }
        public SerializedComponent Ability1 { get; set; }
        public string SpritePath1 { get; set; }
        public SerializedComponent Ability2 { get; set; }
        public string SpritePath2 { get; set; }
        public SerializedComponent Ability3 { get; set; }
        public string SpritePath3 { get; set; }
        public SerializedComponent Ability4 { get; set; }
        public string SpritePath4 { get; set; }
        public SerializedComponent Ability5 { get; set; }
        public string SpritePath5 { get; set; }
        public SerializedComponent Ability6 { get; set; }
        public string SpritePath6 { get; set; }
        public SerializedVector<SerializedComponent> BotDifficultyAbilityModSets { get; set; }
        public SerializedVector<SerializedComponent> CompsToInspectInAbilityKitInspector { get; set; }
        public string SequenceDirNameOverride { get; set; }
        public string AbilitySetupNotes { get; set; }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            stream.AlignTo();

            IgnoreForLocalization = stream.ReadBoolean();
            stream.AlignTo();

            Ability0 = new SerializedComponent();
            Ability0.DeserializeAsset(assetFile,stream);
            SpritePath0 = stream.ReadString32();
            Ability1 = new SerializedComponent();
            Ability1.DeserializeAsset(assetFile,stream);
            SpritePath1 = stream.ReadString32();
            Ability2 = new SerializedComponent();
            Ability2.DeserializeAsset(assetFile,stream);
            SpritePath2 = stream.ReadString32();
            Ability3 = new SerializedComponent();
            Ability3.DeserializeAsset(assetFile,stream);
            SpritePath3 = stream.ReadString32();
            Ability4 = new SerializedComponent();
            Ability4.DeserializeAsset(assetFile,stream);
            SpritePath4 = stream.ReadString32();
            Ability5 = new SerializedComponent();
            Ability5.DeserializeAsset(assetFile,stream);
            SpritePath5 = stream.ReadString32();
            Ability6 = new SerializedComponent();
            Ability6.DeserializeAsset(assetFile,stream);
            SpritePath6 = stream.ReadString32();
            BotDifficultyAbilityModSets = new SerializedVector<SerializedComponent>();
            BotDifficultyAbilityModSets.DeserializeAsset(assetFile, stream);
            CompsToInspectInAbilityKitInspector = new SerializedVector<SerializedComponent>();
            CompsToInspectInAbilityKitInspector.DeserializeAsset(assetFile, stream);
            SequenceDirNameOverride = stream.ReadString32();
            AbilitySetupNotes = stream.ReadString32();
        }

        public override string ToString()
        {
            return $"{nameof(SerializedAbilityData)}(" +
                   $"{nameof(IgnoreForLocalization)}: {IgnoreForLocalization}, " +
                   $"{nameof(Ability0)}: {Ability0}, " +
                   $"{nameof(SpritePath0)}: {SpritePath0}, " +
                   $"{nameof(Ability1)}: {Ability1}, " +
                   $"{nameof(SpritePath1)}: {SpritePath1}, " +
                   $"{nameof(Ability2)}: {Ability2}, " +
                   $"{nameof(SpritePath2)}: {SpritePath2}, " +
                   $"{nameof(Ability3)}: {Ability3}, " +
                   $"{nameof(SpritePath3)}: {SpritePath3}, " +
                   $"{nameof(Ability4)}: {Ability4}, " +
                   $"{nameof(SpritePath4)}: {SpritePath4}, " +
                   $"{nameof(Ability5)}: {Ability5}, " +
                   $"{nameof(SpritePath5)}: {SpritePath5}, " +
                   $"{nameof(Ability6)}: {Ability6}, " +
                   $"{nameof(SpritePath6)}: {SpritePath6}, " +
                   $"{nameof(BotDifficultyAbilityModSets)}: {BotDifficultyAbilityModSets.Count} entries, " +
                   $"{nameof(CompsToInspectInAbilityKitInspector)}: {CompsToInspectInAbilityKitInspector.Count} entries, " +
                   $"{nameof(SequenceDirNameOverride)}: {SequenceDirNameOverride}, " +
                   $"{nameof(AbilitySetupNotes)}: {AbilitySetupNotes}" +
                   ")";
        }
    }
}
