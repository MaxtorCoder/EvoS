using EvoS.Framework.Network.Static;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.LobbyServer.Utils
{
    class CharacterUtils
    {
        private static Dictionary<CharacterRole, List<CharacterType>> Roles = new Dictionary<CharacterRole, List<CharacterType>>
        {
            {
                CharacterRole.Assassin, new List<CharacterType>()
                {
                    CharacterType.Soldier,
                    CharacterType.Thief,
                    CharacterType.Blaster,
                    CharacterType.Gremlins,
                    CharacterType.Tracker,
                    CharacterType.Exo,
                    CharacterType.TeleportingNinja,
                    CharacterType.Fireborg,
                    CharacterType.Scoundrel,
                    CharacterType.Neko,
                    CharacterType.Sniper,
                    CharacterType.Trickster,
                    CharacterType.RobotAnimal,
                    CharacterType.Samurai,
                    CharacterType.Iceborg,
                    CharacterType.BazookaGirl,// <3
                }
            },
            { 
                CharacterRole.Tank, new List<CharacterType>
                {
                    CharacterType.BattleMonk,
                    CharacterType.Valkyrie,
                    CharacterType.SpaceMarine,
                    CharacterType.Scamp,
                    CharacterType.Dino,
                    CharacterType.Manta,
                    CharacterType.Rampart,
                    CharacterType.RageBeast,
                    CharacterType.Claymore,
                }
            },
            {
                CharacterRole.Support, new List<CharacterType>
                {
                    CharacterType.DigitalSorceress,
                    CharacterType.FishMan,
                    CharacterType.NanoSmith,
                    CharacterType.Archer,
                    CharacterType.Cleric,
                    CharacterType.Martyr,
                    CharacterType.Spark,
                    CharacterType.Sensei
                }
            }
        };
        public static bool IsFromRole(CharacterType type, CharacterRole role) {
            if (type == CharacterType.None) return false;
            if (role == CharacterRole.None) return false;

            if (type == CharacterType.PendingWillFill || type == CharacterType.FemaleWillFill) {
                return true;
            }

            return Roles[role].Contains(type);
        }

        public static bool MatchesCharacter(CharacterType character, FreelancerSet freelancerSet)
        {
            if (freelancerSet.Types != null && !freelancerSet.Types.Contains(character)) return false;
            if (freelancerSet.Roles != null) {
                bool matchesAny = false;
                foreach (CharacterRole role in freelancerSet.Roles) {
                    if (IsFromRole(character, role)) {
                        return true;
                    }
                }
                
            }
            return false;
        }

        public static LobbyPlayerInfo CreateBotCharacterFromFreelancerSet(FreelancerSet fset)
        {
            CharacterType characterType = CharacterType.PunchingDummy;

            return new LobbyPlayerInfo
            {
                AccountId = 0,
                BannerID = 0,
                BotCanTaunt = true,
                BotsMasqueradeAsHumans = false,
                CharacterInfo = new LobbyCharacterInfo
                {
                    CharacterAbilityVfxSwaps = new CharacterAbilityVfxSwapInfo(),
                    CharacterCards = new CharacterCardInfo(),
                    CharacterLevel = 1,
                    CharacterLoadouts = null,
                    CharacterMatches = 0,
                    CharacterMods = new CharacterModInfo(),
                    CharacterSkin = new CharacterVisualInfo(),
                    CharacterTaunts = null,
                    CharacterType = characterType
                    
                },
                EffectiveClientAccessLevel = ClientAccessLevel.Unknown,
                EmblemID = 0,
                Handle = "Bot",
                IsGameOwner = false,
                IsLoadTestBot = false,
                IsNPCBot = true,
                ReadyState = Framework.Constants.Enums.ReadyState.Ready, // Robots are always ready to fight.
                RibbonID = 0,
                TitleID = 0,
                TitleLevel = 0
            };
        }
    }
}
