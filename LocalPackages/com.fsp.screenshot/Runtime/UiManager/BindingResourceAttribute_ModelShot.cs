namespace fsp.modelshot.ui
{
    public class BindingResource_ModelShotAttribute : fsp.ui.BindingResourceAttribute
    {
        public BindingResource_ModelShotAttribute(UiAssetIndex assetId) : base((fsp.ui.UiAssetIndex)(assetId))
        {
            AssetId = (int)assetId;
        }
    }
}