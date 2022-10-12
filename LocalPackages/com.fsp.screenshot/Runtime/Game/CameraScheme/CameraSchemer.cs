using System;
using System.Collections.Generic;
using DG.DemiEditor;
using UnityEditor;
using UnityEngine;

namespace fsp.CameraScheme
{
    public class CameraSchemer : SingletonMonoBehavior<CameraSchemer>
    {
        public Camera MainCamera = null;
        public Transform MainCameraTrans = null;

        public CameraSchemeSO CameraSOData => CameraSchemeSO.Instance;

        private void Update()
        {
            if (MainCamera != null && MainCameraTrans != null) return;
            MainCamera = Camera.main;
            if (MainCamera != null) MainCameraTrans = MainCamera.transform;
        }

        public void SetX(float value) { MainCameraTrans.position = MainCameraTrans.position.SetX(value); }
        public void SetY(float value) { MainCameraTrans.position = MainCameraTrans.position.SetY(value); }
        public void SetZ(float value) { MainCameraTrans.position = MainCameraTrans.position.SetZ(value); }
        public void SetFov(float value) { MainCamera.fieldOfView = value; }

        public void SearchData(string searchName, out CameraSchemeInfo result)
        {
            result = null;
            if (searchName.IsNullOrEmpty()) return;
            
            foreach (CameraSchemeInfo debugRotate in CameraSOData.M_LightDatas)
            {
                if (!debugRotate.Name.Equals(searchName, StringComparison.Ordinal)) continue;
                result = debugRotate;
            }
        }
        
        public void SearchData(string searchName, ref List<CameraSchemeInfo> results)
        {
            if (searchName.IsNullOrEmpty())
            {
                results = CameraSOData.M_LightDatas;
                return;
            }

            results.Clear();
            foreach (CameraSchemeInfo debugRotate in CameraSOData.M_LightDatas)
            {
                if (!debugRotate.Name.Contains(searchName)) continue;
                results.Add(debugRotate);
            }
        }

        public void AddData(CameraSchemeInfo newData)
        {
            CameraSOData.M_LightDatas.Add(newData);
        }

        public void DeleteData(string delName)
        {
            if (delName.IsNullOrEmpty()) return;
            int index = -1;
            foreach (var debugRotate in CameraSOData.M_LightDatas)
            {
                if (debugRotate.Name != delName) continue;
                index = CameraSOData.M_LightDatas.IndexOf(debugRotate);
            }
            if (index != -1) CameraSOData.M_LightDatas.RemoveAt(index);
        }

        public void SaveData(string onlyName, CameraSchemeInfo newData, out int index)
        {
            index = -1;
            foreach (var debugRotate in CameraSOData.M_LightDatas)
            {
                if (debugRotate.Name != onlyName) continue;
                index = CameraSOData.M_LightDatas.IndexOf(debugRotate);
            }

            if (index == -1) return;
            CameraSOData.M_LightDatas[index] = newData;

            EditorUtility.SetDirty(CameraSOData);
            AssetDatabase.SaveAssets();
        }
    }
}