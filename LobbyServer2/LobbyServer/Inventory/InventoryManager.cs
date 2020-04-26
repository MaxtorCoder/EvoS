using EvoS.Framework.Network.Static;
using System;
using System.Collections.Generic;
using System.Text;

namespace CentralServer.LobbyServer.Inventory
{
    class InventoryManager
    {
        public static List<int> GetUnlockedBannerIDs(long accountId)
        {
            // TODO
            return new List<int>();
        }

        public static List<int> GetUnlockedEmojiIDs(long accountId)
        {
            //TODO
            return new List<int>();
        }

        public static List<int> GetUnlockedLoadingScreenBackgroundIds(long accountId)
        {
            //TODO
            return new List<int>();
        }

        public static Dictionary<int, bool> GetActivatedLoadingScreenBackgroundIds(long accountId)
        {
            Dictionary<int, bool> backgrounds = new Dictionary<int, bool>();

            for (int i = 1; i <= 16; i++)
            {
                backgrounds.Add(i, true);
            }

            return backgrounds;
        }

        public static List<int> GetUnlockedOverconIDs(long accountId)
        {
            //TODO
            return new List<int>();
        }

        public static List<int> GetUnlockedTitleIDs(long accountId)
        {
            //TODO
            return new List<int>();
        }

        public static List<int> GetUnlockedRibbonIDs(long accountId)
        {
            //TODO
            return new List<int>();
        }

        public static InventoryComponent GetInventoryComponent(long accountId)
        {
            // TODO
            return new InventoryComponent();
        }

    }
}
