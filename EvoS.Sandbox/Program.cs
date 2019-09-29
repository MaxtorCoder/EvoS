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
            var thread = new Thread(() => DirectoryServer.Program.Main());
            thread.Priority = ThreadPriority.Highest;
            thread.Start();

            // Lobby Server
            new Thread(() => LobbyServer.Program.Main()).Start();
        }
    }
}
