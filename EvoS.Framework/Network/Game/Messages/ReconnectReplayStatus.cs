using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.Game.Messages
{
    [UNetMessage(serverMsgIds: new short[] {56})]
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

        public override string ToString()
        {
            return $"{nameof(ReconnectReplayStatus)}(" +
                   $"{nameof(WithinReconnectReplay)}: {WithinReconnectReplay}" +
                   ")";
        }
    }
}
