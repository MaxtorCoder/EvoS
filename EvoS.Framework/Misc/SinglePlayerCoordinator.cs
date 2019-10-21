using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("SinglePlayerCoordinator")]
    public class SinglePlayerCoordinator : MonoBehaviour
    {
        public SerializedArray<SinglePlayerState> m_script;
        public BoardRegion m_forbiddenSquares;
        public SerializedArray<ActivatableObject> m_activationsOnForbiddenPath;
        public SerializedArray<ActivatableObject> m_activationsOnFailedToShootAndMove;
        public SerializedArray<ActivatableObject> m_activationsOnFailedToUseAllAbilities;
        public SerializedComponent m_initialCameraRotationTarget;
        public SerializedArray<SinglePlayerScriptedChat> m_chatTextOnLowHealth;
        public SerializedArray<SinglePlayerScriptedChat> m_chatTextAtEndOfMatch;

        public SinglePlayerCoordinator()
        {
        }

        public SinglePlayerCoordinator(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_script = new SerializedArray<SinglePlayerState>(assetFile, stream);
            m_forbiddenSquares = new BoardRegion(assetFile, stream);
            m_activationsOnForbiddenPath = new SerializedArray<ActivatableObject>(assetFile, stream);
            m_activationsOnFailedToShootAndMove = new SerializedArray<ActivatableObject>(assetFile, stream);
            m_activationsOnFailedToUseAllAbilities = new SerializedArray<ActivatableObject>(assetFile, stream);
            m_initialCameraRotationTarget = new SerializedComponent(assetFile, stream);
            m_chatTextOnLowHealth = new SerializedArray<SinglePlayerScriptedChat>(assetFile, stream);
            m_chatTextAtEndOfMatch = new SerializedArray<SinglePlayerScriptedChat>(assetFile, stream);
        }

        public override string ToString()
        {
            return $"{nameof(SinglePlayerCoordinator)}>(" +
                   $"{nameof(m_script)}: {m_script}, " +
                   $"{nameof(m_forbiddenSquares)}: {m_forbiddenSquares}, " +
                   $"{nameof(m_activationsOnForbiddenPath)}: {m_activationsOnForbiddenPath}, " +
                   $"{nameof(m_activationsOnFailedToShootAndMove)}: {m_activationsOnFailedToShootAndMove}, " +
                   $"{nameof(m_activationsOnFailedToUseAllAbilities)}: {m_activationsOnFailedToUseAllAbilities}, " +
                   $"{nameof(m_initialCameraRotationTarget)}: {m_initialCameraRotationTarget}, " +
                   $"{nameof(m_chatTextOnLowHealth)}: {m_chatTextOnLowHealth}, " +
                   $"{nameof(m_chatTextAtEndOfMatch)}: {m_chatTextAtEndOfMatch}, " +
                   ")";
        }
    }
}
