using System;
using System.Numerics;

namespace EvoS.Framework.Misc
{
    public static class VectorUtils
    {
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
