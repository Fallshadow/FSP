using System.Collections.Generic;
using UnityEngine;

namespace fsp.modelshot.Game.ObjectStylingDesigne
{
    [System.Serializable]
    public class ObjectStylingStrategyInfo
    {
        public ObjectStylingType IdType;
        public string CreatePlanName;
        public string ResourceFolderAssetsPath;
        public List<string> FileSuffixStrings;
        public Vector3 Postion;
        public List<ObjectStylingWorldTransInfo> ObjectInfos;
    }
}