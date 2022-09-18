using System;
using System.Collections.Generic;
using System.IO;
using fsp.assetbundlecore;
using fsp.data;
using fsp.utility;
using UnityEditor;
using UnityEngine;

namespace fsp.ui
{
    [CreateAssetMenu]
    public class AssetHashMap_UI : AssetHashMapBaseSO
    {
        public List<UiAssetInfo> UiAssetInfos = new List<UiAssetInfo>();
        protected virtual string resFolder => ResourcesPathSetting.RESOURCES_FOLDER;

        public override UNITY_ASSETTYPE GetAssetType()
        {
            return UNITY_ASSETTYPE.E_UI;
        }

        public int GetUiAssetInfo(UiAssetType type, string name)
        {
            for (int i = 0; i < UiAssetInfos.Count; i++)
            {
                if (UiAssetInfos[i].AssetType == type && UiAssetInfos[i].AssetName == name)
                {
                    return UiAssetInfos[i].HashCode;
                }
            }

            return -1;
        }

#if UNITY_EDITOR
        public override void Automatic()
        {
            if (UiAssetInfos == null)
            {
                debug.PrintSystem.LogError("UiAssetInfoSO can`t find!");
                return;
            }

            UiAssetInfos.Clear();
            RefreshBaseInfo();
            AssetDatabase.SaveAssets();
        }

        public virtual void RefreshBaseInfo()
        {
            if (resFolder == null) return;
            CreateAssetInfo(resFolder);
        }

        protected void CreateAssetInfo(string folder)
        {
            string uiFolderPath = $"{Application.dataPath}/{folder}UI/Logic";
            if (Directory.Exists(uiFolderPath))
            {
                DirectoryInfo root = new DirectoryInfo(uiFolderPath);
                DirectoryInfo[] allDir = root.GetDirectories();
                for (int i = 0; i < allDir.Length; i++)
                {
                    string tmpPath = $"{allDir[i].FullName}/Prefabs/Main";
                    if (Directory.Exists(tmpPath))
                    {
                        AddAssetInfo(tmpPath, UiAssetType.UAT_PREFAB);
                    }

                    tmpPath = $"{allDir[i].FullName}/Animations/Main";
                    if (Directory.Exists(tmpPath))
                    {
                        AddAssetInfo(tmpPath, UiAssetType.UAT_ANIMATION);
                    }

                    tmpPath = $"{allDir[i].FullName}/Materials/Main";
                    if (Directory.Exists(tmpPath))
                    {
                        AddAssetInfo(tmpPath, UiAssetType.UAT_MATERIAL);
                    }

                    tmpPath = $"{allDir[i].FullName}/Atlas";
                    if (Directory.Exists(tmpPath))
                    {
                        AddAssetInfo(tmpPath, UiAssetType.UAT_ATLAS);
                    }
                }
            }

            uiFolderPath = $"{Application.dataPath}/{folder}UI/Common/Prefabs/Main"; //通用文件夹内预制体
            if (Directory.Exists(uiFolderPath))
            {
                AddAssetInfo(uiFolderPath, UiAssetType.UAT_PREFAB);
            }

            uiFolderPath = $"{Application.dataPath}/{folder}UI/Common/Atlas"; //通用文件夹图集
            if (Directory.Exists(uiFolderPath))
            {
                AddAssetInfo(uiFolderPath, UiAssetType.UAT_ATLAS);
            }

            uiFolderPath = $"{Application.dataPath}/{folder}UI/HighDefinitionTex"; //高清图片
            if (Directory.Exists(uiFolderPath))
            {
                AddAssetInfo(uiFolderPath, UiAssetType.UAT_HIGH_DEFINITION_TEX);
            }

            uiFolderPath = $"{Application.dataPath}/{folder}UI/Fonts"; //字体资源
            if (Directory.Exists(uiFolderPath))
            {
                AddAssetInfo(uiFolderPath, UiAssetType.UAT_TMP_FONTASSET);
                AddAssetInfo(uiFolderPath, UiAssetType.UAT_MATERIAL);
            }

            uiFolderPath = $"{Application.dataPath}/{folder}UI/Emoji"; //表情资源
            if (Directory.Exists(uiFolderPath))
            {
                AddAssetInfo(uiFolderPath, UiAssetType.UAT_TXT);
            }

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
            Debug.Log("-----------已刷新UI资源Hash信息------------");
        }


        public void AddAssetInfo(string assetFolderPath, UiAssetType assetType)
        {
            if (!Directory.Exists(assetFolderPath)) return;
            FileInfo[] files = new DirectoryInfo(assetFolderPath).GetFiles("*.*", SearchOption.AllDirectories);
            
            string suffix = "";
            if (assetType == UiAssetType.UAT_ATLAS)
            {
                suffix = ".spriteatlas";
            }
            else if (assetType == UiAssetType.UAT_PREFAB)
            {
                suffix = ".prefab";
            }
            else if (assetType == UiAssetType.UAT_TMP_FONTASSET)
            {
                suffix = ".asset";
            }
            else if (assetType == UiAssetType.UAT_MATERIAL)
            {
                suffix = ".mat";
            }
            else if (assetType == UiAssetType.UAT_TXT)
            {
                suffix = ".txt";
            }
            else if (assetType == UiAssetType.UAT_ANIMATION)
            {
                suffix = ".anim";
            }
            else if (assetType == UiAssetType.UAT_HIGH_DEFINITION_TEX) //图片特殊处理
            {
                foreach (FileInfo file in files)
                {
                    if (!file.Name.EndsWith(".jpg") && !file.Name.EndsWith(".png")) continue;
                    UiAssetInfo info = generateAssetInfo(file, assetType, 4); // .png 和 .jpg的长度
                    if (info == null) continue;
                    UiAssetInfos.Add(info);
                }
                return;
            }
            
            int strLen = suffix.Length;
            foreach (FileInfo file in files)
            {
                if (!file.Name.EndsWith(suffix)) continue;
                
                UiAssetInfo info = generateAssetInfo(file, assetType, strLen);
                if (info == null) continue;
                UiAssetInfos.Add(info);
            }
        }

        protected UiAssetInfo generateAssetInfo(FileInfo file, UiAssetType assetType, int suffixLen)
        {
            UiAssetInfo info = new UiAssetInfo();
            info.AssetType = assetType;
            info.AssetName = file.Name.Substring(0, file.Name.Length - suffixLen);
            
            if (!trygetAssetInfoIndexByName(info.AssetName, out int index))
            {
                return null;
            }

            info.Index = index;
            info.HashCode = initInfoHashCode(file.FullName);
            return info;
        }

        protected virtual int initInfoHashCode(string fullname)
        {
            return Utility.GetHashCodeByFullAssetPath(fullname);
        }
        

        protected virtual bool trygetAssetInfoIndexByName(string assetInfoName, out int index)
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
#endif
    }
}