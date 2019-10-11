using System;
using System.Collections.Generic;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("MatchObjectiveKill")]
    public class MatchObjectiveKill : MatchObjective
    {
        public KillObjectiveType killType;
        public string m_tag;
        public int pointAdjustForKillingTeam;
        public int pointAdjustForDyingTeam;
        public SerializedVector<CharacterKillPointAdjustOverride> m_characterTypeOverrides;

        public MatchObjectiveKill()
        {
        }

        public MatchObjectiveKill(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            killType = (KillObjectiveType) stream.ReadInt32();
            m_tag = stream.ReadString32();
            pointAdjustForKillingTeam = stream.ReadInt32();
            pointAdjustForDyingTeam = stream.ReadInt32();
            m_characterTypeOverrides = new SerializedVector<CharacterKillPointAdjustOverride>(assetFile, stream);
        }

        public override string ToString()
        {
            return $"{nameof(MatchObjectiveKill)}>(" +
                   $"{nameof(killType)}: {killType}, " +
                   $"{nameof(m_tag)}: {m_tag}, " +
                   $"{nameof(pointAdjustForKillingTeam)}: {pointAdjustForKillingTeam}, " +
                   $"{nameof(pointAdjustForDyingTeam)}: {pointAdjustForDyingTeam}, " +
                   $"{nameof(m_characterTypeOverrides)}: {m_characterTypeOverrides}, " +
                   ")";
        }

        [Serializable]
        public class CharacterKillPointAdjustOverride : ISerializedItem
        {
            public List<CharacterType> m_killedCharacterTypes;
            public int m_pointAdjustOverrideForKillingTeam;
            public int m_pointAdjustOverrideForDyingTeam;

            public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
            {
                m_killedCharacterTypes = new SerializedArray<CharacterType>(assetFile, stream);
                m_pointAdjustOverrideForKillingTeam = stream.ReadInt32();
                m_pointAdjustOverrideForDyingTeam = stream.ReadInt32();
            }

            public override string ToString()
            {
                return $"{nameof(CharacterKillPointAdjustOverride)}(" +
                       $"{nameof(m_killedCharacterTypes)}: {m_killedCharacterTypes}, " +
                       $"{nameof(m_pointAdjustOverrideForKillingTeam)}: {m_pointAdjustOverrideForKillingTeam}, " +
                       $"{nameof(m_pointAdjustOverrideForDyingTeam)}: {m_pointAdjustOverrideForDyingTeam}" +
                       ")";
            }
        }


        public enum KillObjectiveType
        {
            Actor,
            NPC,
            Player,
            Minion,
            ActorWithTag,
            ActorWithoutTag
        }
    }
}
