using System;
using System.Collections.Generic;
using EvoS.Framework.Logging;
using Newtonsoft.Json;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    public abstract class QueueRequirement
    {
        public abstract QueueRequirement.RequirementType Requirement { get; }

        public abstract bool AnyGroupMember { get; }

        public static IEnumerable<Type> MessageTypes
        {
            get
            {
                return new Type[]
                {
                    typeof(QueueRequirement_AccessLevel),
                    typeof(QueueRequirement_Character),
                    typeof(QueueRequirement_DateTime),
                    typeof(QueueRequirement_Environement),
                    typeof(QueueRequirement_GreaterThan),
                    typeof(QueueRequirement_MaxLeavingPoints),
                    typeof(QueueRequirement_Never),
                    typeof(QueueRequirement_TimeOfWeek)
                };
            }
        }

        [EvosMessage(791)]
        public enum RequirementType
        {
            ERROR,
            TotalMatches,
            CharacterMatches,
            VsHumanMatches,
            AccessLevel,
            TotalLevel,
            SeasonLevel,
            MaxLeavingPoints,
            TimeOfWeek,
            AdminDisabled,
            BeforeDate,
            AfterDate,
            ProhibitEnvironment,
            AvailableCharacterCount,
            HasUnlockedCharacter
        }
    }
}
