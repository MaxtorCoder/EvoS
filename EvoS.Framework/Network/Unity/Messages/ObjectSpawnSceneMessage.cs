using System.Numerics;

namespace EvoS.Framework.Network.Unity.Messages
{
    [UNetMessage(serverMsgIds: new short[] {10})]
    public class ObjectSpawnSceneMessage : MessageBase
    {
        public override void Deserialize(NetworkReader reader)
        {
            netId = reader.ReadNetworkId();
            sceneId = reader.ReadSceneId();
            position = reader.ReadVector3();
            payload = reader.ReadBytesAndSize();
        }

        public override void Serialize(NetworkWriter writer)
        {
            writer.Write(netId);
            writer.Write(sceneId);
            writer.Write(position);
            writer.WriteBytesFull(payload);
        }

        public NetworkInstanceId netId;
        public NetworkSceneId sceneId;
        public Vector3 position;
        public byte[] payload;
    }
}
