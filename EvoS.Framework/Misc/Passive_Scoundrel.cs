using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Network.NetworkBehaviours;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("Passive_Scoundrel")]
    public class Passive_Scoundrel : Passive
    {
        public int m_trapwireLastCastTurn;
        public bool m_trapwireDidDamage;

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            base.DeserializeAsset(assetFile, stream);

            m_trapwireLastCastTurn = stream.ReadInt32(); // int32
            m_trapwireDidDamage = stream.ReadBoolean();
            stream.AlignTo(); // bool
        }

        public override string ToString()
        {
            return $"{nameof(Passive_Scoundrel)}>(" +
                   $"{nameof(m_trapwireLastCastTurn)}: {m_trapwireLastCastTurn}, " +
                   $"{nameof(m_trapwireDidDamage)}: {m_trapwireDidDamage}, " +
                   ")";
        }
    }
}
