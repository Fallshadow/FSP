using System;
using System.Collections.Generic;
using fsp.CameraScheme;
using fsp.evt;
using fsp.LittleSceneEnvironment;
using fsp.ui.utility;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace fsp.modelshot.ui
{
    public class ChooseEnvironmentToolPanel : MonoBehaviour
    {
        public UiButton HideOrShowPanelButton = null;
        public GameObject HideOrShowPanelGO = null;
        public EnvironmentSelectItem DebugEnvironmentSelectItemPrefab = null;
        public Transform CameraSelectCellParent = null;

        private UiItemList<LittleEnvironmentInfo, EnvironmentSelectItem> CameraList = null;

        public virtual void Init()
        {
            CameraList = new UiItemList<LittleEnvironmentInfo, EnvironmentSelectItem>(DebugEnvironmentSelectItemPrefab, CameraSelectCellParent,
                (uiItem) => { },
                (index, data, uiItem) => { uiItem.SetInfo(index, data); });

            CameraList.UpdateItems(LittleEnvironmentSO.Instance.littleEnvironmentInfos);
            HideOrShowPanelButton.onClick.AddListener(hideOrShowPanelMethod);
        }
        
        private void hideOrShowPanelMethod()
        {
            HideOrShowPanelGO.SetActive(!HideOrShowPanelGO.activeSelf);
        }
    }
}