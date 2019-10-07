using System.Collections.Generic;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    public class PowerUp //: NetworkBehaviour
    {
        private static int kRpcRpcOnPickedUp = -430057904;

        public string m_audioEventPickUp = "ui_pickup_health";

//  [SyncVar]
        private Team m_pickupTeam = Team.Objects;

//  [SyncVar(hook = "HookSetGuid")]
        private int m_guid = -1;
        private List<int> m_clientSequenceIds = new List<int>();
        private static int s_nextPowerupGuid;
        public Ability m_ability;
        public string m_powerUpName;
        public string m_powerUpToolTip;
        public GameObject m_sequencePrefab;
        public bool m_restrictPickupByTeam;

        public PowerUp.PowerUpCategory m_chatterCategory;

//  [SyncVar]
        private uint m_sequenceSourceId;

//  private BoardSquare m_boardSquare;
//  [SyncVar]
        public bool m_isSpoil;

//  [SyncVar]
        public bool m_ignoreSpawnSplineForSequence;
        public int m_duration;
        private int m_age;
        private bool m_pickedUp;

        private bool m_stolen;

//  private ActorTag m_tags;
        private bool m_markedForRemoval;

//  private SequenceSource _sequenceSource;
        private static int kRpcRpcOnSteal;

        static PowerUp()
        {
//    NetworkBehaviour.RegisterRpcDelegate(typeof (PowerUp), PowerUp.kRpcRpcOnPickedUp, new NetworkBehaviour.CmdDelegate(PowerUp.InvokeRpcRpcOnPickedUp));
//    PowerUp.kRpcRpcOnSteal = 1919536730;
//    NetworkBehaviour.RegisterRpcDelegate(typeof (PowerUp), PowerUp.kRpcRpcOnSteal, new NetworkBehaviour.CmdDelegate(PowerUp.InvokeRpcRpcOnSteal));
//    NetworkCRC.RegisterBehaviour(nameof (PowerUp), 0);
        }

        public enum PowerUpCategory
        {
                NoCategory,
                Healing,
                Might,
                Movement,
                Energy,
        }

    }
}
