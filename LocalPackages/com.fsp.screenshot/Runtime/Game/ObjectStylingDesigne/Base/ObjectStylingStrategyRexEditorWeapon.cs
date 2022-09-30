using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace fsp.modelshot.Game.ObjectStylingDesigne
{
    public class ObjectStylingStrategyRexEditorWeapon : ObjectStylingStrategyBase
    {
        public List<ObjectStringPath> ObjectNameList_Knife     = new List<ObjectStringPath>();
        public List<ObjectStringPath> ObjectNameList_Hammer    = new List<ObjectStringPath>();
        public List<ObjectStringPath> ObjectNameList_DualBlade = new List<ObjectStringPath>();
        public List<ObjectStringPath> ObjectNameList_KallaGun  = new List<ObjectStringPath>();
        public List<ObjectStringPath> ObjectNameList_Spear     = new List<ObjectStringPath>();
        public List<ObjectStringPath> ObjectNameList_Bow       = new List<ObjectStringPath>();
        
        public ObjectStylingStrategyRexEditorWeapon(ObjectStylingStrategyInfo info) : base(info)
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
                if (modelName.Contains("Knife"))     ObjectNameList_Knife    .Add(objectStringPath);
                if (modelName.Contains("Hammer"))    ObjectNameList_Hammer   .Add(objectStringPath);
                if (modelName.Contains("DualBlade")) ObjectNameList_DualBlade.Add(objectStringPath);
                if (modelName.Contains("KallaGun"))  ObjectNameList_KallaGun .Add(objectStringPath);
                if (modelName.Contains("Spear"))     ObjectNameList_Spear    .Add(objectStringPath);
                if (modelName.Contains("Bow"))       ObjectNameList_Bow      .Add(objectStringPath);
            }
        }

        private ObjectStringPath getObjectStringPath(string modelName)
        {
            string assetPath = modelName.Replace(Application.dataPath, "");
            int lastIndex = assetPath.LastIndexOf("\\", StringComparison.Ordinal);
            string lastString = assetPath.Substring(lastIndex + 1, assetPath.Length - lastIndex - 1);
            return new ObjectStringPath()
            {
                FilePath = assetPath,
                FilterName = lastString
            };
        }
        
        public override void ApplySubStrategy(int subStategyIndex)
        {
            switch (subStategyIndex)
            {
                case 0:
                    
                default: break;
            }
        }
    }
}