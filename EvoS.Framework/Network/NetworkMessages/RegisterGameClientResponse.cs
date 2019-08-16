using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;
using System;
using System.IO;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    public class RegisterGameClientResponse : WebSocketResponseMessage
    {
        public new static int MessageTypeID = 778;

        public LobbySessionInfo SessionInfo;
        public AuthInfo AuthInfo;
        public LobbyStatusNotification Status;
        public string DevServerConnectionUrl;
        public LocalizationPayload LocalizedFailure;

        public override void CreateFromStream(EvosMessageStream message)
        {
            throw new NotImplementedException();
        }

        public void WriteData(EvosMessageStream stream)
        {
            //TODO: write self data to stream in alphabetic order
            //TODO modify dll to test if type id comes as 0 if object is null

            /*
            this.AuthInfo.WriteData(stream);
            stream.WriteString(this.DevServerConnectionUrl);
            this.LocalizedFailure.WriteData();
            this.SessionInfo.WriteData();
            this.Status.WriteData();
            */
        }
    }
}
