using System;
using System.Collections.Generic;
using System.IO;
using fsp.utility;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace fsp.ObjectStylingDesigne
{
    public class ObjectStylingStrategyRexEditorFashionWeapon : ObjectStylingStrategyBase
    {
        public List<ObjectStringPath> ObjectNameList_0_Knife     = new List<ObjectStringPath>();
        public List<ObjectStringPath> ObjectNameList_1_Hammer    = new List<ObjectStringPath>();
        public List<ObjectStringPath> ObjectNameList_2_DualBlade = new List<ObjectStringPath>();
        public List<ObjectStringPath> ObjectNameList_3_KallaGun  = new List<ObjectStringPath>();
        public List<ObjectStringPath> ObjectNameList_4_Spear     = new List<ObjectStringPath>();
        public List<ObjectStringPath> ObjectNameList_5_Bow       = new List<ObjectStringPath>();
        
        public ObjectStylingStrategyRexEditorFashionWeapon(ObjectStylingStrategyInfo info) : base(info)
        {
            
        }
        
        public override void Init()
        {
            base.Init();
            SubStrategyNames = new List<string>()
            {
                "斩裂刀",
                "锤子",
                "双刀",
                "塑能枪",
                "饲灵枪",
                "弓箭",
            };
            string[] modelNames = Directory.GetFiles(curInfo.ResourceFolderAssetsPath);
            foreach (var modelName in modelNames)
            {
                if (modelName.EndsWith("meta")) continue;
                ObjectStringPath objectStringPath = getObjectStringPath(modelName);
                if (modelName.Contains("Weapon_Fashion_1")) ObjectNameList_0_Knife    .Add(objectStringPath);
                if (modelName.Contains("Weapon_Fashion_2")) ObjectNameList_1_Hammer   .Add(objectStringPath);
                if (modelName.Contains("Weapon_Fashion_3")) ObjectNameList_2_DualBlade.Add(objectStringPath);
                if (modelName.Contains("Weapon_Fashion_4")) ObjectNameList_3_KallaGun .Add(objectStringPath);
                if (modelName.Contains("Weapon_Fashion_5")) ObjectNameList_4_Spear    .Add(objectStringPath);
                if (modelName.Contains("Weapon_Fashion_6")) ObjectNameList_5_Bow      .Add(objectStringPath);
            }
        }

        public override void ApplySubStrategy(int subStategyIndex)
        {
            curSubStategyIndex = subStategyIndex;
            
            switch (curSubStategyIndex)
            {
                case 0: objectWorldInfos = ObjectWorldInfoSO.Instance.GetObjectStylingWorldTransInfos("Rex_Knife"); break;
                case 1: objectWorldInfos = ObjectWorldInfoSO.Instance.GetObjectStylingWorldTransInfos("Rex_Hammer"); break;
                case 2: objectWorldInfos = ObjectWorldInfoSO.Instance.GetObjectStylingWorldTransInfos("Rex_DualBlade"); break;
                case 3: objectWorldInfos = ObjectWorldInfoSO.Instance.GetObjectStylingWorldTransInfos("Rex_KallaGun"); break;
                case 4: objectWorldInfos = ObjectWorldInfoSO.Instance.GetObjectStylingWorldTransInfos("Rex_Spear"); break;
                case 5: objectWorldInfos = ObjectWorldInfoSO.Instance.GetObjectStylingWorldTransInfos("Rex_Bow"); break;
            }
        }

        public override void ApplySub2Strategy(int sub2StategyIndex)
        {
            
        }

        public override void LoadObject(string objectFilePath)
        {
            Object prefab0 = null;
            Object prefab1 = null;
            string filePath = "";
            foreach (var item in Objects)
            {
                Object.DestroyImmediate(item);
            }
            Objects.Clear();
            switch (curSubStategyIndex)
            {
                case 0:
                case 1:
                case 3: 
                    prefab0 = AssetDatabase.LoadAssetAtPath<Object>(objectFilePath);
                    if (objectWorldInfos == null || prefab0 == null) break;
                    Objects.Add(Utility.InstantiateObject(prefab0));
                    break;
                case 2:
                    filePath = objectFilePath.Replace("_L.prefab", "");
                    filePath = filePath.Replace("_R.prefab", "");
                    string pathLeft = $"{filePath}_L.prefab";
                    string pathRight = $"{filePath}_R.prefab";
                    prefab0 = AssetDatabase.LoadAssetAtPath<Object>(pathLeft);
                    prefab1 = AssetDatabase.LoadAssetAtPath<Object>(pathRight);
                    if (objectWorldInfos == null || prefab0 == null || prefab1 == null) break;
                    Objects.Add(Utility.InstantiateObject(prefab0));
                    Objects.Add(Utility.InstantiateObject(prefab1));
                    break;
                case 4:
                    filePath = objectFilePath.Replace("_Body.prefab", "");
                    filePath = filePath.Replace("_Head.prefab", "");
                    string pathBody = $"{filePath}_Body.prefab";
                    string pathHead = $"{filePath}_Head.prefab";
                    prefab0 = AssetDatabase.LoadAssetAtPath<Object>(pathBody);
                    prefab1 = AssetDatabase.LoadAssetAtPath<Object>(pathHead);
                    if (objectWorldInfos == null || prefab0 == null || prefab1 == null) break;
                    Objects.Add(Utility.InstantiateObject(prefab0));
                    Objects.Add(Utility.InstantiateObject(prefab1));
                    break;
                case 5: 
                    default: break;
            }

            stylingObejcts();
        }
    }
}