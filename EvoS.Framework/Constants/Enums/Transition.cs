using System;

namespace EvoS.Framework.Constants.Enums
{
    [Serializable]
    public enum Transition
    {
        NullTransition,
        DoNothing,
        Patrol,
        TookDamage,
        Healed,
        Buffed,
    }
}
