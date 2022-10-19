using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace fsp.ObjectStylingDesigne
{
    public class ObjectStylingStrategyRexEditorMonster : ObjectStylingStrategyBase
    {
        public List<ObjectStringPath> ObjectNameList_0_Boss = new List<ObjectStringPath>();
        public List<ObjectStringPath> ObjectNameList_1_NPC = new List<ObjectStringPath>();

        public ObjectStylingStrategyRexEditorMonster(ObjectStylingStrategyInfo info) : base(info)
        {
        }
        
        public override void RotateZeroTransY(Vector2 rotate)
        {
            if (objectWorldInfos.Count == 0 || Objects.Count == 0) return;
            osSkeleton.RotateRootY(rotate.x);
        }
        
        public override void Init()
        {
            base.Init();
            SubStrategyNames = new List<string>()
            {
                "BOSS",
                "NPC",
            };
            string[] mobBossDirectories = Directory.GetDirectories(curInfo.ResourceFolderAssetsPath + "/Boss");
            foreach (var mobDirectory in mobBossDirectories)
            {
                addMonster(mobDirectory, 4, ObjectNameList_0_Boss);
            }

            string[] mobNpcDirectories = Directory.GetDirectories(curInfo.ResourceFolderAssetsPath + "/NPC_Monster");
            foreach (var mobDirectory in mobNpcDirectories)
            {
                addMonster(mobDirectory, 3, ObjectNameList_1_NPC);
            }
        }

        private void addMonster(string mobDirectory, int length, List<ObjectStringPath> listPath)
        {
            string subFileSuffix = mobDirectory.Substring(mobDirectory.Length - length);
            string filePath = mobDirectory + $"/Prefabs/Model_{subFileSuffix}.prefab";
            filePath = filePath.Replace('\\', '/');
            if (!File.Exists(filePath)) return;
            ObjectStringPath objectStringPath = getObjectStringPath(filePath);
            listPath.Add(objectStringPath);
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