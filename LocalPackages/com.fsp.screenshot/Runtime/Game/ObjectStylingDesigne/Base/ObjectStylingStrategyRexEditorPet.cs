using System.Collections.Generic;
using System.IO;

namespace fsp.ObjectStylingDesigne
{
    public class ObjectStylingStrategyRexEditorPet : ObjectStylingStrategyBase
    {
        public List<ObjectStringPath> ObjectNameList_0_AiYing  = new List<ObjectStringPath>();
        public List<ObjectStringPath> ObjectNameList_1_MoGu    = new List<ObjectStringPath>();
        public List<ObjectStringPath> ObjectNameList_2_Wolf    = new List<ObjectStringPath>();
        public List<ObjectStringPath> ObjectNameList_3_Humman  = new List<ObjectStringPath>();

        public ObjectStylingStrategyRexEditorPet(ObjectStylingStrategyInfo info) : base(info)
        {
            
        }
        
        public override void Init()
        {
            base.Init();
            SubStrategyNames = new List<string>()
            {
                "矮鹰族",
                "罩菇组",
                "狼族",
                "人族",
            };
            string[] mobDirectories = Directory.GetDirectories(curInfo.ResourceFolderAssetsPath);
            foreach (var mobDirectory in mobDirectories)
            {
                addPetOSP(mobDirectory, "Mob_10", ObjectNameList_0_AiYing  );
                addPetOSP(mobDirectory, "Mob_11", ObjectNameList_1_MoGu    );
                addPetOSP(mobDirectory, "Mob_12", ObjectNameList_2_Wolf    );
                addPetOSP(mobDirectory, "Mob_13", ObjectNameList_3_Humman  );
            }
        }

        private void addPetOSP(string mobDirectory, string fixStr, List<ObjectStringPath> listPath)
        {
            if (mobDirectory.Contains(fixStr))
            {
                string subFileSuffix = mobDirectory.Substring(mobDirectory.Length - 5);
                string filePath = mobDirectory + $"/Prefabs/Model_{subFileSuffix}.prefab";
                filePath = filePath.Replace('\\', '/');
                if (!File.Exists(filePath)) return;
                ObjectStringPath objectStringPath = getObjectStringPath(filePath);
                listPath.Add(objectStringPath);
            }
        }
        
        public override void ApplySubStrategy(int subStategyIndex)
        {
            curSubStategyIndex = subStategyIndex;
            objectWorldInfos = ObjectWorldInfoSO.Instance.GetObjectStylingWorldTransInfos("默认方案");
        }

        public override void ApplySub2Strategy(int sub2StategyIndex)
        {

        }
    }
}