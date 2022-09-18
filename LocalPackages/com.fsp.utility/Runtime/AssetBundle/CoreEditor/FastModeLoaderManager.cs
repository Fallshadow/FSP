using System.Collections.Generic;
using System.IO;
using fsp.debug;
using UnityEngine;

namespace fsp.assetbundlecore
{
    public class FastModeLoaderManager : ResourceLoaderManager
    {
        private readonly Dictionary<int, AssetInfoSimplify> assetInfoDict = new Dictionary<int, AssetInfoSimplify>();

        public override void Init()
        {
            PrintSystem.Log("[ResourceLoaderManager] Current ResourceLoader is FastModeLoaderManager");
            
            base.Init();
            IsInitOK = true;
            LoadJsonToMem();
        }

        protected virtual string getDepTreeInfoPath()
        {
            return AssetBundleUtility.GetDepTreeInfoFolderPath();
        }
        
        private void LoadJsonToMem()
        {
            assetInfoDict.Clear();
            List<AssetInfo_E> infos = new List<AssetInfo_E>();
            
            var soPath = getDepTreeInfoPath();
            var files = Directory.GetFiles(soPath);
            foreach (var file in files)
            {
                var text = File.ReadAllText(file);
                AssetBundleDependecesSO_E soE = JsonUtility.FromJson<AssetBundleDependecesSO_E>(text);
                infos.AddRange(soE.assetInfos);
            }
                
            foreach (var info in infos)
            {
                AssetInfoSimplify assetInfoSimplify;
                assetInfoSimplify.hash = info.hash;
                assetInfoSimplify.ownerBundleHash = info.ownerBundleHash;
                assetInfoSimplify.asset = info.assetPath;
                    
                assetInfoDict[info.hash] = assetInfoSimplify;
            }
        }
        
        public override T LoadAsset<T>(int hash)
        {
            if (!assetInfoDict.TryGetValue(hash, out AssetInfoSimplify assetInfo)) return default;
            
            if (syncProxys.TryGetValue(hash, out ISyncProxy syncloader))
            {
                PrintSystem.LogWarning($"[FastModeLoaderManager] 加载了 {hash} path:{assetInfo.asset} 多次，注意需要对应卸载多次");
                syncloader.Addreference();

                if (syncloader is SyncAssetProxy_Fast<T> cachedLoader) return (cachedLoader).GetAsset();
                
                PrintSystem.LogError($"[FastModeLoaderManager] 加载的类型出错 {hash} path:{assetInfo.asset}，存在该类型被加载为其他类型，请检查加载 {typeof(T)}");
                return default;
            }
            else
            {
                SyncAssetProxy_Fast<T> proxyFast = new SyncAssetProxy_Fast<T>();
                proxyFast.SetInfoData(ref assetInfo);
                proxyFast.Begin();
                proxyFast.OnDestroy = OnDesotrySyncAsset;
                T reault = proxyFast.GetAsset();
                syncProxys.Add(hash, proxyFast);

                return reault;
            }
        }
        
        void OnDesotrySyncAsset(ISyncProxy proxy)
        {
            syncProxys.Remove(proxy.GetHash());
        }

        public override void Unload(int hash)
        {
            if (syncProxys.TryGetValue(hash, out ISyncProxy assetSyncProxy))
            {
                assetSyncProxy.DefReference();
            }
        }

    }
}