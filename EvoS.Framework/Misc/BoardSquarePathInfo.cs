using System;
using EvoS.Framework.Logging;
using EvoS.Framework.Network.NetworkBehaviours;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    public class BoardSquarePathInfo : IComparable
    {
        public ChargeEndType chargeEndType = ChargeEndType.None;
        public BoardSquare square;
        public float moveCost;
        public float heuristicCost;
        public BoardSquarePathInfo prev;
        public BoardSquarePathInfo next;
        public bool m_unskippable;
        public bool m_reverse;
        public bool m_visibleToEnemies;
        public bool m_updateLastKnownPos;
        public bool m_moverDiesHere;
        public bool m_moverHasGameplayHitHere;
        public bool m_moverClashesHere;
        public bool m_moverBumpedFromClash;
        public int m_expectedBackupNum;
        public ConnectionType connectionType;
        public ChargeCycleType chargeCycleType;
        public float segmentMovementSpeed;
        public float segmentMovementDuration;

        public float F_cost
        {
            get
            {
                var num = Mathf.Max(m_expectedBackupNum - 1, 0);
                if (num > 0)
                    return moveCost + heuristicCost + (float) (1.5 * num + 0.100000001490116);
                return moveCost + heuristicCost;
            }
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            if (!(obj is BoardSquarePathInfo boardSquarePathInfo))
                throw new ArgumentException("Object is not a BoardSquarePathInfo");
            return FindMoveCostToEnd().CompareTo(boardSquarePathInfo.FindMoveCostToEnd());
        }

        public void ResetValuesToDefault()
        {
            square = null;
            moveCost = 0.0f;
            heuristicCost = 0.0f;
            prev = null;
            next = null;
            m_unskippable = false;
            m_reverse = false;
            m_visibleToEnemies = false;
            m_updateLastKnownPos = false;
            m_moverDiesHere = false;
            m_moverHasGameplayHitHere = false;
            m_moverClashesHere = false;
            m_moverBumpedFromClash = false;
            m_expectedBackupNum = 0;
        }

        public bool IsSamePathAs(BoardSquarePathInfo other)
        {
            if (other == null)
                return false;
            bool flag1 = true;
            bool flag2;
            if (flag2 =
                (flag1 =
                    (flag1 =
                        (flag1 =
                            (flag1 =
                                (flag1 =
                                    (flag1 =
                                        (flag1 =
                                            (flag1 = (flag1 = (flag1 = (1 & (other.square == square ? 1 : 0)) != 0) &
                                                              other.moveCost == (double) moveCost) &
                                                     other.heuristicCost == (double) heuristicCost) &
                                            other.m_unskippable == m_unskippable) &
                                        (other.m_moverClashesHere = m_moverClashesHere)) &
                                    (other.m_moverBumpedFromClash = m_moverBumpedFromClash)) &
                                other.m_reverse == m_reverse) & other.chargeEndType == chargeEndType) &
                        other.chargeCycleType == chargeCycleType) &
                    other.segmentMovementSpeed == (double) segmentMovementSpeed) &
                other.segmentMovementDuration == (double) segmentMovementDuration)
            {
                if (next != null && other.next != null)
                    flag2 = next.IsSamePathAs(other.next);
                else if (next == null && other.next != null)
                    flag2 = false;
                else if (next != null && other.next == null)
                    flag2 = false;
            }

            return flag2;
        }

        public BoardSquarePathInfo Clone(BoardSquarePathInfo previous)
        {
            BoardSquarePathInfo previous1 = new BoardSquarePathInfo();
            previous1.square = square;
            previous1.moveCost = moveCost;
            previous1.heuristicCost = heuristicCost;
            previous1.m_unskippable = m_unskippable;
            previous1.m_reverse = m_reverse;
            previous1.chargeEndType = chargeEndType;
            previous1.chargeCycleType = chargeCycleType;
            previous1.segmentMovementSpeed = segmentMovementSpeed;
            previous1.segmentMovementDuration = segmentMovementDuration;
            previous1.connectionType = connectionType;
            previous1.m_moverDiesHere = m_moverDiesHere;
            previous1.m_moverHasGameplayHitHere = m_moverHasGameplayHitHere;
            previous1.m_updateLastKnownPos = m_updateLastKnownPos;
            previous1.m_visibleToEnemies = m_visibleToEnemies;
            previous1.m_moverClashesHere = m_moverClashesHere;
            previous1.m_moverBumpedFromClash = m_moverBumpedFromClash;
            if (previous != null)
                previous1.prev = previous;
            if (next != null)
                previous1.next = next.Clone(previous1);
            return previous1;
        }

//        public void CalcAndSetMoveCostToEnd()
//        {
//            float moveCost = this.moveCost;
//            for (BoardSquarePathInfo next = this.next; next != null; next = next.next)
//            {
//                bool flag1 = Board.smethod_0().method_19(next.square, next.prev.square);
//                bool flag2 = Board.smethod_0().method_18(next.square, next.prev.square);
//                bool flag3 = next.square == next.prev.square;
//                if (flag1)
//                    moveCost += 1.5f;
//                else if (flag2)
//                    ++moveCost;
//                else if (flag3)
//                {
//                    if (next.next != null)
//                        Log.Print(LogType.Warning,
//                            "Calculating move costs on a path, but it has the same square twice in a row.");
//                }
//                else if (next.connectionType != ConnectionType.Run && next.connectionType != ConnectionType.Vault)
//                    moveCost += next.square.HorizontalDistanceOnBoardTo(next.prev.square);
//                else
//                    Log.Print(LogType.Warning,
//                        "Calculating move costs on a path, but it has two non-adjacent consecutive squares.");
//
//                next.moveCost = moveCost;
//            }
//        }

        public float FindMoveCostToEnd()
        {
            float moveCost = this.moveCost;
            for (BoardSquarePathInfo next = this.next; next != null; next = next.next)
                moveCost = next.moveCost;
            return moveCost - this.moveCost;
        }

        public float FindMoveCostToOneBeforeEnd()
        {
            var moveCost = this.moveCost;
            for (var next = this.next; next != null && next.next != null; next = next.next)
                moveCost = next.moveCost;
            return moveCost - this.moveCost;
        }

        internal float FindDistanceToEnd()
        {
            var num = 0.0f;
            for (var next = this.next; next != null; next = next.next)
                num += (next.square.ToVector3() - square.ToVector3()).Length();
            return num;
        }

        public int GetNumSquaresToEnd(bool checkDuplicate = true)
        {
            var boardSquarePathInfo = this;
            var num = 1;
            for (; boardSquarePathInfo.next != null; boardSquarePathInfo = boardSquarePathInfo.next)
            {
                if (!checkDuplicate || boardSquarePathInfo.next.square != boardSquarePathInfo.square)
                    ++num;
            }

            return num;
        }

        public BoardSquarePathInfo GetPathMidpoint()
        {
            var moveCostToEnd1 = FindMoveCostToEnd();
            var num1 = moveCostToEnd1 / 2f;
            var boardSquarePathInfo = this;
            var num2 = moveCostToEnd1;
            for (var next = this.next; next != null; next = next.next)
            {
                var moveCostToEnd2 = next.FindMoveCostToEnd();
                if (Math.Abs(moveCostToEnd2 - num1) < (double) Math.Abs(num2 - num1))
                {
                    boardSquarePathInfo = next;
                    num2 = moveCostToEnd2;
                }
                else
                    break;
            }

            return boardSquarePathInfo;
        }

        public BoardSquarePathInfo GetPathEndpoint()
        {
            var boardSquarePathInfo = this;
            while (boardSquarePathInfo.next != null)
                boardSquarePathInfo = boardSquarePathInfo.next;
            return boardSquarePathInfo;
        }

        public BoardSquarePathInfo GetPathStartPoint()
        {
            var boardSquarePathInfo = this;
            while (boardSquarePathInfo.prev != null)
                boardSquarePathInfo = boardSquarePathInfo.prev;
            return boardSquarePathInfo;
        }

        public bool IsPathEndpoint()
        {
            return next == null;
        }

        public bool IsPathStartPoint()
        {
            return prev == null;
        }

        public BoardSquarePathInfo BackUpOnceFromEnd()
        {
            var pathEndpoint = GetPathEndpoint();
            if (pathEndpoint == null || pathEndpoint.prev == null)
                return pathEndpoint;
            var prev = pathEndpoint.prev;
            prev.next = null;
            return prev;
        }

//        public List<GridPos> ToGridPosPath()
//        {
//            var gridPosList = new List<GridPos>();
//            for (var boardSquarePathInfo = this;
//                boardSquarePathInfo != null;
//                boardSquarePathInfo = boardSquarePathInfo.next)
//                gridPosList.Add(boardSquarePathInfo.square.method_3());
//            return gridPosList;
//        }

        public bool IsValidPathForMaxMovement(float maxMovement)
        {
            bool flag;
            if (maxMovement <= 0.0)
            {
                flag = next == null;
            }
            else
            {
                var costToOneBeforeEnd = FindMoveCostToOneBeforeEnd();
                flag = maxMovement > (double) costToOneBeforeEnd;
            }

            return flag;
        }

        public static bool IsConnectionTypeConventional(ConnectionType connectionType)
        {
            if (connectionType != ConnectionType.Flight && connectionType != ConnectionType.Teleport &&
                connectionType != ConnectionType.Knockback)
                return connectionType != ConnectionType.Charge;
            return false;
        }

        public string GetDebugPathStringToEnd(string prefix)
        {
            var str1 = prefix;
            var num = 0;
            for (var boardSquarePathInfo = this; boardSquarePathInfo != null && num < 100; ++num)
            {
                var str2 = boardSquarePathInfo.square?.ToString();
                str1 = $"{str1}\n{str2} | Connection Type = {boardSquarePathInfo.connectionType}";
                if (boardSquarePathInfo != boardSquarePathInfo.next)
                    boardSquarePathInfo = boardSquarePathInfo.next;
                else
                    break;
            }

            return str1;
        }

        public string GetDebugPathStringToBeginning(string prefix)
        {
            var str1 = prefix;
            var num = 0;
            for (var boardSquarePathInfo = this; boardSquarePathInfo != null && num < 100; ++num)
            {
                var str2 = boardSquarePathInfo.square?.ToString();
                str1 = $"{str1}\n{str2} | Connection Type = {boardSquarePathInfo.connectionType}";
                if (boardSquarePathInfo != boardSquarePathInfo.prev)
                    boardSquarePathInfo = boardSquarePathInfo.prev;
                else
                    break;
            }

            return str1;
        }

        public bool CheckPathConnectionForSelfReference()
        {
            return true;
        }

        public bool WillDieAtEnd()
        {
            var pathEndpoint = GetPathEndpoint();
            return pathEndpoint != null && pathEndpoint.m_moverDiesHere;
        }

        public void CheckIsValidTriggeringPath(ActorData mover)
        {
            var num1 = 0;
            var num2 = 0;
            var num3 = 0;
            var flag = false;
            for (var boardSquarePathInfo = GetPathStartPoint(); boardSquarePathInfo != null && num2 < 100; ++num2)
            {
                if (square == null)
                    ++num1;
                if (flag)
                    ++num3;
                if (boardSquarePathInfo.m_moverDiesHere)
                    flag = true;
                boardSquarePathInfo = boardSquarePathInfo.next;
            }

            if (num1 <= 0 && num2 < 100 && num3 <= 0)
                return;
            var str1 = num1 + " null squares";
            if (num1 != 0)
                str1 = "INVALID SQUARES: " + str1;
            var str2 = num2 + " total path nodes";
            if (num2 >= 100)
                str2 = "INVALID LENGTH: " + str2;
            var str3 = num3 + " steps after death";
            if (num3 > 0)
                str3 = "INVALID DEATH-MOVEMENT: " + str3;
            Log.Print(LogType.Error,
                $"Invalid BoardSquarePathInfo for gameplay!  Path has:\n\t{str2}\n\t{str1}\n\t{str3}\nMover: {mover.method_95()}");
        }

        public bool IsNodePartOfMyFuturePath(BoardSquarePathInfo other, bool includePresent = true)
        {
            bool flag = false;
            for (BoardSquarePathInfo boardSquarePathInfo = !includePresent ? next : this;
                boardSquarePathInfo != null;
                boardSquarePathInfo = boardSquarePathInfo.next)
            {
                if (other == boardSquarePathInfo)
                {
                    flag = true;
                    break;
                }
            }

            return flag;
        }

        public void ResetClashingOfPath()
        {
            for (BoardSquarePathInfo boardSquarePathInfo = this;
                boardSquarePathInfo != null;
                boardSquarePathInfo = boardSquarePathInfo.next)
            {
                if (boardSquarePathInfo.m_moverClashesHere)
                    boardSquarePathInfo.m_moverClashesHere = false;
                if (boardSquarePathInfo.m_moverBumpedFromClash)
                    boardSquarePathInfo.m_moverBumpedFromClash = false;
            }
        }

        public void CalcAndSetMoveCostToEnd(Component context)
        {
            float moveCost = this.moveCost;
            for (BoardSquarePathInfo next = this.next; next != null; next = next.next)
            {
                bool flag1 = context.Board.method_19(next.square, next.prev.square);
                bool flag2 = context.Board.method_18(next.square, next.prev.square);
                bool flag3 = next.square == next.prev.square;
                if (flag1)
                    moveCost += 1.5f;
                else if (flag2)
                    ++moveCost;
                else if (flag3)
                {
                    if (next.next != null)
                        Log.Print(LogType.Warning, "Calculating move costs on a path, but it has the same square twice in a row.");
                }
                else if (next.connectionType != ConnectionType.Run && next.connectionType != ConnectionType.Vault)
                    moveCost += next.square.HorizontalDistanceOnBoardTo(next.prev.square);
                else
                    Log.Print(LogType.Warning, "Calculating move costs on a path, but it has two non-adjacent consecutive squares.");
                next.moveCost = moveCost;
            }
        }

        public enum ConnectionType
        {
            Run,
            Knockback,
            Charge,
            Vault,
            Flight,
            Teleport,
            NumTypes
        }

        public enum ChargeCycleType
        {
            Movement,
            Recovery,
            None
        }

        public enum ChargeEndType
        {
            Pivot,
            Impact,
            Miss,
            Recovery,
            None
        }
    }
}
