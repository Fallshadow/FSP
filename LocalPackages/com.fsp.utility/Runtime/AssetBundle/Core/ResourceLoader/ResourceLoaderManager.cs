using System.Collections.Generic;
using UnityEngine;

namespace fsp.assetbundlecore
{
    public class ResourceLoaderManager
    {
        public const int DEFAULT_ASSET_LOADER_COUNT = 500;
        
        public bool IsInitOK = false;
        public Dictionary<int, ISyncProxy> syncProxys = new Dictionary<int, ISyncProxy>(DEFAULT_ASSET_LOADER_COUNT);
        
        public virtual void Init()
        {

        }
        
        public virtual T LoadAsset<T>(int hash) where T : Object
        {
            return default(T);
        }
        
        public virtual void Update()
        {
                
        }
        
        public virtual void Unload(int hash)
        {
                
        }
    }
}