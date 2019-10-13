using System.Collections.Generic;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.NetworkBehaviours;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.Static
{
    public class Phase
    {
        internal List<ActorAnimation> u000E = new List<ActorAnimation>();
        private Dictionary<int, int> u0012 = new Dictionary<int, int>();
        private Dictionary<int, int> u0015 = new Dictionary<int, int>();
        private List<int> u0016 = new List<int>();
        private int u0013 = -1;
        private int u0018 = -1;
        private bool u0009 = true;
        private int u0019 = -1;
        private int u0011 = -1;
        private int u0004 = -1;
        private float u000A = -1f;
        private int u001E = -1;
        private float u001A;
        private float u000B;
        private float u0003;
        private Turn u000F;
        private float u0017;
        private float u000D;
        private bool u0008;
        private bool u0002;
        private bool u0006;
        private bool u0020;
        private bool u000C;
        private bool u0014;
        private bool u0005;
        private bool u001B;
        private bool u0001;

        public AbilityPriority Index { get; private set; }

        public Phase(Turn _param1)
        {
            u000F = _param1;
        }

        internal void u001C(IBitStream _param1)
        {
            sbyte index1 = (sbyte) Index;
            _param1.Serialize(ref index1);
            Index = (AbilityPriority) index1;
            sbyte num1 = (sbyte) u000E.Count;
            bool flag = _param1.isWriting &&
                        true; // TODO
//                        (u0006 || index1 < (sbyte) ServerClientUtils.GetCurrentAbilityPhase());
            if (_param1.isWriting && flag)
                num1 = 0;
            _param1.Serialize(ref num1);
            for (int index2 = 0; index2 < (int) num1; ++index2)
            {
                while (index2 >= u000E.Count)
                    u000E.Add(new ActorAnimation(u000F));
                u000E[index2].u000Du000E(_param1);
            }

            if (_param1.isWriting)
            {
                sbyte count = (sbyte) u0012.Count;
                _param1.Serialize(ref count);
                foreach (KeyValuePair<int, int> keyValuePair in u0012)
                {
                    sbyte key = (sbyte) keyValuePair.Key;
                    short num2 = (short) keyValuePair.Value;
                    _param1.Serialize(ref key);
                    _param1.Serialize(ref num2);
                }
            }
            else
            {
                sbyte num2 = -1;
                _param1.Serialize(ref num2);
                u0012 = new Dictionary<int, int>();
                for (int index2 = 0; index2 < (int) num2; ++index2)
                {
                    sbyte invalidActorIndex = (sbyte) ActorData.s_invalidActorIndex;
                    short num3 = -1;
                    _param1.Serialize(ref invalidActorIndex);
                    _param1.Serialize(ref num3);
                    u0012.Add(invalidActorIndex, num3);
                }
            }

            if (index1 == 5)
            {
                if (_param1.isWriting)
                {
                    sbyte count = (sbyte) u0015.Count;
                    if (flag)
                    {
                        sbyte num2 = 0;
                        _param1.Serialize(ref num2);
                    }
                    else
                    {
                        _param1.Serialize(ref count);
                        foreach (KeyValuePair<int, int> keyValuePair in u0015)
                        {
                            sbyte key = (sbyte) keyValuePair.Key;
                            sbyte num2 = (sbyte) keyValuePair.Value;
                            _param1.Serialize(ref key);
                            _param1.Serialize(ref num2);
                        }
                    }
                }
                else
                {
                    sbyte num2 = -1;
                    _param1.Serialize(ref num2);
                    u0015 = new Dictionary<int, int>();
                    for (int index2 = 0; index2 < (int) num2; ++index2)
                    {
                        sbyte invalidActorIndex = (sbyte) ActorData.s_invalidActorIndex;
                        sbyte num3 = -1;
                        _param1.Serialize(ref invalidActorIndex);
                        _param1.Serialize(ref num3);
                        u0015.Add(invalidActorIndex, num3);
                    }
                }
            }

            if (_param1.isWriting)
            {
                sbyte count = (sbyte) u0016.Count;
                _param1.Serialize(ref count);
                for (sbyte index2 = 0; (int) index2 < (int) count; ++index2)
                {
                    sbyte num2 = (sbyte) u0016[index2];
                    _param1.Serialize(ref num2);
                }
            }
            else
            {
                sbyte num2 = -1;
                _param1.Serialize(ref num2);
                u0016 = new List<int>();
                for (sbyte index2 = 0; (int) index2 < (int) num2; ++index2)
                {
                    sbyte num3 = -1;
                    _param1.Serialize(ref num3);
                    u0016.Add(num3);
                }
            }
        }
    }
}
