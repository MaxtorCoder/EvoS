using System;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace CentralServer
{
    public class CentralServer
    {
        public static void Main(string[] args)
        {
            WebSocketServer server = new WebSocketServer(6060);
            server.AddWebSocketService<LobbyServer.LobbyServerProtocol>("/LobbyGameClientSessionManager");
            server.AddWebSocketService<BridgeServer.BridgeServerProtocol>("/BridgeServer");
            server.Log.Level = LogLevel.Debug;


            server.Start();
            Console.WriteLine("Lobby server started");
            Console.ReadLine();
            //Console.ReadKey();
            //server.Stop();
        }
    }
}
