using System.Collections;
using System.Collections.Generic;
using System.IO;
using fsp.ui.utility;
using UnityEditor;
using UnityEngine;

namespace fsp.modelshot.ui
{
    public class SelectModelActionPanel : MonoBehaviour
    {
        [Header("选择动作")] public ItemActionPlay ActionPlayProcess = null;
        public ClickSelectUiItem_Action DebugActionSelectItemPrefab = null;
        public Transform ActionSelectCellParent = null;
        public GameObject actionPanel = null;
        
        private UiItemList<AnimationClip, ClickSelectUiItem_Action> actionList = null;
        private List<AnimationClip> cClips = new List<AnimationClip>();
        private AnimationClip cCilp;

        [Header("观察项")] public AnimationClip curActionClip = null;
        public GameObject DisplayGO = null;
        public SelectModelCanvas parent;

        public void InitPanel(SelectModelCanvas parent)
        {
            this.parent = parent;
            actionList = new UiItemList<AnimationClip, ClickSelectUiItem_Action>(DebugActionSelectItemPrefab, ActionSelectCellParent,
                (uiItem) => { uiItem.ClickCallBack = clickAction; },
                (index, data, uiItem) => { uiItem.SetActionInfo(index, data); });
            ActionPlayProcess.Init(this);
        }
        
        public void InitPanel()
        {
            actionList = new UiItemList<AnimationClip, ClickSelectUiItem_Action>(DebugActionSelectItemPrefab, ActionSelectCellParent,
                (uiItem) => { uiItem.ClickCallBack = clickAction; },
                (index, data, uiItem) => { uiItem.SetActionInfo(index, data); });
            ActionPlayProcess.Init(this);
        }

        public void SetDisplayGO(GameObject go)
        {
            DisplayGO = go;
            if (curActionClip != null && DisplayGO != null)
            {
                ActionPlayProcess.ResetAcitonPlay(curActionClip, DisplayGO);
            }
        }

        public void ChooseFolder()
        {
            string path = EditorUtility.OpenFolderPanel("选择动作文件夹", "", "");
            Debug.Log($"选择的文件夹 path {path}");
            cClips.Clear();
            LoadFolder(path);
        }

        public void LoadFolder(string path)
        {
            string[] actionMClipNames = Directory.GetFiles(path);
            AnimationClip anim;
            foreach (var mClipName in actionMClipNames)
            {
                string assetPath = mClipName.Replace(Application.dataPath, "");
                assetPath = "Assets" + assetPath;
                anim = AssetDatabase.LoadAssetAtPath<AnimationClip>(assetPath);
                if (anim != null)
                {
                    cClips.Add(anim);
                }
            }

            actionList.UpdateItems(cClips);
        }

        public void ClearClips()
        {
            cClips.Clear();
            actionList.UpdateItems(cClips);
        }

        public void ChooseFile()
        {
            string filePath = EditorUtility.OpenFilePanel("选择动作文件", "", "anim");
            filePath = filePath.Replace(Application.dataPath, "");
            filePath = "Assets" + filePath;
            AnimationClip anim = UnityEditor.AssetDatabase.LoadAssetAtPath<AnimationClip>(filePath);
            if (anim != null)
            {
                cCilp = anim;
            }
        }

        // 点击动作
        private void clickAction(int id, AnimationClip actionData)
        {
            if (DisplayGO == null)
            {
                return;
            }

            curActionClip = actionData;
            ActionPlayProcess.ResetAcitonPlay(curActionClip, DisplayGO);
        }

        // 点击单个动作
        private void clickOneAction()
        {
            if (DisplayGO == null)
            {
                return;
            }

            curActionClip = cCilp;
            ActionPlayProcess.ResetAcitonPlay(curActionClip, DisplayGO);
        }

        public void HideOrShowActionPanel()
        {
            actionPanel.SetActive(!actionPanel.activeSelf);
        }
    }
}