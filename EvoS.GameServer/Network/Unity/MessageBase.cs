namespace EvoS.GameServer.Network.Unity
{
    public abstract class MessageBase
    {
        public virtual void Deserialize(NetworkReader reader)
        {
        }

        public virtual void Serialize(NetworkWriter writer)
        {
        }
    }
}
