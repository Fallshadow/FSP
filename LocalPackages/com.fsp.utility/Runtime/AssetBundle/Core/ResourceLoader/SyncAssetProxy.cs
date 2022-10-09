using System;
using Object = UnityEngine.Object;

namespace fsp.assetbundlecore
{
    public class SyncAssetProxy<T> : ISyncProxy where T : Object  //主资源
    {
        public Action<ISyncProxy> OnDestroy;
        
        protected T resultObj;
        protected int reference;
        
        public int GetHash()
        {
            return -1;
        }

        public int GetABHash()
        {
            return -1;
        }

        public int GetTypeHash()
        {
            return -1;
        }

        public virtual void Addreference()
        {

        }

        public virtual void DefReference()
        {

        }

        public void Destroy()
        {
            
        }

        public void ZeroReference()
        {
            
        }

        public virtual void Begin()
        {
            
        }
        
        public T GetAsset()
        {
            return resultObj;
        }
    }
}