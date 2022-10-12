using System.Collections.Generic;
using fsp.LittleSceneEnvironment;
using fsp.ObjectStylingDesigne;
using fsp.ui.utility;
using UnityEngine;
using UnityEngine.UI;

namespace fsp.modelshot.ui
{
    [BindingResource_ModelShot(UiAssetIndex.RexFashionPendantCanvas)]
    public class RexFashionPendantCanvas : CaptureScreenShotCanvasBase
    {
        [SerializeField] private Transform FashionPendantGroupRoot = null;
        [SerializeField] private ModelViewerStringPathUiItem FashionPendantGroupPrefab = null;
        private UiItemList<ObjectStringPath, ModelViewerStringPathUiItem> FashionPendantGroupItems = null;
        private readonly List<ObjectStringPath> FashionPendantGroupDatas = new List<ObjectStringPath>();
        private int curFashionPendantGroupIndex = -1;

        [SerializeField] private Transform FashionPendantGroupDatasRoot = null;
        [SerializeField] private ModelViewerStringPathUiItem FashionPendantGroupDatasPrefab = null;
        private UiItemList<ObjectStringPath, ModelViewerStringPathUiItem> FashionPendantGroupDatasItems = null;

        private ObjectStylingStrategyRexEditorPendant _rexEditorFashionPendant = null;

        public override void Initialize()
        {
            base.Initialize();
            _rexEditorFashionPendant = (ObjectStylingStrategyRexEditorPendant) ObjectStylingDesigner.instance.CreateOrGetStrategy(ObjectStylingType.RexEditor_Fashion_Pendant_Library);
            initFashionPendantGroup();
            initFashionPendantGroupDatas();
        }

        public override void Release()
        {
            base.Release();
            ObjectStylingDesigner.instance.ReleaseStrategy(ObjectStylingType.RexEditor_Fashion_Pendant_Library);
        }

        protected override void onShow()
        {
            base.onShow();
            FashionPendantGroupItems.UpdateItems(FashionPendantGroupDatas);
        }

        private void initFashionPendantGroup()
        {
            FashionPendantGroupItems = new UiItemList<ObjectStringPath, ModelViewerStringPathUiItem>(
                FashionPendantGroupPrefab,
                FashionPendantGroupRoot,
                item => { item.GetButton(0).onClick.AddListener(() => { clickFashionPendantGroupBtn(item.Data, item.Index); }); },
                (index, data, item) => { item.UpdateItem(index, data); }
            );
            FashionPendantGroupDatas.Clear();
            foreach (var subStrategyName in _rexEditorFashionPendant.SubStrategyNames)
            {
                FashionPendantGroupDatas.Add(new ObjectStringPath()
                {
                    FilterName = subStrategyName,
                    FilePath = ""
                });
            }
        }

        private void initFashionPendantGroupDatas()
        {
            FashionPendantGroupDatasItems = new UiItemList<ObjectStringPath, ModelViewerStringPathUiItem>(
                FashionPendantGroupDatasPrefab,
                FashionPendantGroupDatasRoot,
                item => { item.GetButton(0).onClick.AddListener(() => { clickFashionPendantGroupDataBtn(item.Data, item.Index); }); },
                (index, data, item) => { item.UpdateItem(index, data); }
            );
        }

        private void clickFashionPendantGroupBtn(ObjectStringPath data, int index)
        {
            FashionPendantGroupDatasRoot.SetActive(curFashionPendantGroupIndex != index);
            curFashionPendantGroupIndex = curFashionPendantGroupIndex != index ? index : -1;
            LittleEnvironmentCreator.instance.SwitchToEnvironment("环境——时装挂件");
            // 显示group下的所有物件按钮
            switch (index)
            {
                case 0: FashionPendantGroupDatasItems.UpdateItems(_rexEditorFashionPendant.ObjectNameList_0_Hang);break;
                case 1: FashionPendantGroupDatasItems.UpdateItems(_rexEditorFashionPendant.ObjectNameList_1_Head);break;
                case 2: FashionPendantGroupDatasItems.UpdateItems(_rexEditorFashionPendant.ObjectNameList_2_Back);break;
                case 3: FashionPendantGroupDatasItems.UpdateItems(_rexEditorFashionPendant.ObjectNameList_3_Tail);break;
            }

            FashionPendantGroupDatasRoot.SetSiblingIndex(index + 2);

            for (int numIndex = 0; numIndex < FashionPendantGroupItems.Count; numIndex++)
            {
                FashionPendantGroupItems[numIndex].ShowApply(index);
            }

            FashionPendantGroupRoot.GetComponent<RectTransform>()?.ForceUpdateRectTransforms();
            FashionPendantGroupRoot.GetComponent<ContentSizeFitter>()?.SetLayoutVertical();
        }

        private void clickFashionPendantGroupDataBtn(ObjectStringPath data, int index)
        {
            if (data.FilterName.Contains("hang")) _rexEditorFashionPendant.ApplySubStrategy(0);
            if (data.FilterName.Contains("head")) _rexEditorFashionPendant.ApplySubStrategy(1);
            if (data.FilterName.Contains("back")) _rexEditorFashionPendant.ApplySubStrategy(2);
            if (data.FilterName.Contains("tail")) _rexEditorFashionPendant.ApplySubStrategy(3);
            _rexEditorFashionPendant.LoadObject(data.FilePath);
            
            for (int numIndex = 0; numIndex < FashionPendantGroupDatasItems.Count; numIndex++)
            {
                FashionPendantGroupDatasItems[numIndex].ShowApply(index);
            }
        }
    }
}