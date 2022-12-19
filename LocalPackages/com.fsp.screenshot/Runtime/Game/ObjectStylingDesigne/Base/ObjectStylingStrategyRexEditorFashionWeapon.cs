using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using fsp.utility;
using Ludiq;
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
            
            string[] dictNames = Directory.GetDirectories(curInfo.ResourceFolderAssetsPath);
            foreach (var mobDirectory in dictNames)
            {
                if (mobDirectory.Contains("GreatSword"))  addOSP(mobDirectory, ObjectNameList_0_Knife    );
                if (mobDirectory.Contains("ForceHammer")) addOSP(mobDirectory, ObjectNameList_1_Hammer   );
                if (mobDirectory.Contains("FuryBlades"))  addOSP(mobDirectory, ObjectNameList_2_DualBlade);
                if (mobDirectory.Contains("Kallagun"))    addOSP(mobDirectory, ObjectNameList_3_KallaGun );
                if (mobDirectory.Contains("KallaGun"))    addOSP(mobDirectory, ObjectNameList_3_KallaGun );
                if (mobDirectory.Contains("Kallaspear"))  addOSP(mobDirectory, ObjectNameList_4_Spear    );
                if (mobDirectory.Contains("SwitchBow"))   addOSP(mobDirectory, ObjectNameList_5_Bow      );
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

        public override void RotateZeroTransY(Vector2 rotate)
        {
            if (objectWorldInfos.Count == 0 || Objects.Count == 0) return;
            
            switch (curSubStategyIndex)
            {
                case 0: 
                case 1: 
                case 3:
                    Objects[0].transform.eulerAngles = Objects[0].transform.eulerAngles.AddY(rotate.x);
                    break;
                case 2: 
                case 4: 
                case 5: 
                    osSkeleton.RotateRootY(rotate.x);
                    break;
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
                    prefab0 = AssetDatabase.LoadAssetAtPath<Object>(objectFilePath);
                    prefab1 = AssetDatabase.LoadAssetAtPath<Object>(objectFilePath);
                    if (objectWorldInfos == null || prefab0 == null || prefab1 == null) break;
                    Objects.Add(Utility.InstantiateObject(prefab0));
                    Objects.Add(Utility.InstantiateObject(prefab1));
                    break;
                case 4:
                    filePath = objectFilePath.Replace("_Body.FBX", "");
                    filePath = filePath.Replace("_Head.FBX", "");
                    string pathBody = $"{filePath}_Body.FBX";
                    string pathHead = $"{filePath}_Head.FBX";
                    prefab0 = AssetDatabase.LoadAssetAtPath<Object>(pathBody);
                    prefab1 = AssetDatabase.LoadAssetAtPath<Object>(pathHead);
                    if (objectWorldInfos == null || prefab0 == null || prefab1 == null)
                    {
                        filePath = objectFilePath.Replace("_Body.fbx", "");
                        filePath = filePath.Replace("_Head.fbx", "");
                        pathBody = $"{filePath}_Body.fbx";
                        pathHead = $"{filePath}_Head.fbx";
                        prefab0 = AssetDatabase.LoadAssetAtPath<Object>(pathBody);
                        prefab1 = AssetDatabase.LoadAssetAtPath<Object>(pathHead);
                        if (objectWorldInfos == null || prefab0 == null || prefab1 == null) break;
                    }
                    
                    Objects.Add(Utility.InstantiateObject(prefab0));
                    GameObject goSpearHead = Utility.InstantiateObject(prefab1);
                    goSpearHead.AddComponent<Animator>();
                    goSpearHead.AddComponent<Animation>();
                    AnimationClip animationClipSpearHead = AssetDatabase.LoadAssetAtPath<AnimationClip>("Assets/ResourceRex/Character/Weapon/Spear/Animations/Clips/Run_1.anim");
                    animationClipSpearHead.SampleAnimation(goSpearHead, animationClipSpearHead.length);
                    Objects.Add(goSpearHead);
                    break;
                case 5:
                    filePath = objectFilePath.Replace("_Bow.FBX", "");
                    filePath = filePath.Replace("_Crossbow.FBX", "");
                    string pathBow = $"{filePath}_Bow.FBX";
                    string pathQuiver = $"{filePath}_Crossbow.FBX";
                    prefab0 = AssetDatabase.LoadAssetAtPath<Object>(pathBow);
                    prefab1 = AssetDatabase.LoadAssetAtPath<Object>(pathQuiver);
                    if (objectWorldInfos == null || prefab0 == null || prefab1 == null)
                    {
                        filePath = objectFilePath.Replace("_Bow.fbx", "");
                        filePath = filePath.Replace("_Crossbow.fbx", "");
                        pathBow = $"{filePath}_Bow.fbx";
                        pathQuiver = $"{filePath}_Crossbow.fbx";
                        prefab0 = AssetDatabase.LoadAssetAtPath<Object>(pathBow);
                        prefab1 = AssetDatabase.LoadAssetAtPath<Object>(pathQuiver);
                        if (objectWorldInfos == null || prefab0 == null || prefab1 == null) break;
                    }

                    GameObject goBow = Utility.InstantiateObject(prefab0);
                    goBow.AddComponent<Animator>();
                    goBow.AddComponent<Animation>();
                    AnimationClip animationClip = AssetDatabase.LoadAssetAtPath<AnimationClip>("Assets/ResourceRex/Character/Weapon/Bow/Animations/Clips/Charge_End_1_B.anim");
                    animationClip.SampleAnimation(goBow, animationClip.length);
                    Objects.Add(goBow);
                    Objects.Add(Utility.InstantiateObject(prefab1));
                    break;
            }

            stylingObejcts();
        }
    }
}