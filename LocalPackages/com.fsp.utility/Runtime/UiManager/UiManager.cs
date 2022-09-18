using System;
using System.Collections.Generic;
using fsp.assetbundlecore;
using fsp.debug;
using UnityEngine;

namespace fsp.ui
{
    public partial class UiManager : SingletonMonoBehavior<UiManager>
    {
        public const int VisibleUiLayer = 5;
        
        [SerializeField] private RectTransform[] canvasRoots = null;
        public RectTransform MainRoot => manageStrat.MainRoot;

        private Dictionary<int, UiBase> loadedUiDict = new Dictionary<int, UiBase>();
        private List<int> destroyUiIds = new List<int>();
        private UiManageStrategy manageStrat = null;
        
        private List<int> assetHashCodes = new List<int>(); //通过GetUiPrefab()加载的资源hashcode
        
        protected override void init()
        {
            manageStrat = new UiManageStrategy(canvasRoots);
        }
        
        public T OpenUi<T>(Action completeCb = null) where T : UiBase
        {
            Type uiType = typeof(T);
            int uiAssetIndex = GetUiAssetIndex(uiType);
            loadedUiDict.TryGetValue(uiAssetIndex, out UiBase ui);
            if (ui == null)
            {
                ui = CreateUi<T>();
            }

            manageStrat.OpenUi(ui, completeCb);
            return ui as T;
        }

        public T CreateUi<T>() where T : UiBase
        {
            Type uiType = typeof(T);
            int uiAssetIndex = GetUiAssetIndex(uiType);
            if (loadedUiDict.TryGetValue(uiAssetIndex, out UiBase ui))
            {
                return ui as T;
            }

            getUiViaType<T>(uiType, out ui);
            if (ui == null)
            {
                return null;
            }

            ui = manageStrat.CreateUi(ui);
            ui.OnCreate();
            loadedUiDict[uiAssetIndex] = ui;
            return ui as T;
        }
        
        private void getUiViaType<T>(Type type, out UiBase ui) where T : UiBase
        {
            int assetId = GetUiAssetIndex(type);
            int hashCode = UiResMgr.instance.GetUiAssetInfoViaIndex(assetId);
            if (hashCode == -1)
            {
                ui = null;
                PrintSystem.LogError($"[UiManager] Load AB failed. Asset:{assetId}---- Can`t find hashCode!");
                return;
            }

            GameObject prefab = ResourceLoaderProxy.instance.LoadAsset<GameObject>(hashCode);
            if (prefab == null)
            {
                PrintSystem.LogError($"[UiManager] Load AB failed. Asset HashCode: {hashCode}");
                ui = null;
                return;
            }

            ui = prefab.GetComponent<T>();
            if (ui == null)
            {
                PrintSystem.LogError($"[UiManager] Get Component Error. Asset HashCode : {hashCode}, prefab:{prefab} type:{typeof(T)}");
                return;
            }
        }
        
        public int GetUiAssetIndex(Type type)
        {
            if (Attribute.GetCustomAttribute(type, typeof(BindingResourceAttribute)) is BindingResourceAttribute attr) return attr.AssetId;
            PrintSystem.LogError($"{type} 的UiAssetIndex不存在，请检查");
            return -1;
        }
        
        public void DestroyAllUi(Type reopenOne = null)
        {
            StopAllCoroutines();
            
            manageStrat.Clear(null);
            destroyUiIds.Clear();
            foreach (KeyValuePair<int, UiBase> kvPair in loadedUiDict)
            {
                if (kvPair.Value == null)
                {
                    continue;
                }

                destroyUiIds.Add(kvPair.Key);
                kvPair.Value.OnRuin();
                Destroy(kvPair.Value.gameObject);
                unloadAsset(kvPair.Key);
            }

            for (int i = 0, count = destroyUiIds.Count; i < count; ++i)
            {
                loadedUiDict.Remove(destroyUiIds[i]);
            }
            unloadAllAssets();
        }
        
        private void unloadAsset(int uiAssetIndex)
        {
            int hashCode = UiResMgr.instance.GetUiAssetInfoViaIndex(uiAssetIndex);
            ResourceLoaderProxy.instance.Unload(hashCode);
        }
        
        private void unloadAllAssets()
        {
            foreach (int hashCode in assetHashCodes)
            {
                ResourceLoaderProxy.instance.Unload(hashCode);
            }
            assetHashCodes.Clear();
        }
    }
}