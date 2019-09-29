using System;
using System.Threading;

namespace EvoS.Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            Banner.PrintBanner();

            // Directory Server
            var thread = new Thread(() => StartDirServer());
            thread.Priority = ThreadPriority.Highest;
            thread.Start();

            // Lobby Server
            new Thread(() => StartLobbyServer()).Start();

            // Game Server
            new Thread(() => StartGameServer()).Start();
        }

        static void StartDirServer()
        {
            DirectoryServer.Program.Main();
        }

        static void StartLobbyServer()
        {
            Thread.Sleep(1500);
            LobbyServer.Program.Main();
        }
        
        static void StartGameServer()
        {
            Thread.Sleep(2500);
            GameServer.GameServer.Main();
        }
    }
}
