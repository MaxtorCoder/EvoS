using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EvoS.Framework.Logging;
using EvoS.Framework.Network;
using EvoS.Framework.Network.Static;
using EvoS.GameServer.Network.Unity;
using NetSerializer;

namespace EvoS.GameServer.Network
{
    public sealed class UNetSerializer
    {
        private readonly Dictionary<short, Type> _serverTypesById = new Dictionary<short, Type>();
        private readonly Dictionary<short, Type> _clientTypesById = new Dictionary<short, Type>();
        private readonly Dictionary<Type, HashSet<short>> _serverIdsByType = new Dictionary<Type, HashSet<short>>();
        private readonly Dictionary<Type, HashSet<short>> _clientIdsByType = new Dictionary<Type, HashSet<short>>();

        private UNetSerializer()
        {
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes()
                .Concat(Assembly.GetEntryAssembly().GetTypes()))
            {
                var attribute = type.GetCustomAttribute<UNetMessageAttribute>();
                if (attribute == null)
                    continue;

                foreach (var msgId in attribute.ClientMsgIds)
                {
                    AddType(type, msgId, "Client", _clientTypesById, _clientIdsByType);
                }

                foreach (var msgId in attribute.ServerMsgIds)
                {
                    AddType(type, msgId, "Server", _serverTypesById, _serverIdsByType);
                }
            }

            Log.Print(LogType.GameServer,
                $"Loaded {_clientIdsByType.Count} client {(_clientIdsByType.Count == 1 ? "type" : "types")} and {_serverIdsByType.Count} server {(_serverIdsByType.Count == 1 ? "type" : "types")}");
        }

        public IEnumerable<MessageBase> ProcessUNetMessage(byte[] rawPacket)
        {
            var unetBytes = UNetMessage.Deserialize(rawPacket);

            var reader = new NetworkReader(unetBytes);
            while (reader.Position < unetBytes.Length)
            {
                var msg = new NetworkMessage();
                msg.msgSeqNum = reader.ReadUInt32();
                var msgSize = reader.ReadUInt16();
                msg.msgType = reader.ReadInt16();
                var buffer = reader.ReadBytes(msgSize);
                msg.reader = new NetworkReader(buffer);

                var deserialized = Deserialize(msg, buffer);

                if (deserialized != null)
                {
                    yield return deserialized;
                }
            }
        }

        public MessageBase Deserialize(NetworkMessage msg, byte[] buffer)
        {
            if (_clientTypesById.ContainsKey(msg.msgType))
            {
                try
                {
                    var type = _clientTypesById[msg.msgType];
                    var readMessage = (MessageBase) Activator.CreateInstance(type);
                    readMessage.Deserialize(msg.reader);
                    return readMessage;
                }
                catch (Exception e)
                {
                    Log.Print(LogType.Error, e);
                    Log.Print(LogType.Error,
                        $"Failed parsing message seq={msg.msgSeqNum}, size={msg.reader.Length}, type={msg.msgType}\n\t{Convert.ToBase64String(buffer)}");
                    return null;
                }
            }

            Log.Print(LogType.Warning,
                $"Received unknown message seq={msg.msgSeqNum}, size={msg.reader.Length}, type={msg.msgType}\n\t{Convert.ToBase64String(buffer)}");
            return null;
        }

        private static void AddType(Type messageType, short typeId, string mappingType,
            Dictionary<short, Type> typesById,
            Dictionary<Type, HashSet<short>> idsByType)
        {
            if (!idsByType.TryAdd(messageType, new HashSet<short> {typeId}) && !idsByType[messageType].Add(typeId))
            {
                throw new EvosSerializerTypeException(
                    $"Cannot map {mappingType} {messageType} to {typeId}, as it is already mapped to {idsByType[messageType]}");
            }

            if (!typesById.TryAdd(typeId, messageType) && typesById[typeId] != messageType)
            {
                throw new EvosSerializerTypeException(
                    $"Cannot map {mappingType} {typeId} to {messageType}, as it is already mapped to {typesById[typeId]}");
            }
        }

        public static UNetSerializer Instance { get; } = new UNetSerializer();
    }
}
