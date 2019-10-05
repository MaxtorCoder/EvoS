using System;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    [Serializable]
    public class TargeterTemplateSwapData
    {
        public string Notes;
        public TargeterTemplateSwapData.TargeterTemplateType TemplateToReplace;
        public GameObject PrefabToUse;

        public enum TargeterTemplateType
        {
            Unknown,
            DynamicCone,
            Laser,
        }
    }
}
