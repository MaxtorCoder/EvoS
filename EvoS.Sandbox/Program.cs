using System;
using System.Threading;
using EvoS.Framework.Assets;
using EvoS.Framework.Logging;
using EvoS.Framework.Network.NetworkBehaviours;
using McMaster.Extensions.CommandLineUtils;

namespace EvoS.Sandbox
{
    class Program
    {
        public static int Main(string[] args)
        {
            CommandLineApplication.Execute<Program>(args);
            Console.ReadLine();
            return 0;
        }
        
        
        [Option(Description = "Path to AtlasReactor_Data", ShortName = "D")]
        public string Assets { get; }

        private void OnExecute()
        {
            Banner.PrintBanner();
            Log.Print(LogType.Debug, "Asset folder hint: "+ System.Convert.ToString(Assets));
            if (!AssetLoader.FindAssetRoot(Assets))
            {
                Log.Print(LogType.Error, "AtlasReactor_Data folder not found, please specify with --assets!");
                Log.Print(LogType.Misc, "Alternatively, place Win64 or AtlasReactor_Data in this folder.");
                //return;
            }


            // Directory Server
            Thread t = new Thread(() => StartDirServer());
            t.Start();

            // Lobby Server
            //new Thread(() => StartLobbyServer()).Start();
            CentralServer.CentralServer.Main(new string[] { });
            
            // Game Server
            //new Thread(() => StartGameServer()).Start();
            
        }

        static void StartDirServer()
        {
            DirectoryServer.Program.Main();
        }

        static void StartLobbyServer()
        {
            Thread.Sleep(1750);
            LobbyServer.LobbyServer.Main();
        }
        
        static void StartGameServer()
        {
            Thread.Sleep(2750);
            GameServer.GameServer.Main();
        }
    }
}
