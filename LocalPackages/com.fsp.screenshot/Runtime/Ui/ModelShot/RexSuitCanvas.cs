using System.Collections.Generic;
using fsp.LittleSceneEnvironment;
using fsp.ObjectStylingDesigne;
using fsp.ui.utility;
using UnityEngine;
using UnityEngine.UI;

namespace fsp.modelshot.ui
{
    [BindingResource_ModelShot(UiAssetIndex.RexSuitCanvas)]
    public class RexSuitCanvas : CaptureScreenShotCanvasBase
    {
        [SerializeField] private Transform SuitGroupRoot = null;
        [SerializeField] private ModelViewerStringPathUiItem SuitGroupPrefab = null;
        private UiItemList<ObjectStringPath, ModelViewerStringPathUiItem> SuitGroupItems = null;
        private readonly List<ObjectStringPath> SuitGroupDatas = new List<ObjectStringPath>();
        private int curSuitGroupIndex = -1;
        
        [SerializeField] private Transform SuitGroupDatasRoot = null;
        [SerializeField] private ModelViewerStringPathUiItem SuitGroupDatasPrefab = null;
        private UiItemList<ObjectStringPath, ModelViewerStringPathUiItem> SuitGroupDatasItems = null;
        
        private ObjectStylingStrategyRexEditorSuit _rexEditorSuit = null;

        public override void Initialize()
        {
            base.Initialize();
            _rexEditorSuit = (ObjectStylingStrategyRexEditorSuit)ObjectStylingDesigner.instance.CreateOrGetStrategy(ObjectStylingType.RexEditor_Suit_Library);
            DragArea.OnDragHandler += _rexEditorSuit.RotateZeroTransY;
            initSuitGroup();
            initSuitGroupDatas();
        }

        public override void Release()
        {
            base.Release();
            DragArea.OnDragHandler -= _rexEditorSuit.RotateZeroTransY;
            ObjectStylingDesigner.instance.ReleaseStrategy(ObjectStylingType.RexEditor_Suit_Library);
        }

        protected override void onShow()
        {
            base.onShow();
            SuitGroupItems.UpdateItems(SuitGroupDatas);
        }

        private void initSuitGroup()
        {
            SuitGroupItems = new UiItemList<ObjectStringPath, ModelViewerStringPathUiItem>(
                SuitGroupPrefab,
                SuitGroupRoot,
                item =>
                {
                    item.GetButton(0).onClick.AddListener(() => { clickSuitGroupBtn(item.Data, item.Index); });
                },
                (index, data, item) => { item.UpdateItem(index, data); }
            );
            SuitGroupDatas.Clear();
            foreach (var subStrategyName in _rexEditorSuit.SubStrategyNames)
            {
                SuitGroupDatas.Add(new ObjectStringPath()
                {
                    FilterName = subStrategyName,
                    FilePath = ""
                });
            }
        }

        private void initSuitGroupDatas()
        {
            SuitGroupDatasItems = new UiItemList<ObjectStringPath, ModelViewerStringPathUiItem>(
                SuitGroupDatasPrefab,
                SuitGroupDatasRoot,
                item =>
                {
                    item.GetButton(0).onClick.AddListener(() => { clickSuitGroupDataBtn(item.Data, item.Index); });
                },
                (index, data, item) => { item.UpdateItem(index, data); }
            );
        }

        private void clickSuitGroupBtn(ObjectStringPath data, int index)
        {
            SuitGroupDatasRoot.SetActive(curSuitGroupIndex != index);
            curSuitGroupIndex = curSuitGroupIndex != index ? index : -1;
            // 显示group下的所有物件按钮
            switch (index)
            {
                case 0: SuitGroupDatasItems.UpdateItems(_rexEditorSuit.ObjectNameList_0_Male  );break;
                case 1: SuitGroupDatasItems.UpdateItems(_rexEditorSuit.ObjectNameList_1_FeMale);break;
            }
            SuitGroupDatasRoot.SetSiblingIndex(index + 2);
            
            for (int numIndex = 0; numIndex < SuitGroupItems.Count; numIndex++)
            {
                SuitGroupItems[numIndex].ShowApply(index);
            }
            
            SuitGroupRoot.GetComponent<RectTransform>()?.ForceUpdateRectTransforms();
            SuitGroupRoot.GetComponent<ContentSizeFitter>()?.SetLayoutVertical();
            _rexEditorSuit.ApplySubStrategy(index);
        }

        private void clickSuitGroupDataBtn(ObjectStringPath data, int index)
        {
            _rexEditorSuit.LoadObject(data.FilePath);
            
            for (int numIndex = 0; numIndex < SuitGroupDatasItems.Count; numIndex++)
            {
                SuitGroupDatasItems[numIndex].ShowApply(index);
            }
        }
    }
}