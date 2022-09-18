using System.IO;

namespace fsp.assetbundlecore
{
    [System.Serializable]
    public class AssetInfo
    {
        public int hash;
        public int ownerBundleHash;
        public bool bRootAsset;

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(hash);
            writer.Write(ownerBundleHash);
            writer.Write(bRootAsset);
        }

        public void DeSerialize(BinaryReader reader)
        {
            hash = reader.ReadInt32();
            ownerBundleHash = reader.ReadInt32();
            bRootAsset = reader.ReadBoolean();
        }
    }
}