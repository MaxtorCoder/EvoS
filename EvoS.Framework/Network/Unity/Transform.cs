using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using Newtonsoft.Json;

namespace EvoS.Framework.Network.Unity
{
    public class Transform : Component, IEnumerable<Transform>
    {
        [JsonIgnore] public Vector3 position => localPosition; // TODO
        public Vector3 localPosition = Vector3.Zero;
        [JsonIgnore] public Quaternion rotation => localRotation; // TODO
        [JsonIgnore] public Quaternion localRotation { get; set; } = Quaternion.Identity;
        [JsonIgnore] public Vector3 localScale { get; set; } = Vector3.One;
        [JsonIgnore] public SerializedVector<Transform> children { get; } = new SerializedVector<Transform>();
        [JsonIgnore] public Transform father { get; set; }
        [JsonIgnore] public int childCount => children.Count;

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            stream.AlignTo();
            var serializedGameObject = (SerializedGameObject) new SerializedComponent(assetFile, stream).LoadValue();
            
            gameObject = serializedGameObject.Instantiate();
            localRotation = stream.ReadQuaternion();
            localPosition = stream.ReadVector3();
            localScale = stream.ReadVector3();
            children.DeserializeAsset(assetFile, stream);
            father = (Transform) new SerializedComponent(assetFile, stream).LoadValue();
        }

        public override string ToString()
        {
            return $"{nameof(Transform)}(" +
//                   $"{nameof(serializedGameObject)}: {serializedGameObject}, " +
                   $"{nameof(localPosition)}: {localPosition}, " +
                   $"{nameof(localRotation)}: {localRotation}, " +
                   $"{nameof(localScale)}: {localScale}, " +
                   $"{nameof(children)}: {children?.Count} entries, " +
                   $"{nameof(father)}: {(father != null ? "Exists" : "null")}" +
                   ")";
        }

        public IEnumerator<Transform> GetEnumerator() => children.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => children.GetEnumerator();
    }
}
