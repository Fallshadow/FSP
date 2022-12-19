using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace fsp.ObjectStylingDesigne
{
    public class ObjectStylingStrategyRexEditorPendant : ObjectStylingStrategyBase
    {
        public List<ObjectStringPath> ObjectNameList_0_Hang    = new List<ObjectStringPath>();
        public List<ObjectStringPath> ObjectNameList_1_Head    = new List<ObjectStringPath>();
        public List<ObjectStringPath> ObjectNameList_2_Back    = new List<ObjectStringPath>();
        public List<ObjectStringPath> ObjectNameList_3_Tail    = new List<ObjectStringPath>();
        public List<ObjectStringPath> ObjectNameList_4_Effect  = new List<ObjectStringPath>();

        public ObjectStylingStrategyRexEditorPendant(ObjectStylingStrategyInfo info) : base(info)
        {
            
        }
        
        public override void Init()
        {
            base.Init();
            SubStrategyNames = new List<string>()
            {
                "悬浮",
                "头部",
                "背部",
                "尾巴",
                "特效",
            };

            string[] dictNames = Directory.GetDirectories(curInfo.ResourceFolderAssetsPath);
            foreach (var mobDirectory in dictNames)
            {
                if (mobDirectory.Contains("hang")) addOSP(mobDirectory, ObjectNameList_0_Hang);
                if (mobDirectory.Contains("head")) addOSP(mobDirectory, ObjectNameList_1_Head);
                if (mobDirectory.Contains("back")) addOSP(mobDirectory, ObjectNameList_2_Back);
                if (mobDirectory.Contains("tail")) addOSP(mobDirectory, ObjectNameList_3_Tail);
                if (mobDirectory.Contains("Pendant"))
                {
                    string[] files = Directory.GetFiles(mobDirectory);
                    foreach (var file in files)
                    {
                        if (file.Contains(".meta")) continue;
                        if (file.Contains("effect") || file.Contains("Effect"))
                        {
                            string filePath = file.Replace('\\', '/');
                            if (!File.Exists(filePath)) continue;
                            ObjectStringPath objectStringPath = getObjectStringPath(filePath);
                            ObjectNameList_4_Effect.Add(objectStringPath);
                        }
                    }
                }
            }
            string[] ModelEffectFiles = Directory.GetFiles("Assets/ResourceRex/Prefab/Effect/Character/Fashion");
            foreach (var file in ModelEffectFiles)
            {
                if (file.Contains(".meta")) continue;
                if (file.Contains("effect") || file.Contains("Effect"))
                {
                    string filePath = file.Replace('\\', '/');
                    if (!File.Exists(filePath)) continue;
                    ObjectStringPath objectStringPath = getObjectStringPath(filePath);
                    ObjectNameList_4_Effect.Add(objectStringPath);
                }
            }
        }
        
        public override void ApplySubStrategy(int subStategyIndex)
        {
            curSubStategyIndex = subStategyIndex;
            
            switch (curSubStategyIndex)
            {
                case 0:
                case 2: 
                case 3: 
                    objectWorldInfos = ObjectWorldInfoSO.Instance.GetObjectStylingWorldTransInfos("缩放方案"); break;
                case 1: 
                    objectWorldInfos = ObjectWorldInfoSO.Instance.GetObjectStylingWorldTransInfos("时装挂件——头部"); break;
                case 4:
                    objectWorldInfos = ObjectWorldInfoSO.Instance.GetObjectStylingWorldTransInfos("默认方案"); break;
            }
        }

        public override void ApplySub2Strategy(int sub2StategyIndex)
        {

        }
    }
}