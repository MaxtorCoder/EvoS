using System;
using System.Collections;
using System.Collections.Generic;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Logging;
using EvoS.Framework.Misc;
using EvoS.Framework.Network.NetworkBehaviours;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Game
{
    public class GameEventManager
    {
        private Dictionary<EventType, List<WeakReference>> m_listenersByEvent =
            new Dictionary<EventType, List<WeakReference>>();

        private List<ReferenceToRemove> m_referencesToRemove =
            new List<ReferenceToRemove>();

        private int m_firingEventsCount;
        private static GameEventManager s_instance;

        ~GameEventManager()
        {
            s_instance = null;
        }

        public void AddAllListenersTo(IGameEventListener whoTo)
        {
            PerformActionOnEvents(whoTo, AddListener);
        }

        public void RemoveAllListenersFrom(IGameEventListener whoTo)
        {
            PerformActionOnEvents(whoTo, RemoveListener);
        }

        private void PerformActionOnEvents(
            IGameEventListener whoTo,
            Action<IGameEventListener, EventType> action)
        {
            var enumerator = Enum.GetValues(typeof(EventType)).GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    EventType current = (EventType) enumerator.Current;
                    if (current != EventType.Invalid)
                        action(whoTo, current);
                }
            }
            finally
            {
                if (enumerator is IDisposable disposable)
                    disposable.Dispose();
            }
        }

        public void AddListener(IGameEventListener listener, EventType eventType)
        {
            WeakReference weakReference = new WeakReference(listener, false);
            List<WeakReference> weakReferenceList1;
            if (!m_listenersByEvent.TryGetValue(eventType, out weakReferenceList1))
            {
                List<WeakReference> weakReferenceList2 = new List<WeakReference>
                {
                    weakReference
                };
                m_listenersByEvent.Add(eventType, weakReferenceList2);
            }
            else
            {
                if (weakReferenceList1.Contains(weakReference))
                    return;
                weakReferenceList1.Add(weakReference);
            }
        }

        public void RemoveListener(IGameEventListener listener, EventType eventType)
        {
            List<WeakReference> weakReferenceList;
            if (!m_listenersByEvent.TryGetValue(eventType, out weakReferenceList))
                return;
            int index = 0;
            for (int count = weakReferenceList.Count; index < count; ++index)
            {
                WeakReference weakReference = weakReferenceList[index];
                if (weakReference.IsAlive && weakReference.Target == listener)
                {
                    if (m_firingEventsCount == 0)
                    {
                        weakReferenceList.Remove(weakReference);
                        break;
                    }

                    m_referencesToRemove.Add(new ReferenceToRemove
                    {
                        referenceList = weakReferenceList,
                        item = weakReference
                    });
                    break;
                }
            }
        }

        public bool FireEvent(EventType eventType, GameEventArgs args)
        {
            return FireEvent(eventType, args, null);
        }

        public bool FireEvent(
            EventType eventType,
            GameEventArgs args,
            IGameEventListener whoTo)
        {
            bool flag = false;
            try
            {
                List<WeakReference> weakReferenceList;
                if (m_listenersByEvent.TryGetValue(eventType, out weakReferenceList))
                {
                    ++m_firingEventsCount;
                    int index = 0;
                    for (int count = weakReferenceList.Count; index < count; ++index)
                    {
                        WeakReference weakReference = weakReferenceList[index];
                        if (weakReference.IsAlive)
                        {
                            if (whoTo == null || whoTo == weakReference.Target)
                            {
                                ((IGameEventListener) weakReference.Target).OnGameEvent(eventType, args);
                                flag = true;
                            }
                        }
                        else
                            m_referencesToRemove.Add(new ReferenceToRemove
                            {
                                referenceList = weakReferenceList,
                                item = weakReference
                            });
                    }

                    --m_firingEventsCount;
                    RemoveDeadReferences();
                }
            }
            catch (Exception ex)
            {
                Log.Print(LogType.Error, ex);
            }

            return flag;
        }

        private void RemoveDeadReferences()
        {
            if (m_firingEventsCount != 0)
                return;
            int index = 0;
            for (int count = m_referencesToRemove.Count; index < count; ++index)
            {
                ReferenceToRemove referenceToRemove = m_referencesToRemove[index];
                referenceToRemove.referenceList.Remove(referenceToRemove.item);
            }

            m_referencesToRemove.Clear();
        }

        public enum EventType
        {
            Invalid,
            CharacterVisualDeath,
            ActorDamaged_Server,
            ActorDamaged_Client,
            ActorKnockback,
            CharacterHealedOrBuffed,
            TurnTick,
            CharacterLocked,
            CharacterRespawn,
            GameTeardown,
            GameObjectsDestroyed,
            VisualSceneLoaded,
            GameCameraCreatedPre,
            GameCameraCreated,
            GameCameraCreatedPost,
            TheatricsAbilityHighlightStart,
            TheatricsAbilitiesEnd,
            TheatricsEvasionMoveStart,
            TheatricsAbilityAnimationStart,
            ServerActionBufferPhaseStart,
            ServerActionBufferActionsDone,
            MatchEnded,
            MatchObjectiveEvent,
            AbilityUsed,
            PowerUpActivated,
            CardUsed,
            GraphicsQualityChanged,
            SystemEscapeMenuOnReturnToGameClick,
            BoardSquareVisibleShadeChanged,
            GameFlowDataStarted,
            NpcSpawned,
            PostCharacterDeath,
            CharacterEnteredQueryArea,
            CharacterExitedQueryArea,
            PatrolPointEvent,
            WanderStateEvent,
            ScriptCommunication,
            UIPhaseStartedPrep,
            UIPhaseStartedEvasion,
            UIPhaseStartedCombat,
            UIPhaseStartedMovement,
            UIPhaseStartedDecision,
            UITutorialHighlightChanged,
            ClientResolutionStarted,
            ReplaceVFXPrefab,
            NormalMovementStart,
            FrontEndSelectionChatterCue,
            FrontEndReady,
            FrontEndEquipMod,
            FrontEndEquipCatalyst,
            AppStateChanged,
            ActorHealed_Client,
            ActorHealed_Server,
            ActorGainedAbsorb_Client,
            ActorGainedAbsorb_Server,
            ActorPing,
            ReconnectReplayStateChanged,
            ReplayRestart,
            ReplaySeekFinished,
            GametimeScaleChange,
            ActiveControlChangedToEnemyTeam,
            ClientRagdollTriggerHit
        }

        private struct ReferenceToRemove
        {
            public List<WeakReference> referenceList;
            public WeakReference item;
        }

        public abstract class GameEventArgs
        {
        }

        public class ActivationInfo : GameEventArgs
        {
            public AbilityData.ActionType actionType;
            public bool active;
        }

        public class CharacterDeathEventArgs : GameEventArgs
        {
            public ActorData deadCharacter;
        }

        public class CharacterRagdollHitEventArgs : GameEventArgs
        {
            public ActorData m_ragdollingActor;
            public ActorData m_triggeringActor;
        }

        public class CharacterRespawnEventArgs : GameEventArgs
        {
            public ActorData respawningCharacter;
        }

        public class ActorHitHealthChangeArgs : GameEventArgs
        {
            public ChangeType m_type;
            public int m_amount;
            public ActorData m_target;
            public ActorData m_caster;
            public bool m_fromCharacterSpecificAbility;

            public ActorHitHealthChangeArgs(
                ChangeType type,
                int amount,
                ActorData target,
                ActorData caster,
                bool fromCharacterSpecificAbility)
            {
                m_type = type;
                m_amount = amount;
                m_target = target;
                m_caster = caster;
                m_fromCharacterSpecificAbility = fromCharacterSpecificAbility;
            }

            public enum ChangeType
            {
                Damage,
                Healing,
                Absorb
            }
        }

        public class ActorKnockback : GameEventArgs
        {
            public ActorData m_target;
        }

        internal class TheatricsAbilityHighlightStartArgs : GameEventArgs
        {
            internal HashSet<ActorData> m_casters = new HashSet<ActorData>();
            internal HashSet<ActorData> m_targets = new HashSet<ActorData>();
        }

        internal class NormalMovementStartAgs : GameEventArgs
        {
            internal List<ActorData> m_actorsBeingHitMidMovement = new List<ActorData>();
        }

        public class CharacterHealBuffArgs : GameEventArgs
        {
            public ActorData targetCharacter;
            public ActorData casterActor;
            public bool healed;
        }

        public class MatchEndedArgs : GameEventArgs
        {
            public GameResult result;
        }

        public class MatchObjectiveEventArgs : GameEventArgs
        {
            public ObjectiveType objective;
            public Team team;
            public ActorData activatingActor;
            public ControlPoint controlPoint;

            public enum ObjectiveType
            {
                CoinCollected,
                FlagPickedUp_Client,
                FlagTurnedIn_Client,
                ControlPointCaptured,
                ObjectivePointsGained,
                CasePickedUp_Client
            }
        }

        public class ActorPingEventArgs : GameEventArgs
        {
            public ActorData byActor;
            public ActorController.PingType pingType;
        }

        public class AbilityUseArgs : GameEventArgs
        {
            public ActorData userActor;
            public Ability ability;
        }

        public class PowerUpActivatedArgs : GameEventArgs
        {
            public ActorData byActor;
            public PowerUp powerUp;
        }

        public class CardUsedArgs : GameEventArgs
        {
            public ActorData userActor;
        }

        public class QueryAreaArgs : GameEventArgs
        {
            public ActorData characterActor;
            public QueryArea area;
        }

        public class WanderStateArgs : GameEventArgs
        {
            public ActorData characterActor;
            public float totalLengthTravelled;
            public float pathLength;
            public BoardSquare destinationSquare;
            public int turnsWandering;
        }

        public class PatrolPointArgs : GameEventArgs
        {
            public ActorData characterActor;
            public WayPoint patrolPoint;
            public int patrolPointIndex;
            public WhatHappenedType whatHappened;
            public PatrolPath patrolPath;
            public bool destinationWasOccupied;

            public PatrolPointArgs(
                WhatHappenedType wht,
                ActorData ad,
                WayPoint pp,
                int index,
                PatrolPath inPath,
                bool dwo)
            {
                whatHappened = wht;
                characterActor = ad;
                patrolPoint = pp;
                patrolPointIndex = index;
                patrolPath = inPath;
                destinationWasOccupied = dwo;
            }

            public enum WhatHappenedType
            {
                PointReached,
                MovingToNextPoint
            }
        }

        public class ScriptCommunicationArgs : GameEventArgs
        {
            public Transition TransistionMessage;
            public NPCBrain NextBrain;
            public bool popBrain;
        }

//        public class ReplaceVFXPrefab : GameEventArgs
//        {
//            public Transform vfxRoot;
//            public CharacterResourceLink characterResourceLink;
//            public CharacterVisualInfo characterVisualInfo;
//            public CharacterAbilityVfxSwapInfo characterAbilityVfxSwapInfo;
//        }

        public class ReconnectReplayStateChangedArgs : GameEventArgs
        {
            public bool m_newReconnectReplayState;

            public ReconnectReplayStateChangedArgs(bool newReconnectReplayState)
            {
                m_newReconnectReplayState = newReconnectReplayState;
            }
        }

        public class TheatricsAbilityAnimationStartArgs : GameEventArgs
        {
            public bool lastInPhase;
        }
    }
}
