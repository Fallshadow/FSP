#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace fsp.assetbundlecore
{
    public class SyncAssetProxy_Fast<T> : SyncAssetProxy<T> where T : Object
    {
        protected AssetInfoSimplify assetInfo;
        
        public override void Begin()
        {                
            resultObj = AssetDatabase.LoadAssetAtPath<T>(assetInfo.asset);
        }
        
        public override void Addreference()
        {
            reference++;
        }

        public override  void DefReference()
        {
            reference--;
            if (reference == 0)
            {
                Destroy();
            }
        }

        public void SetInfoData(ref AssetInfoSimplify assetInfoP)
        {
            reference = 1;
            assetInfo = assetInfoP;
        }
    }
}

#endif