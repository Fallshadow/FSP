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
        public List<string> FileSuffixStrings = new List<string>();
        public Vector3 Postion = Vector3.zero;
        public int MaxLayer = 1;
        public List<ObjectStylingWorldTransInfo> ObjectInfos = new List<ObjectStylingWorldTransInfo>();
    }
}