﻿using fsp.assetbundlecore;
using fsp.debug;

namespace fsp.ui
{
    public class UiResMgr : SingletonMonoBehaviorNoDestroy<UiResMgr>
    {
        public bool IsInitialized { get; private set; }
        
        protected virtual AssetHashMap_UI uiSO
        {
            get;
            set;
        }
        
        public int GetUiAssetInfoViaIndex(int index)
        {
            if (!IsInitialized) InitLoader();
            

            foreach (UiAssetInfo info in uiSO.UiAssetInfos)
            {
                if (info.Index == index)
                {
                    return info.HashCode;
                }
            }

            PrintSystem.LogError($"Can`t find UiAssetInfo : {index}");
            return -1;
        }
        
        private void InitLoader()
        {
            if (IsInitialized) return;
            
            loadAssetHashMap_UI();
            if (uiSO == null)
            {
                PrintSystem.LogError("Asset Mapping Info Load Failed");
                return;
            }
            uiSO.Initialize();
            IsInitialized = true;
        }

        protected virtual void loadAssetHashMap_UI()
        {
            int assetMapId = AssetHashDefine.UISO_ASSET_HASH_MAP;
            uiSO = ResourceLoaderProxy.instance.LoadAsset<AssetHashMap_UI>(assetMapId);
        }
    }
}