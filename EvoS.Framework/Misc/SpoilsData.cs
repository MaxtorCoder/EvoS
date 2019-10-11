using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("SpoilsData")]
    public class SpoilsData : MonoBehaviour
    {
        public SpoilsManager.SpoilsType m_spoilsType;
        public SerializedComponent m_overrideSpoils;

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_spoilsType = (SpoilsManager.SpoilsType) stream.ReadInt32(); // valuetype SpoilsManager/SpoilsType
            m_overrideSpoils = new SerializedComponent(assetFile, stream); // class PowerUp
        }

        public override string ToString()
        {
            return $"{nameof(SpoilsData)}>(" +
                   $"{nameof(m_spoilsType)}: {m_spoilsType}, " +
                   $"{nameof(m_overrideSpoils)}: {m_overrideSpoils}, " +
                   ")";
        }
    }
}
