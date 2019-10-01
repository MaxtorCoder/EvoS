using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using EvoS.Framework.Logging;
using EvoS.GameServer.Network;
using EvoS.GameServer.Network.Messages.GameManager;
using EvoS.GameServer.Network.Messages.Unity;
using EvoS.GameServer.Network.Unity;
using vtortola.WebSockets;

namespace EvoS.GameServer
{
    public delegate void UNetGameMessageDelegate<T>(GamePlayer player, T msg) where T : MessageBase;

    public class ClientConnection
    {
        private WebSocket Socket;
        public GameManager ActiveGame { private get; set; }

        public string address => Socket.RemoteEndpoint.ToString();

        private UNetSerializer Serializer = new UNetSerializer();
        private HashSet<NetworkIdentity> _visList = new HashSet<NetworkIdentity>();
        private HashSet<NetworkInstanceId> _clientOwnedObjects;
        public bool isReady;
        private static int _connectionIdCounter;
        private uint lastMessageOutgoingSeqNum;
        public readonly int connectionId = Interlocked.Increment(ref _connectionIdCounter);
        private NetworkWriter m_Writer = new NetworkWriter();

        public ClientConnection(WebSocket socket)
        {
            Socket = socket;
            isReady = true;

            SetupLoginHandler();
        }

        public void RegisterHandler<T>(short msgId, GamePlayer player, UNetGameMessageDelegate<T> handler)
            where T : MessageBase
        {
            Serializer.RegisterHandler(msgId, msg => handler(player, (T) msg));
        }

        private void SetupLoginHandler()
        {
            // The client sends AddPlayer and then LoginRequest, instead we'll use the session token to determine
            // which Game the connection is fore

            AddPlayerMessage addPlayerMessage = null;
            Serializer.RegisterHandler(37, msg => { addPlayerMessage = (AddPlayerMessage) msg; });
            Serializer.RegisterHandler(51, msg =>
            {
                var loginReq = (LoginRequest) msg;
                if (addPlayerMessage == null)
                {
                    Log.Print(LogType.Error, "Received LoginRequest before AddPlayerMessage!");
                    Disconnect();
                    return;
                }

                var gameManager = GameServer.FindGameManager(loginReq);
                if (gameManager == null)
                {
                    Log.Print(LogType.Error, $"Didn't find a GameManager for {loginReq}'");
                    Disconnect();
                    return;
                }

                gameManager.AddPlayer(this, addPlayerMessage);
            });
        }

        internal void AddToVisList(NetworkIdentity uv)
        {
            _visList.Add(uv);
            ActiveGame.NetworkServer.ShowForConnection(uv, this);
        }

        internal void RemoveFromVisList(NetworkIdentity uv, bool isDestroyed)
        {
            _visList.Remove(uv);
            if (!isDestroyed)
            {
//                NetworkServer.HideForConnection(uv, this);
                Send(13, new ObjectDestroyMessage
                {
                    netId = uv.netId
                });
            }
        }

        internal void RemoveObservers()
        {
            foreach (NetworkIdentity networkIdentity in _visList)
            {
                networkIdentity.RemoveObserverInternal(this);
            }

            _visList.Clear();
        }

        internal void AddOwnedObject(NetworkIdentity obj)
        {
            if (_clientOwnedObjects == null)
            {
                _clientOwnedObjects = new HashSet<NetworkInstanceId>();
            }

            _clientOwnedObjects.Add(obj.netId);
        }

        internal void RemoveOwnedObject(NetworkIdentity obj)
        {
            _clientOwnedObjects?.Remove(obj.netId);
        }


        public void Disconnect()
        {
            Log.Print(LogType.GameServer, "Client disconnected.");
            if (Socket.IsConnected)
                Socket.Close();

            isReady = false;

            Socket.Dispose();
        }

        public virtual void SendByChannel(short msgType, MessageBase msg, int channelId)
        {
            m_Writer.StartMessage(msgType);
            msg.Serialize(m_Writer);
            m_Writer.FinishMessage();
            SendWriter(m_Writer, channelId);
        }

        public virtual void SendWriter(NetworkWriter writer, int channelId)
        {
            writer.WriteSeqNum(++lastMessageOutgoingSeqNum);
            channelId = 0;
            SendWSMessage(writer);
        }

        public virtual bool SendBytes(byte[] bytes, int numBytes, int channelId)
        {
            uint num = ++lastMessageOutgoingSeqNum;
            bytes[0] = (byte) (num & byte.MaxValue);
            bytes[1] = (byte) (num >> 8 & byte.MaxValue);
            bytes[2] = (byte) (num >> 16 & byte.MaxValue);
            bytes[3] = (byte) (num >> 24 & byte.MaxValue);
            channelId = 0;
            throw new NotImplementedException();
        }

        public void Send(short msgType, MessageBase msg)
        {
            SendByChannel(msgType, msg, 0);
        }

        public async Task SendWSMessage(NetworkWriter msg)
        {
            var responseStream = new MemoryStream();
            responseStream.Write(msg.AsArraySegment().Array, 0, msg.AsArraySegment().Count);

            using (var writer = Socket.CreateMessageWriter(WebSocketMessageType.Binary))
            {
                await writer.WriteAsync(UNetMessage.Serialize(responseStream.ToArray()));
                await writer.FlushAsync();
            }
        }

        public async Task SendAsync(short msgId, object message)
        {
            throw new NotImplementedException();
        }

        public async void HandleConnection()
        {
            while (true)
            {
                var message = await Socket.ReadMessageAsync(CancellationToken.None);

                if (message == null)
                {
                    Log.Print(LogType.Warning, "Message is null");
                    Disconnect();
                    return;
                }

                if (message.MessageType != WebSocketMessageType.Binary)
                {
                    throw new NotImplementedException();
                }

                using (var ms = new MemoryStream())
                {
                    await message.CopyToAsync(ms);
                    await ms.FlushAsync();

                    var messageBytes = ms.ToArray();

                    Serializer.ProcessUNetMessage(messageBytes);
                }

                message.Dispose();
            }
        }
    }
}
