using EvoS.Framework.Logging;
using EvoS.Framework.Network.NetworkMessages;
using EvoS.LobbyServer.NetworkMessageHandlers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EvoS.LobbyServer
{
    static class LobbyManager
    {
        /// <summary>
        /// When the player starts the game and connects to lobby
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async static Task OnPlayerConnectToLobby(LobbyServerConnection client, RegisterGameClientRequest request)
        {
            await new RegisterGameClientRequestHandler().OnMessage(client, request);
            Log.Print(LogType.Lobby, $"{client.PlayerInfo.GetHandle()} ID {client.PlayerInfo.GetAccountId()}, Token {client.SessionToken} connected to the server");
        }

        /// <summary>
        /// Activates when the user changes the game mode (Practice, Vs Bots, PVP, Ranked, Custom) or change some options of the game mode
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async static Task OnPlayerSelectGameType(LobbyServerConnection client, SetGameSubTypeRequest request)
        {
            await new SetGameSubTypeRequestHandler().OnMessage(client, request);
        }

        /// <summary>
        /// When the user sends a message to chat
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        public async static Task OnChat(LobbyServerConnection client, ChatNotification notification)
        {
            await new ChatNotificationHandler().OnMessage(client, notification);
        }

        public async static Task OnJoinMatchmakingQueue(LobbyServerConnection client, JoinMatchmakingQueueRequest request)
        {
            await client.SendMessage(new JoinMatchmakingQueueResponse() { ResponseId = request.RequestId });
            Log.Print(LogType.Lobby, $"{client.PlayerInfo.GetHandle()} joined Matchmaking queue");

            client.PlayerInfo.SetGameType(request.GameType);
            client.AllyBotDifficulty = request.AllyBotDifficulty;
            client.EnemyBotDifficulty = request.EnemyBotDifficulty;

            LobbyQueueManager.AddPlayerToQueue(client);
        }

        public async static Task OnLeaveMatchmakingQueue(LobbyServerConnection client, LeaveMatchmakingQueueRequest request)
        {
            Log.Print(LogType.Lobby, $"{client.PlayerInfo.GetHandle()} left Matchmaking queue");
            await new LeaveMatchmakingQueueRequestHandler().OnMessage(client, request);
        }

        public async static Task OnPricesRequest(LobbyServerConnection client, PricesRequest request)
        {
            await new PricesRequestHandler().OnMessage(client, request);
        }

        public async static Task OnCustomKeyBindNotification(LobbyServerConnection client, CustomKeyBindNotification notification)
        {
            await Task.CompletedTask;
        }

        public async static Task OnClientStatusReport(LobbyServerConnection client, ClientStatusReport request)
        {
            await Task.CompletedTask;
        }

        public async static Task OnPlayerUpdateStatus(LobbyServerConnection client, PlayerUpdateStatusRequest request)
        {
            await new PlayerUpdateStatusRequestHandler().OnMessage(client, request);
        }

        public async static Task OnOptionsNotification(LobbyServerConnection client, OptionsNotification notification)
        {
            await new OptionsNotificationHandler().OnMessage(client, notification);
        }

        /// <summary>
        /// The client send this when there is a change in the LobbyPlayerInfo
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async static Task OnPlayerInfoUpdate(LobbyServerConnection client, PlayerInfoUpdateRequest request)
        {
            await new PlayerInfoUpdateRequestHandler().OnMessage(client, request);
        }

        public async static Task OnPreviousGameInfo(LobbyServerConnection client, PreviousGameInfoRequest request)
        {
            // Ask for last game played, it probably has the information needed to reconnect to an active match
            //TODO
            await Task.CompletedTask;
        }

        public async static Task OnCheckAccountStatus(LobbyServerConnection client, CheckAccountStatusRequest request)
        {
            // Offers quest to the player to complete
            //TODO
            await Task.CompletedTask;
        }

        public async static Task OnCheckRAFStatus(LobbyServerConnection client, CheckRAFStatusRequest request)
        {
            await Task.CompletedTask;
        }

        public async static Task OnPlayerMatchData(LobbyServerConnection client, PlayerMatchDataRequest request)
        {
            await new PlayerMatchDataRequestHandler().OnMessage(client, request);
        }

        public async static Task OnCrashReportArchiveNameRequest(LobbyServerConnection client, CrashReportArchiveNameRequest request)
        {
            await new CrashReportArchiveNameRequestHandler().OnMessage(client, request);
        }

        public async static Task OnClientErrorSummary(LobbyServerConnection client, ClientErrorSummary request)
        {
            await Task.CompletedTask;
        }
    }
}
