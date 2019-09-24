using System.Collections.Generic;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Network.Static
{
    public class ELOPlayerKey
    {
        private ELOPlayerKey()
        {
            this.m_components = new List<ELOKeyComponent>();
//            this.m_components.Add(new ELOKeyComponent_Coordination());
//            this.m_components.Add(new ELOKeyComponent_FinalScore());
//            this.m_components.Add(new ELOKeyComponent_Softened());
//            this.m_components.Add(new ELOKeyComponent_Queue());
        }

//        public static uint MaxIterations
//        {
//            get
//            {
//                return ELOKeyComponent_Coordination.PhaseWidth * ELOKeyComponent_FinalScore.PhaseWidth *
//                       ELOKeyComponent_Softened.PhaseWidth * ELOKeyComponent_Queue.PhaseWidth;
//            }
//        }

        public static ELOPlayerKey MatchmakingEloKey
        {
            get { return ELOPlayerKey.s_matchmakingKey; }
        }

//        public static ELOPlayerKey PublicFacingKey
//        {
//            get { return ELOPlayerKey.s_publicKey; }
//        }


        public string KeyText
        {
            get
            {
                string text = string.Empty;
                foreach (ELOKeyComponent elokeyComponent in this.m_components)
                {
                    text += elokeyComponent.GetComponentChar();
                }

                return text;
            }
        }

        public string LogPhaseConcatination
        {
            get
            {
                string text = string.Empty;
                foreach (ELOKeyComponent elokeyComponent in this.m_components)
                {
                    text += elokeyComponent.GetPhaseChar();
                }

                return text;
            }
        }

        private static ELOPlayerKey s_matchmakingKey = new ELOPlayerKey();
//        private static ELOPlayerKey s_publicKey =
//            new ELOPlayerKey().Set(MatchmakingQueueConfig.EloKeyFlags.SOFTENED_PUBLIC, GameType.PvP, false);
        private List<ELOKeyComponent> m_components = new List<ELOKeyComponent>();
        private float teamAAccountElo;
        private float teamBAccountElo;
        private float teamACharElo;
        private float teamBCharElo;
        private float teamAWeight;
        private float teamBWeight;
        private float teamAAccountPrediction;
        private float teamBAccountPrediction;
        private int teamAPredictionWeight;
        private int teamBPredictionWeight;
        private float teamACharPrediction;
        private float teamBCharPrediction;
        private List<int> accountPredictionWeights = new List<int>();
        private List<int> charPredictionWeights = new List<int>();
        private Dictionary<long, ELOPlayerKey.EloTracking> m_eloTrackings =
            new Dictionary<long, ELOPlayerKey.EloTracking>();

        private class EloTracking
        {
            public float accountElo;
            public float characterElo;
            public float weight;
            public int accountMatches;
            public int charMatches;
            public long groupId;
            public Team team = Team.Invalid;
        }

        public enum ACEnum
        {
            ACCOUNT,
            CHARACTER
        }
    }
}
