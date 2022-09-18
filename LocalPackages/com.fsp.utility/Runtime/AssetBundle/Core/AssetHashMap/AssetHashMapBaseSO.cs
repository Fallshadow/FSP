using System.Collections.Generic;
using UnityEngine;

namespace fsp.assetbundlecore
{
    public class AssetHashMapBaseSO : ScriptableObject
    {
        public List<string> assets = new List<string>();
        public List<int> hashs = new List<int>();

        public List<string> stripAssets = new List<string>();
        public List<int> stripHashs = new List<int>();

        public virtual UNITY_ASSETTYPE GetAssetType()
        {
            return UNITY_ASSETTYPE.E_OBJECT;
        }

        public virtual void Initialize()
        {
        }

        public virtual bool GetAssetHash(string asset, out int hash)
        {
            hash = -1;
            int index = assets.IndexOf(asset);
            if (index != -1)
            {
                hash = hashs[index];
                return true;
            }

            debug.PrintSystem.LogWarning($"[AssetHashMapBaseSO] Can't find Asset: {asset}");
            return false;
        }

#if UNITY_EDITOR
        public virtual void Automatic()
        {
        }
#endif
    }
}