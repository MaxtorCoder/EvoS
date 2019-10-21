using EvoS.Framework.Logging;
using EvoS.Framework.Misc;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.Static
{
    public static class MovementUtils
    {
        public static BoardSquarePathInfo DeSerializePath(Component context, NetworkReader reader)
        {
            var boardSquarePathInfo1 = new BoardSquarePathInfo();
            var boardSquarePathInfo2 = boardSquarePathInfo1;
            BoardSquarePathInfo boardSquarePathInfo3 = null;
            float num1 = 0.0f;
            sbyte num2 = 0;
            sbyte num3 = 0;
            float num4 = 0.0f;
            float num5 = 0.0f;
            float num6 = 0.0f;
            bool out0_1 = false;
            bool out1_1 = false;
            bool flag;
            if (flag = reader.ReadBoolean())
            {
                num4 = reader.ReadSingle();
                num5 = reader.ReadSingle();
                num6 = reader.ReadSingle();
            }

            bool out2 = !flag;
            bool out3 = false;
            bool out4 = false;
            bool out5 = false;
            bool out0_2 = false;
            bool out1_2 = false;
            while (!out2)
            {
                byte num7 = reader.ReadByte();
                byte num8 = reader.ReadByte();
                sbyte num9 = reader.ReadSByte();
                int num10;
                switch (num9)
                {
                    case 0:
                    case 3:
                        num10 = 1;
                        break;
                    default:
                        num10 = num9 == (sbyte) 1 ? 1 : 0;
                        break;
                }

                if (num10 == 0)
                {
                    num2 = reader.ReadSByte();
                    num3 = reader.ReadSByte();
                }

                var bitField1 = reader.ReadByte();
                var bitField2 = reader.ReadByte();
                var out6 = false;
                var out7 = false;
                ServerClientUtils.GetBoolsFromBitfield(bitField1, out out0_1, out out1_1, out out2, out out3, out out4,
                    out out5, out out6, out out7);
                ServerClientUtils.GetBoolsFromBitfield(bitField2, out out0_2, out out1_2);
                var num11 = num4;
                var num12 = num5;
                if (out6)
                    num11 = reader.ReadSingle();
                if (out7)
                    num12 = reader.ReadSingle();
                var boardSquare = context.Board.method_10(num7, num8);
                if (boardSquare == null)
                    Log.Print(LogType.Error,
                        "Failed to find square from index [" + num7 + ", " + num8 + "] during serialization of path");
                boardSquarePathInfo2.square = boardSquare;
                boardSquarePathInfo2.moveCost = num1;
                boardSquarePathInfo2.heuristicCost = 0.0f;
                boardSquarePathInfo2.connectionType = (BoardSquarePathInfo.ConnectionType) num9;
                boardSquarePathInfo2.chargeCycleType = (BoardSquarePathInfo.ChargeCycleType) num2;
                boardSquarePathInfo2.chargeEndType = (BoardSquarePathInfo.ChargeEndType) num3;
                boardSquarePathInfo2.segmentMovementSpeed = num11;
                boardSquarePathInfo2.segmentMovementDuration = num12;
                boardSquarePathInfo2.m_reverse = out0_1;
                boardSquarePathInfo2.m_unskippable = out1_1;
                boardSquarePathInfo2.m_visibleToEnemies = out3;
                boardSquarePathInfo2.m_updateLastKnownPos = out4;
                boardSquarePathInfo2.m_moverDiesHere = out5;
                boardSquarePathInfo2.m_moverClashesHere = out0_2;
                boardSquarePathInfo2.m_moverBumpedFromClash = out1_2;
                boardSquarePathInfo2.prev = boardSquarePathInfo3;
                if (boardSquarePathInfo3 != null)
                    boardSquarePathInfo3.next = boardSquarePathInfo2;
                if (!out2)
                {
                    boardSquarePathInfo3 = boardSquarePathInfo2;
                    boardSquarePathInfo2 = new BoardSquarePathInfo();
                }
            }

            boardSquarePathInfo1.moveCost = num6;
            boardSquarePathInfo1.CalcAndSetMoveCostToEnd(context);
            return boardSquarePathInfo1;
        }

        public static BoardSquarePathInfo DeSerializePath(Component context, byte[] data)
        {
            if (data == null)
                return null;
            return DeSerializePath(context, new NetworkReader(data));
        }
    }
}
