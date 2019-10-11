using EvoS.Framework.Network.NetworkBehaviours;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    public class MatchObjective : MonoBehaviour
    {
        public virtual void Client_OnActorDeath(ActorData actor)
        {
        }

        public virtual void Server_OnActorDeath(ActorData actor)
        {
        }
    }
}
