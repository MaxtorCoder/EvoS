namespace EvoS.Framework.Network.Unity
{
    public class SyncListStruct<T> : SyncList<T> where T : struct
    {
        public new void AddInternal(T item)
        {
            base.AddInternal(item);
        }

        public override void SerializeItem(NetworkWriter writer, T item)
        {
        }

        public override T DeserializeItem(NetworkReader reader)
        {
            return new T();
        }

        public T GetItem(int i)
        {
            return this[i];
        }

        public ushort Count => (ushort) base.Count;
    }
}
