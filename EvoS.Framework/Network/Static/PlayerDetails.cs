using System.Collections.Generic;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.Static
{
    public class PlayerDetails
    {
        public Team m_team;
        public bool m_disconnected;
        public string m_handle;
        public long m_accountId;
        public float m_accPrivateElo;
        public float m_charPrivateElo;
        public float m_usedMatchmakingElo;
        public int m_lobbyPlayerInfoId;
        public PlayerGameAccountType m_gameAccountType;
        public bool m_replayGenerator;
        public bool m_botsMasqueradeAsHumans;
        public string m_buildVersion;
        public List<GameObject> m_gameObjects;
        public int m_idleTurns;

        public PlayerDetails(PlayerGameAccountType gameAccountType)
        {
            m_disconnected = false;
            m_gameObjects = new List<GameObject>();
            m_idleTurns = 0;
            m_gameAccountType = gameAccountType;
        }

        public bool ReplacedWithBots { get; private set; }
        public bool IsHumanControlled => !IsAIControlled;
        public bool IsSpectator => m_team == Team.Spectator;
        public bool IsConnected => !m_disconnected;

        public bool IsAIControlled
        {
            get
            {
                if (!IsNPCBot && !IsLoadTestBot)
                    return ReplacedWithBots;
                return true;
            }
        }

        public bool IsNPCBot
        {
            get => m_gameAccountType == PlayerGameAccountType.None;
            set
            {
                if (!value)
                    return;
                m_gameAccountType = PlayerGameAccountType.None;
            }
        }

        public bool IsLoadTestBot
        {
            get => m_gameAccountType == PlayerGameAccountType.LoadTest;
            set
            {
                if (!value)
                    return;
                m_gameAccountType = PlayerGameAccountType.LoadTest;
            }
        }

        public bool IsLocal()
        {
//            if ((bool) (ClientGameManager.Get()) && ClientGameManager.Get().Observer ||
//                (bool) (ReplayPlayManager.Get()) && ReplayPlayManager.Get().IsPlayback())
//                return m_replayGenerator;
//            if (m_accountId != 0L)
//                return m_accountId == HydrogenConfig.Get().Ticket.AccountId;
            return false;
        }

        public void OnSerializeHelper(NetworkWriter stream)
        {
            OnSerializeHelper(new NetworkWriterAdapter(stream));
        }

        public void OnSerializeHelper(IBitStream stream)
        {
            sbyte team = checked((sbyte) m_team);
            bool disconnected = m_disconnected;
            string handle = m_handle;
            long accountId = m_accountId;
            float accPrivateElo = m_accPrivateElo;
            float usedMatchmakingElo = m_usedMatchmakingElo;
            int lobbyPlayerInfoId = m_lobbyPlayerInfoId;
            float charPrivateElo = m_charPrivateElo;
            int gameAccountType = (int) m_gameAccountType;
            bool replayGenerator = m_replayGenerator;
            bool masqueradeAsHumans = m_botsMasqueradeAsHumans;
            stream.Serialize(ref team);
            stream.Serialize(ref disconnected);
            stream.Serialize(ref handle);
            stream.Serialize(ref accountId);
            stream.Serialize(ref accPrivateElo);
            stream.Serialize(ref usedMatchmakingElo);
            stream.Serialize(ref lobbyPlayerInfoId);
            stream.Serialize(ref charPrivateElo);
            stream.Serialize(ref gameAccountType);
            stream.Serialize(ref replayGenerator);
            stream.Serialize(ref masqueradeAsHumans);
            m_team = (Team) team;
            m_disconnected = disconnected;
            m_handle = handle;
            m_accountId = accountId;
            m_accPrivateElo = accPrivateElo;
            m_usedMatchmakingElo = usedMatchmakingElo;
            m_lobbyPlayerInfoId = lobbyPlayerInfoId;
            m_charPrivateElo = charPrivateElo;
            m_gameAccountType = (PlayerGameAccountType) gameAccountType;
            m_replayGenerator = replayGenerator;
            m_botsMasqueradeAsHumans = masqueradeAsHumans;
        }

        public override string ToString()
        {
            return
                $"{m_handle}[{m_accountId}] team={m_team} disconnected={m_disconnected} replacedWithBots={ReplacedWithBots}";
        }
    }
}
