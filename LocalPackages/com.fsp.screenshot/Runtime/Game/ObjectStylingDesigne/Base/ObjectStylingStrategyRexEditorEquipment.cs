using System;
using System.Collections.Generic;
using System.IO;
using fsp.utility;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace fsp.ObjectStylingDesigne
{
    public class ObjectStylingStrategyRexEditorEquipment : ObjectStylingStrategyBase
    {
        public List<ObjectStringPath> ObjectNameList_0_Male     = new List<ObjectStringPath>();
        public List<ObjectStringPath> ObjectNameList_1_Female    = new List<ObjectStringPath>();

        public ObjectStylingStrategyRexEditorEquipment(ObjectStylingStrategyInfo info) : base(info)
        {
            
        }
        
        public override void Init()
        {
            base.Init();
            SubStrategyNames = new List<string>()
            {
                "男性散件",
                "女性散件",
            };
            Sub2StrategyNames = new List<string>()
            {
                "头盔",
                "胸甲",
                "肩甲",
                "臂铠",
                "腿甲",
            };
            string[] modelMaleNames = Directory.GetFiles(curInfo.ResourceFolderAssetsPath + "/Male");
            foreach (var modelName in modelMaleNames)
            {
                if (modelName.EndsWith("meta")) continue;
                ObjectStringPath objectStringPath = getObjectStringPath(modelName);
                ObjectNameList_0_Male    .Add(objectStringPath);
            }
            
            string[] modelFemaleNames = Directory.GetFiles(curInfo.ResourceFolderAssetsPath + "/Female");
            foreach (var modelName in modelFemaleNames)
            {
                if (modelName.EndsWith("meta")) continue;
                ObjectStringPath objectStringPath = getObjectStringPath(modelName);
                ObjectNameList_1_Female  .Add(objectStringPath);
            }
        }

        public override void ApplySubStrategy(int subStategyIndex)
        {
            
        }

        public override void ApplySub2Strategy(int sub2StategyIndex)
        {
            curSub2StategyIndex = sub2StategyIndex;
            
            switch (curSub2StategyIndex)
            {
                case 0: objectWorldInfos = ObjectWorldInfoSO.Instance.GetObjectStylingWorldTransInfos("头盔"); break;
                case 1: objectWorldInfos = ObjectWorldInfoSO.Instance.GetObjectStylingWorldTransInfos("胸甲"); break;
                case 2: objectWorldInfos = ObjectWorldInfoSO.Instance.GetObjectStylingWorldTransInfos("肩甲"); break;
                case 3: objectWorldInfos = ObjectWorldInfoSO.Instance.GetObjectStylingWorldTransInfos("臂铠"); break;
                case 4: objectWorldInfos = ObjectWorldInfoSO.Instance.GetObjectStylingWorldTransInfos("腿甲"); break;
            }
        }

        // 通过文件名称构建ObjectStringPath
        protected override ObjectStringPath getObjectStringPath(string modelName)
        {
            string assetPath = modelName.Replace(Application.dataPath, "");
            int lastIndex = assetPath.LastIndexOf("\\", StringComparison.Ordinal);
            string lastString = assetPath.Substring(lastIndex + 1, assetPath.Length - lastIndex - 1);
            lastString = lastString.Replace("_Recipe.prefab", "");
            return new ObjectStringPath()
            {
                FilePath = assetPath,
                FilterName = lastString
            };
        }
    }
}