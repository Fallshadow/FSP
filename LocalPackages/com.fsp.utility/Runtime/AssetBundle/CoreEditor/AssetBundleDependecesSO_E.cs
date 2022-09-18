#if UNITY_EDITOR

using System.Collections.Generic;

namespace fsp.assetbundlecore
{
    public class AssetBundleDependecesSO_E
    {
        public List<ABInfo_E> abInfos = new List<ABInfo_E>();
        public List<AssetInfo_E> assetInfos = new List<AssetInfo_E>();

        public AssetBundleDependecesSO GetRuntime()
        {
            var so = new AssetBundleDependecesSO();
            so.abInfos = new List<ABInfo>(abInfos.Count);
            so.assetInfos = new List<AssetInfo>(assetInfos.Count);

            foreach (var abInfo in abInfos)
            {
                so.abInfos.Add(abInfo.GetRunTime());
            }

            foreach (var assetInfo in assetInfos)
            {
                so.assetInfos.Add(assetInfo.GetRuntime());
            }

            return so;
        }
    }
}

#endif