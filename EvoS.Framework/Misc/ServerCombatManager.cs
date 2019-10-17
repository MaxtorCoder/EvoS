using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Network.NetworkBehaviours;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("ServerCombatManager")]
    public class ServerCombatManager : MonoBehaviour
    {
        public ServerCombatManager()
        {
        }

        public ServerCombatManager(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public void ExecuteObjectivePointGain(ActorData caster, ActorData target, int finalChange)
        {
            if (target == null)
                throw new ApplicationException("Objective point change requires a target");
            // TODO
//            ObjectivePoints objectivePoints = ObjectivePoints.Get();
//            if (objectivePoints == null)
//                return;
//            Team teamToAdjust = target.\u000E();
//            objectivePoints.AdjustPoints(finalChange, teamToAdjust);
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
        }

        public override string ToString()
        {
            return $"{nameof(ServerCombatManager)}(" +
                   ")";
        }

        public enum DamageType
        {
            Ability,
            Effect,
            Thorns,
            Barrier,
        }

        public enum HealingType
        {
            Ability,
            Effect,
            Card,
            Powerup,
            Lifesteal,
            Barrier,
        }

        public enum TechPointChangeType
        {
            Ability,
            Effect,
            Barrier,
            AbilityInteraction,
            Card,
            Powerup,
            Regen,
        }
    }
}
