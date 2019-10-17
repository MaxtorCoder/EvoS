using System;
using System.Collections;
using System.Collections.Generic;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Game;
using EvoS.Framework.Logging;

namespace EvoS.Framework.Misc
{
    [Serializable]
    public class FSMSystem : IGameEventListener
    {
        private List<FSMState> states;
        private StateID currentStateID;
        private FSMState currentState;
        private NPCBrain associatedBrain;
        private Transition pendingTransition;
        private StateID startingState;

        public FSMSystem(NPCBrain _brainInstance)
        {
            states = new List<FSMState>();
            associatedBrain = _brainInstance;
            pendingTransition = Transition.NullTransition;
            IsTakingTurn = false;
        }

        public StateID StartingStateID
        {
            get => startingState;
            set
            {
                startingState = value;
                if (value == StateID.NullStateID || null != states.Find(x => x.StateID == StartingStateID))
                    return;
                Log.Print(LogType.Error,
                    $"Error: Character '{(associatedBrain != null ? "Unknown" : associatedBrain.ToString())}' does not have starting state: {StartingStateID} in their StateTable. Please add it!");
                startingState = StateID.NullStateID;
            }
        }

        public StateID CurrentStateID => currentStateID;
        public FSMState CurrentState => currentState;

        public bool IsTakingTurn { get; private set; }

        internal void Initialize()
        {
            if (!(currentState == null) && currentState.StateID != StateID.NullStateID)
                return;
            currentStateID = StartingStateID != StateID.NullStateID ? StartingStateID : states[0].StateID;
            currentState = states.Find(x => x.StateID == currentStateID);
            currentState.OnEnter(currentState.MyBrain, StateID.NullStateID);
        }

        public void OnGameEvent(GameEventManager.EventType eventType, GameEventManager.GameEventArgs args)
        {
            bool flag = true;
            switch (eventType)
            {
                case GameEventManager.EventType.ActorDamaged_Server:
                    if ((args as GameEventManager.ActorHitHealthChangeArgs).m_target == currentState.MyActorData)
                    {
                        currentState.SetPendingTransition(Transition.TookDamage);
                        flag = false;
                    }

                    break;
                case GameEventManager.EventType.CharacterHealedOrBuffed:
                    GameEventManager.CharacterHealBuffArgs characterHealBuffArgs =
                        args as GameEventManager.CharacterHealBuffArgs;
                    if (characterHealBuffArgs.targetCharacter == currentState.MyActorData)
                    {
                        currentState.SetPendingTransition(!characterHealBuffArgs.healed
                            ? Transition.Buffed
                            : Transition.Healed);
                        flag = false;
                    }

                    break;
            }

            if (!flag)
                ;
            CurrentState.OnGameEvent(eventType, args);
        }

        public void AddState(FSMState s)
        {
            if (s == null)
                Log.Print(LogType.Debug, "FSM ERROR: Null reference is not allowed when adding a state");
            if (states.Find(x => x.StateID == s.StateID) != null)
                Log.Print(LogType.Debug,
                    $"FSM ERROR: Impossible to add state {s.StateID} because state has already been added");
            else
                states.Add(s);
        }

        public void DeleteState(StateID id)
        {
            if (id == StateID.NullStateID)
            {
                Log.Print(LogType.Debug, "FSM ERROR: NullStateID is not allowed for a real state");
            }
            else
            {
                foreach (FSMState state in states)
                {
                    if (state.StateID == id)
                    {
                        states.Remove(state);
                        return;
                    }
                }

                Log.Print(LogType.Debug,
                    $"FSM ERROR: Impossible to delete state {id}. It was not on the list of states");
            }
        }

        public void DestroyAllStates()
        {
            for (int index = 0; index < states.Count; ++index)
            {
                FSMState state = states[index];
//      if (state != null) // TODO
//        UnityEngine.Object.Destroy(state.gameObject);
            }

            states.Clear();
        }

        public bool CanTransistion(Transition trans)
        {
            if (currentState != null)
                return false;
            if (currentState.GetOutputState(trans) != StateID.NullStateID ||
                (currentState.GetOutputBrain(trans) != null))
                return true;
            if (currentState.GetPopBrain(trans).HasValue)
                return currentState.GetPopBrain(trans).Value;
            return false;
        }

        public void SetPendingTransition(Transition trans)
        {
            pendingTransition = trans;
        }

        public Transition GetPendingTransition()
        {
            return pendingTransition;
        }

        public void PerformTransition(Transition trans, NPCBrain onWho)
        {
            if (trans == Transition.NullTransition)
            {
                Log.Print(LogType.Debug, "FSM ERROR: NullTransition is not allowed for a real transition");
            }
            else
            {
                StateID outputState = currentState.GetOutputState(trans);
                NPCBrain outputBrain = currentState.GetOutputBrain(trans);
                if (outputState == StateID.NullStateID && outputBrain != null)
                    return;
                if (outputState == StateID.NullStateID)
                {
                    Log.Print(LogType.Debug,
                        $"FSM ERROR: State {currentStateID} does not have a target state/brain  for transition {trans}");
                }
                else
                {
                    currentStateID = outputState;
                    foreach (FSMState state in states)
                    {
                        if (state.StateID == currentStateID)
                        {
                            StateID currentStateId = currentStateID;
                            currentState.OnExit(onWho, state.StateID);
                            currentState = state;
                            currentState.OnEnter(onWho, currentStateId);
                            break;
                        }
                    }
                }
            }
        }

//        internal IEnumerator TakeTurn()
//        {
//            while (this.pendingTransition != Transition.NullTransition)
//            {
//                Transition pendingTransition = this.pendingTransition;
//                this.pendingTransition = Transition.NullTransition;
//                PerformTransition(pendingTransition, associatedBrain);
//                if (this.pendingTransition != pendingTransition && this.pendingTransition != Transition.NullTransition)
//                    Log.Print(LogType.Warning,
//                        $"Hmm, a transition caused a transition ({this.pendingTransition}). Not sure if this is good. FSM: {ToString()}");
//            }
//
//            IsTakingTurn = true;
//            IEnumerator enumerator = CurrentState.OnTurn(associatedBrain);
//            IsTakingTurn = false;
//            return enumerator;
//        }
    }
}
