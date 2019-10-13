using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Game;
using EvoS.Framework.Logging;
using EvoS.Framework.Misc;
using EvoS.Framework.Network.NetworkBehaviours;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.Static
{
    public class SequenceSource
    {
        private static uint s_nextID = 1;

        private static Dictionary<uint, List<SequenceSource>>
            s_idsToSrcs = new Dictionary<uint, List<SequenceSource>>();

        private HashSet<Vector3> m_hitPositions = new HashSet<Vector3>();
        private HashSet<ActorData> m_hitActors = new HashSet<ActorData>();
        private int m_hitTurn = -1;
        private AbilityPriority m_hitPhase = AbilityPriority.INVALID;
        private uint _rootID;
        private ActorDelegate m_onHitActor;
        private Vector3Delegate m_onHitPosition;

        public SequenceSource()
        {
        }

        public SequenceSource(
            ActorDelegate onHitActor,
            Vector3Delegate onHitPosition,
            bool removeAtEndOfTurn = true,
            SequenceSource parentSource = null,
            IBitStream stream = null)
        {
            m_onHitActor = onHitActor;
            m_onHitPosition = onHitPosition;
            RemoveAtEndOfTurn = removeAtEndOfTurn;
            WaitForClientEnable = false;
            if (stream == null)
                RootID = !(parentSource == null)
                    ? parentSource.RootID
                    : AllocateID();
            else
                OnSerializeHelper(stream);
        }

        public SequenceSource(
            ActorDelegate onHitActor,
            Vector3Delegate onHitPosition,
            uint rootID,
            bool removeAtEndOfTurn)
        {
            m_onHitActor = onHitActor;
            m_onHitPosition = onHitPosition;
            RootID = rootID;
            RemoveAtEndOfTurn = removeAtEndOfTurn;
        }

        private static uint AllocateID()
        {
            if (!EvoSGameConfig.NetworkIsServer && EvoSGameConfig.NetworkIsClient)
                Log.Print(LogType.Error, "Code Error: SequenceSource IDs should only be allocated on the server");
            return s_nextID++;
        }

        public uint RootID
        {
            get { return _rootID; }
            private set
            {
                if (_rootID == 0U && value > 0U)
                {
                    List<SequenceSource> sequenceSourceList = !s_idsToSrcs.ContainsKey(value)
                        ? new List<SequenceSource>()
                        : s_idsToSrcs[value];
                    sequenceSourceList.Add(this);
                    s_idsToSrcs[value] = sequenceSourceList;
                }

                _rootID = value;
            }
        }

        public bool RemoveAtEndOfTurn { get; set; }

        public bool WaitForClientEnable { get; private set; }

        public void SetWaitForClientEnable(bool value)
        {
            WaitForClientEnable = value;
        }

        public SequenceSource GetShallowCopy()
        {
            return (SequenceSource) MemberwiseClone();
        }

        ~SequenceSource()
        {
            if (!s_idsToSrcs.ContainsKey(_rootID))
                return;
            s_idsToSrcs[_rootID].Remove(this);
        }

        public static void ClearStaticData()
        {
            s_idsToSrcs.Clear();
        }

        public void OnSerializeHelper(NetworkWriter stream)
        {
            OnSerializeHelper(new NetworkWriterAdapter(stream));
        }

        public void OnSerializeHelper(IBitStream stream)
        {
            uint rootId = RootID;
            bool removeAtEndOfTurn = RemoveAtEndOfTurn;
            bool waitForClientEnable = WaitForClientEnable;
            stream.Serialize(ref rootId);
            stream.Serialize(ref removeAtEndOfTurn);
            stream.Serialize(ref waitForClientEnable);
            if ((int) RootID != (int) rootId)
                RootID = rootId;
            if (RemoveAtEndOfTurn != removeAtEndOfTurn)
                RemoveAtEndOfTurn = removeAtEndOfTurn;
            if (WaitForClientEnable == waitForClientEnable)
                return;
            WaitForClientEnable = waitForClientEnable;
        }

//        public void OnSequenceHit(
//            Sequence seq,
//            ActorData target,
//            ActorModelData.ImpulseInfo impulseInfo,
//            ActorModelData.RagdollActivation ragdollActivation = ActorModelData.RagdollActivation.HealthBased,
//            bool tryHitReactIfAlreadyHit = true)
//        {
//            AbilityPriority currentAbilityPhase = ServerClientUtils.GetCurrentAbilityPhase();
//            if (m_hitTurn != GameFlowData.Get().CurrentTurn || m_hitPhase != currentAbilityPhase)
//            {
//                m_hitTurn = GameFlowData.Get().CurrentTurn;
//                m_hitPhase = currentAbilityPhase;
//                m_hitPositions.Clear();
//                m_hitActors.Clear();
//            }
//
//            bool flag = false;
//            if (!m_hitActors.Contains(target))
//            {
//                if (m_onHitActor != null)
//                    m_onHitActor(target);
//            }
//            else
//                flag = true;
//
//            m_hitActors.Add(target);
//            if ((Object) seq != null && (tryHitReactIfAlreadyHit || !flag))
//                TheatricsManager.Get().OnSequenceHit(seq, target, impulseInfo, ragdollActivation);
//            if (!SequenceManager.SequenceDebugTraceOn)
//                return;
//            Debug.LogWarning((object) ("<color=yellow>Sequence Actor Hit: </color><<color=lightblue>" +
//                                       seq.gameObject.name + " | " + (object) seq.GetType() + "</color>> \nhit on: " +
//                                       target.\u0012("white") + " @time= " + (object) Time.time));
//        }
//
//        public void OnSequenceHit(
//            Sequence seq,
//            Vector3 position,
//            ActorModelData.ImpulseInfo impulseInfo = null)
//        {
//            AbilityPriority currentAbilityPhase = ServerClientUtils.GetCurrentAbilityPhase();
//            if (m_hitTurn != GameFlowData.Get().CurrentTurn || m_hitPhase != currentAbilityPhase)
//            {
//                m_hitTurn = GameFlowData.Get().CurrentTurn;
//                m_hitPhase = currentAbilityPhase;
//                m_hitPositions.Clear();
//                m_hitActors.Clear();
//            }
//
//            if (!m_hitPositions.Contains(position))
//            {
//                m_hitPositions.Add(position);
//                if (m_onHitPosition != null)
//                    m_onHitPosition(position);
//            }
//
//            if (!SequenceManager.SequenceDebugTraceOn)
//                return;
//            Debug.LogWarning((object) ("<color=yellow>Sequence Position Hit: </color><<color=lightblue>" +
//                                       seq.gameObject.name + " | " + (object) seq.GetType() + "</color>> \nhit at: " +
//                                       position + " @time= " + (object) Time.time));
//        }

        public static bool DidSequenceHit(SequenceSource src, ActorData target)
        {
            if (s_idsToSrcs.ContainsKey(src.RootID))
            {
                List<SequenceSource> idsToSrc = s_idsToSrcs[src.RootID];
                for (int index = 0; index < idsToSrc.Count; ++index)
                {
                    if (idsToSrc[index].m_hitActors.Contains(target))
                        return true;
                }
            }

            return false;
        }

        public static bool DidSequenceHit(SequenceSource src, Vector3 position)
        {
            if (s_idsToSrcs.ContainsKey(src.RootID))
            {
                List<SequenceSource> idsToSrc = s_idsToSrcs[src.RootID];
                for (int index = 0; index < idsToSrc.Count; ++index)
                {
                    if (idsToSrc[index].m_hitPositions.Contains(position))
                        return true;
                }
            }

            return false;
        }

        public string GetHitActorsString()
        {
            string empty = string.Empty;
            foreach (ActorData hitActor in m_hitActors)
            {
                if (hitActor != null)
                {
                    if (empty.Length > 0)
                        empty += " | ";
                    empty += (string) (object) hitActor.ActorIndex;
                }
            }

            return "Did Hit Actor IDs: " + (empty.Length <= 0 ? "(none)" : empty);
        }

        public string GetHitPositionsString()
        {
            string str = string.Empty;
            foreach (Vector3 hitPosition in m_hitPositions)
                str = str + "\t" + hitPosition + "\n";
            return str;
        }

        public override bool Equals(object obj)
        {
            SequenceSource sequenceSource = obj as SequenceSource;
            if ((object) sequenceSource == null)
                return false;
            return (int) RootID == (int) sequenceSource.RootID;
        }

        public bool Equals(SequenceSource p)
        {
            return (int) RootID == (int) p.RootID;
        }

        public static bool operator ==(SequenceSource a, SequenceSource b)
        {
            if (ReferenceEquals(a, b))
                return true;
            if ((object) a == null || (object) b == null)
                return false;
            return (int) a.RootID == (int) b.RootID;
        }

        public static bool operator !=(SequenceSource a, SequenceSource b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (int) RootID;
        }

        public delegate void ActorDelegate(ActorData target);

        public delegate void Vector3Delegate(Vector3 position);
    }
}
