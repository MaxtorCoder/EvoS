using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("NPCSpawner")]
    public class NPCSpawner : ISerializedItem
    {
        public string m_spawnerTitle;
        public SerializedComponent m_actorPrefab;
        public SerializedComponent m_characterResourceLink;
        public SerializedComponent m_actorBrain;
        public SerializedComponent m_spawnPoint;
        public SerializedComponent m_destination;
        public SerializedComponent m_initialSpawnLookAtPoint;
        public int m_spawnTurn;
        public int m_respawnTime;
        public int m_spawnScriptIndex;
        public int m_despawnScriptIndex;
        public int m_skinIndex;
        public int m_patternIndex;
        public int m_colorIndex;
        public Team m_team;
        public string m_actorNameOverride;
        public bool m_isPlayer;
        public SerializedArray<ActivatableObject> m_activationsOnDeath;
        public SerializedArray<ActivatableObject> m_activationsOnSpawn;
        public bool m_applyEffectOnNPC;
        public StandardActorEffectData m_effectOnNPC;
        public SerializedArray<string> m_tagsToApplyToActor;
        public SerializedComponent m_actor;
        public int m_id;

        public NPCSpawner()
        {
        }

        public NPCSpawner(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_spawnerTitle = stream.ReadString32();
            m_actorPrefab = new SerializedComponent(assetFile, stream);
            m_characterResourceLink = new SerializedComponent(assetFile, stream);
            m_actorBrain = new SerializedComponent(assetFile, stream);
            m_spawnPoint = new SerializedComponent(assetFile, stream);
            m_destination = new SerializedComponent(assetFile, stream);
            m_initialSpawnLookAtPoint = new SerializedComponent(assetFile, stream);
            m_spawnTurn = stream.ReadInt32();
            m_respawnTime = stream.ReadInt32();
            m_spawnScriptIndex = stream.ReadInt32();
            m_despawnScriptIndex = stream.ReadInt32();
            m_skinIndex = stream.ReadInt32();
            m_patternIndex = stream.ReadInt32();
            m_colorIndex = stream.ReadInt32();
            m_team = (Team) stream.ReadInt32();
            m_actorNameOverride = stream.ReadString32();
            m_isPlayer = stream.ReadBoolean();
            stream.AlignTo();
            m_activationsOnDeath = new SerializedArray<ActivatableObject>(assetFile, stream);
            m_activationsOnSpawn = new SerializedArray<ActivatableObject>(assetFile, stream);
            m_applyEffectOnNPC = stream.ReadBoolean();
            stream.AlignTo();
            m_effectOnNPC = new StandardActorEffectData(assetFile, stream);
            m_tagsToApplyToActor = new SerializedArray<string>(assetFile, stream);
            m_actor = new SerializedComponent(assetFile, stream);
            m_id = stream.ReadInt32();
        }

        public override string ToString()
        {
            return $"{nameof(NPCSpawner)}(" +
                   $"{nameof(m_spawnerTitle)}: {m_spawnerTitle}, " +
                   $"{nameof(m_actorPrefab)}: {m_actorPrefab}, " +
                   $"{nameof(m_characterResourceLink)}: {m_characterResourceLink}, " +
                   $"{nameof(m_actorBrain)}: {m_actorBrain}, " +
                   $"{nameof(m_spawnPoint)}: {m_spawnPoint}, " +
                   $"{nameof(m_destination)}: {m_destination}, " +
                   $"{nameof(m_initialSpawnLookAtPoint)}: {m_initialSpawnLookAtPoint}, " +
                   $"{nameof(m_spawnTurn)}: {m_spawnTurn}, " +
                   $"{nameof(m_respawnTime)}: {m_respawnTime}, " +
                   $"{nameof(m_spawnScriptIndex)}: {m_spawnScriptIndex}, " +
                   $"{nameof(m_despawnScriptIndex)}: {m_despawnScriptIndex}, " +
                   $"{nameof(m_skinIndex)}: {m_skinIndex}, " +
                   $"{nameof(m_patternIndex)}: {m_patternIndex}, " +
                   $"{nameof(m_colorIndex)}: {m_colorIndex}, " +
                   $"{nameof(m_team)}: {m_team}, " +
                   $"{nameof(m_actorNameOverride)}: {m_actorNameOverride}, " +
                   $"{nameof(m_isPlayer)}: {m_isPlayer}, " +
                   $"{nameof(m_activationsOnDeath)}: {m_activationsOnDeath}, " +
                   $"{nameof(m_activationsOnSpawn)}: {m_activationsOnSpawn}, " +
                   $"{nameof(m_applyEffectOnNPC)}: {m_applyEffectOnNPC}, " +
                   $"{nameof(m_effectOnNPC)}: {m_effectOnNPC}, " +
                   $"{nameof(m_tagsToApplyToActor)}: {m_tagsToApplyToActor}, " +
                   $"{nameof(m_actor)}: {m_actor}, " +
                   $"{nameof(m_id)}: {m_id}, " +
                   ")";
        }
    }
}
