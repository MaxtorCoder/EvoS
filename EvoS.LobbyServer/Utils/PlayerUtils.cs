using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Logging;
using EvoS.Framework.Network.NetworkMessages;
using EvoS.Framework.Network.Static;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.LobbyServer.Utils
{
    public class PlayerUtils
    {
        public static PersistedAccountData GetAccountData(LobbyServerConnection connection)
        {
            PersistedAccountData accountData = new PersistedAccountData()
            {
                AccountId = connection.PlayerInfo.GetAccountId(),
                AccountComponent = GetAccountComponent(connection),
                BankComponent = GetBankData(connection.PlayerInfo.GetAccountId()),
                CharacterData = GetCharacterData(),
                CreateDate = DateTime.Now.AddHours(-1),
                Handle = connection.PlayerInfo.GetHandle(),
                QuestComponent = new QuestComponent() { ActiveSeason = 9 }, // Provide a SeasonExperience to set Season Level on client
                SchemaVersion = new SchemaVersion<AccountSchemaChange>(0x1FFFF),
                UpdateDate = DateTime.Now,
                UserName = connection.PlayerInfo.GetHandle(),
                InventoryComponent = new InventoryComponent(true)
            };
            return accountData;
        }

        public static AccountComponent GetAccountComponent(LobbyServerConnection connection)
        {
            //TODO this looks awful
            AccountComponent accountComponent = new AccountComponent()
            {
                LastCharacter = connection.PlayerInfo.GetCharacterType(),
                LastRemoteCharacters = new List<CharacterType>(),
                UIStates = new Dictionary<AccountComponent.UIStateIdentifier, int> { { AccountComponent.UIStateIdentifier.HasViewedFluxHighlight, 1 },{ AccountComponent.UIStateIdentifier.HasViewedGGHighlight, 1 } },
                UnlockedTitleIDs = GetUnlockedTitleIDs(connection.PlayerInfo.GetAccountId()),
                UnlockedBannerIDs = GetUnlockedTitleIDs(connection.PlayerInfo.GetAccountId()),
                UnlockedEmojiIDs = GetUnlockedTitleIDs(connection.PlayerInfo.GetAccountId()),
                UnlockedOverconIDs = GetUnlockedTitleIDs(connection.PlayerInfo.GetAccountId()),
            };

            return accountComponent;
        }

        public static BankComponent GetBankData(long AccountID)
        {
            List<CurrencyData> currentBalances = new List<CurrencyData>
            {
                new CurrencyData() { m_Type = CurrencyType.ISO, Amount = 100000 },
                new CurrencyData() { m_Type = CurrencyType.FreelancerCurrency, Amount = 200000 },
                new CurrencyData() { m_Type = CurrencyType.GGPack, Amount = 999 },
                new CurrencyData() { m_Type = CurrencyType.RankedCurrency, Amount = 300 }
            };
            return new BankComponent(currentBalances);
        }

        public static Dictionary<CharacterType, PersistedCharacterData> GetCharacterData()
        {
            Dictionary<CharacterType, PersistedCharacterData> characterData = new Dictionary<CharacterType, PersistedCharacterData>();

            characterData.Add(CharacterType.Scoundrel, new PersistedCharacterData(CharacterType.Scoundrel) { });

            return characterData;
        }

        public static CharacterLoadout GetLoadout(long accountID, CharacterType characterType)
        {
            CharacterModInfo modInfo = new CharacterModInfo() { ModForAbility0 = -1, ModForAbility1 = -1, ModForAbility2 = -1, ModForAbility3 = 1, ModForAbility4 = 0 };
            CharacterAbilityVfxSwapInfo vfxInfo = new CharacterAbilityVfxSwapInfo() { VfxSwapForAbility0 = 0, VfxSwapForAbility1 = 0, VfxSwapForAbility2 = 0, VfxSwapForAbility3 = 0, VfxSwapForAbility4 = 0 };
            CharacterLoadout loadout = new CharacterLoadout(modInfo, vfxInfo, "Empty");
            return loadout;
        }

        public static List<int> GetUnlockedTitleIDs(long AccountId)
        {
            return new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        }
    }
}
