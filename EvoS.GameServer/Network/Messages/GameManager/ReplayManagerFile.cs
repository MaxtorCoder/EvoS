using EvoS.GameServer.Network.Unity;

namespace EvoS.GameServer.Network
{
    [UNetMessage(48)]
    public class ReplayManagerFile : MessageBase
    {
        public string Fragment;
        public bool Restart;
        public bool Save;

        public override void Serialize(NetworkWriter writer)
        {
            writer.Write(Fragment);
            writer.Write(Restart);
            writer.Write(Save);
        }

        public override void Deserialize(NetworkReader reader)
        {
            Fragment = reader.ReadString();
            Restart = reader.ReadBoolean();
            Save = reader.ReadBoolean();
        }
    }
}
