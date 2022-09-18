using System.Collections.Generic;

namespace fsp.assetbundleeditor
{
    public class AssetGroup
    {
        public int Importance;
        public bool IsCommon; // 供优化使用
        public bool IsSVC = false; // 是否SVC标签
        public int InstanceId;
        public List<AssetItem> Assets = new List<AssetItem>();
        public bool IsValid => Assets.Count != 0;
        public int AssetCount => Assets.Count;
        
        public string GroupName
        {
            get => groupName;
            set
            {
                groupName = value;
                if (groupName.IndexOf('&') != -1)
                {
                    IsCommon = true;
                }
            }
        }
        private string groupName;
    }
}

