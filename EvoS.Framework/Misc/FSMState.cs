using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Game;
using EvoS.Framework.Logging;
using EvoS.Framework.Network.NetworkBehaviours;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("FSMState")]
    public class FSMState : MonoBehaviour
    {
        protected Dictionary<Transition, TransitionTable> transitionTableMap =
            new Dictionary<Transition, TransitionTable>();

        public StateID stateID;

//        [Tooltip("Turn on to enable logging (OnEnter, OnExit, OnTurn) for this state")]
        public bool bool_0;
        private FSMSystem _myFSMCached;
        private NPCBrain _myBrainCached;

        public FSMState()
        {
        }

        public FSMState(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public StateID StateID => stateID;

        internal FSMSystem MyFSM
        {
            get
            {
                if (!_myBrainCached.enabled)
                {
                    int num1 = 0;
                    int num2 = 1;
                    if (MyFSMBrain.fsm == _myFSMCached)
                        num1 = num2 + 1;
                }

                return _myFSMCached;
            }
            private set => _myFSMCached = value;
        }

        internal NPCBrain MyBrain
        {
            get
            {
                if (_myBrainCached.enabled || transform?.gameObject == null) return _myBrainCached;
                var components = transform.gameObject.GetComponents<NPCBrain>();
                foreach (var npcBrain in components)
                {
                    if (npcBrain.enabled)
                        return npcBrain;
                }

                return _myBrainCached;
            }
            private set => _myBrainCached = value;
        }

        internal NPCBrain_StateMachine MyFSMBrain => MyBrain as NPCBrain_StateMachine;

        internal AbilityData MyAbilityData => MyBrain?.GetComponent<AbilityData>();

        internal ActorData MyActorData => MyBrain?.GetComponent<ActorData>();

        internal ActorTurnSM MyActorTurnSM => MyBrain?.GetComponent<ActorTurnSM>();

        internal BotController MyBOTController => MyBrain?.GetComponent<BotController>();

        private void Start()
        {
        }

        internal void Initalize(NPCBrain assoicatedBrain, FSMSystem associatedFSM)
        {
            MyBrain = assoicatedBrain;
            MyFSM = associatedFSM;
        }

        public virtual void OnGameEvent(
            GameEventManager.EventType eventType,
            GameEventManager.GameEventArgs args)
        {
            if (eventType != GameEventManager.EventType.ScriptCommunication)
                return;
            GameEventManager.ScriptCommunicationArgs communicationArgs =
                args as GameEventManager.ScriptCommunicationArgs;
            if (communicationArgs.NextBrain == null && !communicationArgs.popBrain)
            {
                if (communicationArgs.TransistionMessage == Transition.NullTransition)
                    return;
                SetPendingTransition(communicationArgs.TransistionMessage);
            }
            else
                MyBrain.NextBrain = communicationArgs.NextBrain;
        }

        public bool SetPendingTransition(Transition trans)
        {
            if (!MyFSM.CanTransistion(trans))
                return false;
            Transition pendingTransition = MyFSM.GetPendingTransition();
            if (pendingTransition != Transition.NullTransition && pendingTransition != trans)
                Log.Print(LogType.Debug,
                    "NPC: " + MyBrain.name + " in state " + StateID + " already has a pending transition of " +
                    pendingTransition + " but received a transition request of: " + trans + ". Overwriting!");
            MyBrain.SetPendingTransition(trans);
            return true;
        }

        public void AddTransition(Transition trans, TransitionTable inTable)
        {
            if (trans != Transition.NullTransition && inTable != null &&
                (inTable.StateID != StateID.NullStateID || inTable.BrainToPush != null) || inTable.PopBrain)
            {
                if (transitionTableMap.ContainsKey(trans))
                    Log.Print(LogType.Error,
                        "FSMState ERROR: Assign State - State " + inTable.StateID + " already has transition " + trans +
                        " - Impossible to assign to another state/brain to that transition");
                else
                    transitionTableMap.Add(trans, inTable);
            }
            else
                Log.Print(LogType.Error,
                    "FSMState ERROR: Either the Transistion is NULL or you didnt specific a state/brain to pop/push for transition: " +
                    trans);
        }

        public void DeleteTransition(Transition trans)
        {
            if (trans == Transition.NullTransition)
            {
                Log.Print(LogType.Warning, "FSMState ERROR: NullTransition and NULL brain can not be removed");
            }
            else
            {
                if (MyFSM.GetPendingTransition() == trans)
                {
                    Log.Print(LogType.Warning,
                        "Ack - tried to remove a transition of " + trans +
                        " that I have a pending change to. Deleting pending transition");
                    MyFSM.SetPendingTransition(Transition.NullTransition);
                }

                transitionTableMap.Remove(trans);
            }
        }

        public StateID GetOutputState(Transition trans)
        {
            if (transitionTableMap.ContainsKey(trans))
                return transitionTableMap[trans].StateID;
            return StateID.NullStateID;
        }

        public NPCBrain GetOutputBrain(Transition trans)
        {
            if (transitionTableMap.ContainsKey(trans))
                return transitionTableMap[trans].BrainToPush;
            return null;
        }

        public bool? GetPopBrain(Transition trans)
        {
            if (transitionTableMap.ContainsKey(trans))
                return transitionTableMap[trans].PopBrain;
            return new bool?();
        }

//  public virtual FSMState DeepCopy()
//  {
//    return Object.Instantiate<FSMState>(this);
//  }

        public virtual void OnEnter(NPCBrain npc, StateID previousState)
        {
            if (!bool_0)
                return;
            Log.Print(LogType.Game, $"OnEnter(): '{npc.name}' NewState: '{StateID}' PreviousState: '{previousState}'");
        }

        public virtual void OnExit(NPCBrain npc, StateID nextState)
        {
            if (!bool_0)
                return;
            Log.Print(LogType.Game, $"OnEnter(): '{npc.name}' NewState: '{StateID}' NextState: '{nextState}'");
        }

//        public virtual IEnumerator OnTurn(NPCBrain npc)
//        {
//            // ISSUE: object of a compiler-generated type is created
//            // ISSUE: variable of a compiler-generated type
//            FSMState.\u003COnTurn\u003Ec__Iterator0 onTurnCIterator0 = new FSMState.\u003COnTurn\u003Ec__Iterator0();
//            return (IEnumerator) onTurnCIterator0;
//        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            stateID = (StateID) stream.ReadInt32();
            bool_0 = stream.ReadBoolean();
        }

        public override string ToString()
        {
            return $"{nameof(FSMState)}(" +
                   $"{nameof(stateID)}: {stateID}, " +
                   $"{nameof(bool_0)}: {bool_0}, " +
                   ")";
        }
    }
}
