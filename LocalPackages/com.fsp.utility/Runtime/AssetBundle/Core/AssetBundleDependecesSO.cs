using System.Collections.Generic;
using System.IO;

namespace fsp.assetbundlecore
{
    public class AssetBundleDependecesSO
    {
        public List<ABInfo> abInfos = new List<ABInfo>();
        public List<AssetInfo> assetInfos = new List<AssetInfo>();
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(abInfos.Count);
            foreach (var t in abInfos)
            {
                t.Serialize(writer);
            }

            writer.Write(assetInfos.Count);
            foreach (var t in assetInfos)
            {
                t.Serialize(writer);
            }
        }

        public void DeSerialize(BinaryReader reader)
        {
            var abCount = reader.ReadInt32();
            for (int i = 0; i < abCount; i++)
            {
                var newAB = new ABInfo();
                newAB.DeSerialize(reader);
                abInfos.Add(newAB);
            }

            var assetCount = reader.ReadInt32();
            for (int i = 0; i < assetCount; i++)
            {
                var newAsset = new AssetInfo();
                newAsset.DeSerialize(reader);
                assetInfos.Add(newAsset);
            }
                
        }

    }
}