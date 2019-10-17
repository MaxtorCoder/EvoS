using System.Collections.Generic;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.NetworkBehaviours;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    public static class GameplayUtils
    {
        public static bool IsMinion(MonoBehaviour entity)
        {
            return entity.GetComponent<MinionData>() != null;
        }

        public static bool IsMinion(GameObject obj)
        {
            return obj.GetComponent<MinionData>() != null;
        }

        public static bool IsBot(ActorData actor)
        {
            bool flag = false;
            if (actor != null)
                flag = actor.GetComponent<BotController>() != null;
            return flag;
        }

        public static bool IsBot(MonoBehaviour entity)
        {
            return IsBot(entity.gameObject);
        }

        public static bool IsBot(GameObject obj)
        {
            return IsBot(obj.GetComponent<ActorData>());
        }

        public static bool IsPlayerControlled(ActorData actor)
        {
            bool flag = false;
            if (actor != null)
                flag = actor.PlayerIndex != PlayerData.s_invalidPlayerIndex;
            return flag;
        }

        public static bool IsPlayerControlled(MonoBehaviour entity)
        {
            bool flag = false;
            ActorData component = entity.GetComponent<ActorData>();
            if (component != null)
                flag = component.PlayerIndex != PlayerData.s_invalidPlayerIndex;
            return flag;
        }

        public static bool IsPlayerControlled(GameObject obj)
        {
            return IsPlayerControlled(obj.GetComponent<ActorData>());
        }

        public static bool IsHumanControlled(ActorData actor)
        {
            bool flag = false;
            if (actor != null)
                flag = actor.method_67();
            return flag;
        }

        public static bool IsHumanControlled(MonoBehaviour entity)
        {
            return IsHumanControlled(entity.gameObject);
        }

        public static bool IsHumanControlled(GameObject obj)
        {
            return IsHumanControlled(obj.GetComponent<ActorData>());
        }

        public static bool IsValidPlayer(ActorData actor)
        {
            if (actor != null)
                return actor.PlayerIndex != PlayerData.s_invalidPlayerIndex;
            return false;
        }

        public static List<Team> GetOtherTeamsThan(Team team)
        {
            var teamList = new List<Team>();
            if (team != Team.TeamA)
                teamList.Add(Team.TeamA);
            if (team != Team.TeamB)
                teamList.Add(Team.TeamB);
            if (team != Team.Objects)
                teamList.Add(Team.Objects);
            return teamList;
        }

        public static int GetActorIndexOfActor(ActorData actor)
        {
            return actor?.ActorIndex ?? ActorData.s_invalidActorIndex;
        }

//        public static ActorData GetActorOfActorIndex(int actorIndex)
//        {
//            return actorIndex != ActorData.s_invalidActorIndex
//                ? GameFlowData.Get().FindActorByActorIndex(actorIndex)
//                : (ActorData) null;
//        }
    }
}
