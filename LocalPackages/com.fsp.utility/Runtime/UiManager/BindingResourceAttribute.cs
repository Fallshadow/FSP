using System;

namespace fsp.ui
{
    public class BindingResourceAttribute : Attribute
    {
        public int AssetId { get; protected set; }

        public BindingResourceAttribute(UiAssetIndex assetId)
        {
            AssetId = (int)assetId;
        }
    }
}