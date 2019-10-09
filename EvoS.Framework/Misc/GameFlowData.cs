using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("GameFlowData")]
    public class GameFlowData : ISerializedItem
    {
        public SerializedVector<SerializedActorData> m_ownedActorDatas;
        public bool m_oneClassOnTeam;
        public SerializedArray<SerializedComponent> m_availableCharacterResourceLinkPrefabs;
        public float m_startTime;
        public float m_deploymentTime;
        public float m_turnTime;
        public float m_maxTurnTime;
        public float m_resolveTimeoutLimit;

        public GameFlowData()
        {
        }

        public GameFlowData(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_ownedActorDatas = new SerializedVector<SerializedActorData>(assetFile, stream);
            m_oneClassOnTeam = stream.ReadBoolean();
            stream.AlignTo();
            m_availableCharacterResourceLinkPrefabs = new SerializedArray<SerializedComponent>(assetFile, stream);
            m_startTime = stream.ReadSingle();
            m_deploymentTime = stream.ReadSingle();
            m_turnTime = stream.ReadSingle();
            m_maxTurnTime = stream.ReadSingle();
            m_resolveTimeoutLimit = stream.ReadSingle();
        }

        public override string ToString()
        {
            return $"{nameof(GameFlowData)}>(" +
                   $"{nameof(m_ownedActorDatas)}: {m_ownedActorDatas}, " +
                   $"{nameof(m_oneClassOnTeam)}: {m_oneClassOnTeam}, " +
                   $"{nameof(m_availableCharacterResourceLinkPrefabs)}: {m_availableCharacterResourceLinkPrefabs}, " +
                   $"{nameof(m_startTime)}: {m_startTime}, " +
                   $"{nameof(m_deploymentTime)}: {m_deploymentTime}, " +
                   $"{nameof(m_turnTime)}: {m_turnTime}, " +
                   $"{nameof(m_maxTurnTime)}: {m_maxTurnTime}, " +
                   $"{nameof(m_resolveTimeoutLimit)}: {m_resolveTimeoutLimit}, " +
                   ")";
        }
    }
}
