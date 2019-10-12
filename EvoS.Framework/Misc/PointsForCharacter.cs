using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("PointsForCharacter")]
    public class PointsForCharacter : ISerializedItem
    {
        public CharacterType m_characterType;
        public CalculationType m_givePointsFor;
        public int m_points;

        public PointsForCharacter()
        {
        }

        public PointsForCharacter(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_characterType = (CharacterType) stream.ReadInt32();
            m_givePointsFor = (CalculationType) stream.ReadInt32();
            m_points = stream.ReadInt32();
        }

        public override string ToString()
        {
            return $"{nameof(PointsForCharacter)}>(" +
                   $"{nameof(m_characterType)}: {m_characterType}, " +
                   $"{nameof(m_givePointsFor)}: {m_givePointsFor}, " +
                   $"{nameof(m_points)}: {m_points}, " +
                   ")";
        }

        public enum CalculationType
        {
            AtLeastOneMatchingActor,
            PerEachMatchingActor
        }
    }
}
