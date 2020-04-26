using CentralServer.LobbyServer.Character;
using CentralServer.LobbyServer.Inventory;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.NetworkMessages;
using EvoS.Framework.Network.Static;
using System;
using System.Collections.Generic;
using System.Text;

namespace CentralServer.LobbyServer.Account
{
    public class AccountManager
    {
        public static PersistedAccountData GetPersistedAccountData(long accountId)
        {
            Database.Account account = Database.Account.GetByAccountId(accountId);

            PersistedAccountData accountData = new PersistedAccountData()
            {
                AccountComponent =  GetAccountComponent(account),
                AccountId = accountId,
                BankComponent = Bank.GetBankComponent(account.AccountId),
                CharacterData = CharacterManager.GetPersistedCharacterData(account.AccountId),
                Handle = account.UserName,
                InventoryComponent = InventoryManager.GetInventoryComponent(accountId),
                QuestComponent = new QuestComponent() { ActiveSeason = 9 },
                SchemaVersion = new SchemaVersion<AccountSchemaChange>(0x1FFFF),
                UserName = account.UserName
            };

            return accountData;
        }
        public static AccountComponent GetAccountComponent(Database.Account account)
        {
            AccountComponent accountComponent = new AccountComponent()
            {
                AppliedEntitlements = new Dictionary<string, int>(),
                DailyQuestsAvailable = Config.ConfigManager.DailyQuestsAvailable,
                DisplayDevTag = false,
                FactionCompetitionData = new Dictionary<int, PlayerFactionCompetitionData>(),
                FreeRotationCharacters = new CharacterType[] { },
                LastCharacter = account.LastCharacter,
                SelectedBackgroundBannerID = account.BannerID,
                SelectedForegroundBannerID = account.EmblemID,
                SelectedRibbonID = account.RibbonID,
                SelectedTitleID = account.TitleID,
                UnlockedBannerIDs = InventoryManager.GetUnlockedBannerIDs(account.AccountId),
                UIStates = new Dictionary<AccountComponent.UIStateIdentifier, int>
                {
                    { AccountComponent.UIStateIdentifier.HasViewedFluxHighlight, 1 },
                    { AccountComponent.UIStateIdentifier.HasViewedGGHighlight, 1 }
                },
                UnlockedEmojiIDs = InventoryManager.GetUnlockedEmojiIDs(account.AccountId),
                UnlockedLoadingScreenBackgroundIdsToActivatedState = InventoryManager.GetActivatedLoadingScreenBackgroundIds(account.AccountId),
                UnlockedOverconIDs = InventoryManager.GetUnlockedOverconIDs(account.AccountId),
                UnlockedTitleIDs = InventoryManager.GetUnlockedTitleIDs(account.AccountId),
                UnlockedRibbonIDs = InventoryManager.GetUnlockedRibbonIDs(account.AccountId)
            };

            return accountComponent;
        }
    }
}
