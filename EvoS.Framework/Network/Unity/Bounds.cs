using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace EvoS.Framework.Network.Unity
{
    public struct Bounds
    {
        private Vector3 m_Center;
        private Vector3 m_Extents;

        public Bounds(Vector3 center, Vector3 size)
        {
            m_Center = center;
            m_Extents = size * 0.5f;
        }

        public bool Contains(Vector3 point)
        {
            throw new NotImplementedException();
        }

        public float SqrDistance(Vector3 point)
        {
            throw new NotImplementedException();
        }

//        public bool IntersectRay(Ray ray)
//        {
//            throw new NotImplementedException();
//        }
//
//        public bool IntersectRay(Ray ray, out float distance)
//        {
//            throw new NotImplementedException();
//        }

        public Vector3 ClosestPoint(Vector3 point)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            return center.GetHashCode() ^ extents.GetHashCode() << 2;
        }

        public override bool Equals(object other)
        {
            if (!(other is Bounds))
                return false;
            Bounds bounds = (Bounds) other;
            return center.Equals(bounds.center) && extents.Equals(bounds.extents);
        }

        public Vector3 center
        {
            get => m_Center;
            set => m_Center = value;
        }

        public Vector3 size
        {
            get => m_Extents * 2f;
            set => m_Extents = value * 0.5f;
        }

        public Vector3 extents
        {
            get => m_Extents;
            set => m_Extents = value;
        }

        public Vector3 min
        {
            get => center - extents;
            set => SetMinMax(value, max);
        }

        public Vector3 max
        {
            get => center + extents;
            set => SetMinMax(min, value);
        }

        public static bool operator ==(Bounds lhs, Bounds rhs)
        {
            return lhs.center == rhs.center && lhs.extents == rhs.extents;
        }

        public static bool operator !=(Bounds lhs, Bounds rhs)
        {
            return !(lhs == rhs);
        }

        public void SetMinMax(Vector3 min, Vector3 max)
        {
            extents = (max - min) * 0.5f;
            center = min + extents;
        }

        public void Encapsulate(Vector3 point)
        {
            SetMinMax(Vector3.Min(min, point), Vector3.Max(max, point));
        }

        public void Encapsulate(Bounds bounds)
        {
            Encapsulate(bounds.center - bounds.extents);
            Encapsulate(bounds.center + bounds.extents);
        }

        public void Expand(float amount)
        {
            amount *= 0.5f;
            extents += new Vector3(amount, amount, amount);
        }

        public void Expand(Vector3 amount)
        {
            extents += amount * 0.5f;
        }

        public bool Intersects(Bounds bounds)
        {
            return min.X <= (double) bounds.max.X && max.X >= (double) bounds.min.X &&
                   (min.Y <= (double) bounds.max.Y && max.Y >= (double) bounds.min.Y) &&
                   min.Z <= (double) bounds.max.Z && max.Z >= (double) bounds.min.Z;
        }

        public override string ToString()
        {
            return $"Center: {m_Center}, Extents: {m_Extents}";
        }

        public string ToString(string format)
        {
            return $"Center: {m_Center.ToString(format)}, Extents: {m_Extents.ToString(format)}";
        }
    }
}
