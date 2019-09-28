using System;
using System.Collections.Generic;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Misc;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    public class LobbyPlayerCommonInfo
    {
        public bool IsRemoteControlled => ControllingPlayerInfo != null;

        public bool IsSpectator => TeamId == Team.Spectator;

        public CharacterType CharacterType => (CharacterInfo != null) ? CharacterInfo.CharacterType : CharacterType.None;

        public bool IsReady => ReadyState == ReadyState.Ready || IsAIControlled || IsRemoteControlled;

        public bool ReplacedWithBots { get; set; }

        public bool IsAIControlled => IsNPCBot || IsLoadTestBot || ReplacedWithBots;

        public bool IsHumanControlled => !IsAIControlled;

        public bool IsNPCBot
        {
            get => GameAccountType == PlayerGameAccountType.None;
            set
            {
                if (value)
                {
                    GameAccountType = PlayerGameAccountType.None;
                }
            }
        }

        public bool IsLoadTestBot
        {
            get => GameAccountType == PlayerGameAccountType.LoadTest;
            set
            {
                if (value)
                {
                    GameAccountType = PlayerGameAccountType.LoadTest;
                }
            }
        }

        public void SetGameOption(LobbyGameplayOverrides gameplayOverrides)
        {
            if (IsLoadTestBot)
            {
                if (gameplayOverrides.UseFakeGameServersForLoadTests)
                {
                    GameConnectionType = PlayerGameConnectionType.None;
                }
                else if (gameplayOverrides.UseFakeClientConnectionsForLoadTests)
                {
                    GameConnectionType = PlayerGameConnectionType.None;
                }
                else
                {
                    GameConnectionType = PlayerGameConnectionType.RawSocket;
                }
            }
        }

        public void SetGameOption(PlayerGameOptionFlag flag, bool on)
        {
            if (on)
            {
                GameOptionFlags = GameOptionFlags.WithGameOption(flag);
            }
            else
            {
                GameOptionFlags = GameOptionFlags.WithoutGameOption(flag);
            }
        }

        public long AccountId;
        public int PlayerId;
        public int CustomGameVisualSlot;
        public string Handle;
        public int TitleID;
        public int TitleLevel;
        public int BannerID;
        public int EmblemID;
        public int RibbonID;
        public bool IsGameOwner;
        public bool IsReplayGenerator;
        public BotDifficulty Difficulty;
        public bool BotCanTaunt;
        public Team TeamId;
        public LobbyCharacterInfo CharacterInfo = new LobbyCharacterInfo();
        public List<LobbyCharacterInfo> RemoteCharacterInfos = new List<LobbyCharacterInfo>();
        public ReadyState ReadyState;
        public int ControllingPlayerId;
        public LobbyServerPlayerInfo ControllingPlayerInfo;
        public PlayerGameAccountType GameAccountType;
        public PlayerGameConnectionType GameConnectionType;
        public PlayerGameOptionFlag GameOptionFlags;
    }
}
