using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Game;
using EvoS.Framework.Logging;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.NetworkBehaviours
{
    [SerializedMonoBehaviour("AbilityData")]
    public class AbilityData : NetworkBehaviour
    {
        private const int kListm_cooldownsSync = -1695732229;
        private const int kListm_consumedStockCount = 1389109193;
        private const int kListm_stockRefreshCountdowns = -1016879281;
        private const int kListm_currentCardIds = 384100343;
        private const int kCmdCmdClearCooldowns = 36238543;
        private const int kCmdCmdRefillStocks = 1108895015;

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

        private SyncListInt _cooldownsSync = new SyncListInt();
        private SyncListInt _consumedStockCount = new SyncListInt();
        private SyncListInt _stockRefreshCountdowns = new SyncListInt();
        private SyncListInt _currentCardIds = new SyncListInt();
        private ActionType _selectedActionForTargeting = ActionType.INVALID_ACTION;

        public SyncListInt CooldownsSync => _cooldownsSync;
        public SyncListInt ConsumedStockCount => _consumedStockCount;
        public SyncListInt StockRefreshCountdowns => _stockRefreshCountdowns;
        public SyncListInt CurrentCardIds => _currentCardIds;
        public ActionType SelectedActionForTargeting => _selectedActionForTargeting;

        static AbilityData()
        {
            RegisterSyncListDelegate(typeof(AbilityData), kListm_cooldownsSync, InvokeSyncListm_cooldownsSync);
            RegisterSyncListDelegate(typeof(AbilityData), kListm_consumedStockCount,
                InvokeSyncListm_consumedStockCount);
            RegisterSyncListDelegate(typeof(AbilityData), kListm_stockRefreshCountdowns,
                InvokeSyncListm_stockRefreshCountdowns);
            RegisterSyncListDelegate(typeof(AbilityData), kListm_currentCardIds, InvokeSyncListm_currentCardIds);
        }

        public override void Awake()
        {
            _cooldownsSync.InitializeBehaviour(this, kListm_cooldownsSync);
            _consumedStockCount.InitializeBehaviour(this, kListm_consumedStockCount);
            _stockRefreshCountdowns.InitializeBehaviour(this, kListm_stockRefreshCountdowns);
            _currentCardIds.InitializeBehaviour(this, kListm_currentCardIds);
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            stream.AlignTo();

            IgnoreForLocalization = stream.ReadBoolean();
            stream.AlignTo();

            Ability0 = new SerializedComponent();
            Ability0.DeserializeAsset(assetFile, stream);
            SpritePath0 = stream.ReadString32();
            Ability1 = new SerializedComponent();
            Ability1.DeserializeAsset(assetFile, stream);
            SpritePath1 = stream.ReadString32();
            Ability2 = new SerializedComponent();
            Ability2.DeserializeAsset(assetFile, stream);
            SpritePath2 = stream.ReadString32();
            Ability3 = new SerializedComponent();
            Ability3.DeserializeAsset(assetFile, stream);
            SpritePath3 = stream.ReadString32();
            Ability4 = new SerializedComponent();
            Ability4.DeserializeAsset(assetFile, stream);
            SpritePath4 = stream.ReadString32();
            Ability5 = new SerializedComponent();
            Ability5.DeserializeAsset(assetFile, stream);
            SpritePath5 = stream.ReadString32();
            Ability6 = new SerializedComponent();
            Ability6.DeserializeAsset(assetFile, stream);
            SpritePath6 = stream.ReadString32();
            BotDifficultyAbilityModSets = new SerializedVector<SerializedComponent>();
            BotDifficultyAbilityModSets.DeserializeAsset(assetFile, stream);
            CompsToInspectInAbilityKitInspector = new SerializedVector<SerializedComponent>();
            CompsToInspectInAbilityKitInspector.DeserializeAsset(assetFile, stream);
            SequenceDirNameOverride = stream.ReadString32();
            AbilitySetupNotes = stream.ReadString32();
        }

        public override bool OnSerialize(NetworkWriter writer, bool forceAll)
        {
            if (forceAll)
            {
                SyncListInt.WriteInstance(writer, _cooldownsSync);
                SyncListInt.WriteInstance(writer, _consumedStockCount);
                SyncListInt.WriteInstance(writer, _stockRefreshCountdowns);
                SyncListInt.WriteInstance(writer, _currentCardIds);
                writer.Write((int) _selectedActionForTargeting);
                return true;
            }

            bool flag = false;
            if (((int) syncVarDirtyBits & 1) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                SyncListInt.WriteInstance(writer, _cooldownsSync);
            }

            if (((int) syncVarDirtyBits & 2) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                SyncListInt.WriteInstance(writer, _consumedStockCount);
            }

            if (((int) syncVarDirtyBits & 4) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                SyncListInt.WriteInstance(writer, _stockRefreshCountdowns);
            }

            if (((int) syncVarDirtyBits & 8) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                SyncListInt.WriteInstance(writer, _currentCardIds);
            }

            if (((int) syncVarDirtyBits & 16) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.Write((int) _selectedActionForTargeting);
            }

            if (!flag)
                writer.WritePackedUInt32(syncVarDirtyBits);
            return flag;
        }

        public override void OnDeserialize(NetworkReader reader, bool initialState)
        {
            if (initialState)
            {
                SyncListInt.ReadReference(reader, _cooldownsSync);
                SyncListInt.ReadReference(reader, _consumedStockCount);
                SyncListInt.ReadReference(reader, _stockRefreshCountdowns);
                SyncListInt.ReadReference(reader, _currentCardIds);
                _selectedActionForTargeting = (ActionType) reader.ReadInt32();
            }
            else
            {
                int num = (int) reader.ReadPackedUInt32();
                if ((num & 1) != 0)
                    SyncListInt.ReadReference(reader, _cooldownsSync);
                if ((num & 2) != 0)
                    SyncListInt.ReadReference(reader, _consumedStockCount);
                if ((num & 4) != 0)
                    SyncListInt.ReadReference(reader, _stockRefreshCountdowns);
                if ((num & 8) != 0)
                    SyncListInt.ReadReference(reader, _currentCardIds);
                if ((num & 16) == 0)
                    return;
                _selectedActionForTargeting = (ActionType) reader.ReadInt32();
            }
        }

        protected static void InvokeSyncListm_cooldownsSync(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!EvoSGameConfig.NetworkIsClient)
                Log.Print(LogType.Error, "SyncList _cooldownsSync called on server.");
            else
                ((AbilityData) obj)._cooldownsSync.HandleMsg(reader);
        }

        protected static void InvokeSyncListm_consumedStockCount(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!EvoSGameConfig.NetworkIsClient)
                Log.Print(LogType.Error, "SyncList _consumedStockCount called on server.");
            else
                ((AbilityData) obj)._consumedStockCount.HandleMsg(reader);
        }

        protected static void InvokeSyncListm_stockRefreshCountdowns(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!EvoSGameConfig.NetworkIsClient)
                Log.Print(LogType.Error, "SyncList _stockRefreshCountdowns called on server.");
            else
                ((AbilityData) obj)._stockRefreshCountdowns.HandleMsg(reader);
        }

        protected static void InvokeSyncListm_currentCardIds(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!EvoSGameConfig.NetworkIsClient)
                Log.Print(LogType.Error, "SyncList _currentCardIds called on server.");
            else
                ((AbilityData) obj)._currentCardIds.HandleMsg(reader);
        }

        public override string ToString()
        {
            return $"{nameof(AbilityData)}(" +
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

        public enum ActionType
        {
            INVALID_ACTION = -1, // 0xFFFFFFFF
            ABILITY_0 = 0,
            ABILITY_1 = 1,
            ABILITY_2 = 2,
            ABILITY_3 = 3,
            ABILITY_4 = 4,
            ABILITY_5 = 5,
            ABILITY_6 = 6,
            CARD_0 = 7,
            CARD_1 = 8,
            CARD_2 = 9,
            CHAIN_0 = 10, // 0x0000000A
            CHAIN_1 = 11, // 0x0000000B
            CHAIN_2 = 12, // 0x0000000C
            CHAIN_3 = 13, // 0x0000000D
            NUM_ACTIONS = 14 // 0x0000000E
        }
    }
}
