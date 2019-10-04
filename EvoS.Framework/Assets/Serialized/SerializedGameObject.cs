using System.Collections.Generic;
using System.Linq;

namespace EvoS.Framework.Assets.Serialized
{
    public class SerializedGameObject : ISerializedItem
    {
        public SerializedVector<SerializedComponent> Components { get; set; }
        public uint Layer { get; set; }
        public string Name { get; set; }
        public ushort Tag { get; set; }
        public bool IsActive { get; set; }

        public void Deserialize(AssetFile assetFile, StreamReader stream)
        {
            Components = new SerializedVector<SerializedComponent>();
            Components.Deserialize(assetFile, stream);
            Layer = stream.ReadUInt32();
            Name = stream.ReadString32();
            Tag = stream.ReadUInt16();
            IsActive = stream.ReadBoolean();
        }

        public IEnumerable<SerializedMonoChildBase> ComponentChildren()
        {
            return from component in Components
                select (SerializedMonoBehaviour) component.LoadValue()
                into compValue
                where compValue?.Child != null
                select compValue.Child;
        }

        public List<string> ComponentNames()
        {
            return (from component in Components
                select (SerializedMonoBehaviour) component.LoadValue()
                into value
                where value?.Script != null
                select value.Script.ClassName).ToList();
        }

        public T GetComponent<T>() where T : SerializedMonoChildBase
        {
            foreach (var component in ComponentChildren())
            {
                if (component is T childBase)
                {
                    return childBase;
                }
            }

            return null;
        }

        public override string ToString()
        {
            return $"{nameof(SerializedGameObject)}(" +
                   $"{nameof(Name)}: {Name}, " +
//                   $"{nameof(Components)}: {Components}, " +
                   $"{nameof(Components)}: {Components.Count} components, " +
//                   $"{nameof(Layer)}: {Layer}, " +
//                   $"{nameof(Tag)}: {Tag}, " +
//                   $"{nameof(IsActive)}: {IsActive}" +
                   ")";
        }
    }
}
