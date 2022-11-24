using System.Collections.Generic;
using fsp.modelshot.Game;
using fsp.ui.utility;
using Ludiq;
using UnityEngine;

namespace fsp.modelshot.ui
{
    public class CommonFuncMono : MonoBehaviour
    {
        private MonoBehaviour parent;
        public UiButton BackToMenu;
        public UiButton HideUi;
        public UiButton ScreenShot;
        public ChooseCameraToolPanel cameraPanel = null;
        public SelectModelActionPanel ActionPanel = null;
        public ChooseEnvironmentToolPanel EnvironmentToolPanel = null;

        public CanvasGroup parentCanvasGroup => parent.GetOrAddComponent<CanvasGroup>();
        private bool cacheAlphaSwitch = true;
        public string FileName = "New PNG";
        
        public void Initialize(MonoBehaviour parentP)
        {
            parent = parentP;
            BackToMenu.onClick.AddListener(OpenDebugCanvas);
            HideUi.onClick.AddListener(HideUiGoFunc);
            ScreenShot.onClick.AddListener(CaptureScreenShot);
            cameraPanel?.Init();
            ActionPanel?.InitPanel();
            EnvironmentToolPanel?.Init();
        }

        public void Release()
        {
            BackToMenu.onClick.RemoveListener(OpenDebugCanvas);
            HideUi.onClick.RemoveListener(HideUiGoFunc);
            ScreenShot.onClick.RemoveListener(CaptureScreenShot);
        }
        
        public void OpenDebugCanvas()
        {
            GameController_ModelShot.instance.FSM.SwitchToState((int) ModelShotGameFsmState.LOAD_ENTRY);
        }
        
        public void HideUiGoFunc()
        {
            parentCanvasGroup.alpha = cacheAlphaSwitch ? 1 : 0;
            cacheAlphaSwitch = !cacheAlphaSwitch;
        }
        
        public void CaptureScreenShot()
        {
            DebugCameraControl.instance.ExportCaptureScreenShot(FileName);
        }
        
        public void ApplyData(string fullName)
        {
            cameraPanel.ApplyData(fullName);
        }
    }
}