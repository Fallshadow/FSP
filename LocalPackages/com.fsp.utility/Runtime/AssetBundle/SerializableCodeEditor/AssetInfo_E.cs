#if UNITY_EDITOR

namespace fsp.assetbundlecore
{
    [System.Serializable]
    public class AssetInfo_E
    {
        public string assetPath;
        public int hash;
        public int ownerBundleHash;

        public bool bRootAsset;

        public AssetInfo GetRuntime()
        {
            AssetInfo assetInfo = new AssetInfo();
            assetInfo.hash = hash;
            assetInfo.ownerBundleHash = ownerBundleHash;
            assetInfo.bRootAsset = bRootAsset;

            return assetInfo;
        }
    }
}

#endif