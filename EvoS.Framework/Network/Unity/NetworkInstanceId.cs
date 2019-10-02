using System;

namespace EvoS.Framework.Network.Unity
{
    [Serializable]
    public struct NetworkInstanceId
    {
        public static NetworkInstanceId Invalid = new NetworkInstanceId(uint.MaxValue);
        public static NetworkInstanceId Zero = new NetworkInstanceId(0U);
        [SerializeField] private readonly uint m_Value;

        public NetworkInstanceId(uint value)
        {
            m_Value = value;
        }

        public bool IsEmpty()
        {
            return m_Value == 0U;
        }

        public override int GetHashCode()
        {
            return (int) m_Value;
        }

        public override bool Equals(object obj)
        {
            return obj is NetworkInstanceId id && this == id;
        }

        public static bool operator ==(NetworkInstanceId c1, NetworkInstanceId c2)
        {
            return (int) c1.m_Value == (int) c2.m_Value;
        }

        public static bool operator !=(NetworkInstanceId c1, NetworkInstanceId c2)
        {
            return (int) c1.m_Value != (int) c2.m_Value;
        }

        public override string ToString()
        {
            return m_Value.ToString();
        }

        public uint Value => m_Value;
    }
}
