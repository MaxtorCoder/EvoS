using EvoS.GameServer.Network.Unity;

namespace EvoS.GameServer.Network
{
    [UNetMessage(56)]
    public class ReconnectReplayStatus : MessageBase
    {
        public bool WithinReconnectReplay;

        public override void Serialize(NetworkWriter writer)
        {
            writer.Write(WithinReconnectReplay);
        }

        public override void Deserialize(NetworkReader reader)
        {
            WithinReconnectReplay = reader.ReadBoolean();
        }
    }
}
