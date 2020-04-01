using EvoS.Framework.Constants.Enums;
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

                Log.Print(LogType.Debug, "Match Created succesfully");
            }
        }

        private bool FillTeam(Team team, TeamCompositionRules compositionRules, int maxPlayers, int maxBots, ref List<ClientConnection> availablePlayers, ref LobbyTeamInfo teamInfo)
        {
            Log.Print(LogType.Debug, $"Fill Team {team.ToString()}");
            int usedPlayers = 0;
            int usedBots = 0;

            List<LobbyPlayerInfo> playerInfo = new List<LobbyPlayerInfo>();
            
            for (int slot = 1; slot <= 5; slot++) {
                Log.Print(LogType.Debug, "fillteam:: slot "+slot);
                FreelancerSet botSet = new FreelancerSet() { Roles = new List<CharacterRole>(), Types = new List<CharacterType>() };
                bool filled = false;

                if (usedPlayers != maxPlayers)
                {
                    foreach (ClientConnection player in availablePlayers)
                    {
                        bool matchedAny = false;
                        bool matched = true;

                        Log.Print(LogType.Debug, "matching rules with " + player.UserName);

                        foreach (TeamCompositionRules.SlotTypes slotType in compositionRules.Rules.Keys)
                        {
                            if (compositionRules.MatchesSlotType(slotType, team, slot))
                            {
                                matchedAny = true;
                                Log.Print(LogType.Debug, "Matched slotType " + slotType.ToString());

                                if (!CharacterUtils.MatchesCharacter(player.SelectedCharacter, compositionRules.Rules[slotType]))
                                {
                                    Log.Print(LogType.Debug, "not matched rule");
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
                            Log.Print(LogType.Debug, "Adding player");
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
                        Log.Print(LogType.Debug, "creating bot");
                        LobbyPlayerInfo botInfo = CharacterUtils.CreateBotCharacterFromFreelancerSet(botSet);
                        botInfo.TeamId = team;
                        playerInfo.Add(botInfo);
                        usedBots++;
                    }
                }


            }

            Log.Print(LogType.Debug, "For finished");

            if (usedPlayers != maxPlayers)
            {
                Log.Print(LogType.Debug, "Bot overflow");
                QueueInfo.QueueStatus = QueueStatus.QueueDoesntHaveEnoughHumans;
                return false;
            }

            Log.Print(LogType.Debug, "Filling team info");
            foreach (LobbyPlayerInfo playerinfo in playerInfo)
            {
                teamInfo.TeamPlayerInfo.Add(playerinfo);
            }
            Log.Print(LogType.Debug, "TeamFilled!");

            return true;

        }

        private String GenerateRoomName() { return $"Evos-{GameType.ToString()}-{MatchId++}"; }



    }
}
