using UnityEngine;

namespace fsp.assetbundlecore
{
    public class ResourceLoaderMonoManager : SingletonMonoBehaviorNoDestroy<ResourceLoaderMonoManager>
    {
        private void Update()
        {
            ResourceLoaderProxy.instance.Update();
        }
    }
}