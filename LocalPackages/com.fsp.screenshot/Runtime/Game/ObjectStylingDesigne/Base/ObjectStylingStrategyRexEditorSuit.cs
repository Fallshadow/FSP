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
                addOSP(mobDirectory, ObjectNameList_0_Male);
            }
            
            string[] mobFemaleDirectories = Directory.GetDirectories(curInfo.ResourceFolderAssetsPath + "/Female/Fashion");
            foreach (var mobDirectory in mobFemaleDirectories)
            {
                addOSP(mobDirectory, ObjectNameList_1_FeMale);
            }
        }

        public override void ApplySubStrategy(int subStategyIndex)
        {
            curSubStategyIndex = subStategyIndex;
            objectWorldInfos = ObjectWorldInfoSO.Instance.GetObjectStylingWorldTransInfos("缩放方案");
        }

        public override void ApplySub2Strategy(int sub2StategyIndex)
        {

        }
    }
}