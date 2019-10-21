using System.Collections.Generic;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.Static
{
    public class Turn
    {
        public List<Phase> u000E = new List<Phase>(7);
        public int TurnID { get; private set; }
        public float TimeInPhase { get; private set; }
        public float TimeInResolve { get; private set; }

        public void OnSerializeHelper(IBitStream _param1)
        {
            int turnId = TurnID;
            _param1.Serialize(ref turnId);
            TurnID = turnId;
            sbyte count = (sbyte) u000E.Count;
            _param1.Serialize(ref count);
            for (int index = 0; index < (int) count; ++index)
            {
                while (index >= u000E.Count)
                    u000E.Add(new Phase(this));
                u000E[index].u001C(_param1);
            }
        }

        public void UnknownMethod(AbilityPriority _param1)
        {
        }
    }
}
