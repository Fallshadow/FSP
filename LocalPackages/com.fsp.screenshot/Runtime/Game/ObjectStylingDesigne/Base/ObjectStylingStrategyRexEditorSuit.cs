using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using fsp.utility;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace fsp.ObjectStylingDesigne
{
    public class ObjectStylingStrategyRexEditorSuit : ObjectStylingStrategyBase
    {
        public List<ObjectStringPath> ObjectNameList_0_Male   = new List<ObjectStringPath>();
        public List<ObjectStringPath> ObjectNameList_1_FeMale = new List<ObjectStringPath>();
        public ObjectStylingStrategyRexEditorSuit(ObjectStylingStrategyInfo info) : base(info)
        {
            
        }
        
        public override void Init()
        {
            base.Init();
            SubStrategyNames = new List<string>()
            {
                "男性套装",
                "女性套装",
            };
            string[] mobMaleDirectories = Directory.GetDirectories(curInfo.ResourceFolderAssetsPath + "/Male/Fashion");
            foreach (var mobDirectory in mobMaleDirectories)
            {
                addSuitOSP(mobDirectory, ObjectNameList_0_Male);
            }
            
            string[] mobFemaleDirectories = Directory.GetDirectories(curInfo.ResourceFolderAssetsPath + "/Female/Fashion");
            foreach (var mobDirectory in mobFemaleDirectories)
            {
                addSuitOSP(mobDirectory, ObjectNameList_1_FeMale);
            }
        }

        private void addSuitOSP(string mobDirectory, List<ObjectStringPath> listPath)
        {
            IEnumerable<string> filePaths = Directory.GetFiles(mobDirectory, "*.*",
                SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".FBX"));

            foreach (var item in filePaths)
            {
                string filePath = item.Replace('\\', '/');
                if (!File.Exists(filePath)) continue;
                ObjectStringPath objectStringPath = getObjectStringPath(filePath);
                listPath.Add(objectStringPath);
            }
        }
        

        public override void ApplySubStrategy(int subStategyIndex)
        {
            curSubStategyIndex = subStategyIndex;
            objectWorldInfos = ObjectWorldInfoSO.Instance.GetObjectStylingWorldTransInfos("人形方案");
        }

        public override void ApplySub2Strategy(int sub2StategyIndex)
        {

        }
    }
}