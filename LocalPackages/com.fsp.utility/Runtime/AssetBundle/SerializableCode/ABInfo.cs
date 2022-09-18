using System.Collections.Generic;
using System.IO;
using fsp.utility;

namespace fsp.assetbundlecore
{
    [System.Serializable]
    public class ABInfo
    {
        public string abName;
        public int hash;
        public string ownerFileName;
        public ulong offset;
        public List<int> abAssets;
        public List<int> rootAssets;
        public List<int> depABs;
            
        public void Serialize(BinaryWriter writer)
        {
            Utility.WriteStringByUTF8Byte(writer, abName);
            writer.Write(hash);
            Utility.WriteStringByUTF8Byte(writer, ownerFileName);
            writer.Write(offset);

            Utility.WriteListByte(writer, abAssets);
            Utility.WriteListByte(writer, rootAssets);
            Utility.WriteListByte(writer, depABs);
        }
            
        public void DeSerialize(BinaryReader reader)
        {
            abName = Utility.ReadStringFromUTF8Byte(reader);
            hash = reader.ReadInt32();
            ownerFileName = Utility.ReadStringFromUTF8Byte(reader);
            offset = reader.ReadUInt64();

            abAssets = Utility.ReadListFromByte(reader);
            rootAssets = Utility.ReadListFromByte(reader);
            depABs = Utility.ReadListFromByte(reader);

        }
    }
}