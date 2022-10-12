using System;
using fsp.CameraScheme;
using fsp.ui.utility;

namespace fsp.modelshot.ui
{
    public class CameraSelectItem : ClickSelectUiItem
    {
        public Action<int, CameraSchemeInfo> ClickCallBack;
        public CameraSchemeInfo CameraData;
        
        private const int text_Name = 0;
        private const int text_Value = 1;
        
        public void SetCamera(int index, CameraSchemeInfo data)
        {
            Index = index;
            CameraData = data;
            GetText(text_Name).text = CameraData.Name;
            GetText(text_Value).text =
                $"{CameraData.XValue:N2} : " +
                $"{CameraData.YValue:N2} : " +
                $"{CameraData.ZValue:N2} : " +
                $"{CameraData.FOVValue:N2} : " +
                $"{CameraData.SpeedValue:N2} : " +
                $"OrthoMode:{CameraData.OrthoMode}";
        }

        protected override void onRealClcik()
        {
            base.onRealClcik();
            ClickCallBack?.Invoke(Index, CameraData);
        }
    }
}