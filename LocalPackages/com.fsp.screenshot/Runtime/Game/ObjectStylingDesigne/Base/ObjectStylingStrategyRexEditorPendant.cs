using System.Collections.Generic;
using System.IO;

namespace fsp.ObjectStylingDesigne
{
    public class ObjectStylingStrategyRexEditorPendant : ObjectStylingStrategyBase
    {
        public List<ObjectStringPath> ObjectNameList_0_Hang    = new List<ObjectStringPath>();
        public List<ObjectStringPath> ObjectNameList_1_Head    = new List<ObjectStringPath>();
        public List<ObjectStringPath> ObjectNameList_2_Back    = new List<ObjectStringPath>();
        public List<ObjectStringPath> ObjectNameList_3_Tail    = new List<ObjectStringPath>();

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
            };

            string[] modelNames = Directory.GetFiles(curInfo.ResourceFolderAssetsPath);
            foreach (var modelName in modelNames)
            {
                if (modelName.EndsWith("meta")) continue;
                ObjectStringPath objectStringPath = getObjectStringPath(modelName);
                if (modelName.Contains("hang")) ObjectNameList_0_Hang.Add(objectStringPath);
                if (modelName.Contains("head")) ObjectNameList_1_Head.Add(objectStringPath);
                if (modelName.Contains("back")) ObjectNameList_2_Back.Add(objectStringPath);
                if (modelName.Contains("tail")) ObjectNameList_3_Tail.Add(objectStringPath);
            }
        }

        public override void ApplySubStrategy(int subStategyIndex)
        {
            curSubStategyIndex = subStategyIndex;
            
            switch (curSubStategyIndex)
            {
                case 0: objectWorldInfos = ObjectWorldInfoSO.Instance.GetObjectStylingWorldTransInfos("时装挂件——悬浮"); break;
                case 1: objectWorldInfos = ObjectWorldInfoSO.Instance.GetObjectStylingWorldTransInfos("时装挂件——头部"); break;
                case 2: objectWorldInfos = ObjectWorldInfoSO.Instance.GetObjectStylingWorldTransInfos("时装挂件——背部"); break;
                case 3: objectWorldInfos = ObjectWorldInfoSO.Instance.GetObjectStylingWorldTransInfos("时装挂件——尾部"); break;
            }
        }

        public override void ApplySub2Strategy(int sub2StategyIndex)
        {

        }
    }
}