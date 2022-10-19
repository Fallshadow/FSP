using fsp.LittleSceneEnvironment;
using fsp.ui;
using UnityEngine;

namespace fsp.modelshot.ui
{
    [BindingResource_ModelShot(UiAssetIndex.SelectModelCanvas)]
    public class SelectModelCanvas : CaptureScreenShotCanvasBase
    {
        [Header("配置项")]
        public SelectModelActionPanel ActionPanel = null;
        public SelectAndInitModelPanel ModelPanel = null;
        public override void Initialize()
        {
            base.Initialize();
            ActionPanel.InitPanel(this);
            ModelPanel.InitPanel();
            DragArea.OnDragHandler += ModelPanel._freeScreenShot.RotateZeroTransY;
            ModelPanel.selectCallBackGO += o =>
            {
                ActionPanel.SetDisplayGO(o);
            };
            LittleEnvironmentCreator.instance.SwitchToEnvironment("环境——基础");
        }

        public override void Release()
        {
            base.Release();
            DragArea.OnDragHandler -= ModelPanel._freeScreenShot.RotateZeroTransY;
            ModelPanel.Release();
        }
    }
}