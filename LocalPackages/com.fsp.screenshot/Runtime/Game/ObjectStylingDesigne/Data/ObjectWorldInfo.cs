using System;
using System.Collections.Generic;

namespace fsp.ObjectStylingDesigne
{
    [Serializable]
    public class ObjectWorldInfo
    {
        public string WorldInfoName = "无名";
        public List<ObjectStylingWorldTransInfo> CInfos = new List<ObjectStylingWorldTransInfo>();
    }
}