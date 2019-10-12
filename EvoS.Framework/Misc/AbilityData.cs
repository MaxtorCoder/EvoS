namespace EvoS.Framework.Misc
{
    public class AbilityData
    {
        public enum ActionType
        {
            INVALID_ACTION = -1, // 0xFFFFFFFF
            ABILITY_0 = 0,
            ABILITY_1 = 1,
            ABILITY_2 = 2,
            ABILITY_3 = 3,
            ABILITY_4 = 4,
            ABILITY_5 = 5,
            ABILITY_6 = 6,
            CARD_0 = 7,
            CARD_1 = 8,
            CARD_2 = 9,
            CHAIN_0 = 10, // 0x0000000A
            CHAIN_1 = 11, // 0x0000000B
            CHAIN_2 = 12, // 0x0000000C
            CHAIN_3 = 13, // 0x0000000D
            NUM_ACTIONS = 14, // 0x0000000E
        }
    }
}
