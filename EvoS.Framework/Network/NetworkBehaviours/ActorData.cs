using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.NetworkBehaviours
{
    [SerializedMonoBehaviour("ActorData")]
    public class ActorData : NetworkBehaviour
    {
        public int PlayerIndex { get; set; }
        public SerializedComponent PlayerData { get; set; }
        public int CharacterType { get; set; }
        public SerializedComponent TauntCamSetData { get; set; }
        public string AliveHudIconResourceString { get; set; }
        public string DeadHudIconResourceString { get; set; }
        public string ScreenIndicatorIconResourceString { get; set; }
        public string ScreenIndicatorBwIconResourceString { get; set; }
        public SerializedPrefabResourceLink ActionSkinPrefabLink { get; set; }
        public CharacterVisualInfo VisualInfo { get; set; }
        public CharacterAbilityVfxSwapInfo AbilityVfxSwapInfo { get; set; }
        public CharacterModInfo SelectedMods { get; set; }
        public CharacterAbilityVfxSwapInfo SelectedAbilityVfxSwaps { get; set; }
        public CharacterCardInfo SelectedCards { get; set; }
        public SerializedVector<int> AvailableTauntIds { get; set; }
        public int MaxHitPoints { get; set; }
        public int HpPointRegen { get; set; }
        public int MaxTechPoints { get; set; }
        public int TechPointRegen { get; set; }
        public int TechPointsOnSpawn { get; set; }
        public int TechPointsOnRespawn { get; set; }
        public float MaxHorizontalMovement { get; set; }
        public float PostAbilityHorizontalMovement { get; set; }
        public int MaxVerticalUpwardMovement { get; set; }
        public int MaxVerticalDownwardMovement { get; set; }
        public float SightRange { get; set; }
        public float RunSpeed { get; set; }
        public float VaultSpeed { get; set; }
        public float KnockbackSpeed { get; set; }
        public string OnDeathAudioEvent { get; set; }
        public SerializedVector<SerializedComponent> AdditionalNetworkObjectsToRegister { get; set; }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            stream.AlignTo();
            PlayerIndex = stream.ReadInt32();
            PlayerData = new SerializedComponent();
            PlayerData.DeserializeAsset(assetFile, stream);
            CharacterType = stream.ReadInt32();
            TauntCamSetData = new SerializedComponent();
            TauntCamSetData.DeserializeAsset(assetFile, stream);
            AliveHudIconResourceString = stream.ReadString32();
            DeadHudIconResourceString = stream.ReadString32();
            ScreenIndicatorIconResourceString = stream.ReadString32();
            ScreenIndicatorBwIconResourceString = stream.ReadString32();
            ActionSkinPrefabLink = new SerializedPrefabResourceLink();
            ActionSkinPrefabLink.DeserializeAsset(assetFile, stream);
            VisualInfo = new CharacterVisualInfo();
            VisualInfo.DeserializeAsset(assetFile, stream);
            AbilityVfxSwapInfo = new CharacterAbilityVfxSwapInfo();
            AbilityVfxSwapInfo.DeserializeAsset(assetFile, stream);
            SelectedMods = new CharacterModInfo();
            SelectedMods.DeserializeAsset(assetFile, stream);
            SelectedAbilityVfxSwaps = new CharacterAbilityVfxSwapInfo();
            SelectedAbilityVfxSwaps.DeserializeAsset(assetFile, stream);
            SelectedCards = new CharacterCardInfo();
            SelectedCards.DeserializeAsset(assetFile, stream);
            AvailableTauntIds = new SerializedVector<int>();
            AvailableTauntIds.DeserializeAsset(assetFile, stream);
            MaxHitPoints = stream.ReadInt32();
            HpPointRegen = stream.ReadInt32();
            MaxTechPoints = stream.ReadInt32();
            TechPointRegen = stream.ReadInt32();
            TechPointsOnSpawn = stream.ReadInt32();
            TechPointsOnRespawn = stream.ReadInt32();
            MaxHorizontalMovement = stream.ReadSingle();
            PostAbilityHorizontalMovement = stream.ReadSingle();
            MaxVerticalUpwardMovement = stream.ReadInt32();
            MaxVerticalDownwardMovement = stream.ReadInt32();
            SightRange = stream.ReadSingle();
            RunSpeed = stream.ReadSingle();
            VaultSpeed = stream.ReadSingle();
            KnockbackSpeed = stream.ReadSingle();
            OnDeathAudioEvent = stream.ReadString32();
            AdditionalNetworkObjectsToRegister = new SerializedVector<SerializedComponent>();
            AdditionalNetworkObjectsToRegister.DeserializeAsset(assetFile, stream);
        }

        public override string ToString()
        {
            return $"{nameof(ActorData)}(" +
                   $"{nameof(PlayerIndex)}: {PlayerIndex}, " +
                   $"{nameof(PlayerData)}: {PlayerData}, " +
                   $"{nameof(CharacterType)}: {CharacterType}, " +
                   $"{nameof(TauntCamSetData)}: {TauntCamSetData}, " +
                   $"{nameof(AliveHudIconResourceString)}: {AliveHudIconResourceString}, " +
                   $"{nameof(DeadHudIconResourceString)}: {DeadHudIconResourceString}, " +
                   $"{nameof(ScreenIndicatorIconResourceString)}: {ScreenIndicatorIconResourceString}, " +
                   $"{nameof(ScreenIndicatorBwIconResourceString)}: {ScreenIndicatorBwIconResourceString}, " +
                   $"{nameof(ActionSkinPrefabLink)}: {ActionSkinPrefabLink}, " +
                   $"{nameof(VisualInfo)}: {VisualInfo}, " +
                   $"{nameof(AbilityVfxSwapInfo)}: {AbilityVfxSwapInfo}, " +
                   $"{nameof(SelectedMods)}: {SelectedMods}, " +
                   $"{nameof(SelectedAbilityVfxSwaps)}: {SelectedAbilityVfxSwaps}, " +
                   $"{nameof(SelectedCards)}: {SelectedCards}, " +
                   $"{nameof(AvailableTauntIds)}: {AvailableTauntIds}, " +
                   $"{nameof(MaxHitPoints)}: {MaxHitPoints}, " +
                   $"{nameof(HpPointRegen)}: {HpPointRegen}, " +
                   $"{nameof(MaxTechPoints)}: {MaxTechPoints}, " +
                   $"{nameof(TechPointRegen)}: {TechPointRegen}, " +
                   $"{nameof(TechPointsOnSpawn)}: {TechPointsOnSpawn}, " +
                   $"{nameof(TechPointsOnRespawn)}: {TechPointsOnRespawn}, " +
                   $"{nameof(MaxHorizontalMovement)}: {MaxHorizontalMovement}, " +
                   $"{nameof(PostAbilityHorizontalMovement)}: {PostAbilityHorizontalMovement}, " +
                   $"{nameof(MaxVerticalUpwardMovement)}: {MaxVerticalUpwardMovement}, " +
                   $"{nameof(MaxVerticalDownwardMovement)}: {MaxVerticalDownwardMovement}, " +
                   $"{nameof(SightRange)}: {SightRange}, " +
                   $"{nameof(RunSpeed)}: {RunSpeed}, " +
                   $"{nameof(VaultSpeed)}: {VaultSpeed}, " +
                   $"{nameof(KnockbackSpeed)}: {KnockbackSpeed}, " +
                   $"{nameof(OnDeathAudioEvent)}: {OnDeathAudioEvent}, " +
                   $"{nameof(AdditionalNetworkObjectsToRegister)}: {AdditionalNetworkObjectsToRegister.Count} entries" +
                   ")";
        }
    }
}
