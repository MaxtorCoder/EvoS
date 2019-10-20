namespace EvoS.Framework.Network.Unity.Messages
{
    [UNetMessage(serverMsgIds: new short[] {12})]
    public class ObjectSpawnFinishedMessage : MessageBase
    {
        public uint state;

        public override void Deserialize(NetworkReader reader)
        {
            state = reader.ReadPackedUInt32();
        }

        public override void Serialize(NetworkWriter writer)
        {
            writer.WritePackedUInt32(state);
        }

        public override string ToString()
        {
            return $"{nameof(ObjectSpawnFinishedMessage)}(" +
                   $"{nameof(state)}: {state}" +
                   ")";
        }
    }
}
