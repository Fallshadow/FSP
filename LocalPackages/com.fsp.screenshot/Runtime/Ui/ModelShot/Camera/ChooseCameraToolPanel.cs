using System;
using System.Collections.Generic;
using fsp.CameraScheme;
using fsp.evt;
using fsp.ui.utility;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace fsp.modelshot.ui
{
    public class ChooseCameraToolPanel : MonoBehaviour
    {
        [Header("配置项")] public InputField M_InputField = null;
        public UiButton M_Search = null;
        public UiButton M_Add = null;
        public UiButton M_Delete = null;
        public UiButton M_Save = null;

        public EditorSliderInput XValue = null;
        public EditorSliderInput YValue = null;
        public EditorSliderInput ZValue = null;
        public EditorSliderInput FOVValue = null;
        public EditorSliderInput SpeedValue = null;
        public BtnChangeTextInput OrthoModeValue = null;

        public float PosMin = -100;
        public float PosMax = 100;
        public float FovMin = 1;
        public float FovMax = 179;
        public float SpeedMin = 0;
        public float SpeedMax = 20;

        public UiButton HideOrShowPanelButton = null;
        public GameObject HideOrShowPanelGO = null;

        private List<CameraSchemeInfo> tempSearch = new List<CameraSchemeInfo>();
        private CameraSchemeInfo curSelectData = new CameraSchemeInfo();

        [Header("选择相机方案")] public CameraSelectItem DebugCameraSelectItemPrefab = null;
        public Transform CameraSelectCellParent = null;

        private UiItemList<CameraSchemeInfo, CameraSelectItem> CameraList = null;

        public virtual void Init()
        {
            M_Search.onClick.AddListener(SearchData);
            M_Add.onClick.AddListener(AddData);
            M_Delete.onClick.AddListener(DeleteData);
            M_Save.onClick.AddListener(SaveData);

            CameraList = new UiItemList<CameraSchemeInfo, CameraSelectItem>(DebugCameraSelectItemPrefab, CameraSelectCellParent,
                (uiItem) => { uiItem.ClickCallBack = clickCamera; },
                (index, data, uiItem) => { uiItem.SetCamera(index, data); });
            if (CameraSchemer.instance.CameraSOData.M_LightDatas.Count > 0)
            {
                curSelectData = CameraSchemer.instance.CameraSOData.M_LightDatas[0];
            }

            CameraList.UpdateItems(CameraSchemer.instance.CameraSOData.M_LightDatas);

            var position = CameraSchemer.instance.MainCameraTrans.position;
            XValue.Init(PosMin, PosMax, value => { CameraSchemer.instance.SetX(value); }, value => { CameraSchemer.instance.SetX(float.Parse(value)); }, position.x);
            YValue.Init(PosMin, PosMax, value => { CameraSchemer.instance.SetY(value); }, value => { CameraSchemer.instance.SetY(float.Parse(value)); }, position.y);
            ZValue.Init(PosMin, PosMax, value => { CameraSchemer.instance.SetZ(value); }, value => { CameraSchemer.instance.SetZ(float.Parse(value)); }, position.z);
            FOVValue.Init(FovMin, FovMax, value => { CameraSchemer.instance.SetFov(value); }, value => { CameraSchemer.instance.SetFov(float.Parse(value)); },
                CameraSchemer.instance.MainCamera.fieldOfView);
            SpeedValue.Init(SpeedMin, SpeedMax, value => { DebugCameraControl.instance.ViewCameraSpeed = value; }, value => { DebugCameraControl.instance.ViewCameraSpeed = float.Parse(value); },
                DebugCameraControl.instance.ViewCameraSpeed);
            OrthoModeValue.Init("正交模式", "透视模式", (nameValue) => { CameraSchemer.instance.MainCamera.orthographic = string.Equals(nameValue, "正交模式", StringComparison.Ordinal); },
                CameraSchemer.instance.MainCamera.orthographic ? "正交模式" : "透视模式");

            HideOrShowPanelButton.onClick.AddListener(hideOrShowPanelMethod);
            EventManager.instance.Register(EventGroup.CAMERA, (short) CameraEvent.DEBUG_CAMERA_MOVE, SetCameraMove);
        }

        private void OnDestroy()
        {
            EventManager.instance.Unregister(EventGroup.CAMERA, (short) CameraEvent.DEBUG_CAMERA_MOVE, SetCameraMove);
        }

        private void hideOrShowPanelMethod()
        {
            HideOrShowPanelGO.SetActive(!HideOrShowPanelGO.activeSelf);
        }

        public void SetCameraMove()
        {
            var position = CameraSchemer.instance.MainCameraTrans.position;
            XValue.SetValue(position.x);
            YValue.SetValue(position.y);
            ZValue.SetValue(position.z);
        }

        public void ApplyData(string fullName)
        {
            CameraSchemer.instance.SearchData(fullName, out CameraSchemeInfo data);
            if (data != null) clickCamera(0, data);
        }

        public void SearchData()
        {
            string searchText = M_InputField.text;
            if (searchText == "")
            {
                CameraList.UpdateItems(CameraSchemer.instance.CameraSOData.M_LightDatas);
            }
            else
            {
                CameraSchemer.instance.SearchData(searchText, ref tempSearch);
                CameraList.UpdateItems(tempSearch);
            }

            for (int index = 0; index < CameraList.Count; index++)
            {
                if (CameraList[index].CameraData.Name != curSelectData.Name) continue;
                CameraList[index].DoClick();
                break;
            }
        }

        public void AddData()
        {
            if (M_InputField.text == "") return;


            for (int index = 0; index < CameraList.Count; index++)
            {
                if (CameraList[index].CameraData.Name != M_InputField.text) continue;
                return;
            }

            CameraSchemeInfo newData = new CameraSchemeInfo()
            {
                Name = M_InputField.text,
                XValue = XValue.GetValue(),
                YValue = YValue.GetValue(),
                ZValue = ZValue.GetValue(),
                FOVValue = FOVValue.GetValue(),
                SpeedValue = SpeedValue.GetValue(),
                OrthoMode = OrthoModeValue.GetValue() == "正交模式",
            };

            CameraSchemer.instance.AddData(newData);
            CameraList.UpdateItems(CameraSchemer.instance.CameraSOData.M_LightDatas);
            CameraList[CameraList.Count - 1].DoClick();
        }

        public void DeleteData()
        {
            string delName = curSelectData.Name;
            if (delName == "") return;

            CameraSchemer.instance.DeleteData(delName);
            CameraList.UpdateItems(CameraSchemer.instance.CameraSOData.M_LightDatas);
            CameraList[0].DoClick();
        }

        public void SaveData()
        {
            CameraSchemeInfo newData = new CameraSchemeInfo()
            {
                Name = curSelectData.Name,
                XValue = XValue.GetValue(),
                YValue = YValue.GetValue(),
                ZValue = ZValue.GetValue(),
                FOVValue = FOVValue.GetValue(),
                SpeedValue = SpeedValue.GetValue(),
                OrthoMode = OrthoModeValue.GetValue() == "正交模式",
            };

            CameraSchemer.instance.SaveData(curSelectData.Name, newData, out int index);
            CameraList.UpdateItems(CameraSchemer.instance.CameraSOData.M_LightDatas);
            CameraList[index].DoClick();
        }

        private void clickCamera(int index, CameraSchemeInfo data)
        {
            XValue.SetValue(data.XValue);
            YValue.SetValue(data.YValue);
            ZValue.SetValue(data.ZValue);
            FOVValue.SetValue(data.FOVValue);
            SpeedValue.SetValue(data.SpeedValue);
            OrthoModeValue.SetValue(data.OrthoMode ? "正交模式" : "透视模式");
            curSelectData = data;
        }
    }
}