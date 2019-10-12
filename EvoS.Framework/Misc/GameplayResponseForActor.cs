using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("GameplayResponseForActor")]
    public class GameplayResponseForActor : ISerializedItem
    {
        public StandardEffectInfo m_effect;
        public int m_credits;
        public int m_healing;
        public int m_damage;
        public int m_techPoints;
        public SerializedArray<AbilityStatMod> m_permanentStatMods;
        public SerializedArray<StatusType> m_permanentStatusChanges;
        public SerializedComponent m_sequenceToPlay;

        public GameplayResponseForActor()
        {
        }

        public GameplayResponseForActor(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_effect = new StandardEffectInfo(assetFile, stream);
            m_credits = stream.ReadInt32();
            m_healing = stream.ReadInt32();
            m_damage = stream.ReadInt32();
            m_techPoints = stream.ReadInt32();
            m_permanentStatMods = new SerializedArray<AbilityStatMod>(assetFile, stream);
            m_permanentStatusChanges = new SerializedArray<StatusType>(assetFile, stream);
            m_sequenceToPlay = new SerializedComponent(assetFile, stream); // class [UnityEngine]UnityEngine.GameObject
        }

        public override string ToString()
        {
            return $"{nameof(GameplayResponseForActor)}>(" +
                   $"{nameof(m_effect)}: {m_effect}, " +
                   $"{nameof(m_credits)}: {m_credits}, " +
                   $"{nameof(m_healing)}: {m_healing}, " +
                   $"{nameof(m_damage)}: {m_damage}, " +
                   $"{nameof(m_techPoints)}: {m_techPoints}, " +
                   $"{nameof(m_permanentStatMods)}: {m_permanentStatMods}, " +
                   $"{nameof(m_permanentStatusChanges)}: {m_permanentStatusChanges}, " +
                   $"{nameof(m_sequenceToPlay)}: {m_sequenceToPlay}, " +
                   ")";
        }
    }
}
