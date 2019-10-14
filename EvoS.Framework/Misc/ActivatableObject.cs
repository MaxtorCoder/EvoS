using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("ActivatableObject")]
    public class ActivatableObject : ISerializedItem
    {
        public SerializedComponent m_sceneObject;
        public ActivationAction m_activation;

        public ActivatableObject()
        {
        }

        public ActivatableObject(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_sceneObject = new SerializedComponent(assetFile, stream);
            m_activation = (ActivationAction) stream.ReadInt32();
        }

        public override string ToString()
        {
            return $"{nameof(ActivatableObject)}>(" +
                   $"{nameof(m_sceneObject)}: {m_sceneObject}, " +
                   $"{nameof(m_activation)}: {m_activation}, " +
                   ")";
        }

        public enum ActivationAction
        {
            SetActive,
            ClearActive,
            ToggleActive
        }
    }
}
