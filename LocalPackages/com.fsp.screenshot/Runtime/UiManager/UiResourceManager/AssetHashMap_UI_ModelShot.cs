using System;
using System.IO;
using fsp.modelshot.data;
using fsp.utility;
using UnityEngine;

namespace fsp.modelshot.ui
{
    [CreateAssetMenu]
    public class AssetHashMap_UI_ModelShot : fsp.ui.AssetHashMap_UI
    {
        protected override string resFolder => ResourcesPathSetting.APPLICATIONPATH_UI_PACKAGE;

        public override void RefreshBaseInfo()
        {
            if (resFolder == null) return;
            CreatePackageAssetInfo(resFolder);
        }

        protected void CreatePackageAssetInfo(string folder)
        {
            string uiFolderPath = Path.Combine(Application.dataPath, folder);
            
            if (Directory.Exists(uiFolderPath))
            {
                DirectoryInfo[] directories = new DirectoryInfo(uiFolderPath).GetDirectories();
                foreach (DirectoryInfo dir in directories)
                {
                    var tmpPath = dir.FullName + ResourcesPathSetting.UI_PREFABS_FOLDER;
                    if (!Directory.Exists(tmpPath)) continue;
                    AddAssetInfo(tmpPath, fsp.ui.UiAssetType.UAT_PREFAB);
                }
            }
        }

        protected override bool trygetAssetInfoIndexByName(string assetInfoName, out int index)
        {
            index = -1;
            bool suc = Enum.TryParse(assetInfoName, false, out UiAssetIndex res);
            if (!suc)
            {
                Debug.LogError($"在这个枚举里找不到 {assetInfoName}");
                return false;
            }
            index = (int) res;
            return true;
        }

        protected override int initInfoHashCode(string fullname)
        {
            return Utility.GetHashCodeUnderUnknowPackagesPath(fullname);
        }
    }
}