using EvoS.Framework.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace EvoS.LobbyServer.Network.Message
{
    public class LobbyGameClientSessionManager : WebSocketBehavior
    {
        private ILog Log;

        public LobbyGameClientSessionManager() : this(null) { }

        public LobbyGameClientSessionManager(ILog newLog)
        {
            Log = newLog;
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            Log.Print(LogType.Debug, $"{e.Data}");
        }

        protected override void OnOpen()
        {
            
            base.OnOpen();
        }
    }
}
