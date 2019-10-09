using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("ObjectivePoints")]
    public class ObjectivePoints : ISerializedItem
    {
        public bool m_skipEndOfGameCheck;
        public int m_startingPointsTeamA;
        public int m_startingPointsTeamB;
        public int m_timeLimitTurns;
        public bool m_disablePowerupsAfterTimeLimit;
        public string m_victoryCondition;
        public string m_victoryConditionOneTurnLeft;
        public VictoryCondition m_teamAVictoryCondition;
        public VictoryCondition m_teamBVictoryCondition;
        public bool m_allowTies;
        public SerializedVector<PointsForCharacter> m_passivePointsForTeamWithCharacter;
        public SerializedComponent m_gameModePanelPrefab;
        public ObjectivePoints.MatchState m_matchState;

        public ObjectivePoints()
        {
        }

        public ObjectivePoints(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_skipEndOfGameCheck = stream.ReadBoolean();
            stream.AlignTo();
            m_startingPointsTeamA = stream.ReadInt32();
            m_startingPointsTeamB = stream.ReadInt32();
            m_timeLimitTurns = stream.ReadInt32();
            m_disablePowerupsAfterTimeLimit = stream.ReadBoolean();
            stream.AlignTo();
            m_victoryCondition = stream.ReadString32();
            m_victoryConditionOneTurnLeft = stream.ReadString32();
            m_teamAVictoryCondition = new VictoryCondition(assetFile, stream);
            m_teamBVictoryCondition = new VictoryCondition(assetFile, stream);
            m_allowTies = stream.ReadBoolean();
            stream.AlignTo();
            m_passivePointsForTeamWithCharacter = new SerializedVector<PointsForCharacter>(assetFile, stream);
            m_gameModePanelPrefab = new SerializedComponent(assetFile, stream);
            m_matchState = (ObjectivePoints.MatchState) stream.ReadInt32();
        }

        public override string ToString()
        {
            return $"{nameof(ObjectivePoints)}>(" +
                   $"{nameof(m_skipEndOfGameCheck)}: {m_skipEndOfGameCheck}, " +
                   $"{nameof(m_startingPointsTeamA)}: {m_startingPointsTeamA}, " +
                   $"{nameof(m_startingPointsTeamB)}: {m_startingPointsTeamB}, " +
                   $"{nameof(m_timeLimitTurns)}: {m_timeLimitTurns}, " +
                   $"{nameof(m_disablePowerupsAfterTimeLimit)}: {m_disablePowerupsAfterTimeLimit}, " +
                   $"{nameof(m_victoryCondition)}: {m_victoryCondition}, " +
                   $"{nameof(m_victoryConditionOneTurnLeft)}: {m_victoryConditionOneTurnLeft}, " +
                   $"{nameof(m_teamAVictoryCondition)}: {m_teamAVictoryCondition}, " +
                   $"{nameof(m_teamBVictoryCondition)}: {m_teamBVictoryCondition}, " +
                   $"{nameof(m_allowTies)}: {m_allowTies}, " +
                   $"{nameof(m_passivePointsForTeamWithCharacter)}: {m_passivePointsForTeamWithCharacter}, " +
                   $"{nameof(m_gameModePanelPrefab)}: {m_gameModePanelPrefab}, " +
                   $"{nameof(m_matchState)}: {m_matchState}, " +
                   ")";
        }

        public enum MatchState
        {
            InMatch,
            MatchEnd,
        }
    }
}
