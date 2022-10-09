using System.Collections.Generic;
using System.IO;
using fsp.data;
using UnityEngine;

namespace fsp.assetbundleeditor
{
    public class AssetBundleGrouper_ScriptableObject : AssetBundleGrouper
    {
        private const int DEFAULT_SO_IMPORTANCE = 4;
        public const string GROUPER_NAME = "ScriptableObject";

        public override List<AssetGroup> WorkForEditor()
        {
            List<AssetGroup> retGroups = buildGroup();
            return retGroups;
        }
        
        // 打包group资源
        // 文件夹格式根据经验：总文件夹（UI/SO） -> 各个功能文件夹（A界面/B界面/战斗动作相关/镜头相关） -> 各个子分类文件夹
        // 根据实际情况可能存在 要筛选出不应打包的group
        // TODO：日后有时间要把这个抽象出一个类型 然后在基类里定义，让各个子类可以受益
        private List<AssetGroup> buildGroup()
        {
            string folderPath = Path.Combine(Application.dataPath, ResourcesPathSetting.APPLICATIONPATH_SO_PACKAGE);
            
            List<string> resFolderPaths = new List<string>(8);
            
            if (Directory.Exists(folderPath))
            {
                DirectoryInfo[] directories = new DirectoryInfo(folderPath).GetDirectories();
                foreach (DirectoryInfo dir in directories)
                {
                    resFolderPaths.Add(dir.FullName);
                }
            }

            List<AssetGroup> result = new List<AssetGroup>(64);
            foreach (string resFolderPath in resFolderPaths)
            {
                packLogicGroup(resFolderPath, result);
            }
            
            return result;
        }
        
        // 一个功能打成一个包。如家园功能在 application.path + ResourceRex/UI/Logic/Home/Prefab/Main，需要打包资源都放在Main文件夹中
        private void packLogicGroup(string folderPath, List<AssetGroup> result)
        {
            if (!Directory.Exists(folderPath))
            {
                return;
            }
            
            AssetGroup group = new AssetGroup
            {
                GroupName = GROUPER_NAME
            };

            FileInfo[] fileInfos = new DirectoryInfo(folderPath).GetFiles("*.*", SearchOption.AllDirectories);
            foreach (FileInfo fileInfo in fileInfos)
            {
                string filePath = GetAssetPathUnderUnknowPackages(fileInfo.FullName);
                // 能这么判断是在大家严格按照规定路径存放制定资源
                if (filePath.EndsWith(".asset"))
                {
                    AssetItem item = new AssetItem
                    {
                        assetPath = filePath,
                        importance = DEFAULT_SO_IMPORTANCE,
                        subLevelNames = null
                    };
                    group.Assets.Add(item);
                }
            }

            if (group.AssetCount > 0)
            {
                result.Add(group);
            }
        }
    }
}