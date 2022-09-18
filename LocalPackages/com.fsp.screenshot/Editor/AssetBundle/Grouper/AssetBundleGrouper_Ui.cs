using System.Collections.Generic;
using System.IO;
using UnityEngine;
using fsp.assetbundleeditor;
using fsp.modelshot.data;

namespace fsp.modelshot.editor
{
    public class AssetBundleGrouper_Ui : AssetBundleGrouper
    {
        private const int DEFAULT_UI_IMPORTANCE = 4;
        private const string GROUPER_NAME = "ModelShotUi";
        
        public override List<AssetGroup> WorkForEditor()
        {
            List<AssetGroup> retGroups = buildGroup();
            return retGroups;
        }
        
        private List<AssetGroup> buildGroup()
        {
            string uiFolderPath = Path.Combine(Application.dataPath, ResourcesPathSetting.APPLICATIONPATH_UI_PACKAGE);
            List<AssetGroup> result = new List<AssetGroup>(64);
            List<string> resFolderPaths = new List<string>(8);
            
            if (Directory.Exists(uiFolderPath))
            {
                DirectoryInfo[] directories = new DirectoryInfo(uiFolderPath).GetDirectories();
                foreach (DirectoryInfo dir in directories)
                {
                    resFolderPaths.Add(dir.FullName + ResourcesPathSetting.UI_PREFABS_FOLDER);
                }
            }

            foreach (string folderPath in resFolderPaths)
            {
                packLogicGroup(folderPath, result);
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
                if (filePath.EndsWith(".prefab") ||
                    filePath.EndsWith(".anim") ||
                    filePath.EndsWith(".mat") ||
                    filePath.EndsWith(".png") ||
                    filePath.EndsWith(".jpg"))
                {

                    AssetItem item = new AssetItem
                    {
                        assetPath = filePath,
                        importance = DEFAULT_UI_IMPORTANCE,
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