using System;
using System.Collections.Generic;

namespace EvoS.Framework.Misc
{
    [Serializable]
    public class QuestPrerequisites
    {
        public int RequiredSeason;
        public int RequiredChapter;
        public List<QuestCondition> Conditions;
        public string LogicStatement;
    }
}
