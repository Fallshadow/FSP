using fsp.data;
using UnityEngine;
using fsp.utility;

namespace fsp.assetbundlecore
{
    public static class AssetBundleUtility
    {
#if  UNITY_EDITOR
        public static string GetPackageDepTreeInfoJsonName(string ext)
        {
            string folder = Utility.CreateDirectoryFolderPathAndReturn(Application.dataPath + ResourcesPathSetting.ABDepInfo_DT_Package_Application_Path_Suffix);
            return $"{folder}/{ext}.json";
        }

        public static string GetDepTreeInfoJsonName(string ext)
        {
            string folder = Utility.CreateDirectoryFolderPathAndReturn(Application.dataPath + ResourcesPathSetting.ABDepInfo_DT_Application_Path_Suffix);
            return $"{folder}/{ext}.json";
        }
        
        public static string GetPackageDepTreeInfoFolderPath()
        {
            return Utility.CreateDirectoryFolderPathAndReturn(Application.dataPath + ResourcesPathSetting.ABDepInfo_DT_Package_Application_Path_Suffix);
        }
        
        public static string GetDepTreeInfoFolderPath()
        {
            return Utility.CreateDirectoryFolderPathAndReturn(Application.dataPath + ResourcesPathSetting.ABDepInfo_DT_Application_Path_Suffix);
        }
#endif
    }
}