using System;
using System.Collections.Generic;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Game;
using EvoS.Framework.Logging;
using EvoS.Framework.Misc;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.NetworkBehaviours
{
    [Serializable]
    [SerializedMonoBehaviour("ActorStats")]
    public class ActorStats : NetworkBehaviour
    {
        private static int kListm_modifiedStats = -1034899976;
        private SyncListFloat m_modifiedStats = new SyncListFloat();
        private bool m_shouldUpdateFull;
        private Dictionary<StatType, List<StatMod>> m_statMods;
        private float[] m_modifiedStatsPrevious;
        private ActorData m_actorData;

        public SyncListFloat ModifiedStats => m_modifiedStats;
        public bool ShouldUpdateFull => m_shouldUpdateFull;
        public Dictionary<StatType, List<StatMod>> StatMods => m_statMods;
        public float[] ModifiedStatsPrevious => m_modifiedStatsPrevious;

        static ActorStats()
        {
            RegisterSyncListDelegate(typeof(ActorStats), kListm_modifiedStats,
                InvokeSyncListm_modifiedStats);
        }

        public ActorStats()
        {
        }

        public ActorStats(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }


        public void AddStatMod(AbilityStatMod statMod)
        {
            AddStatMod(statMod.stat, statMod.modType, statMod.modValue);
        }

        public void AddStatMod(StatType stat, ModType mod, float val)
        {
            if (EvoSGameConfig.NetworkIsServer)
            {
                float modifiedStatFloat = GetModifiedStatFloat(stat);
                StatMod statMod = new StatMod();
                statMod.Setup(mod, val);
                m_statMods[stat].Add(statMod);
                OnStatModified(stat, modifiedStatFloat, true);
            }
            else
            {
                if (!EvoSGameConfig.NetworkIsClient)
                    return;
                Log.Print(LogType.Error, "called AddStatMod when server is not active");
            }
        }

        public void RemoveStatMod(AbilityStatMod statMod)
        {
            RemoveStatMod(statMod.stat, statMod.modType, statMod.modValue);
        }

        public void RemoveStatMod(StatType stat, ModType mod, float val)
        {
            if (EvoSGameConfig.NetworkIsServer)
            {
                List<StatMod> statMod1 = m_statMods[stat];
                foreach (StatMod statMod2 in statMod1)
                {
                    if (statMod2.mod == mod && statMod2.val == (double) val)
                    {
                        float modifiedStatFloat = GetModifiedStatFloat(stat);
                        statMod1.Remove(statMod2);
                        OnStatModified(stat, modifiedStatFloat, false);
                        break;
                    }
                }
            }
            else
            {
                if (!EvoSGameConfig.NetworkIsClient)
                    return;
                Log.Print(LogType.Error, "called RemoveStat when server is not active");
            }
        }

        public float GetModifiedStatFloat(StatType stat)
        {
            float statBaseValueFloat = GetStatBaseValueFloat(stat);
            return CalculateModifiedStatValue(stat, statBaseValueFloat);
        }

        public int GetModifiedStatInt(StatType stat)
        {
            int statBaseValueInt = GetStatBaseValueInt(stat);
            return GetModifiedStatInt(stat, statBaseValueInt);
        }

        public int GetModifiedStatInt(StatType stat, int baseValue)
        {
            float baseValue1 = baseValue;
            return Mathf.FloorToInt(CalculateModifiedStatValue(stat, baseValue1));
        }

        private void CalculateAdjustmentForStatMod(
            StatMod statMod,
            ref float baseAdd,
            ref float bonusAdd,
            ref float percentAdd,
            ref float multipliers)
        {
            switch (statMod.mod)
            {
                case ModType.BaseAdd:
                    baseAdd += statMod.val;
                    break;
                case ModType.PercentAdd:
                    percentAdd += statMod.val;
                    break;
                case ModType.Multiplier:
                    multipliers *= statMod.val;
                    break;
                case ModType.BonusAdd:
                    bonusAdd += statMod.val;
                    break;
            }
        }

        private void CalculateAdjustments(
            StatType stat,
            ref float baseAdd,
            ref float bonusAdd,
            ref float percentAdd,
            ref float multipliers)
        {
            if (m_statMods == null || !m_statMods.ContainsKey(stat))
                return;
            var statMod = m_statMods[stat];
            foreach (var t in statMod)
                CalculateAdjustmentForStatMod(t, ref baseAdd, ref bonusAdd, ref percentAdd,
                    ref multipliers);
        }

        private void CalculateAdjustments(
            StatType stat,
            ref float baseAdd,
            ref float bonusAdd,
            ref float percentAdd,
            ref float multipliers,
            StatModFilterDelegate filterDelegate)
        {
            if (m_statMods == null || !m_statMods.ContainsKey(stat))
                return;
            List<StatMod> statMod = m_statMods[stat];
            for (int index = 0; index < statMod.Count; ++index)
            {
                if (filterDelegate(statMod[index]))
                    CalculateAdjustmentForStatMod(statMod[index], ref baseAdd, ref bonusAdd, ref percentAdd,
                        ref multipliers);
            }
        }

        private float CalculateModifiedStatValue(StatType stat, float baseValue)
        {
            float num;
            if (m_statMods != null && m_statMods.ContainsKey(stat))
            {
                if (EvoSGameConfig.NetworkIsServer)
                {
                    var baseAdd = 0.0f;
                    var bonusAdd = 0.0f;
                    var percentAdd = 1f;
                    var multipliers = 1f;
                    if (stat == StatType.Movement_Horizontal)
                        CalculateAdjustmentsForMovementHorizontal(ref baseAdd, ref bonusAdd, ref percentAdd,
                            ref multipliers);
                    else
                        CalculateAdjustments(stat, ref baseAdd, ref bonusAdd, ref percentAdd, ref multipliers);
                    num = (baseValue + baseAdd) * percentAdd * multipliers + bonusAdd;
                }
                else
                {
                    var index = (int) stat;
                    num = index >= m_modifiedStats.Count
                        ? m_modifiedStatsPrevious[index]
                        : m_modifiedStats[index];
                }
            }
            else
                num = baseValue;

            return num;
        }

        public void CalculateAdjustmentsForMovementHorizontal(
            ref float baseAdd,
            ref float bonusAdd,
            ref float percentAdd,
            ref float multipliers)
        {
            if (GetComponent<ActorStatus>().HasStatus(StatusType.MovementDebuffSuppression))
            {
                StatModFilterDelegate filterDelegate = statMod =>
                    !(statMod.mod != ModType.Multiplier ? statMod.val < 0.0 : statMod.val < 1.0);
                CalculateAdjustments(StatType.Movement_Horizontal, ref baseAdd, ref bonusAdd, ref percentAdd,
                    ref multipliers, filterDelegate);
            }
            else
                CalculateAdjustments(StatType.Movement_Horizontal, ref baseAdd, ref bonusAdd, ref percentAdd,
                    ref multipliers);
        }

        public int GetStatBaseValueInt(StatType stat)
        {
            return Mathf.RoundToInt(GetStatBaseValueFloat(stat));
        }

        public float GetStatBaseValueFloat(StatType stat)
        {
            float num = 0.0f;
            switch (stat)
            {
                case StatType.Movement_Horizontal:
                    num = GetComponent<ActorData>().m_maxHorizontalMovement;
                    break;
                case StatType.Movement_Upward:
                    num = GetComponent<ActorData>().m_maxVerticalUpwardMovement;
                    break;
                case StatType.Movement_Downward:
                    num = GetComponent<ActorData>().m_maxVerticalDownwardMovement;
                    break;
                case StatType.MaxHitPoints:
                    num = GetComponent<ActorData>().m_maxHitPoints;
                    break;
                case StatType.MaxTechPoints:
                    num = GetComponent<ActorData>().m_maxTechPoints;
                    break;
                case StatType.HitPointRegen:
                    num = GetComponent<ActorData>().m_hitPointRegen;
                    break;
                case StatType.TechPointRegen:
                    num = GetComponent<ActorData>().m_techPointRegen;
                    break;
                case StatType.SightRange:
                    num = GetComponent<ActorData>().m_sightRange;
                    break;
                case StatType.CreditsPerTurn:
                    num = GameplayData.m_creditsPerTurn;
                    break;
                case StatType.ControlPointCaptureSpeed:
                    num = GameplayData.m_capturePointsPerTurn;
                    break;
                case StatType.CoverIncomingDamageMultiplier:
                    num = GameplayData.m_coverProtectionDmgMultiplier;
                    break;
                case StatType.HitPointRegenPercentOfMax:
                    num = 0.0f;
                    break;
            }

            return num;
        }

        private void OnStatModified(StatType stat, float oldStatValue, bool addingMod)
        {
            ActorData component = GetComponent<ActorData>();
            switch (stat)
            {
                case StatType.Movement_Horizontal:
                    GetComponent<ActorMovement>().UpdateSquaresCanMoveTo();
                    break;
                case StatType.Movement_Upward:
                    GetComponent<ActorMovement>().UpdateSquaresCanMoveTo();
                    break;
                case StatType.Movement_Downward:
                    GetComponent<ActorMovement>().UpdateSquaresCanMoveTo();
                    break;
                case StatType.MaxHitPoints:
                    if (EvoSGameConfig.NetworkIsServer)
                    {
                        component.OnMaxHitPointsChanged((int) oldStatValue);
                    }

                    break;
                case StatType.MaxTechPoints:
                    if (EvoSGameConfig.NetworkIsServer)
                    {
                        component.OnMaxHitPointsChanged((int) oldStatValue);
                    }

                    break;
                case StatType.SightRange:
                    GetComponent<FogOfWar>().MarkForRecalculateVisibility();
                    break;
            }

            if (EvoSGameConfig.NetworkIsServer)
                MarkAllForUpdate();
            Board.MarkForUpdateValidSquares();
        }

        private void MarkAllForUpdate()
        {
            m_shouldUpdateFull = true;
        }

        public override void Awake()
        {
            m_statMods =
                new Dictionary<StatType, List<StatMod>>(
                    new FuncEqualityComparer<StatType>((a, b) => a == b, a => (int) a));
            for (var index = 0; index < 24; ++index)
            {
                var statModList = new List<StatMod>();
                m_statMods.Add((StatType) index, statModList);
            }

            m_modifiedStats.InitializeBehaviour(this, kListm_modifiedStats);
            m_modifiedStatsPrevious = new float[24];
            m_actorData = GetComponent<ActorData>();
        }

        protected static void InvokeSyncListm_modifiedStats(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!EvoSGameConfig.NetworkIsClient)
                Log.Print(LogType.Error, "SyncList _modifiedStats called on server.");
            else
                ((ActorStats) obj).m_modifiedStats.HandleMsg(reader);
        }

        public override bool OnSerialize(NetworkWriter writer, bool forceAll)
        {
            if (forceAll)
            {
                SyncListFloat.WriteInstance(writer, m_modifiedStats);
                return true;
            }

            var flag = false;
            if (((int) syncVarDirtyBits & 1) != 0)
            {
                writer.WritePackedUInt32(syncVarDirtyBits);
                flag = true;

                SyncListFloat.WriteInstance(writer, m_modifiedStats);
            }

            if (!flag)
                writer.WritePackedUInt32(syncVarDirtyBits);
            return flag;
        }

        public override void OnDeserialize(NetworkReader reader, bool initialState)
        {
            if (initialState)
            {
                SyncListFloat.ReadReference(reader, m_modifiedStats);
            }
            else
            {
                if (((int) reader.ReadPackedUInt32() & 1) == 0)
                    return;
                SyncListFloat.ReadReference(reader, m_modifiedStats);
            }
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
        }

        public override string ToString()
        {
            return $"{nameof(ActorStats)}>(" +
                   ")";
        }

        private delegate bool StatModFilterDelegate(StatMod statMod);
    }
}
