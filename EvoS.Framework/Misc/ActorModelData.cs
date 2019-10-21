using System.Net.Mime;
using System.Numerics;
using EvoS.Framework.Logging;
using EvoS.Framework.Network.NetworkBehaviours;

namespace EvoS.Framework.Misc
{
    public class ActorModelData
    {
        public enum RagdollActivation
        {
            None,
            HealthBased
        }

        public class ImpulseInfo
        {
            private Vector3 m_position;

            public ImpulseInfo(float explosionRadius, Vector3 explosionCenter)
            {
                ExplosionRadius = explosionRadius;
                m_position = explosionCenter;
                ExplosionMagnitude = 1; // TheatricsManager.GetRagdollImpactForce();
            }

            public ImpulseInfo(Vector3 hitPosition, Vector3 hitDirection)
            {
                hitDirection.Y = Mathf.Max(0.75f, hitDirection.Y);
                float num = 1; //TheatricsManager.GetRagdollImpactForce();
                if ((double) hitDirection.LengthSquared() > 0.0)
                {
                    Vector3.Normalize(hitDirection);
                }
                else
                {
//                    if (MediaTypeNames.Application.isEditor)
//                        Log.Warning("Ragdoll impulse has 0 vector as impulse direction");
//                    hitDirection = Vector3.up;
                    num = 0.1f;
                }

                m_position = hitPosition;
                HitImpulse = hitDirection * num;
            }

            public string GetDebugString()
            {
                if (IsExplosion)
                    return $"Explosion Type Impulse, Radius= {ExplosionRadius} | " +
                           $"Magnitude = {ExplosionMagnitude} | " +
                           $"Center= {ExplosionCenter}";
                return $"Impulse FromPos= {HitPosition} | " +
                       $"Magnitude= {HitImpulse.Length()} | " +
                       $"ImpulseDir= {Vector3.Normalize(HitImpulse)}";
            }

            public bool IsExplosion => ExplosionRadius > 0.0;
            public float ExplosionRadius { get; set; }
            public Vector3 ExplosionCenter => m_position;
            public float ExplosionMagnitude { get; set; }
            public Vector3 HitPosition => m_position;
            public Vector3 HitImpulse { get; set; }
        }

        public enum ActionAnimationType
        {
            None,
            Ability1,
            Ability2,
            Ability3,
            Ability4,
            Ability5,
            Ability6,
            Ability7,
            Ability8,
            Ability9,
            Ability10,
            Item1,
            Item2,
            Item3,
            Item4,
            Item5,
            Item6,
            Item7,
            Item8,
            Item9,
            Item10
        }
    }
}
