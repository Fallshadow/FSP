using Debug = UnityEngine.Debug;
using System.Collections.Generic;
using fsp.assetbundlecore;
using fsp.utility;

namespace fsp.assetbundleeditor
{
    public partial class AssetBundlePackageManager
    {

        public static void SerializeAssetDepenceInfo_ForFastMode_UnderEditorGamePlay(AssetBundleGrouper grouper)
        {
            var module = grouper.GetType().Name.Substring("AssetBundleGrouper_".Length);
            var savePath = AssetBundleUtility.GetDepTreeInfoJsonName(module);
            SerializeAssetDepenceInfo_ForFastMode_ByPath(grouper, savePath);
            Debug.Log($"序列化依赖信息完成:{module}");
        }
        
        public static void SerializeAssetDepenceInfo_ForFastMode_UnderEditorPackage(AssetBundleGrouper grouper)
        {
            var module = grouper.GetType().Name.Substring("AssetBundleGrouper_".Length);
            var savePath = AssetBundleUtility.GetPackageDepTreeInfoJsonName(module);
            SerializeAssetDepenceInfo_ForFastMode_ByPath(grouper, savePath);
            Debug.Log($"序列化Package依赖信息完成:{module}");
        }
        
        public static void SerializeAssetDepenceInfo_ForFastMode_ByPath(AssetBundleGrouper grouper, string savePath)
        {
            grouper.Init();
            List<AssetGroup> assetItems = grouper.WorkForEditor();
            
            List<AssetInfo_E> infos = new List<AssetInfo_E>();
            foreach (var group in assetItems)
            {
                foreach (var asset in group.Assets)
                {
                    asset.assetPath = asset.assetPath.Replace('\\', '/');

                    AssetInfo_E infoE = new AssetInfo_E();
                    infoE.hash = Utility.GetHashCodeByAssetPath(asset.assetPath, false);
                    infoE.assetPath = asset.assetPath;
                    infos.Add(infoE);
                }
            }
            
            AssetBundleDependecesSO_E soE = new AssetBundleDependecesSO_E();
            soE.assetInfos = infos;
            Utility.WriteObjectToJson(soE, savePath);
        }
    }
}