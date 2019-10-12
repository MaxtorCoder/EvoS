namespace EvoS.Framework.Network.Unity
{
    public abstract class MessageBase
    {
        public uint msgSeqNum;
        public short msgType;
        
        public virtual void Deserialize(NetworkReader reader)
        {
        }

        public virtual void Serialize(NetworkWriter writer)
        {
        }
    }
}
