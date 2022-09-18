using fsp.assetbundlecore;
using fsp.ui;

namespace fsp.modelshot.ui
{
    public class UiResMge_ModelShot : UiResMgr
    {
        protected override void loadAssetHashMap_UI()
        {
            int assetMapId = AssetHashDefine.UISO_ASSET_HASH_MAP;
            uiSO = ResourceLoaderProxy.instance.LoadAsset<AssetHashMap_UI>(assetMapId);
        }
    }
}