using fsp.assetbundlecore;
using fsp.ui;
using fsp.utility;

namespace fsp.modelshot.ui
{
    public class UiResMge_ModelShot : UiResMgr
    {
        protected override void loadAssetHashMap_UI()
        {
            uiSO = ResourceLoaderProxy.instance.LoadAsset<AssetHashMap_UI>(data.ResourcesPathSetting.ASSETHASHMAP_UI_VIRTUAL_FILE_PATH);
        }
    }
}