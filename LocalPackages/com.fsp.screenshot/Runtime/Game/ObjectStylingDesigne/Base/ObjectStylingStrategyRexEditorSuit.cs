using System;
using System.Collections.Generic;
using System.IO;
using fsp.utility;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace fsp.ObjectStylingDesigne
{
    public class ObjectStylingStrategyRexEditorSuit : ObjectStylingStrategyBase
    {
        
        public ObjectStylingStrategyRexEditorSuit(ObjectStylingStrategyInfo info) : base(info)
        {
            
        }
        
        public override void Init()
        {
            base.Init();
            SubStrategyNames = new List<string>()
            {

            };


        }

        public override void ApplySubStrategy(int subStategyIndex)
        {
            curSubStategyIndex = subStategyIndex;
            

        }

        public override void ApplySub2Strategy(int sub2StategyIndex)
        {

        }
    }
}