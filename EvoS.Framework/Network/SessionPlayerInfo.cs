using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Logging;
using EvoS.Framework.Network.Static;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Framework.Network
{
    public class SessionPlayerInfo
    {
        long AccountId;
        int BannerID;
        CharacterType CharacterType;
        GameType GameType;
        ushort SubTypeMask;
        string Handle;
        int TitleID;
        int TitleLevel;
        int EmblemID;
        int RibbonID;
        ReadyState ReadyState;
        LobbyPlayerInfo LobbyPlayerInfo;
        public BotDifficulty AllyBotDifficulty;
        public BotDifficulty EnemyBotDifficulty;
        string RoomName;
        private Dictionary<CharacterType, CharacterComponent> CharacterComponents = new Dictionary<CharacterType, CharacterComponent>();

        public SessionPlayerInfo() {
            this.LobbyPlayerInfo = new LobbyPlayerInfo
            {
                Handle = "default",
                BotCanTaunt = true,
                BotsMasqueradeAsHumans = false,
                EffectiveClientAccessLevel = ClientAccessLevel.Full,
                IsGameOwner = true,
                IsLoadTestBot = false,
                IsNPCBot = false,
                ReadyState = ReadyState.Unknown,
                ReplacedWithBots = false,
                TitleLevel = 0,
                CharacterInfo = new LobbyCharacterInfo { CharacterLevel = 1 }
            };
        }

        public long GetAccountId() { return this.AccountId; }
        public void SetAccountId(long accountId) {
            this.AccountId = accountId;
            this.LobbyPlayerInfo.AccountId = accountId;
        }

        public LobbyPlayerInfo GetLobbyPlayerInfo(){return LobbyPlayerInfo;}

        public int GetTitleId() { return TitleID; }
        public void SetTitleId(int titleID) { this.TitleID = titleID; LobbyPlayerInfo.TitleID = titleID; }
        public int GetTitleLevel() { return TitleLevel; }

        public void SetBannerID(int bannerID) { BannerID = bannerID; LobbyPlayerInfo.BannerID = bannerID; }
        public void SetEmblemID(int emblemID) { EmblemID = emblemID; LobbyPlayerInfo.EmblemID = emblemID; }
        public void SetRibbonID(int ribbonID) { RibbonID = ribbonID; LobbyPlayerInfo.RibbonID = ribbonID; }
        public void SetTitleID(int titleID) { TitleID = titleID; LobbyPlayerInfo.TitleID = titleID; }
        public void SetSubTypeMask(ushort subtypemask) { SubTypeMask = subtypemask; }

        public String GetHandle() { return this.Handle; }
        public void SetHandle(String handle) { Log.Print(LogType.Debug, "SetHandle " + handle); Handle = handle; LobbyPlayerInfo.Handle = handle; }

        public CharacterType GetCharacterType() { return CharacterType; }
        public void SetCharacterType(CharacterType characterType) {
            CharacterType = characterType;
            CharacterComponent newCharacter = GetCharacterComponent(characterType);
            LobbyPlayerInfo.CharacterInfo.CharacterType = characterType;
            SetSkin(newCharacter.LastSkin);
            SetCards(newCharacter.LastCards);
            SetMods(newCharacter.LastMods);
            SetAbilityVfxSwaps(newCharacter.LastAbilityVfxSwaps);
            SetTaunts(newCharacter.Taunts);
            LobbyPlayerInfo.CharacterInfo.CharacterLoadouts = newCharacter.CharacterLoadouts;
        }

        public void SetCards(CharacterCardInfo cardInfo) {
            LobbyPlayerInfo.CharacterInfo.CharacterCards = cardInfo;
            CharacterComponent characterComponent = GetCharacterComponent(CharacterType);
            characterComponent.LastCards = cardInfo;
        }

        

        public void SetMods(CharacterModInfo modInfo) {
            LobbyPlayerInfo.CharacterInfo.CharacterMods = modInfo;
            CharacterComponent characterComponent = GetCharacterComponent(CharacterType);
            characterComponent.CharacterLoadouts[characterComponent.LastSelectedLoadout].ModSet = modInfo;
            characterComponent.LastMods = modInfo;
            List<PlayerModData> playerModDatas = new List<PlayerModData>();
            for (int i = 0; i < 5; i++) {
                playerModDatas.Add( new PlayerModData { AbilityId = i, AbilityModID = modInfo.GetModForAbility(i) });
            }
            characterComponent.Mods = playerModDatas;
            
        }

        public CharacterVisualInfo GetSkin(){
            return GetCharacterComponent(this.CharacterType).LastSkin;
        }

        public void SetSkin(CharacterVisualInfo skin)
        {
            LobbyPlayerInfo.CharacterInfo.CharacterSkin = skin;
            CharacterComponent characterComponent = GetCharacterComponent(this.CharacterType);
            characterComponent.LastSkin = skin;
        }

        public void SetAbilityVfxSwaps(CharacterAbilityVfxSwapInfo characterAbilityVfxSwapInfo) {
            LobbyPlayerInfo.CharacterInfo.CharacterAbilityVfxSwaps = characterAbilityVfxSwapInfo;
            CharacterComponent characterComponent = GetCharacterComponent(this.CharacterType);
            characterComponent.LastAbilityVfxSwaps = characterAbilityVfxSwapInfo;
            characterComponent.CharacterLoadouts[characterComponent.LastSelectedLoadout].VFXSet = characterAbilityVfxSwapInfo;
        }

        public void SetTaunts(List<PlayerTauntData> taunts) {
            LobbyPlayerInfo.CharacterInfo.CharacterTaunts = taunts;
            CharacterComponent characterComponent = GetCharacterComponent(this.CharacterType);
            characterComponent.Taunts = taunts;
        }

        public GameType GetGameType() { return GameType; }
        public void SetGameType(GameType gameType) {
            GameType = gameType;
        }

        private CharacterComponent GetCharacterComponent(CharacterType characterType)
        {
            if (CharacterComponents.ContainsKey(characterType))
            {
                return CharacterComponents[characterType];
            }
            else
            {
                // TODO get this from DB
                int selectedLoadout = 0;
                List<CharacterLoadout> loadouts = new List<CharacterLoadout>{new CharacterLoadout( new CharacterModInfo().Reset(), new CharacterAbilityVfxSwapInfo().Reset(), "Default", ModStrictness.AllModes )};
                CharacterCardInfo lastCardInfo = new CharacterCardInfo { CombatCard = CardType.None, DashCard = CardType.None, PrepCard = CardType.None };
                CharacterVisualInfo characterVisualInfo = new CharacterVisualInfo { skinIndex=0, colorIndex=0, patternIndex=0 };
                List<PlayerModData> playerModDatas = new List<PlayerModData>();
                for (int i = 0; i < 5; i++) {
                    playerModDatas.Add(new PlayerModData { AbilityId = i, AbilityModID = loadouts[selectedLoadout].ModSet.GetModForAbility(i) });
                }

                CharacterComponent characterComponent = new CharacterComponent
                {
                    AbilityVfxSwaps = new List<PlayerAbilityVfxSwapData>() {  }, // TODO list is empty
                    CharacterLoadoutsRanked = new List<CharacterLoadout>(), // TODO list is empty
                    CharacterLoadouts = loadouts,
                    LastAbilityVfxSwaps = loadouts[selectedLoadout].VFXSet,
                    LastCards = lastCardInfo,
                    LastMods = loadouts[selectedLoadout].ModSet,
                    LastRankedMods = loadouts[selectedLoadout].ModSet, // TODO make ranked specific
                    LastSelectedRankedLoadout = selectedLoadout, // TODO make ranked specific
                    LastSelectedLoadout = selectedLoadout,
                    LastSkin = characterVisualInfo, // TODO
                    Mods = playerModDatas,
                    NumCharacterLoadouts = loadouts.Count,
                    Skins = new List<PlayerSkinData>(), // TODO load skins for this character
                    Taunts = new List<PlayerTauntData>(), // TODO load taunts for this character
                    Unlocked = true
                };
                CharacterComponents.Add(characterType, characterComponent);
                return characterComponent;
            }
        }
    }

    
}
