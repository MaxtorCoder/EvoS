using EvoS.Framework.Logging;
using EvoS.LobbyServer.Network.Message;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using WebSocketSharp.Server;

namespace EvoS.LobbyServer.Network
{
    public class AuthServer : IDisposable
    {
        private ILog Log = new Log();
        private WebSocketServer webServer;

        public void StartServer()
        {
            try
            {
                webServer = new WebSocketServer(6060);
                webServer.AddWebSocketService("/LobbyGameClientSessionManager", () => new LobbyGameClientSessionManager(Log));
                webServer.Start();

                Log.Print(LogType.Server, "Started Webserver..");
                Console.ReadKey(true);
                Dispose();
            }
            catch (Exception ex)
            {
                Log.Print(LogType.Error, ex.Message);
                Log.Print(LogType.Error, ex.StackTrace);
            }
        }

        public void Dispose()
        {
            webServer.Stop();
        }
    }
}
