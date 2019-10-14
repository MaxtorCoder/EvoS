using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    public class GridPos
    {
        public static GridPos Invalid = new GridPos(-1, -1, 0);
        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }
        public float worldX => (float) X; // TODO * Board.SquareSizeStatic;
        public float worldY => (float) Y; // TODO * Board.SquareSizeStatic;

        private GridPos()
        {
        }

        public GridPos(int x, int y, int height)
        {
            X = x;
            Y = y;
            Height = height;
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        public string ToStringWithCross()
        {
            return $"({X} x {Y})";
        }

//        public static GridPos FromVector3(Vector3 vec)
//        {
//            return new GridPos()
//            {
//                // TODO need to pull a Board instance in
//                x = Mathf.RoundToInt(vec.X / Board.\u000E().squareSize),
//                y = Mathf.RoundToInt(vec.Z / Board.\u000E().squareSize),
//            };
//        }

        public static GridPos FromGridPosProp(GridPosProp gpp)
        {
            return new GridPos
            {
                X = gpp.m_x,
                Y = gpp.m_y,
                Height = gpp.m_height
            };
        }

        public bool CoordsEqual(GridPos other)
        {
            if (X == other.X)
                return Y == other.Y;
            return false;
        }

        public void OnSerializeHelper(IBitStream stream)
        {
            var x = X;
            var y = Y;
            var height = Height;
            stream.Serialize(ref x);
            stream.Serialize(ref y);
            stream.Serialize(ref height);
            X = x;
            Y = y;
            Height = height;
        }
    }
}
