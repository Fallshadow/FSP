using System.Collections.Generic;
using fsp.modelshot.Game;
using fsp.ui.utility;
using UnityEngine;

namespace fsp.modelshot.ui
{
    public class CommonFuncMono : MonoBehaviour
    {
        public UiButton BackToMenu;
        public UiButton HideUi;
        public UiButton ScreenShot;
        public ChooseCameraToolPanel cameraPanel = null;
        public List<GameObject> HideUiGos = null;
        public string FileName = "New PNG";
        
        public void Initialize()
        {
            BackToMenu.onClick.AddListener(OpenDebugCanvas);
            HideUi.onClick.AddListener(HideUiGoFunc);
            ScreenShot.onClick.AddListener(CaptureScreenShot);
            cameraPanel?.Init();
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
            foreach (var HideUiGo in HideUiGos)
            {
                HideUiGo.SetActive(!HideUiGo.activeSelf);
            }
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