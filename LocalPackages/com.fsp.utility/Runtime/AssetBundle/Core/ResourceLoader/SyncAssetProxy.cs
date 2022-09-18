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
            throw new System.NotImplementedException();
        }

        public int GetABHash()
        {
            throw new System.NotImplementedException();
        }

        public int GetTypeHash()
        {
            throw new System.NotImplementedException();
        }

        public virtual void Addreference()
        {

        }

        public virtual void DefReference()
        {

        }

        public void Destroy()
        {
            throw new System.NotImplementedException();
        }

        public void ZeroReference()
        {
            throw new System.NotImplementedException();
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