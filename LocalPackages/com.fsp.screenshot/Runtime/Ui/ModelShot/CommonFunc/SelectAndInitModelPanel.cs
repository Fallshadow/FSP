using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using fsp.ObjectStylingDesigne;
using fsp.ui.utility;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace fsp.modelshot.ui
{
    public class SelectAndInitModelPanel : MonoBehaviour
    {
        [Header("配置项")] 
        public UiButton chooseAndInit = null;
        public UiButton choosePathAndInit = null;
        public Action<GameObject> selectCallBackGO;

        [SerializeField] private Transform modelRoot = null;
        [SerializeField] private FreeModelViewerStringPathUiItem modelPrefab = null;
        private UiItemList<ObjectStringPath, FreeModelViewerStringPathUiItem> modelItems = null;
        
        public ObjectStylingStrategyFreeScreenShot _freeScreenShot = null;

        public void InitPanel()
        {
            _freeScreenShot = (ObjectStylingStrategyFreeScreenShot)ObjectStylingDesigner.instance.CreateOrGetStrategy(ObjectStylingType.Free_ScreenShot_Library);
            modelItems = new UiItemList<ObjectStringPath, FreeModelViewerStringPathUiItem>(
                modelPrefab,
                modelRoot,
                item =>
                {
                    item.DeleteCurIndex += deleteCurIndex;
                    item.SelectCurIndex += selectCurIndex;
                    item.AppearCurIndex += appearCurIndex;
                    item.DisAppearCurIndex += disAppearCurIndex;
                },
                (index, data, item) => { item.UpdateItem(index, data); }
            );

            chooseAndInit.onClick.AddListener(() => { ChooseFileAndInit(); });
            choosePathAndInit.onClick.AddListener(() => { ChoosePathAndInit(); });
        }

        private void deleteCurIndex(ObjectStringPath path)
        {
            _freeScreenShot.DestoryObjectByData(path);
            modelItems.UpdateItems(_freeScreenShot.ObjectNameList);
        }

        private void selectCurIndex(ObjectStringPath path,int index)
        {
            GameObject go = _freeScreenShot.GetObjectByData(path);
            if (go == null) return;
            selectCallBackGO?.Invoke(go);
            for (int numIndex = 0; numIndex < modelItems.Count; numIndex++)
            {
                modelItems[numIndex].ShowApply(index);
            }
        }

        private void disAppearCurIndex(ObjectStringPath path)
        {
            _freeScreenShot.DestoryObjectByData(path);
        }
        
        private void appearCurIndex(ObjectStringPath path)
        {
            _freeScreenShot.RealLoadObject(path);
        }

        public void ChooseFileAndInit()
        {
            string[] type = {"Prefab", "prefab", "FBX", "fbx"};
            string filePath = EditorUtility.OpenFilePanelWithFilters("选择模型预制体", "", type);
            filePath = filePath.Replace(Application.dataPath, "");
            filePath = "Assets" + filePath;
            
            _freeScreenShot.FakeLoadObject(filePath);
            modelItems.UpdateItems(_freeScreenShot.ObjectNameList);
        }

        public void ChoosePathAndInit()
        {
            string path = EditorUtility.OpenFolderPanel("选择模型文件夹", Application.dataPath, "ResourceRex");
            Debug.Log($"选择的文件夹 path {path}");
            string[] actionMClipNames = Directory.GetFiles(path);
            foreach (var mClipName in actionMClipNames)
            {
                string filePath = mClipName.Replace(Application.dataPath, "");
                filePath = "Assets" + filePath;
                
                _freeScreenShot.FakeLoadObject(filePath);
            }

            modelItems.UpdateItems(_freeScreenShot.ObjectNameList);
        }

        public void Release()
        {
            _freeScreenShot.DestoryAllObjects();
        }
    }
}