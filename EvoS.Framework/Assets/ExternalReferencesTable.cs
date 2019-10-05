using System;
using System.Collections.Generic;

namespace EvoS.Framework.Assets
{
    public class ExternalReferencesTable : List<ExternalReference>
    {
        public ExternalReferencesTable(StreamReader stream)
        {
            var entryCount = stream.ReadInt32();
            for (var i = 0; i < entryCount; i++)
            {
                Add(new ExternalReference(stream));
            }
        }

        public override string ToString()
        {
            return $"{nameof(ExternalReferencesTable)}(" +
                   $"{String.Join(", ", this)}" +
                   ")";
        }
    }

    public class ExternalReference
    {
        public string AssetPath { get; set; }
        public byte[] Guid { get; set; }
        public int ReferenceType { get; set; }
        public string FileName { get; set; }

        public ExternalReference(StreamReader stream)
        {
            AssetPath = stream.ReadNullString();
            Guid = stream.ReadBytes(16);
            ReferenceType = stream.ReadInt32();
            FileName = stream.ReadNullString();
        }

        public override string ToString()
        {
            return $"{nameof(ExternalReferencesTable)}(" +
                   $"{nameof(AssetPath)}: {AssetPath}, " +
                   $"{nameof(Guid)}: {Guid}, " +
                   $"{nameof(ReferenceType)}: {ReferenceType}, " +
                   $"{nameof(FileName)}: {FileName}" +
                   ")";
        }
    }
}
