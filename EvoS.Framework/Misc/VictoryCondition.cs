using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("VictoryCondition")]
    public class VictoryCondition : ISerializedItem
    {
        public string m_conditionString;
        public string m_PointName;
        public SerializedArray<PointCondition> m_conditions_anyMet;
        public SerializedArray<PointCondition> m_conditions_allRequired;
        public SerializedArray<PointCondition> m_conditions_noneAllowed;

        public VictoryCondition()
        {
            
        }
        
        public VictoryCondition(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_conditionString = stream.ReadString32(); 
            m_PointName = stream.ReadString32(); 
            m_conditions_anyMet = new SerializedArray<PointCondition>(assetFile, stream); 
            m_conditions_allRequired = new SerializedArray<PointCondition>(assetFile, stream); 
            m_conditions_noneAllowed = new SerializedArray<PointCondition>(assetFile, stream); 
        }

        public override string ToString()
        {
            return $"{nameof(VictoryCondition)}>(" +
                   $"{nameof(m_conditionString)}: {m_conditionString}, " +
                   $"{nameof(m_PointName)}: {m_PointName}, " +
                   $"{nameof(m_conditions_anyMet)}: {m_conditions_anyMet}, " +
                   $"{nameof(m_conditions_allRequired)}: {m_conditions_allRequired}, " +
                   $"{nameof(m_conditions_noneAllowed)}: {m_conditions_noneAllowed}, " +
                   ")";
        }
    }
}
