using System.Collections.Generic;
using fsp.LittleSceneEnvironment;
using fsp.ObjectStylingDesigne;
using fsp.ui.utility;
using UnityEngine;
using UnityEngine.UI;

namespace fsp.modelshot.ui
{
    [BindingResource_ModelShot(UiAssetIndex.RexEquipmentCanvas)]
    public class RexEquipmentCanvas : CaptureScreenShotCanvasBase
    {
        [SerializeField] private Transform EquipmentGroupRoot = null;
        [SerializeField] private ModelViewerStringPathUiItem EquipmentGroupPrefab = null;
        private UiItemList<ObjectStringPath, ModelViewerStringPathUiItem> EquipmentGroupItems = null;
        private readonly List<ObjectStringPath> EquipmentGroupDatas = new List<ObjectStringPath>();
        private int curEquipmentGroupIndex = -1;

        [SerializeField] private Transform EquipmentGroupDatasRoot = null;
        [SerializeField] private ModelViewerStringPathUiItem EquipmentGroupDatasPrefab = null;
        private UiItemList<ObjectStringPath, ModelViewerStringPathUiItem> EquipmentGroupDatasItems = null;

        private ObjectStylingStrategyRexEditorEquipment _rexEditorEquipment = null;

        public override void Initialize()
        {
            base.Initialize();
            _rexEditorEquipment = (ObjectStylingStrategyRexEditorEquipment) ObjectStylingDesigner.instance.CreateOrGetStrategy(ObjectStylingType.RexEditor_Equipment_Library);
            DragArea.OnDragHandler += _rexEditorEquipment.RotateZeroTransY;
            initEquipmentGroup();
            initEquipmentGroupDatas();
        }

        public override void Release()
        {
            base.Release();
            DragArea.OnDragHandler -= _rexEditorEquipment.RotateZeroTransY;
            ObjectStylingDesigner.instance.ReleaseStrategy(ObjectStylingType.RexEditor_Equipment_Library);
        }

        protected override void onShow()
        {
            base.onShow();
            EquipmentGroupItems.UpdateItems(EquipmentGroupDatas);
        }

        private void initEquipmentGroup()
        {
            EquipmentGroupItems = new UiItemList<ObjectStringPath, ModelViewerStringPathUiItem>(
                EquipmentGroupPrefab,
                EquipmentGroupRoot,
                item => { item.GetButton(0).onClick.AddListener(() => { clickEquipmentGroupBtn(item.Data, item.Index); }); },
                (index, data, item) => { item.UpdateItem(index, data); }
            );
            EquipmentGroupDatas.Clear();
            foreach (var subStrategyName in _rexEditorEquipment.SubStrategyNames)
            {
                EquipmentGroupDatas.Add(new ObjectStringPath()
                {
                    FilterName = subStrategyName,
                    FilePath = ""
                });
            }
        }

        private void initEquipmentGroupDatas()
        {
            EquipmentGroupDatasItems = new UiItemList<ObjectStringPath, ModelViewerStringPathUiItem>(
                EquipmentGroupDatasPrefab,
                EquipmentGroupDatasRoot,
                item => { item.GetButton(0).onClick.AddListener(() => { clickEquipmentGroupDataBtn(item.Data, item.Index); }); },
                (index, data, item) => { item.UpdateItem(index, data); }
            );
        }

        private void clickEquipmentGroupBtn(ObjectStringPath data, int index)
        {
            EquipmentGroupDatasRoot.SetActive(curEquipmentGroupIndex != index);
            curEquipmentGroupIndex = curEquipmentGroupIndex != index ? index : -1;
            // 显示group下的所有物件按钮
            switch (index)
            {
                case 0:
                    EquipmentGroupDatasItems.UpdateItems(_rexEditorEquipment.ObjectNameList_0_Male);
                    LittleEnvironmentCreator.instance.SwitchToEnvironment("环境——装备");
                    break;
                case 1:
                    EquipmentGroupDatasItems.UpdateItems(_rexEditorEquipment.ObjectNameList_1_Female);
                    LittleEnvironmentCreator.instance.SwitchToEnvironment("环境——装备");
                    break;
            }

            EquipmentGroupDatasRoot.SetSiblingIndex(index + 2);

            for (int numIndex = 0; numIndex < EquipmentGroupItems.Count; numIndex++)
            {
                EquipmentGroupItems[numIndex].ShowApply(index);
            }

            EquipmentGroupRoot.GetComponent<RectTransform>()?.ForceUpdateRectTransforms();
            EquipmentGroupRoot.GetComponent<ContentSizeFitter>()?.SetLayoutVertical();
        }

        private void clickEquipmentGroupDataBtn(ObjectStringPath data, int index)
        {
            if (data.FilterName.Contains("Helmet"))   _rexEditorEquipment.ApplySub2Strategy(0);
            if (data.FilterName.Contains("Chest"))    _rexEditorEquipment.ApplySub2Strategy(1);
            if (data.FilterName.Contains("Shoulder")) _rexEditorEquipment.ApplySub2Strategy(2);
            if (data.FilterName.Contains("Glove"))    _rexEditorEquipment.ApplySub2Strategy(3);
            if (data.FilterName.Contains("Leg"))      _rexEditorEquipment.ApplySub2Strategy(4);
            _rexEditorEquipment.LoadObject(data.FilePath);
            
            for (int numIndex = 0; numIndex < EquipmentGroupDatasItems.Count; numIndex++)
            {
                EquipmentGroupDatasItems[numIndex].ShowApply(index);
            }
        }
    }
}