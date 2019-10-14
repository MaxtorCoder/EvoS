using System;
using System.Collections.Generic;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.NetworkBehaviours
{
    [Serializable]
    [SerializedMonoBehaviour("TheatricsManager")]
    public class TheatricsManager : NetworkBehaviour
    {
        public bool m_allowAbilityAnimationInterruptHitReaction = true;
        public int m_numClientPhaseTimeoutsUntilForceDisconnect = 3;
//        [Separator("Ragdoll Force Settings", true)]
        public float m_ragdollImpactForce = 15f;
//        [Header("-- Whether to apply force only to single joint")]
        public bool m_ragdollApplyForceOnSingleJointOnly;
        private Turn m_turn = new Turn();
        private AbilityPriority m_phaseToUpdate = AbilityPriority.INVALID;
        private HashSet<long> m_playerConnectionIdsInUpdatePhase = new HashSet<long>();
        private AbilityPriority m_lastPhaseEnded = AbilityPriority.INVALID;
        private SerializeHelper m_serializeHelper = new SerializeHelper();
        internal string m_debugSerializationInfoStr = string.Empty;
        public const float c_timeoutAdvancePhaseSlowClient = 1.3f;
        public const float c_maxPhaseAdvanceTimeoutDuration = 45f;
        private int m_turnToUpdate;
        private int m_numConnectionIdsAddedForPhase;
        private float m_phaseStartTime;
        internal const string c_actorAnimDebugHeader = "<color=cyan>Theatrics: </color>";

        public TheatricsManager()
        {
        }

        public TheatricsManager(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override bool OnSerialize(NetworkWriter writer, bool initialState)
        {
            return OnSerializeHelper(new NetworkWriterAdapter(writer), initialState);
        }

        public override void OnDeserialize(NetworkReader reader, bool initialState)
        {
            uint num = uint.MaxValue;
            if (!initialState)
                num = reader.ReadPackedUInt32();
            if (num == 0U)
                return;
            OnSerializeHelper(new NetworkReaderAdapter(reader), initialState);
        }

        private bool OnSerializeHelper(IBitStream stream, bool initialState)
        {
            if (!initialState && m_serializeHelper.ShouldReturnImmediately(ref stream))
                return false;
            int turnToUpdate = m_turnToUpdate;
            stream.Serialize(ref turnToUpdate);
            bool flag1 = turnToUpdate != m_turnToUpdate;
            m_turnToUpdate = turnToUpdate;
            int phaseToUpdate = (int) m_phaseToUpdate;
            stream.Serialize(ref phaseToUpdate);
            bool flag2 = m_phaseToUpdate != (AbilityPriority) phaseToUpdate;
            if (flag2 || flag1)
            {
                m_phaseToUpdate = (AbilityPriority) phaseToUpdate;
                m_phaseStartTime = Time.time;
                if (flag1)
                    m_turn = new Turn();
            }
            m_turn.OnSerializeHelper(stream);
            if (flag2)
                m_turn.UnknownMethod(m_phaseToUpdate);
            return m_serializeHelper.End(initialState, syncVarDirtyBits);
        }
        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_allowAbilityAnimationInterruptHitReaction = stream.ReadBoolean();
            stream.AlignTo();
            m_numClientPhaseTimeoutsUntilForceDisconnect = stream.ReadInt32();
            m_ragdollImpactForce = stream.ReadSingle();
            m_ragdollApplyForceOnSingleJointOnly = stream.ReadBoolean();
            stream.AlignTo();
        }

        public override string ToString()
        {
            return $"{nameof(TheatricsManager)}>(" +
                   $"{nameof(m_allowAbilityAnimationInterruptHitReaction)}: {m_allowAbilityAnimationInterruptHitReaction}, " +
                   $"{nameof(m_numClientPhaseTimeoutsUntilForceDisconnect)}: {m_numClientPhaseTimeoutsUntilForceDisconnect}, " +
                   $"{nameof(m_ragdollImpactForce)}: {m_ragdollImpactForce}, " +
                   $"{nameof(m_ragdollApplyForceOnSingleJointOnly)}: {m_ragdollApplyForceOnSingleJointOnly}, " +
                   ")";
        }
    }
}
