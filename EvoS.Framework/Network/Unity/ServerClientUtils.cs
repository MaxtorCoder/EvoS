using System;
using System.Collections.Generic;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Game;
using EvoS.Framework.Logging;

namespace EvoS.Framework.Network.Unity
{
    public class ServerClientUtils
    {
        public static ActionBufferPhase GetCurrentActionPhase()
        {
            ActionBufferPhase actionBufferPhase = ActionBufferPhase.Done;
//            if (!NetworkServer.active)
//            {
//                if (ClientActionBuffer.Get() != null)
//                    actionBufferPhase = ClientActionBuffer.Get().CurrentActionPhase;
//                else if (GameManager.Get() != null && GameManager.Get().GameStatus == GameStatus.Started)
//                    Log.Error("Trying to examine current action phase, but ClientActionBuffer does not exist.");
//            }
            return actionBufferPhase;
        }

        public static AbilityPriority GetCurrentAbilityPhase()
        {
            AbilityPriority abilityPriority = AbilityPriority.INVALID;
//            if (!NetworkServer.active)
//            {
//                if (ClientActionBuffer.Get() != null)
//                    abilityPriority = ClientActionBuffer.Get().AbilityPhase;
//                else
//                    Log.Error("Trying to examine current ability phase, but ClientActionBuffer does not exist.");
//            }
            return abilityPriority;
        }
        public static byte CreateBitfieldFromBoolsList(List<bool> bools)
        {
            byte num1 = 0;
            int num2 = Math.Min(bools.Count, 8);
            for (int index = 0; index < num2; ++index)
            {
                if (bools[index])
                    num1 |= (byte) (1 << index);
            }

            return num1;
        }

        public static short CreateBitfieldFromBoolsList_16bit(List<bool> bools)
        {
            short num1 = 0;
            int num2 = Math.Min(bools.Count, 16);
            for (int index = 0; index < num2; ++index)
            {
                if (bools[index])
                    num1 |= (short) (1 << index);
            }

            return num1;
        }

        public static int CreateBitfieldFromBoolsList_32bit(List<bool> bools)
        {
            int num1 = 0;
            int num2 = Math.Min(bools.Count, 32);
            for (int index = 0; index < num2; ++index)
            {
                if (bools[index])
                    num1 |= 1 << index;
            }

            return num1;
        }

        public static byte CreateBitfieldFromBools(
            bool b0,
            bool b1,
            bool b2,
            bool b3,
            bool b4,
            bool b5,
            bool b6,
            bool b7)
        {
            byte num = 0;
            if (b0)
                num |= 1;
            if (b1)
                num |= 2;
            if (b2)
                num |= 4;
            if (b3)
                num |= 8;
            if (b4)
                num |= 16;
            if (b5)
                num |= 32;
            if (b6)
                num |= 64;
            if (b7)
                num |= 128;
            return num;
        }

        public static void GetBoolsFromBitfield(
            byte bitField,
            out bool out0,
            out bool out1,
            out bool out2,
            out bool out3,
            out bool out4,
            out bool out5,
            out bool out6,
            out bool out7)
        {
            out0 = (bitField & 1) != 0;
            out1 = (bitField & 2) != 0;
            out2 = (bitField & 4) != 0;
            out3 = (bitField & 8) != 0;
            out4 = (bitField & 16) != 0;
            out5 = (bitField & 32) != 0;
            out6 = (bitField & 64) != 0;
            out7 = (bitField & 128) != 0;
        }

        public static void GetBoolsFromBitfield(
            byte bitField,
            out bool out0,
            out bool out1,
            out bool out2,
            out bool out3,
            out bool out4,
            out bool out5,
            out bool out6)
        {
            out0 = (bitField & 1) != 0;
            out1 = (bitField & 2) != 0;
            out2 = (bitField & 4) != 0;
            out3 = (bitField & 8) != 0;
            out4 = (bitField & 16) != 0;
            out5 = (bitField & 32) != 0;
            out6 = (bitField & 64) != 0;
        }

        public static void GetBoolsFromBitfield(
            byte bitField,
            out bool out0,
            out bool out1,
            out bool out2,
            out bool out3,
            out bool out4,
            out bool out5)
        {
            out0 = (bitField & 1) != 0;
            out1 = (bitField & 2) != 0;
            out2 = (bitField & 4) != 0;
            out3 = (bitField & 8) != 0;
            out4 = (bitField & 16) != 0;
            out5 = (bitField & 32) != 0;
        }

        public static void GetBoolsFromBitfield(
            byte bitField,
            out bool out0,
            out bool out1,
            out bool out2,
            out bool out3,
            out bool out4)
        {
            out0 = (bitField & 1) != 0;
            out1 = (bitField & 2) != 0;
            out2 = (bitField & 4) != 0;
            out3 = (bitField & 8) != 0;
            out4 = (bitField & 16) != 0;
        }

        public static void GetBoolsFromBitfield(
            byte bitField,
            out bool out0,
            out bool out1,
            out bool out2,
            out bool out3)
        {
            out0 = (bitField & 1) != 0;
            out1 = (bitField & 2) != 0;
            out2 = (bitField & 4) != 0;
            out3 = (bitField & 8) != 0;
        }

        public static void GetBoolsFromBitfield(
            byte bitField,
            out bool out0,
            out bool out1,
            out bool out2)
        {
            out0 = (bitField & 1) != 0;
            out1 = (bitField & 2) != 0;
            out2 = (bitField & 4) != 0;
        }

        public static void GetBoolsFromBitfield(byte bitField, out bool out0, out bool out1)
        {
            out0 = (bitField & 1) != 0;
            out1 = (bitField & 2) != 0;
        }

        public static void GetBoolsFromBitfield(byte bitField, out bool out0)
        {
            out0 = (bitField & 1) != 0;
        }
    }
}
