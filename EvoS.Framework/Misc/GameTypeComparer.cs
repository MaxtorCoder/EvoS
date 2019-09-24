using System;
using System.Collections.Generic;

namespace EvoS.Framework.Misc
{
    [Serializable]
    public struct GameTypeComparer : IEqualityComparer<GameType>
    {
        public bool Equals(GameType x, GameType y) => x == y;

        public int GetHashCode(GameType obj) => (int) obj;
    }
}
