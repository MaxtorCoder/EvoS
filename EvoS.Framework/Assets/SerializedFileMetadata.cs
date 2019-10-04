using System;

namespace EvoS.Framework.Assets
{
    public class SerializedFileMetadata
    {
        public TypeTree TypeTree { get; set; }
        public ObjectInfoTable ObjectInfoTable { get; set; }
        public ObjectIdTable ObjectIdTable { get; set; }
        public ExternalReferencesTable ExternalReferencesTable { get; set; }

        public SerializedFileMetadata(StreamReader stream)
        {
            TypeTree = new TypeTree(stream);
            ObjectInfoTable = new ObjectInfoTable(stream);
            stream.AlignTo();
            ObjectIdTable = new ObjectIdTable(stream);
            stream.AlignTo();
            ExternalReferencesTable = new ExternalReferencesTable(stream);
            stream.AlignTo();
        }

        public override string ToString()
        {
            return $"{nameof(SerializedFileMetadata)}(" +
                   $"{nameof(TypeTree)}: {TypeTree}, " +
                   $"{nameof(ObjectInfoTable)}: {ObjectInfoTable}, " +
                   $"{nameof(ObjectIdTable)}: {ObjectIdTable}, " +
                   $"{nameof(ExternalReferencesTable)}: {ExternalReferencesTable}" +
                   ")";
        }
    }
}
