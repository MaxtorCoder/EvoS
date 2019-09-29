using System;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(541)]
    public struct CharacterCardInfo
    {
        public CharacterCardInfo Reset()
        {
            PrepCard = CardType.None;
            CombatCard = CardType.None;
            DashCard = CardType.None;
            return this;
        }

        public string ToIdString()
        {
            return string.Format("{0}/{1}/{2}", (int) PrepCard, (int) CombatCard, (int) DashCard);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is CharacterCardInfo))
            {
                return false;
            }

            CharacterCardInfo characterCardInfo = (CharacterCardInfo) obj;
            return PrepCard == characterCardInfo.PrepCard && CombatCard == characterCardInfo.CombatCard &&
                   DashCard == characterCardInfo.DashCard;
        }

        public bool HasEmptySelection()
        {
            return PrepCard <= CardType.NoOverride || DashCard <= CardType.NoOverride ||
                   CombatCard <= CardType.NoOverride;
        }

        public bool Uninitialized()
        {
            return PrepCard == CardType.NoOverride && DashCard == CardType.NoOverride &&
                   CombatCard == CardType.NoOverride;
        }

        public override int GetHashCode()
        {
            return PrepCard.GetHashCode() ^ CombatCard.GetHashCode() ^ DashCard.GetHashCode();
        }

        public CardType PrepCard;
        public CardType CombatCard;
        public CardType DashCard;
    }
}
