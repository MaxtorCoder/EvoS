using System;
using System.Runtime.InteropServices;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Size = 1)]
    [EvosMessage(79)]
    public struct PlayerModData
    {
        public int AbilityId { get; set; }

        public int AbilityModID { get; set; }

        public override string ToString() => $"Ability[{AbilityId}]->Mod[{AbilityModID}]";
    }
}
