using System;
using System.Collections.Generic;

// Token: 0x02000768 RID: 1896
public struct CharacterTypeComparer : IEqualityComparer<CharacterType>
{
    // Token: 0x06003E59 RID: 15961 RVA: 0x00036ABB File Offset: 0x00034CBB
    public bool Equals(CharacterType x, CharacterType y)
    {
        return x == y;
    }

    // Token: 0x06003E5A RID: 15962 RVA: 0x00002DC2 File Offset: 0x00000FC2
    public int GetHashCode(CharacterType obj)
    {
        return (int)obj;
    }
}
