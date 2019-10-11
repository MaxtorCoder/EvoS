using System.Collections.Generic;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Misc;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.NetworkBehaviours
{
    [SerializedMonoBehaviour("ActorData")]
    public class ActorData : NetworkBehaviour
    {
        public static int s_invalidActorIndex = -1;
        private static int kCmdCmdSetPausedForDebugging = 1605310550;
        private SerializeHelper SerializeHelper = new SerializeHelper();

        public string DisplayName { get; private set; } = "Connecting Player";
        public int ActorIndex { get; private set; }
        public Team Team { get; private set; }
        public int LastVisibleTurnToClient { get; private set; }
        public BoardSquare ServerLastKnownPosSquare { get; private set; }
        public float RemainingHorizontalMovement { get;private  set; }
        public float RemainingMovementWithQueuedAbility { get;private  set; }
        public bool QueuedMovementAllowsAbility { get;private  set; }
        public bool QueuedMovementRequest { get; private set; }
        public bool QueuedChaseRequest { get; private set; }
        public ActorData QueuedChaseTarget { get; private set; }
        public int HitPoints { get; private set; } = 1;
        public int UnresolvedDamage { get; private set; }
        public int UnresolvedHealing { get; private set; }
        public int TechPoints { get; private set; }
        public int ReservedTechPoints { get; private set; }
        public int UnresolvedTechPointGain { get; private set; }
        public int UnresolvedTechPointLoss { get; private set; }
        public int AbsorbPoints { get; private set; }
        public int MechanicPoints { get; private set; }
        public int ServerExpectedHoTTotal { get; private set; }
        public int ServerExpectedHoTThisTurn { get; private set; }
        public int LastDeathTurn { get; private set; }
        public int LastSpawnTurn { get; private set; } = -1;
        public int NextRespawnTurn { get; private set; } = -1;
        public int SpawnerId { get; private set; }
        public readonly List<ActorData> LineOfSightVisibleExceptions = new List<ActorData>();
        public bool HasBotController { get; private set; }
        public bool ShowInGameHud { get; private set; } = true;
        public bool VisibleTillEndOfPhase { get; private set; }
        public bool IgnoreFromAbilityHits { get; private set; }
        public bool AlwaysHideNameplate { get; private set; }

        /* Asset fields */
        public int PlayerIndex { get; set; }
        public SerializedComponent PlayerData { get; set; }
        public int CharacterType { get; set; }
        public SerializedComponent TauntCamSetData { get; set; }
        public string AliveHudIconResourceString { get; set; }
        public string DeadHudIconResourceString { get; set; }
        public string ScreenIndicatorIconResourceString { get; set; }
        public string ScreenIndicatorBwIconResourceString { get; set; }
        public SerializedPrefabResourceLink ActionSkinPrefabLink { get; set; }
        private CharacterVisualInfo _visualInfo;
        public CharacterVisualInfo VisualInfo => _visualInfo;
        private CharacterAbilityVfxSwapInfo _abilityVfxSwapInfo;
        public CharacterAbilityVfxSwapInfo AbilityVfxSwapInfo => _abilityVfxSwapInfo;
        private CharacterModInfo _selectedMods;
        public CharacterModInfo SelectedMods => _selectedMods;
        public CharacterAbilityVfxSwapInfo SelectedAbilityVfxSwaps { get; set; }
        private CharacterCardInfo _selectedCards;
        public CharacterCardInfo SelectedCards => _selectedCards;
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
        /* Asset fields end */

        public override bool OnSerialize(NetworkWriter writer, bool initialState)
        {
            return OnSerializeHelper(new NetworkWriterAdapter(writer), initialState);
        }

        public override void OnDeserialize(NetworkReader reader, bool initialState)
        {
            uint num = uint.MaxValue;
            if (!initialState)
                num = reader.ReadPackedUInt32();
            if (num != 0U)
                OnSerializeHelper(new NetworkReaderAdapter(reader), initialState);
        }

        private bool OnSerializeHelper(IBitStream stream, bool initialState)
        {
            if (!initialState && SerializeHelper.ShouldReturnImmediately(ref stream))
                return false;
            uint position = stream.Position;
            sbyte num1 = 0;
            sbyte num2 = 0;
            string displayName = DisplayName;
            float num3 = 0.0f;
            float num4 = 0.0f;
            sbyte num5 = (sbyte) s_invalidActorIndex;
            bool out0_1 = true;
            bool out1_1 = false;
            bool out2_1 = false;
            byte bitField1 = 0;
            sbyte num6 = 0;
            short num7 = 0;
            short num8 = 0;
            short num9 = 0;
            short num10 = 0;
            short num11 = 0;
            short num12 = 0;
            short num13 = 0;
            short num14 = 0;
            short num15 = 0;
            short num16 = 0;
            short num17 = 0;
            byte bitField2 = 0;
            int num18 = -1;
            int num19 = -1;
            int num20 = -1;
            sbyte num21 = -1;
            sbyte num22 = 0;
            int num23 = 0;
            short num24 = 0;
            short num25 = 0;
            bool out0_2 = false;
            bool out1_2 = true;
            bool out2_2 = false;
            bool out3_1 = false;
            bool out4_1 = false;
            byte bitField3 = 0;
            if (stream.isWriting)
            {
                num1 = (sbyte) PlayerIndex;
                num2 = (sbyte) ActorIndex;
                num6 = (sbyte) Team;
                num23 = LastVisibleTurnToClient;
                if (ServerLastKnownPosSquare != null)
                {
                    num24 = (short) ServerLastKnownPosSquare.X;
                    num25 = (short) ServerLastKnownPosSquare.Y;
                }
                else
                {
                    num24 = -1;
                    num25 = -1;
                }

                num3 = RemainingHorizontalMovement;
                num4 = RemainingMovementWithQueuedAbility;
                out0_1 = QueuedMovementAllowsAbility;
                out1_1 = QueuedMovementRequest;
                out2_1 = QueuedChaseRequest;
                bitField1 = ServerClientUtils.CreateBitfieldFromBools(out0_1, out1_1, out2_1, false, false, false,
                    false, false);
                num5 = (sbyte) (QueuedChaseTarget?.ActorIndex ?? s_invalidActorIndex);
                num7 = (short) HitPoints;
                num8 = (short) UnresolvedDamage;
                num9 = (short) UnresolvedHealing;
                num10 = (short) TechPoints;
                num11 = (short) ReservedTechPoints;
                num12 = (short) UnresolvedTechPointGain;
                num13 = (short) UnresolvedTechPointLoss;
                num14 = (short) AbsorbPoints;
                num15 = (short) MechanicPoints;
                num16 = (short) ServerExpectedHoTTotal;
                num17 = (short) ServerExpectedHoTThisTurn;
                bool b7 = num16 > 0 || num17 > 0;
                bitField2 = ServerClientUtils.CreateBitfieldFromBools(num8 > 0, num9 > 0,
                    num12 > 0, num13 > 0, num11 != 0, num14 > 0, num15 > 0, b7);
                num18 = LastDeathTurn;
                num19 = LastSpawnTurn;
                num20 = NextRespawnTurn;
                num21 = (sbyte) SpawnerId;
                num22 = (sbyte) LineOfSightVisibleExceptions.Count;
                out0_2 = HasBotController;
                out1_2 = ShowInGameHud;
                out2_2 = VisibleTillEndOfPhase;
                out3_1 = IgnoreFromAbilityHits;
                out4_1 = AlwaysHideNameplate;
                bitField3 = ServerClientUtils.CreateBitfieldFromBools(out0_2, out1_2, out2_2, out3_1, out4_1, false,
                    false, false);
                stream.Serialize(ref num1);
                stream.Serialize(ref num2);
                stream.Serialize(ref displayName);
                stream.Serialize(ref num6);
                stream.Serialize(ref bitField1);
                stream.Serialize(ref num3);
                stream.Serialize(ref num4);
                stream.Serialize(ref num5);
                stream.Serialize(ref num7);
                stream.Serialize(ref num10);
                stream.Serialize(ref bitField2);
                if (num8 > 0)
                    stream.Serialize(ref num8);
                if (num9 > 0)
                    stream.Serialize(ref num9);
                if (num12 > 0)
                    stream.Serialize(ref num12);
                if (num13 > 0)
                    stream.Serialize(ref num13);
                if (num11 != 0)
                    stream.Serialize(ref num11);
                if (num14 > 0)
                    stream.Serialize(ref num14);
                if (num15 > 0)
                    stream.Serialize(ref num15);
                if (b7)
                {
                    stream.Serialize(ref num16);
                    stream.Serialize(ref num17);
                }

                stream.Serialize(ref num18);
                stream.Serialize(ref num19);
                stream.Serialize(ref num20);
                stream.Serialize(ref num21);
                stream.Serialize(ref bitField3);
//                this.DebugSerializeSizeBeforeVisualInfo = stream.Position - position;
                SerializeCharacterVisualInfo(stream, ref _visualInfo);
                SerializeCharacterCardInfo(stream, ref _selectedCards);
                SerializeCharacterModInfo(stream, ref _selectedMods);
                SerializeCharacterAbilityVfxSwapInfo(stream, ref _abilityVfxSwapInfo);
//                this.DebugSerializeSizeBeforeSpawnSquares = stream.Position - position;
                stream.Serialize(ref num22);
                foreach (ActorData visibleException in LineOfSightVisibleExceptions)
                {
                    short actorIndex = (short) visibleException.ActorIndex;
                    stream.Serialize(ref actorIndex);
                }

                stream.Serialize(ref num23);
                stream.Serialize(ref num24);
                stream.Serialize(ref num25);
            }

            if (stream.isReading)
            {
                stream.Serialize(ref num1);
                stream.Serialize(ref num2);
                stream.Serialize(ref displayName);
                stream.Serialize(ref num6);
                stream.Serialize(ref bitField1);
                ServerClientUtils.GetBoolsFromBitfield(bitField1, out out0_1, out out1_1, out out2_1);
                stream.Serialize(ref num3);
                stream.Serialize(ref num4);
                stream.Serialize(ref num5);
                stream.Serialize(ref num7);
                stream.Serialize(ref num10);
                stream.Serialize(ref bitField2);
                bool out0_3 = false;
                bool out1_3 = false;
                bool out2_3 = false;
                bool out3_2 = false;
                bool out4_2 = false;
                bool out5 = false;
                bool out6 = false;
                bool out7 = false;
                ServerClientUtils.GetBoolsFromBitfield(bitField2, out out0_3, out out1_3, out out2_3, out out3_2,
                    out out4_2, out out5, out out6, out out7);
                if (out0_3)
                    stream.Serialize(ref num8);
                if (out1_3)
                    stream.Serialize(ref num9);
                if (out2_3)
                    stream.Serialize(ref num12);
                if (out3_2)
                    stream.Serialize(ref num13);
                if (out4_2)
                    stream.Serialize(ref num11);
                if (out5)
                    stream.Serialize(ref num14);
                if (out6)
                    stream.Serialize(ref num15);
                if (out7)
                {
                    stream.Serialize(ref num16);
                    stream.Serialize(ref num17);
                }

                stream.Serialize(ref num18);
                stream.Serialize(ref num19);
                stream.Serialize(ref num20);
                stream.Serialize(ref num21);
                stream.Serialize(ref bitField3);
                ServerClientUtils.GetBoolsFromBitfield(bitField3, out out0_2, out out1_2, out out2_2, out out3_1,
                    out out4_1);
                SerializeCharacterVisualInfo(stream, ref _visualInfo);
                SerializeCharacterCardInfo(stream, ref _selectedCards);
                SerializeCharacterModInfo(stream, ref _selectedMods);
                SerializeCharacterAbilityVfxSwapInfo(stream, ref _abilityVfxSwapInfo);
                PlayerIndex = num1;
                ActorIndex = num2;
                Team = (Team) num6;
                SpawnerId = num21;

                DisplayName = displayName;
//                if (initialState)
//                    TeamSensitiveDataMatchmaker.Get().SetTeamSensitiveDataForActor(this);
                stream.Serialize(ref num22);
                LineOfSightVisibleExceptions.Clear();
                for (var index = 0; index < (int) num22; ++index)
                {
                    sbyte num26 = 0;
                    stream.Serialize(ref num26);
//                    ActorData actorByActorIndex = GameFlowData.Get().FindActorByActorIndex((int) num26);
//                    if (actorByActorIndex != null)
//                        LineOfSightVisibleExceptions.Add(actorByActorIndex);
                }

                stream.Serialize(ref num23);
                stream.Serialize(ref num24);
                stream.Serialize(ref num25);
                if (num23 > LastVisibleTurnToClient)
                    LastVisibleTurnToClient = num23;
                ServerLastKnownPosSquare = num24 !=  -1 || num25 !=  -1
                    ? Board.GetBoardSquare((int) num24, (int) num25)
                    : (BoardSquare) null;
                IgnoreFromAbilityHits = out3_1;
                AlwaysHideNameplate = out4_1;
//                this.\u000E().MarkForRecalculateVisibility();
                ShowInGameHud = out1_2;
                VisibleTillEndOfPhase = out2_2;

                UnresolvedDamage = num8;
                UnresolvedHealing = num9;
                ReservedTechPoints = num11;
                UnresolvedTechPointGain = num12;
                UnresolvedTechPointLoss = num13;
                LastDeathTurn = num18;
                LastSpawnTurn = num19;
                NextRespawnTurn = num20;
                HasBotController = out0_2;
                AbsorbPoints = num14;
                TechPoints = num10;
                HitPoints = num7;
                MechanicPoints = num15;
                ServerExpectedHoTTotal = num16;
                ServerExpectedHoTThisTurn = num17;
                RemainingHorizontalMovement = num3;
                RemainingMovementWithQueuedAbility = num4;
                QueuedMovementAllowsAbility = out0_1;
                QueuedMovementRequest = out1_1;
                QueuedChaseRequest = out2_1;
            }

            return SerializeHelper.End(initialState, syncVarDirtyBits);
        }

        public static void SerializeCharacterVisualInfo(
            IBitStream stream,
            ref CharacterVisualInfo visualInfo)
        {
            sbyte skinIndex = (sbyte) visualInfo.skinIndex;
            sbyte patternIndex = (sbyte) visualInfo.patternIndex;
            short colorIndex = (short) visualInfo.colorIndex;
            stream.Serialize(ref skinIndex);
            stream.Serialize(ref patternIndex);
            stream.Serialize(ref colorIndex);
            if (!stream.isReading)
                return;
            visualInfo.skinIndex = skinIndex;
            visualInfo.patternIndex = patternIndex;
            visualInfo.colorIndex = colorIndex;
        }

        public static void SerializeCharacterAbilityVfxSwapInfo(
            IBitStream stream,
            ref CharacterAbilityVfxSwapInfo abilityVfxSwapInfo)
        {
            short vfxSwapForAbility0 = (short) abilityVfxSwapInfo.VfxSwapForAbility0;
            short vfxSwapForAbility1 = (short) abilityVfxSwapInfo.VfxSwapForAbility1;
            short vfxSwapForAbility2 = (short) abilityVfxSwapInfo.VfxSwapForAbility2;
            short vfxSwapForAbility3 = (short) abilityVfxSwapInfo.VfxSwapForAbility3;
            short vfxSwapForAbility4 = (short) abilityVfxSwapInfo.VfxSwapForAbility4;
            stream.Serialize(ref vfxSwapForAbility0);
            stream.Serialize(ref vfxSwapForAbility1);
            stream.Serialize(ref vfxSwapForAbility2);
            stream.Serialize(ref vfxSwapForAbility3);
            stream.Serialize(ref vfxSwapForAbility4);
            if (!stream.isReading)
                return;
            abilityVfxSwapInfo.VfxSwapForAbility0 = vfxSwapForAbility0;
            abilityVfxSwapInfo.VfxSwapForAbility1 = vfxSwapForAbility1;
            abilityVfxSwapInfo.VfxSwapForAbility2 = vfxSwapForAbility2;
            abilityVfxSwapInfo.VfxSwapForAbility3 = vfxSwapForAbility3;
            abilityVfxSwapInfo.VfxSwapForAbility4 = vfxSwapForAbility4;
        }

        public static void SerializeCharacterCardInfo(IBitStream stream, ref CharacterCardInfo cardInfo)
        {
            sbyte num1 = 0;
            sbyte num2 = 0;
            sbyte num3 = 0;
            if (stream.isWriting)
            {
                sbyte prepCard = (sbyte) cardInfo.PrepCard;
                sbyte dashCard = (sbyte) cardInfo.DashCard;
                sbyte combatCard = (sbyte) cardInfo.CombatCard;
                stream.Serialize(ref prepCard);
                stream.Serialize(ref dashCard);
                stream.Serialize(ref combatCard);
            }
            else
            {
                stream.Serialize(ref num1);
                stream.Serialize(ref num2);
                stream.Serialize(ref num3);
                cardInfo.PrepCard = (CardType) num1;
                cardInfo.DashCard = (CardType) num2;
                cardInfo.CombatCard = (CardType) num3;
            }
        }

        public static void SerializeCharacterModInfo(IBitStream stream, ref CharacterModInfo modInfo)
        {
            sbyte num1 = -1;
            sbyte num2 = -1;
            sbyte num3 = -1;
            sbyte num4 = -1;
            sbyte num5 = -1;
            if (stream.isWriting)
            {
                sbyte modForAbility0 = (sbyte) modInfo.ModForAbility0;
                sbyte modForAbility1 = (sbyte) modInfo.ModForAbility1;
                sbyte modForAbility2 = (sbyte) modInfo.ModForAbility2;
                sbyte modForAbility3 = (sbyte) modInfo.ModForAbility3;
                sbyte modForAbility4 = (sbyte) modInfo.ModForAbility4;
                stream.Serialize(ref modForAbility0);
                stream.Serialize(ref modForAbility1);
                stream.Serialize(ref modForAbility2);
                stream.Serialize(ref modForAbility3);
                stream.Serialize(ref modForAbility4);
            }
            else
            {
                stream.Serialize(ref num1);
                stream.Serialize(ref num2);
                stream.Serialize(ref num3);
                stream.Serialize(ref num4);
                stream.Serialize(ref num5);
                modInfo.ModForAbility0 = num1;
                modInfo.ModForAbility1 = num2;
                modInfo.ModForAbility2 = num3;
                modInfo.ModForAbility3 = num4;
                modInfo.ModForAbility4 = num5;
            }
        }

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
            _visualInfo = new CharacterVisualInfo();
            _visualInfo.DeserializeAsset(assetFile, stream);
            _abilityVfxSwapInfo = new CharacterAbilityVfxSwapInfo();
            _abilityVfxSwapInfo.DeserializeAsset(assetFile, stream);
            _selectedMods = new CharacterModInfo();
            _selectedMods.DeserializeAsset(assetFile, stream);
            SelectedAbilityVfxSwaps = new CharacterAbilityVfxSwapInfo();
            SelectedAbilityVfxSwaps.DeserializeAsset(assetFile, stream);
            _selectedCards = new CharacterCardInfo();
            _selectedCards.DeserializeAsset(assetFile, stream);
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
