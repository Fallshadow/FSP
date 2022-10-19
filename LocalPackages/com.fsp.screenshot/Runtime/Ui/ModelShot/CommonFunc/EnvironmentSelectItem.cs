using System;
using fsp.CameraScheme;
using fsp.LittleSceneEnvironment;
using fsp.ui.utility;

namespace fsp.modelshot.ui
{
    public class EnvironmentSelectItem : ClickSelectUiItem
    {
        public LittleEnvironmentInfo EnvironmentData;
        
        private const int text_Name = 0;

        public void SetInfo(int index, LittleEnvironmentInfo data)
        {
            Index = index;
            EnvironmentData = data;
            GetText(text_Name).text = EnvironmentData.environmentName;
        }

        protected override void onRealClcik()
        {
            base.onRealClcik();
            LittleEnvironmentCreator.instance.SwitchToEnvironment(EnvironmentData.environmentName);
        }
    }
}