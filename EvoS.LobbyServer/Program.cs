using EvoS.LobbyServer.Network;
using System;

namespace EvoS.LobbyServer
{
    class Program
    {
        static AuthServer server;

        static void Main(string[] args)
        {
            server = new AuthServer();
            server.StartServer();
        }
    }
}
