using System;
using System.Collections.Generic;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(134)]
    public class TeamCompositionRules
    {
        public bool MatchesSlotType(SlotTypes st, Team team, int slot)
        {
            switch (st)
            {
                case SlotTypes.All:
                    return true;
                case SlotTypes.TeamA:
                    return team == Team.TeamA;
                case SlotTypes.TeamB:
                    return team == Team.TeamB;
                case SlotTypes.A1:
                    return team == Team.TeamA && slot == 1;
                case SlotTypes.A2:
                    return team == Team.TeamA && slot == 2;
                case SlotTypes.A3:
                    return team == Team.TeamA && slot == 3;
                case SlotTypes.A4:
                    return team == Team.TeamA && slot == 4;
                case SlotTypes.A5:
                    return team == Team.TeamA && slot == 5;
                case SlotTypes.B1:
                    return team == Team.TeamB && slot == 1;
                case SlotTypes.B2:
                    return team == Team.TeamB && slot == 2;
                case SlotTypes.B3:
                    return team == Team.TeamB && slot == 3;
                case SlotTypes.B4:
                    return team == Team.TeamB && slot == 4;
                case SlotTypes.B5:
                    return team == Team.TeamB && slot == 5;
                case SlotTypes.Slot1:
                    return slot == 1;
                case SlotTypes.Slot2:
                    return slot == 2;
                case SlotTypes.Slot3:
                    return slot == 3;
                case SlotTypes.Slot4:
                    return slot == 4;
                case SlotTypes.Slot5:
                    return slot == 5;
                default:
                    throw new Exception("Unimplemented slot type");
            }
        }

        [EvosMessage(135)]
        public Dictionary<SlotTypes, FreelancerSet> Rules;

        [EvosMessage(144)]
        public enum SlotTypes
        {
            All,
            TeamA,
            TeamB,
            A1,
            A2,
            A3,
            A4,
            A5,
            B1,
            B2,
            B3,
            B4,
            B5,
            Slot1,
            Slot2,
            Slot3,
            Slot4,
            Slot5
        }
    }
}
