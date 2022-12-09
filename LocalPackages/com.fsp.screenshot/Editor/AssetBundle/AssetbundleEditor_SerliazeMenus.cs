using System.IO;
using System.Text.RegularExpressions;
using fsp.assetbundleeditor;
using fsp.modelshot.data;
using fsp.modelshot.ui;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace fsp.modelshot.editor
{
    public class AssetbundleEditor_SerliazeMenus
    {
        [MenuItem("ModelShot/一键配置环境",false,2)]
        public static void 一键配置环境()
        {
            SerializeAssetDepenceInfoForFastMode_UI();
            SerializeAssetDepenceInfoForFastMode_ScriptableObject();
            ModelShotPrepareWork.AddSampleToBuildingSetting();
            ChangeProjectURP();
        }
        
        [MenuItem("ModelShot/1：生成Ui资源",false,2)]
        public static void SerializeAssetDepenceInfoForFastMode_UI()
        {
            EditorUtility.DisplayProgressBar("弹窗", "正在快速生成依赖信息...UI", 0.25f);
            var uiso = AssetDatabase.LoadAssetAtPath<AssetHashMap_UI_ModelShot>(ResourcesPathSetting.ASSETHASHMAP_UI_VIRTUAL_FILE_PATH);
            uiso.Automatic();
            AssetBundlePackageManager.SerializeAssetDepenceInfo_ForFastMode_UnderEditorPackage(new AssetBundleGrouper_Ui());
            EditorUtility.DisplayProgressBar("弹窗", "依赖信息生成完毕！", 1f);
            EditorUtility.ClearProgressBar();
        }
        
        [MenuItem("ModelShot/2：刷新ModelShot_So资源", false, 3)]
        private static void SerializeAssetDepenceInfoForFastMode_ScriptableObject()
        {
            EditorUtility.DisplayProgressBar("弹窗", "正在快速生成依赖信息...SO", 0.25f);
            AssetBundlePackageManager.SerializeAssetDepenceInfo_ForFastMode_UnderEditorPackage(new AssetBundleGrouper_ScriptableObject());
            EditorUtility.DisplayProgressBar("弹窗", "依赖信息生成完毕！", 1f);
            EditorUtility.ClearProgressBar();
        }
        
        private static void ChangeProjectURP()
        {
            string strFilePath = Application.dataPath + "/ResourceRex/settings/UniversalRP-HighQuality.asset";
            if (File.Exists(strFilePath))
            {
                //读取全部数据
                string strContent = File.ReadAllText(strFilePath);
                string[] allLines = Regex.Split(strContent,"\n");
                string strContentNew = "";
                for (int index = 0; index < allLines.Length; index++)
                {
                    if (allLines[index].Contains("  m_ShadowDistance: "))
                    {
                        allLines[index] = "  m_ShadowDistance: 50";
                    }

                    strContentNew += allLines[index] + "\n";
                }

                File.WriteAllText(strFilePath, strContentNew);
            }
        }
    }
}