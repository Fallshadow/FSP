using UnityEditor;

namespace fsp.assetbundleeditor
{
    public class AssetbundleEditor_SerliazeMenus
    {
        [MenuItem("FSP/2: 刷新ScriptableObject资源", false, 3)]
        private static void SerializeAssetDepenceInfoForFastMode_ScriptableObject()
        {
            EditorUtility.DisplayProgressBar("弹窗", "正在快速生成依赖信息...SO", 0.25f);
            AssetBundlePackageManager.SerializeAssetDepenceInfo_ForFastMode_UnderEditorPackage(new AssetBundleGrouper_ScriptableObject());
            EditorUtility.DisplayProgressBar("弹窗", "依赖信息生成完毕！", 1f);
            EditorUtility.ClearProgressBar();
        }
    }
}