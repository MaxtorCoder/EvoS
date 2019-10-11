using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("SpawnPointManager")]
    public class SpawnPointManager : MonoBehaviour
    {
        public bool m_playersSelectRespawn;
        public bool m_spawnInDuringMovement;
        public int m_respawnDelay;
        public float m_respawnInnerRadius;
        public float m_respawnOuterRadius;
        public RespawnMethod m_respawnMethod;
        public BoardRegion m_spawnRegionsTeamA;
        public BoardRegion m_spawnRegionsTeamB;
        public BoardRegion m_initialSpawnPointsTeamA;
        public BoardRegion m_initialSpawnPointsTeamB;
        public SerializedComponent m_initialSpawnLookAtPoint;
        public float m_startMinDistToFriend;
        public float m_startMinDistToEnemy;
        public float m_minDistToFriend;
        public float m_minDistToEnemy;
        public float m_generatePerimeterSize;
        public bool m_onlyAvoidVisibleEnemies;
        public bool m_brushHidesRespawnFlares;
        public bool m_respawnActorsCanCollectPowerUps;
        public bool m_respawnActorsCanBeHitDuringMovement;
        public int m_minValidRespawnSquaresForPlayerSelection;

        public SpawnPointManager()
        {
        }

        public SpawnPointManager(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_playersSelectRespawn = stream.ReadBoolean();
            stream.AlignTo();
            m_spawnInDuringMovement = stream.ReadBoolean();
            stream.AlignTo();
            m_respawnDelay = stream.ReadInt32();
            m_respawnInnerRadius = stream.ReadSingle();
            m_respawnOuterRadius = stream.ReadSingle();
            m_respawnMethod = (RespawnMethod) stream.ReadInt32();
            m_spawnRegionsTeamA = new BoardRegion(assetFile, stream);
            m_spawnRegionsTeamB = new BoardRegion(assetFile, stream);
            m_initialSpawnPointsTeamA = new BoardRegion(assetFile, stream);
            m_initialSpawnPointsTeamB = new BoardRegion(assetFile, stream);
            m_initialSpawnLookAtPoint = new SerializedComponent(assetFile, stream);
            m_startMinDistToFriend = stream.ReadSingle();
            m_startMinDistToEnemy = stream.ReadSingle();
            m_minDistToFriend = stream.ReadSingle();
            m_minDistToEnemy = stream.ReadSingle();
            m_generatePerimeterSize = stream.ReadSingle();
            m_onlyAvoidVisibleEnemies = stream.ReadBoolean();
            stream.AlignTo();
            m_brushHidesRespawnFlares = stream.ReadBoolean();
            stream.AlignTo();
            m_respawnActorsCanCollectPowerUps = stream.ReadBoolean();
            stream.AlignTo();
            m_respawnActorsCanBeHitDuringMovement = stream.ReadBoolean();
            stream.AlignTo();
            m_minValidRespawnSquaresForPlayerSelection = stream.ReadInt32();
        }

        public override string ToString()
        {
            return $"{nameof(SpawnPointManager)}>(" +
                   $"{nameof(m_playersSelectRespawn)}: {m_playersSelectRespawn}, " +
                   $"{nameof(m_spawnInDuringMovement)}: {m_spawnInDuringMovement}, " +
                   $"{nameof(m_respawnDelay)}: {m_respawnDelay}, " +
                   $"{nameof(m_respawnInnerRadius)}: {m_respawnInnerRadius}, " +
                   $"{nameof(m_respawnOuterRadius)}: {m_respawnOuterRadius}, " +
                   $"{nameof(m_respawnMethod)}: {m_respawnMethod}, " +
                   $"{nameof(m_spawnRegionsTeamA)}: {m_spawnRegionsTeamA}, " +
                   $"{nameof(m_spawnRegionsTeamB)}: {m_spawnRegionsTeamB}, " +
                   $"{nameof(m_initialSpawnPointsTeamA)}: {m_initialSpawnPointsTeamA}, " +
                   $"{nameof(m_initialSpawnPointsTeamB)}: {m_initialSpawnPointsTeamB}, " +
                   $"{nameof(m_initialSpawnLookAtPoint)}: {m_initialSpawnLookAtPoint}, " +
                   $"{nameof(m_startMinDistToFriend)}: {m_startMinDistToFriend}, " +
                   $"{nameof(m_startMinDistToEnemy)}: {m_startMinDistToEnemy}, " +
                   $"{nameof(m_minDistToFriend)}: {m_minDistToFriend}, " +
                   $"{nameof(m_minDistToEnemy)}: {m_minDistToEnemy}, " +
                   $"{nameof(m_generatePerimeterSize)}: {m_generatePerimeterSize}, " +
                   $"{nameof(m_onlyAvoidVisibleEnemies)}: {m_onlyAvoidVisibleEnemies}, " +
                   $"{nameof(m_brushHidesRespawnFlares)}: {m_brushHidesRespawnFlares}, " +
                   $"{nameof(m_respawnActorsCanCollectPowerUps)}: {m_respawnActorsCanCollectPowerUps}, " +
                   $"{nameof(m_respawnActorsCanBeHitDuringMovement)}: {m_respawnActorsCanBeHitDuringMovement}, " +
                   $"{nameof(m_minValidRespawnSquaresForPlayerSelection)}: {m_minValidRespawnSquaresForPlayerSelection}, " +
                   ")";
        }

        public enum RespawnMethod
        {
            RespawnAnywhere,
            RespawnOnlyAtInitialSpawnPoints,
            RespawnInGraveyards
        }
    }
}
