using System.Numerics;

namespace EvoS.GameServer.Network.Unity
{
    public class Transform : Component
    {
        public Vector3 position = Vector3.Zero;
        public Vector3 localPosition = Vector3.Zero;
        public Quaternion rotation = Quaternion.Identity;
        public Quaternion localRotation = Quaternion.Identity;
        public Vector3 localScale = Vector3.One;

        public Transform()
        {
        }
    }
}
