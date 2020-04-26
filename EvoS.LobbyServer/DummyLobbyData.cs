using System;
using System.Collections.Generic;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.NetworkMessages;
using EvoS.Framework.Network.Static;
using EvoS.LobbyServer.Utils;

namespace EvoS.LobbyServer
{
    public class DummyLobbyData
    {
        public static List<PersistedCharacterData> CreateCharacterDataList()
        {
            var data = new List<PersistedCharacterData>();

            foreach (CharacterType characterType in Enum.GetValues(typeof(CharacterType)))
            {
                var characterData = new PersistedCharacterData(characterType)
                {
                    CharacterComponent = {
                        Unlocked = true,
                        LastSelectedLoadout = 0,
                        CharacterLoadouts = new List<CharacterLoadout>(){
                            PlayerUtils.GetLoadout(0, characterType)
                        },
                        Skins = new List<PlayerSkinData>()
                        {
                            new PlayerSkinData() {
                                Unlocked = true,
                                Patterns = new List<PlayerPatternData>()
                                {
                                    new PlayerPatternData()
                                    {
                                        Colors = new List<PlayerColorData>()
                                        {
                                            new PlayerColorData() { Unlocked = true},
                                            new PlayerColorData() { Unlocked = true},
                                            new PlayerColorData() { Unlocked = true},
                                            new PlayerColorData() { Unlocked = true}
                                        },
                                        Unlocked = true,
                                    },
                                    new PlayerPatternData()
                                    {
                                        Colors = new List<PlayerColorData>()
                                        {
                                            new PlayerColorData() { Unlocked = true},
                                            new PlayerColorData() { Unlocked = true}
                                        },
                                        Unlocked = true,
                                    }
                                }
                            }
                        }
                    }
                };
                data.Add(characterData);
            }

            return data;
        }

        public static ServerQueueConfigurationUpdateNotification CreateServerQueueConfiguration()
        {
            return new ServerQueueConfigurationUpdateNotification
            {
                FreeRotationAdditions = new Dictionary<CharacterType, RequirementCollection>(),
                GameTypeAvailabilies = GameTypesUtils.GetAvailableGameTypes(),
                TierInstanceNames = new List<LocalizationPayload>(),
                AllowBadges = true,
                NewPlayerPvPQueueDuration = 0
            };
        }



        public static Dictionary<CharacterType, CharacterConfig> CreateCharacterConfigs()
        {
            var data = new Dictionary<CharacterType, CharacterConfig>();

            foreach (CharacterType characterType in Enum.GetValues(typeof(CharacterType)))
            {
                data.Add(characterType, new CharacterConfig
                {
                    CharacterType = characterType,
                    Difficulty = 1,
                    CharacterRole = CharacterRole.Assassin,
                    IsHidden = false,
                    AllowForBots = true,
                    AllowForPlayers = true,
                    IsHiddenFromFreeRotationUntil = DateTime.UnixEpoch
                });
            }

            return data;
        }

        public static LobbyGameplayOverrides CreateLobbyGameplayOverrides()
        {
            return new LobbyGameplayOverrides
            {
                CharacterConfigs = CreateCharacterConfigs(),
                EnableHiddenCharacters = true,
                EnableAllMods = true,
                EnableQuests = false,
                EnableSeasons = false, // Seasson tab
                AllowReconnectingToGameInstantly = true,
                EnableClientPerformanceCollecting = false,
                EnableDiscordSdk = false,
                EnableShop = false,
                EnableFacebook = false,
                EnableSteamAchievements = false,
                EnableDiscord = false
            };
        }
    }
}
