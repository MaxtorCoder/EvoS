using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Game;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("NPCBrain")]
    public class NPCBrain : MonoBehaviour, IGameEventListener
    {
//        [Tooltip("Create new states in your scene, then add them to this list.")]
        public List<StateEntry> StateTable = new List<StateEntry>();

//        [Tooltip("ID of the starting state for this FSM. NullStateID will use the first state in StateTable. Make sure StartingState exists in the StateTable")]
        public StateID StartingState;
        private GameObject m_allocatedStateTableParent;

        public FSMSystem fsm { get; private set; }

        public NPCBrain NextBrain { get; internal set; }

        public NPCBrain()
        {
        }

        public NPCBrain(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        private void Start()
        {
            if (GetComponent<BotController>() != null)
                return;
            name += " [Prime]";
            enabled = false;
        }

//        public void OnDestroy()
//        {
//            GameEventManager.Get().RemoveAllListenersFrom((IGameEventListener) this);
//            if (m_allocatedStateTableParent != null)
//            {
//                Object.Destroy((Object) m_allocatedStateTableParent);
//                m_allocatedStateTableParent = null;
//            }
//
//            if (fsm == null)
//                return;
//            fsm.DestroyAllStates();
//        }

        public bool CanTransistion(Transition trans)
        {
            if (fsm != null)
                return fsm.CanTransistion(trans);
            return false;
        }

        public void OnGameEvent(GameEventManager.EventType eventType, GameEventManager.GameEventArgs args)
        {
            if (!(this != null) || fsm == null || !enabled)
                return;
            fsm.OnGameEvent(eventType, args);
        }

        public void SetTransition(Transition t)
        {
            fsm.PerformTransition(t, this);
        }

        public void SetPendingTransition(Transition t)
        {
            fsm.SetPendingTransition(t);
        }

        public Transition GetPendingTransition()
        {
            return fsm.GetPendingTransition();
        }

        public virtual NPCBrain Create(BotController bot, Transform destination)
        {
            return null;
        }

//        [DebuggerHidden]
//        public virtual IEnumerator DecideTurn()
//        {
//            // ISSUE: object of a compiler-generated type is created
//            // ISSUE: variable of a compiler-generated type
//            NPCBrain.\u003CDecideTurn\u003Ec__Iterator0 decideTurnCIterator0 =
//                new NPCBrain.\u003CDecideTurn\u003Ec__Iterator0();
//            return (IEnumerator) decideTurnCIterator0;
//        }
//
//        public virtual void SelectBotAbilityMods()
//        {
//            GetComponent<BotController>().SelectBotAbilityMods_Brainless();
//        }
//
//        public virtual void SelectBotCards()
//        {
//            GetComponent<BotController>().SelectBotCards_Brainless();
//        }
//
//        [DebuggerHidden]
//        public IEnumerator FSMTakeTurn()
//        {
//            // ISSUE: object of a compiler-generated type is created
//            return (IEnumerator) new NPCBrain.\u003CFSMTakeTurn\u003Ec__Iterator1
//            {
//                \u0024this = this
//            };
//        }

        protected virtual void MakeFSM(NPCBrain brainInstance)
        {
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            StateTable =
                new SerializedVector<StateEntry>(assetFile,
                    stream); // class [mscorlib]System.Collections.Generic.List`1<class StateEntry>
            StartingState = (StateID) stream.ReadInt32(); // valuetype StateID
        }

        public override string ToString()
        {
            return $"{nameof(NPCBrain)}(" +
                   $"{nameof(StateTable)}: {StateTable}, " +
                   $"{nameof(StartingState)}: {StartingState}, " +
                   ")";
        }
    }
}
