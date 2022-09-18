#if UNITY_EDITOR

using System.Collections.Generic;
using System.Text;
using fsp.utility;

namespace fsp.assetbundlecore
{
    [System.Serializable]
    public class ABInfo_E
    {
        public string abName;
        public string orginName;
        public int hash;
        public string ownerFileName;
        public ulong offset;
        public List<int> abAssets = new List<int>();
        public string abFileHash;
        public List<int> rootAssets = new List<int>();
        public bool isScene;
        public List<int> depABs = new List<int>();

        public ABInfo GetRunTime()
        {
            ABInfo abInfo = new ABInfo();
            abInfo.abName = abName;
            abInfo.hash = hash;
            abInfo.ownerFileName = ownerFileName;

            abInfo.offset = offset;
            abInfo.abAssets = new List<int>(abAssets.Count);
            foreach (var abAsset in abAssets)
            {
                abInfo.abAssets.Add(abAsset);
            }

            abInfo.rootAssets = rootAssets;
            abInfo.depABs = new List<int>(depABs);
            return abInfo;
        }

        public void Merge(ABInfo_E abInfoE)
        {
            abAssets.AddRange(abInfoE.abAssets);
            if (abInfoE.rootAssets != null)
            {
                rootAssets.AddRange(abInfoE.rootAssets);
            }
        }

        public void MendMsg(string prefix, Dictionary<int, int> assetBundleHashs)
        {
            abAssets.Sort();
            var tempABName = new StringBuilder(prefix);
            for (int j = 0; j < abAssets.Count; j++)
            {
                var assetName = abAssets[j];
                tempABName.Append(assetName);
            }

            var abHashCode = Utility.GetStringHashCode(tempABName.ToString());
            var proHashCode = Utility.GetStringHashCode(tempABName.ToString());

            if (rootAssets == null || rootAssets.Count == 0)
            {
                abName = abHashCode.ToString();
                orginName = proHashCode.ToString();
            }

            hash = abHashCode;
            for (int j = 0; j < abAssets.Count; j++)
            {
                var asset = abAssets[j];
                assetBundleHashs.Add(asset, abHashCode);
            }

            if (isScene)
            {
                abAssets.RemoveAll(x => x != rootAssets[0]);
            }
        }

        public bool Equal(ABInfo_E other)
        {
            if (abAssets.Count != other.abAssets.Count
                || rootAssets.Count != other.rootAssets.Count
                || depABs.Count != other.depABs.Count
                || abFileHash != other.abFileHash)
            {
                return false;
            }

            var myAssets = new HashSet<int>();
            foreach (var asset in abAssets)
            {
                myAssets.Add(asset);
            }

            var otherAssets = other.abAssets;

            foreach (var asset in otherAssets)
            {
                if (!myAssets.Contains(asset))
                {
                    return false;
                }
            }

            var myDeps = new HashSet<int>();
            foreach (var dep in depABs)
            {
                myDeps.Add(dep);
            }

            foreach (var dep in other.depABs)
            {
                if (!myDeps.Contains(dep))
                {
                    return false;
                }
            }

            return true;
        }
    }
}

#endif