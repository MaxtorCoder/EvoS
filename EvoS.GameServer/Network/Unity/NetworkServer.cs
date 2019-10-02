using System;
using System.Collections.Generic;
using EvoS.Framework.Logging;
using EvoS.Framework.Network.Unity;
using EvoS.GameServer.Network.Messages.Unity;

namespace EvoS.GameServer.Network.Unity
{
    public class NetworkServer
    {
        public static ushort maxPacketSize = 1440;
        public List<ClientConnection> connections = new List<ClientConnection>();
        public int numChannels => 36; // TODO

        public void SendWriterToReady(
            GameObject contextObj,
            NetworkWriter writer,
            int channelId)
        {
            if (writer.AsArraySegment().Count > short.MaxValue)
                throw new IndexOutOfRangeException("NetworkWriter used buffer is too big!");
            SendBytesToReady(contextObj, writer.AsArraySegment().Array, writer.AsArraySegment().Count, channelId);
        }

        public void SendBytesToReady(GameObject contextObj, byte[] buffer, int numBytes, int channelId)
        {
            if (contextObj == null)
            {
                bool flag = true;
                for (int index = 0; index < connections.Count; ++index)
                {
                    ClientConnection connection = connections[index];
                    if (connection != null && connection.isReady && !connection.SendBytes(buffer, numBytes, channelId))
                        flag = false;
                }

                if (flag)
                    return;
                Log.Print(LogType.Warning, "SendBytesToReady failed");
            }
            else
            {
                var component = contextObj.GetComponent<NetworkIdentity>();
                try
                {
                    var flag = true;
                    var count = component.observers.Count;
                    for (int index = 0; index < count; ++index)
                    {
                        var observer = component.observers[index];
                        if (observer.isReady && !observer.SendBytes(buffer, numBytes, channelId))
                            flag = false;
                    }

                    if (flag)
                        return;
                    Log.Print(LogType.Warning, $"SendBytesToReady failed for {contextObj}");
                }
                catch (NullReferenceException ex)
                {
                    Log.Print(LogType.Warning, $"SendBytesToReady object {contextObj} has not been spawned");
                }
            }
        }

        public bool SendToReady(GameObject contextObj, short msgType, MessageBase msg)
        {
            Log.Print(LogType.Debug, $"Server.SendToReady id:{msgType}");
            if (contextObj == null)
            {
                foreach (var networkConnection in connections)
                {
                    if (networkConnection != null && networkConnection.isReady)
                    {
                        networkConnection.Send(msgType, msg);
                    }
                }

                return true;
            }

            NetworkIdentity component = contextObj.GetComponent<NetworkIdentity>();
            if (component?.observers == null)
            {
                return false;
            }

            int count = component.observers.Count;
            for (int j = 0; j < count; j++)
            {
                ClientConnection networkConnection2 = component.observers[j];
                if (networkConnection2.isReady)
                {
                    networkConnection2.Send(msgType, msg);
                }
            }

            return true;
        }


        public void SendSpawnMessage(NetworkIdentity uv, ClientConnection conn)
        {
            if (!uv.serverOnly)
            {
                if (uv.sceneId.IsEmpty())
                {
                    ObjectSpawnMessage objectSpawnMessage = new ObjectSpawnMessage();
                    objectSpawnMessage.netId = uv.netId;
                    objectSpawnMessage.assetId = uv.assetId;
                    objectSpawnMessage.position = uv.transform.position;
                    objectSpawnMessage.rotation = uv.transform.rotation;
                    NetworkWriter networkWriter = new NetworkWriter();
                    uv.UNetSerializeAllVars(networkWriter);
                    if (networkWriter.Position > 0)
                    {
                        objectSpawnMessage.payload = networkWriter.ToArray();
                    }

                    if (conn != null)
                    {
                        conn.Send(3, objectSpawnMessage);
                    }
                    else
                    {
                        SendToReady(uv.gameObject, 3, objectSpawnMessage);
                    }
                }
                else
                {
                    ObjectSpawnSceneMessage objectSpawnSceneMessage = new ObjectSpawnSceneMessage();
                    objectSpawnSceneMessage.netId = uv.netId;
                    objectSpawnSceneMessage.sceneId = uv.sceneId;
                    objectSpawnSceneMessage.position = uv.transform.position;
                    NetworkWriter networkWriter2 = new NetworkWriter();
                    uv.UNetSerializeAllVars(networkWriter2);
                    if (networkWriter2.Position > 0)
                    {
                        objectSpawnSceneMessage.payload = networkWriter2.ToArray();
                    }

                    if (conn != null)
                    {
                        conn.Send(10, objectSpawnSceneMessage);
                    }
                    else
                    {
                        SendToReady(uv.gameObject, 3, objectSpawnSceneMessage);
                    }
                }
            }
        }

        public void ShowForConnection(NetworkIdentity uv, ClientConnection conn)
        {
            if (conn.isReady)
            {
                SendSpawnMessage(uv, conn);
            }
        }
    }
}
