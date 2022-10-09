using fsp.debug;
using fsp.utility;
using UnityEditor;
using UnityEngine;

namespace fsp.assetbundlecore
{
    public class ResourceLoaderProxy : Singleton<ResourceLoaderProxy>
    {
        public bool IsOK => manager?.IsInitOK ?? false;
        public bool IsInited = false;
        
        protected ResourceLoaderManager manager;
        
        //只能当作临时开放的接口
        public T LoadAsset<T>(string assetPath) where T : Object
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                return AssetDatabase.LoadAssetAtPath<T>(assetPath);
            }
#endif
            
            int hash = Utility.GetHashCodeByAssetPath(assetPath);
            return manager.LoadAsset<T>(hash);
        }
        
        public T LoadAsset<T>(int hashCode) where T : Object
        {
            return manager.LoadAsset<T>(hashCode);
        }

        public void InitRunTimeLoadManager()
        {
            if (IsInited)
            {
                PrintSystem.LogError($"[ResourceLoaderProxy] Init muliply times");
                return;
            }
            // TODO：使用打包出来的AB加载游戏资源
            manager?.Init();
        }
        
        public void Update()
        {
            manager?.Update();
        }
        
#if UNITY_EDITOR
        public virtual void InitEditorLoadManager(FastModeLoaderManager managerP)
        {
            if (IsInited)
            {
                PrintSystem.LogError($"[ResourceLoaderProxy] Init muliply times");
                return;
            }
            manager = managerP;
            manager?.Init();
        }
#endif
        
        //卸载调用Load加载的资源，即同步加载的资源
        public void Unload(int hashCode)
        {
            manager.Unload(hashCode);
        }
    }
}