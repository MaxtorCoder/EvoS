using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.Game.Messages
{
    [UNetMessage(serverMsgIds: new short[] {54})]
    public class SpawningObjectsNotification : MessageBase
    {
        public int PlayerId;
        public int SpawnableObjectCount;

        public override void Serialize(NetworkWriter writer)
        {
            writer.WritePackedUInt32((uint) PlayerId);
            writer.WritePackedUInt32((uint) SpawnableObjectCount);
        }

        public override void Deserialize(NetworkReader reader)
        {
            PlayerId = (int) reader.ReadPackedUInt32();
            SpawnableObjectCount = (int) reader.ReadPackedUInt32();
        }

        public override string ToString()
        {
            return $"{nameof(SpawningObjectsNotification)}(" +
                   $"{nameof(PlayerId)}: {PlayerId}, " +
                   $"{nameof(SpawnableObjectCount)}: {SpawnableObjectCount}" +
                   ")";
        }
    }
}
