using fsp.ObjectStylingDesigne;
using fsp.ui;
using UnityEngine;

namespace fsp.modelshot.ui
{
    [BindingResource_ModelShot(UiAssetIndex.SelectModelCanvas)]
    public class SelectModelCanvas : FullScreenCanvasBase
    {
        [Header("配置项")]
        public SelectModelActionPanel ActionPanel = null;
        public SelectAndInitModelPanel ModelPanel = null;
        public override void Initialize()
        {
            base.Initialize();
            ActionPanel.InitPanel(this);
            ModelPanel.InitPanel();
            ModelPanel.selectCallBackGO += o =>
            {
                ActionPanel.SetDisplayGO(o);
            };
        }
    }
}