using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net.Mime;
using System.Numerics;
using EvoS.Framework.Network.NetworkBehaviours;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    public class ActorDebugUtils : MonoBehaviour
    {
        private static ActorDebugUtils s_instance;
        private Dictionary<DebugCategory, DebugCategoryInfo> m_categoryToDebugContainer;
        private ActorData m_debugContextActor;

        public static ActorDebugUtils Get()
        {
            return s_instance;
        }

        private void Awake()
        {
            s_instance = this;
        }

        private void OnDestroy()
        {
            s_instance = null;
        }

        public bool ShowDebugGUI { get; set; }

        public bool ShowingCategory(DebugCategory cat, bool requireDebugWindowVisible = true)
        {
            if (!ShowDebugGUI && requireDebugWindowVisible)
                return false;
            DebugCategoryInfo debugCategoryInfo = GetDebugCategoryInfo(cat);
            if (debugCategoryInfo != null)
                return debugCategoryInfo.m_enabled;
            return false;
        }

        public DebugCategoryInfo GetDebugCategoryInfo(
            DebugCategory cat)
        {
            if (m_categoryToDebugContainer != null && m_categoryToDebugContainer.ContainsKey(cat))
                return m_categoryToDebugContainer[cat];
            return null;
        }

        public ActorData GetDebugContextActor()
        {
            return m_debugContextActor;
        }

        public static void smethod_0(Bounds bounds_0, Color color_0, float float_0 = 0.0f)
        {
        }

        public static void SetTempDebugString(string value)
        {
        }

        public enum DebugCategory
        {
            None,
            CameraManager,
            Chatter,
            CursorPosition,
            CursorState,
            FreelancerSpecificStats,
            GeneralStats,
            LastKnownPosition,
            TheatricsOrder,
            ForTempDebug,
            const_10
        }

        public class DebugCategoryInfo
        {
            public string m_stringToDisplay = string.Empty;
            public bool m_enabled;
            public float m_spacing;
        }
    }
}
