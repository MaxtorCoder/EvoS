using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EvoS.Framework.Network.NetworkMessages;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Logging;
using System.Reflection;

namespace EvoS.Framework.Network
{
    public class EvosMessageStream
    {
        private MemoryStream stream;
        private MemoryStream outputStream;
        private static Dictionary<int, Type> typesById = new Dictionary<int, Type>
        {
            /*[757] = typeof(JoinGameRequest),
            [758] = typeof(CreateGameResponse),
            [759] = typeof(CreateGameRequest),
            [760] = typeof(SyncNotification),
            [761] = typeof(SetRegionRequest),
            [762] = typeof(UnsubscribeFromCustomGamesRequest),
            [763] = typeof(SubscribeToCustomGamesRequest),
            [764] = typeof(LobbyCustomGamesNotification),
            [765] = typeof(List`1),
            [766] = typeof(LobbyGameInfo[]),
            [767] = typeof(LobbyGameplayOverridesNotification),// */
            [768] = typeof(LobbyStatusNotification),
            [769] = typeof(ServerMessageOverrides),
            [770] = typeof(ServerMessage),
            [771] = typeof(ServerLockState),
            [772] = typeof(ConnectionQueueInfo),
            /*[773] = typeof(LobbyServerReadyNotification),
            [774] = typeof(LobbyPlayerGroupInfo),
            [775] = typeof(EnvironmentType),
            [776] = typeof(List`1),
            [777] = typeof(PersistedCharacterData[]),// */
            [778] = typeof(RegisterGameClientResponse),
            [779] = typeof(LobbySessionInfo),
            [780] = typeof(ProcessType),
            [781] = typeof(AuthInfo),
            [782] = typeof(AuthType),
            [783] = typeof(RegisterGameClientRequest),
            [784] = typeof(LobbyGameClientSystemInfo),
            //[785] = typeof(AssignGameClientResponse),
            [786] = typeof(LobbyGameClientProxyInfo),
            //[787] = typeof(LobbyGameClientProxyStatus),
            //[788] = typeof(AssignGameClientRequest),


        };

        public EvosMessageStream(MemoryStream ms)
        {
            stream = ms;
            stream.Seek(0, SeekOrigin.Begin);

            outputStream = new MemoryStream();
        }

        public MemoryStream GetOutputStream()
        {
            return outputStream;
        }

        public bool ReadBool()
        {
            return stream.ReadByte() != 0;
        }

        public long ReadLong()
        {
            long n = (long)ReadVarInt();
            return (long)(n >> 1 ^ -(n & 1L));
        }

        public int ReadVarInt()
        {
            int shift = 0;
            int result = 0;
            int byteValue;

            while (true)
            {
                byteValue = stream.ReadByte();
                //Console.WriteLine("read value: " + byteValue);
                if (byteValue == -1) break;
                result |= ((byteValue & 0x7f) << shift);
                shift += 7;
                if ((byteValue & 0x80) != 0x80) break;
            }

            return result;
        }

        public String ReadString()
        {
            int data_length = ReadVarInt();

            byte[] buffer;

            if (data_length == 0)
            {
                return null;
            }
            else if (data_length == 1)
            {
                return string.Empty;
            }
            else
            {
                int string_length = ReadVarInt();
                buffer = new byte[string_length];
                stream.Read(buffer, 0, string_length);
                return Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            }
        }

        /*
         * Writes a number an returns the amount of bytes written
         */
        public int WriteVarInt(int value)
        {
            int byteValue;
            int byteCount = 0;

            while (true)
            {
                byteValue = value & 0x7f;
                value >>= 7;

                if (value != 0)
                {
                    byteValue |= 0x80;
                }
                //Console.WriteLine("writing byte: " + byteValue);
                outputStream.WriteByte((byte)byteValue);
                byteCount++;

                if (value == 0) return byteCount;
            }
        }

        public int WriteString(String str)
        {
            int byteCount = 0;

            if (str == null)
            {
                return WriteVarInt(0);
            }
            if (str.Length == 0)
            {
                return WriteVarInt(1);
            }

            byteCount += WriteVarInt(str.Length + 1);
            byteCount += WriteVarInt(str.Length);
            byte[] buffer = Encoding.GetEncoding("UTF-8").GetBytes(str.ToCharArray());
            outputStream.Write(buffer, 0, buffer.Length);
            byteCount += buffer.Length;

            return byteCount;
        }

        public int WriteBool(bool value)
        {
            outputStream.WriteByte(value ? ((byte)1) : ((byte)0));
            return 1;
        }

        public object ReadGeneral()
        {
            /*
             * Note to self: properties and fields are read by declaration order, so i should sort them by name on declaration
             */
            ILog Log = new Log();
            int type_id = ReadVarInt();
            
            if (type_id == 0){
                return null;
            } else if (type_id == 1) {
                return new object();
            } else {
                Type T;
                if (EvosMessageStream.typesById.TryGetValue(type_id, out T))
                {
                    Console.WriteLine("Deserializing " + T.Name);
                    object obj = T.GetConstructor(Type.EmptyTypes).Invoke(new object[] { });
                    //BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly;
                    MemberInfo[] members = T.GetMembers();

                    for (int i = 0; i < members.Length; i++) {
                        MemberInfo member = members[i];
                        

                        if (member.MemberType == MemberTypes.Field && !(((FieldInfo)member).IsNotSerialized))
                        {
                            FieldInfo field = (FieldInfo)member;

                            if (field.FieldType == typeof(String))
                            {
                                String value = ReadString();
                                Console.WriteLine($"{T.Name}.{member.Name} ({field.FieldType}) = {value}");
                                T.GetField(field.Name).SetValue(obj, value);
                            }
                            else if (field.FieldType == typeof(long))
                            {
                                long value = ReadLong();
                                Console.WriteLine($"{T.Name}.{member.Name} ({field.FieldType}) = {value}");
                                T.GetField(field.Name).SetValue(obj, value);
                            }
                            else if (field.FieldType == typeof(Int32) || field.FieldType == typeof(int))
                            {
                                int value = ReadVarInt();
                                Console.WriteLine($"{T.Name}.{member.Name} ({field.FieldType}) = {value}");
                                T.GetField(field.Name).SetValue(obj, value);
                            }
                            else if (field.FieldType.IsEnum)
                            {
                                int value = ReadVarInt();
                                Console.WriteLine($"{T.Name}.{member.Name} ({field.FieldType}) = {value}");
                                T.GetField(field.Name).SetValue(obj, value);
                            }
                            else if (field.FieldType == typeof(bool))
                            {
                                bool value = ReadBool();
                                Console.WriteLine($"{T.Name}.{member.Name} ({field.FieldType}) = {value}");
                                T.GetField(field.Name).SetValue(obj, value);
                            }
                            else
                            {
                                Console.WriteLine($"==== Woops! =====: I dont know how to read {field.GetType().Name}");
                                T.GetField(field.Name).SetValue(obj, ReadGeneral());
                            }
                        }
                        else if (member.MemberType == MemberTypes.Property)
                        {
                            PropertyInfo property = (PropertyInfo)member;

                            if (property.PropertyType == typeof(String))
                            {
                                String value = ReadString();
                                Console.WriteLine($"{T.Name}.{member.Name} ({property.PropertyType}) = {value}");
                                T.GetProperty(property.Name).SetValue(obj, value);
                            }
                            else if (property.PropertyType == typeof(long))
                            {
                                long value = ReadLong();
                                Console.WriteLine($"{T.Name}.{member.Name} ({property.PropertyType}) = {value}");
                                T.GetProperty(property.Name).SetValue(obj, value);
                            }
                            else if (property.PropertyType == typeof(Int32) || property.PropertyType == typeof(int))
                            {
                                int value = ReadVarInt();
                                Console.WriteLine($"{T.Name}.{member.Name} ({property.PropertyType}) = {value}");
                                T.GetProperty(property.Name).SetValue(obj, value);
                            }
                            else if (property.PropertyType == typeof(bool))
                            {
                                bool value = ReadBool();
                                Console.WriteLine($"{T.Name}.{member.Name} ({property.PropertyType}) = {value}");
                                T.GetProperty(property.Name).SetValue(obj, value);
                            }
                            else if (property.PropertyType.IsEnum)
                            {
                                int value = ReadVarInt();
                                Console.WriteLine($"{T.Name}.{member.Name} ({property.PropertyType}) = {value}");
                                T.GetProperty(property.Name).SetValue(obj, value);
                            }
                            else
                            {
                                Console.WriteLine($"{T.Name}.{member.Name}");
                                Console.WriteLine($"==== Woops! =====: trying to read property of type {property.PropertyType.Name}");
                                T.GetProperty(property.Name).SetValue(obj, ReadGeneral());
                            }
                        }
                        else {
                            // Not a property and not a field, should do nothing
                            //Console.WriteLine($"what teh heck is a {member.MemberType.GetTypeCode()}");
                        }
                        
                    }

                    return obj;
                }
                else
                {
                    throw new EvosMessageStreamException($"Can't Deserialize TypeID {type_id}: Not registered");
                }
            }
        }
    }
}
