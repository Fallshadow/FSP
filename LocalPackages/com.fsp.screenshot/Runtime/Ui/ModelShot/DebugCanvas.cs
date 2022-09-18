using fsp.modelshot.Game;
using fsp.ui;
using UnityEngine.UI;

namespace fsp.modelshot.ui
{
    [BindingResource_ModelShot(UiAssetIndex.DebugCanvas)]
    public class DebugCanvas : FullScreenCanvasBase
    {
        public Button testBtn;

        public void OpenSelectModelCanvas()
        {
            GameController_ModelShot.instance.FSM.SwitchToState((int) ModelShotGameFsmState.ENTRY_SELECT_MODEL);
        }
        
        public override void Initialize()
        {
            testBtn.onClick.AddListener(OpenSelectModelCanvas);
        }

        public override void Release()
        {
            testBtn.onClick.RemoveListener(OpenSelectModelCanvas);
        }
    }
}