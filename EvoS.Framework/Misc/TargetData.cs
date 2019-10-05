using System;

namespace EvoS.Framework.Misc
{
    [Serializable]
    public class TargetData
    {
        public string Description;
        public float Range;
        public float MinRange;
        public bool CheckLineOfSight;
        public Ability.TargetingParadigm TargetingParadigm;
    }

}
