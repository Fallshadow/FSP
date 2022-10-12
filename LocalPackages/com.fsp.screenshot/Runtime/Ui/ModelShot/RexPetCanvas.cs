using System.Collections.Generic;
using fsp.LittleSceneEnvironment;
using fsp.ObjectStylingDesigne;
using fsp.ui.utility;
using UnityEngine;
using UnityEngine.UI;

namespace fsp.modelshot.ui
{
    [BindingResource_ModelShot(UiAssetIndex.RexPetCanvas)]
    public class RexPetCanvas : CaptureScreenShotCanvasBase
    {
        [SerializeField] private Transform FashionPetGroupRoot = null;
        [SerializeField] private ModelViewerStringPathUiItem FashionPetGroupPrefab = null;
        private UiItemList<ObjectStringPath, ModelViewerStringPathUiItem> FashionPetGroupItems = null;
        private readonly List<ObjectStringPath> FashionPetGroupDatas = new List<ObjectStringPath>();
        private int curFashionPetGroupIndex = -1;

        [SerializeField] private Transform FashionPetGroupDatasRoot = null;
        [SerializeField] private ModelViewerStringPathUiItem FashionPetGroupDatasPrefab = null;
        private UiItemList<ObjectStringPath, ModelViewerStringPathUiItem> FashionPetGroupDatasItems = null;

        private ObjectStylingStrategyRexEditorPet _rexEditorFashionPet = null;

        public override void Initialize()
        {
            base.Initialize();
            _rexEditorFashionPet = (ObjectStylingStrategyRexEditorPet) ObjectStylingDesigner.instance.CreateOrGetStrategy(ObjectStylingType.RexEditor_Pet_Library);
            initFashionPetGroup();
            initFashionPetGroupDatas();
        }

        public override void Release()
        {
            base.Release();
            ObjectStylingDesigner.instance.ReleaseStrategy(ObjectStylingType.RexEditor_Pet_Library);
        }

        protected override void onShow()
        {
            base.onShow();
            FashionPetGroupItems.UpdateItems(FashionPetGroupDatas);
        }

        private void initFashionPetGroup()
        {
            FashionPetGroupItems = new UiItemList<ObjectStringPath, ModelViewerStringPathUiItem>(
                FashionPetGroupPrefab,
                FashionPetGroupRoot,
                item => { item.GetButton(0).onClick.AddListener(() => { clickFashionPetGroupBtn(item.Data, item.Index); }); },
                (index, data, item) => { item.UpdateItem(index, data); }
            );
            FashionPetGroupDatas.Clear();
            foreach (var subStrategyName in _rexEditorFashionPet.SubStrategyNames)
            {
                FashionPetGroupDatas.Add(new ObjectStringPath()
                {
                    FilterName = subStrategyName,
                    FilePath = ""
                });
            }
        }

        private void initFashionPetGroupDatas()
        {
            FashionPetGroupDatasItems = new UiItemList<ObjectStringPath, ModelViewerStringPathUiItem>(
                FashionPetGroupDatasPrefab,
                FashionPetGroupDatasRoot,
                item => { item.GetButton(0).onClick.AddListener(() => { clickFashionPetGroupDataBtn(item.Data, item.Index); }); },
                (index, data, item) => { item.UpdateItem(index, data); }
            );
        }

        private void clickFashionPetGroupBtn(ObjectStringPath data, int index)
        {
            FashionPetGroupDatasRoot.SetActive(curFashionPetGroupIndex != index);
            curFashionPetGroupIndex = curFashionPetGroupIndex != index ? index : -1;
            LittleEnvironmentCreator.instance.SwitchToEnvironment("环境——宠物");
            // 显示group下的所有物件按钮
            switch (index)
            {
                case 0: FashionPetGroupDatasItems.UpdateItems(_rexEditorFashionPet.ObjectNameList_0_AiYing );break;
                case 1: FashionPetGroupDatasItems.UpdateItems(_rexEditorFashionPet.ObjectNameList_1_MoGu   );break;
                case 2: FashionPetGroupDatasItems.UpdateItems(_rexEditorFashionPet.ObjectNameList_2_Wolf   );break;
                case 3: FashionPetGroupDatasItems.UpdateItems(_rexEditorFashionPet.ObjectNameList_3_Humman );break;
            }

            FashionPetGroupDatasRoot.SetSiblingIndex(index + 2);

            for (int numIndex = 0; numIndex < FashionPetGroupItems.Count; numIndex++)
            {
                FashionPetGroupItems[numIndex].ShowApply(index);
            }

            FashionPetGroupRoot.GetComponent<RectTransform>()?.ForceUpdateRectTransforms();
            FashionPetGroupRoot.GetComponent<ContentSizeFitter>()?.SetLayoutVertical();
        }

        private void clickFashionPetGroupDataBtn(ObjectStringPath data, int index)
        {
            _rexEditorFashionPet.ApplySubStrategy(0);
            _rexEditorFashionPet.LoadObject(data.FilePath);
            
            for (int numIndex = 0; numIndex < FashionPetGroupDatasItems.Count; numIndex++)
            {
                FashionPetGroupDatasItems[numIndex].ShowApply(index);
            }
        }
    }
}