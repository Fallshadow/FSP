using fsp.assetbundlecore;

namespace fsp.modelshot
{
    public class FastModeLoaderManager_ModelShot : FastModeLoaderManager
    {
        protected override string getDepTreeInfoPath()
        {
            return AssetBundleUtility.GetPackageDepTreeInfoFolderPath();
        }
    }
}