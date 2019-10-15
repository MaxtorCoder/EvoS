using System;
using System.Numerics;

namespace EvoS.Framework.Misc
{
    public static class VectorUtils
    {
        public static float HorizontalAngle_Rad(Vector3 vec)
        {
            var vector2 = new Vector2(vec.X, vec.Z);
            Vector2.Normalize(vector2);
            return Mathf.Atan2(vector2.Y, vector2.X);
        }

        public static float HorizontalAngle_Deg(Vector3 vec)
        {
            var num = HorizontalAngle_Rad(vec) * 57.29578f;
            if (num < 0.0)
                num += 360f;
            return num;
        }
        
        public static Vector3 AngleRadToVector(float angle)
        {
            return new Vector3(Mathf.Cos(angle), 0.0f, Mathf.Sin(angle));
        }

        public static Vector3 AngleDegreesToVector(float angle)
        {
            return AngleRadToVector(angle * ((float) Math.PI / 180f));
        }
    }
}
