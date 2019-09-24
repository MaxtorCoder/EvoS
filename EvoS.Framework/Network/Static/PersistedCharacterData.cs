using System;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(504)]
    public class PersistedCharacterData
    {
        public PersistedCharacterData(CharacterType characterType)
        {
            CharacterType = characterType;
            SchemaVersion = new SchemaVersion<CharacterSchemaChange>();
            CharacterComponent = new CharacterComponent();
            ExperienceComponent = new ExperienceComponent();
        }

        [EvosMessage(505, ignoreGenericArgs: true)]
        public SchemaVersion<CharacterSchemaChange> SchemaVersion { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public CharacterType CharacterType { get; set; }

        public CharacterComponent CharacterComponent { get; set; }

        public ExperienceComponent ExperienceComponent { get; set; }
    }
}
