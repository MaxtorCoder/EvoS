using EvoS.Framework.Network.Game;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.Static
{
    public struct Player
    {
        private static byte s_nextId;
        public bool m_valid;
        public byte m_id;
        public int m_connectionId;
        public long m_accountId;

        public Player(GameServerConnection connection, long accountId)
        {
            m_valid = true;
            m_id = checked(s_nextId++);
            m_connectionId = connection?.connectionId ?? -1;
            m_accountId = accountId;
        }

        public bool WasEverHuman => m_accountId > 0L;

        public override bool Equals(object obj)
        {
            if (obj is Player o)
                return this == o;
            return false;
        }

        public override int GetHashCode()
        {
            return m_id.GetHashCode();
        }

        public static bool operator ==(Player x, Player y)
        {
            if (x.m_id == y.m_id)
                return x.m_valid == y.m_valid;
            return false;
        }

        public static bool operator !=(Player x, Player y)
        {
            return !(x == y);
        }

        public void OnSerializeHelper(NetworkWriter stream)
        {
            OnSerializeHelper(new NetworkWriterAdapter(stream));
        }

        public void OnSerializeHelper(IBitStream stream)
        {
            var valid = m_valid;
            var id = m_id;
            var connectionId = checked((sbyte) m_connectionId);
            stream.Serialize(ref valid);
            stream.Serialize(ref id);
            stream.Serialize(ref connectionId);
            m_valid = valid;
            m_id = id;
            m_connectionId = connectionId;
        }

        public override string ToString()
        {
            return $"[Player: id={m_id}, connectionId={m_connectionId}, accountId={m_accountId}]";
        }
    }
}
