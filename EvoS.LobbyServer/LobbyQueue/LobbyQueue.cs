using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Game;
using EvoS.Framework.Logging;
using EvoS.Framework.Network.NetworkMessages;
using EvoS.Framework.Network.Static;
using EvoS.LobbyServer.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.LobbyServer.LobbyQueue
{
    public class LobbyQueue
    {
        GameType GameType;
        public List<GameSubType> SubTypes;
        private List<ClientConnection> Players;
        private LobbyMatchmakingQueueInfo QueueInfo;
        private int MatchId;

        private class QueueSlot {

            public bool Empty;
            public bool IsBot;
            public ClientConnection Client;

            public QueueSlot() {
                Empty = true;
                IsBot = false;
                Client = null;
            }
        }


        public LobbyQueue(GameType gameType, LobbyGameConfig gameConfig)
        {
            GameType = gameType;
            SubTypes = gameConfig.SubTypes;
            Players = new List<ClientConnection>();
            QueueInfo = new LobbyMatchmakingQueueInfo()
            {
                QueuedPlayers = 0,
                AverageWaitTime = TimeSpan.FromSeconds(500),
                GameConfig = gameConfig,
                PlayersPerMinute = 1,
                QueueStatus = Framework.Constants.Enums.QueueStatus.Idle,
                ShowQueueSize = true
            };
            MatchId = 0;
        }

        public GameSubType GetDefaulSubType()
        {
            if (SubTypes.Count > 0)
                return SubTypes[0];
            throw new LobbyQueueExceptions.MissingDefaultSubType();
        }

        public void AddPlayer(ClientConnection client)
        {
            Log.Print(LogType.Lobby, $"Player {client.UserName} joined {GameType.ToString()} Queue");

            Players.Add(client);
            QueueInfo.QueuedPlayers++;

            LobbyMatchmakingQueueInfo info = QueueInfo.Clone();
            info.QueueStatus = Framework.Constants.Enums.QueueStatus.Success;


            MatchmakingQueueAssignmentNotification assignmentNotification = new MatchmakingQueueAssignmentNotification()
            {
                Reason = "",
                MatchmakingQueueInfo = info
            };

            _ = client.SendMessage(assignmentNotification);

            Update();
        }

        public void RemovePlayer(ClientConnection client)
        {
            Log.Print(LogType.Debug, $"Removing player {client} from {GameType.ToString()} Queue");
            Players.Remove(client);
            QueueInfo.QueuedPlayers--;
        }

        public void Notify(ClientConnection client)
        {
            MatchmakingQueueStatusNotification notification = new MatchmakingQueueStatusNotification()
            {
                MatchmakingQueueInfo = QueueInfo
            };

            _ = client.SendMessage(notification);
        }

        public void Update()
        {
            foreach(ClientConnection client in Players)
            {
                Notify(client);
            }

            foreach (GameSubType subType in SubTypes)
            {
                MakeMatchSubType(subType);
            }
        }

        private void MakeMatchSubType(GameSubType subType) {

            TeamCompositionRules composition = subType.TeamComposition;
            List<ClientConnection> availablePlayers = new List<ClientConnection>(Players);
            LobbyTeamInfo teamInfo = new LobbyTeamInfo() { TeamPlayerInfo = new List<LobbyPlayerInfo>() };

            if (
                FillTeam(Team.TeamA, composition, subType.TeamAPlayers, subType.TeamABots, ref availablePlayers, ref teamInfo)
                && FillTeam(Team.TeamB, composition, subType.TeamBPlayers, subType.TeamBBots, ref availablePlayers, ref teamInfo)
            ) {
                for (int i = 0; i<teamInfo.TeamPlayerInfo.Count; i++)
                {
                    teamInfo.TeamPlayerInfo[i].PlayerId = i;
                    teamInfo.TeamPlayerInfo[i].ControllingPlayerId = i;

                    if (!teamInfo.TeamPlayerInfo[i].IsNPCBot) {

                    }
                }

                LobbyGameConfig gameConfig = QueueInfo.GameConfig.Clone();
                gameConfig.RoomName = GenerateRoomName();
                gameConfig.Map = subType.GameMapConfigs[new Random().Next(0, subType.GameMapConfigs.Count - 1)].Map;

                LobbyGameInfo gameInfo = new LobbyGameInfo()
                {
                    GameConfig = gameConfig,
                    AcceptedPlayers = teamInfo.TotalPlayerCount,
                    ActiveSpectators = 0,
                    AcceptTimeout = TimeSpan.FromSeconds(30),
                    CreateTimestamp = DateTime.Now.Ticks,
                    GameResult = GameResult.NoResult,
                    GameServerAddress = "ws://127.0.0.1:6061",
                    GameStatus = GameStatus.Launched,
                    GameServerHost = gameConfig.RoomName,
                    GameServerProcessCode = null,
                    LoadoutSelectTimeout = TimeSpan.FromSeconds(30),
                    IsActive = true,
                    LoadoutSelectionStartTimestamp = DateTime.Now.Ticks,
                    SelectionStartTimestamp = DateTime.Now.Ticks,
                    SelectedBotSkillTeamA = BotDifficulty.Medium,
                    SelectedBotSkillTeamB = BotDifficulty.Medium,
                    SelectionSubPhase = FreelancerResolutionPhaseSubType.UNDEFINED,
                    SelectionSubPhaseStartTimestamp = DateTime.Now.Ticks,
                    SelectTimeout = TimeSpan.FromSeconds(20),
                    UpdateTimestamp = DateTime.Now.Ticks
                };

                if (GameType == GameType.Practice) {
                    gameInfo.AcceptTimeout = TimeSpan.Zero;
                }
                
                Log.Print(LogType.Debug, "Removing players from queue...");
                foreach (LobbyPlayerInfo player in teamInfo.TeamPlayerInfo)
                {
                    if (!player.IsNPCBot) {
                        //Log.Print(LogType.Debug, $"found player {player.Handle}");
                        for (int i = 0; i < Players.Count; i++) {
                            if (Players[i].AccountId == player.AccountId)
                            {
                                RemovePlayer(Players[i]);
                                break;
                            }
                        }
                    }
                }
                
                LobbyQueueManager.CreateGame(gameInfo, teamInfo);
            }
        }

        private bool FillTeam(Team team, TeamCompositionRules compositionRules, int maxPlayers, int maxBots, ref List<ClientConnection> availablePlayers, ref LobbyTeamInfo teamInfo)
        {
            Log.Print(LogType.Debug, $"Filling Team {team.ToString()}");
            int usedPlayers = 0;
            int usedBots = 0;

            List<LobbyPlayerInfo> playerInfo = new List<LobbyPlayerInfo>();
            
            for (int slot = 1; slot <= 5; slot++) {
                //Log.Print(LogType.Debug, "fillteam:: slot "+slot);
                FreelancerSet botSet = new FreelancerSet() { Roles = new List<CharacterRole>(), Types = new List<CharacterType>() };
                bool filled = false;

                if (usedPlayers != maxPlayers)
                {
                    foreach (ClientConnection player in availablePlayers)
                    {
                        bool matchedAny = false;
                        bool matched = true;

                        //Log.Print(LogType.Debug, "matching rules with " + player.UserName);

                        foreach (TeamCompositionRules.SlotTypes slotType in compositionRules.Rules.Keys)
                        {
                            if (compositionRules.MatchesSlotType(slotType, team, slot))
                            {
                                matchedAny = true;
                                //Log.Print(LogType.Debug, "Matched slotType " + slotType.ToString());

                                if (!CharacterUtils.MatchesCharacter(player.SelectedCharacter, compositionRules.Rules[slotType]))
                                {
                                    //Log.Print(LogType.Debug, "not matched rule");
                                    matched = false;

                                    if (usedBots == maxBots)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        FreelancerSet tempSet = compositionRules.Rules[slotType];
                                        if (tempSet.Roles != null) 
                                            botSet.Roles.AddRange(tempSet.Roles);
                                        if (tempSet.Types != null)
                                            botSet.Types.AddRange(tempSet.Types);
                                    }
                                }
                            }
                        }

                        if (matchedAny && matched) {
                            //Log.Print(LogType.Debug, "Adding player");
                            LobbyPlayerInfo info = new LobbyPlayerInfo()
                            {
                                AccountId = player.AccountId,
                                BannerID = player.SelectedBackgroundBannerID,
                                BotCanTaunt = true,
                                BotsMasqueradeAsHumans = false,
                                CharacterInfo = player.GetLobbyCharacterInfo(),
                                Difficulty = BotDifficulty.Medium,
                                EffectiveClientAccessLevel = ClientAccessLevel.Full,
                                EmblemID = player.SelectedForegroundBannerID,
                                Handle = player.UserName,
                                IsGameOwner = true,
                                IsNPCBot = false,
                                IsLoadTestBot = false,
                                ReadyState = ReadyState.Ready,
                                RibbonID = player.SelectedRibbonID,
                                TeamId = team,
                                TitleID = player.SelectedTitleID,
                                TitleLevel = 1,
                            };
                            playerInfo.Add(info);

                            filled = true;
                            usedPlayers++;
                            availablePlayers.Remove(player);
                            break;
                        }
                    }
                }

                if (!filled) {
                    // try fill with bots
                    if (usedBots != maxBots) // Bots remaining to fill
                    {
                        // create bot
                        //Log.Print(LogType.Debug, "creating bot");
                        LobbyPlayerInfo botInfo = CharacterUtils.CreateBotCharacterFromFreelancerSet(botSet);
                        botInfo.TeamId = team;
                        playerInfo.Add(botInfo);
                        usedBots++;
                    }
                }


            }

            //Log.Print(LogType.Debug, "For finished");

            if (usedPlayers != maxPlayers)
            {
                //Log.Print(LogType.Debug, "Bot overflow");
                QueueInfo.QueueStatus = QueueStatus.QueueDoesntHaveEnoughHumans;
                return false;
            }

            //Log.Print(LogType.Debug, "Filling team info");
            foreach (LobbyPlayerInfo playerinfo in playerInfo)
            {
                teamInfo.TeamPlayerInfo.Add(playerinfo);
            }
            //Log.Print(LogType.Debug, "TeamFilled!");

            return true;

        }

        private String GenerateRoomName() { return $"Evos-{GameType.ToString()}-{MatchId++}"; }



    }
}
