using System;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(99)]
    public class KeyCodeData
    {
        public int m_primary;
        public int m_modifierKey1;
        public int m_additionalModifierKey1;
        public int m_secondary;
        public int m_modifierKey2;
        public int m_additionalModifierKey2;
    }
}
