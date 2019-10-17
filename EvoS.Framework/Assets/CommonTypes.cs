namespace EvoS.Framework.Assets
{
    public enum CommonTypeIds
    {
        GameObject = 1,
        Component = 2,
        Transform = 4,
        Behaviour = 8,
        BoxCollider = 65,
        MonoBehaviour = 114,
        MonoScript = 115,
    }
    
    public class CommonTypes
    {
        public const string NetworkIdentity = "UnityEngine.Networking.NetworkIdentity";
        public const string BoardSquare = ".BoardSquare";
        public const string SinglePlayerCoordinator = ".SinglePlayerCoordinator";
    }
}
