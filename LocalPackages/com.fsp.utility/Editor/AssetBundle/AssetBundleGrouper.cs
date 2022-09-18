using System.Collections.Generic;
using System.IO;
using fsp.assetbundlecore;
using fsp.data;
using fsp.utility;
using UnityEditor;
using UnityEngine;

namespace fsp.assetbundleeditor
{
    // 整理资源组，写到Json里
    // 实际项目里不同平台可能对应的资源也不一样，这边先简单些后续再统一完善处理
    // 注意这边有些Editor下的资源在开发游戏时也是会用到的，需要与实际游戏的资源分开处理
    public class AssetBundleGrouper
    {
        protected BuildTarget buildTarget;
        
        public virtual void Init() { }
        
        public virtual List<AssetGroup> WorkForEditor()
        {
#if UNITY_ANDROID
            return Work(BuildTarget.Android);
#elif UNITY_IPHONE
            return Work(BuildTarget.iOS);
#elif UNITY_STANDALONE_WIN
            return Work(BuildTarget.StandaloneWindows64);
#else
            return Work();
#endif
        }
        
        public virtual List<AssetGroup> Work(BuildTarget buildTargetP = BuildTarget.Android)
        {
            buildTarget = buildTargetP;
            return null;
        }
        
        public static string GetTargetPlatformFolder(BuildTarget buildTarget)
        {
            switch (buildTarget)
            {
                case BuildTarget.StandaloneWindows64:
                    return ResourcesPathSetting.RESOURCES_PC_FOLDER;
                case BuildTarget.Android:
                    return ResourcesPathSetting.RESOURCES_ANDROID_FOLDER;
                case BuildTarget.iOS:
                    return ResourcesPathSetting.RESOURCES_IOS_FOLDER;
                default:
                    return ResourcesPathSetting.RESOURCES_FOLDER;
            }
        }

        public static string GetAssetPathUnderAssets(string fullPath)
        {
            return fullPath.Remove(0, Utility.LocalDataAssetsPathLength);
        }

        // LocalPackage 要映射为 package
        public static string GetAssetPathUnderUnknowPackages(string fullPath)
        {
            fullPath = fullPath.Replace("LocalPackages", "Packages");
            return fullPath.Remove(0, Utility.LocalDataAssetsPathLength);
        }
    }
}