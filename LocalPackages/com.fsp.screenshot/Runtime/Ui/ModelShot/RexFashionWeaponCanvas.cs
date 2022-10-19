using System.Collections.Generic;
using fsp.LittleSceneEnvironment;
using fsp.ObjectStylingDesigne;
using fsp.ui.utility;
using UnityEngine;
using UnityEngine.UI;

namespace fsp.modelshot.ui
{
    [BindingResource_ModelShot(UiAssetIndex.RexFashionWeaponCanvas)]
    public class RexFashionWeaponCanvas : CaptureScreenShotCanvasBase
    {
        [SerializeField] private Transform FashionWeaponGroupRoot = null;
        [SerializeField] private ModelViewerStringPathUiItem FashionWeaponGroupPrefab = null;
        private UiItemList<ObjectStringPath, ModelViewerStringPathUiItem> FashionWeaponGroupItems = null;
        private readonly List<ObjectStringPath> FashionWeaponGroupDatas = new List<ObjectStringPath>();
        private int curFashionWeaponGroupIndex = -1;
        
        [SerializeField] private Transform FashionWeaponGroupDatasRoot = null;
        [SerializeField] private ModelViewerStringPathUiItem FashionWeaponGroupDatasPrefab = null;
        private UiItemList<ObjectStringPath, ModelViewerStringPathUiItem> FashionWeaponGroupDatasItems = null;
        
        private ObjectStylingStrategyRexEditorFashionWeapon _rexEditorFashionWeapon = null;

        public override void Initialize()
        {
            base.Initialize();
            _rexEditorFashionWeapon = (ObjectStylingStrategyRexEditorFashionWeapon)ObjectStylingDesigner.instance.CreateOrGetStrategy(ObjectStylingType.RexEditor_Fashion_Weapon_Library);
            DragArea.OnDragHandler += _rexEditorFashionWeapon.RotateZeroTransY;
            initFashionWeaponGroup();
            initFashionWeaponGroupDatas();
        }

        public override void Release()
        {
            base.Release();
            DragArea.OnDragHandler -= _rexEditorFashionWeapon.RotateZeroTransY;
            ObjectStylingDesigner.instance.ReleaseStrategy(ObjectStylingType.RexEditor_Fashion_Weapon_Library);
        }

        protected override void onShow()
        {
            base.onShow();
            FashionWeaponGroupItems.UpdateItems(FashionWeaponGroupDatas);
        }

        private void initFashionWeaponGroup()
        {
            FashionWeaponGroupItems = new UiItemList<ObjectStringPath, ModelViewerStringPathUiItem>(
                FashionWeaponGroupPrefab,
                FashionWeaponGroupRoot,
                item =>
                {
                    item.GetButton(0).onClick.AddListener(() => { clickFashionWeaponGroupBtn(item.Data, item.Index); });
                },
                (index, data, item) => { item.UpdateItem(index, data); }
            );
            FashionWeaponGroupDatas.Clear();
            foreach (var subStrategyName in _rexEditorFashionWeapon.SubStrategyNames)
            {
                FashionWeaponGroupDatas.Add(new ObjectStringPath()
                {
                    FilterName = subStrategyName,
                    FilePath = ""
                });
            }
        }

        private void initFashionWeaponGroupDatas()
        {
            FashionWeaponGroupDatasItems = new UiItemList<ObjectStringPath, ModelViewerStringPathUiItem>(
                FashionWeaponGroupDatasPrefab,
                FashionWeaponGroupDatasRoot,
                item =>
                {
                    item.GetButton(0).onClick.AddListener(() => { clickFashionWeaponGroupDataBtn(item.Data, item.Index); });
                },
                (index, data, item) => { item.UpdateItem(index, data); }
            );
        }

        private void clickFashionWeaponGroupBtn(ObjectStringPath data, int index)
        {
            FashionWeaponGroupDatasRoot.SetActive(curFashionWeaponGroupIndex != index);
            curFashionWeaponGroupIndex = curFashionWeaponGroupIndex != index ? index : -1;
            // 显示group下的所有物件按钮
            switch (index)
            {
                case 0: FashionWeaponGroupDatasItems.UpdateItems(_rexEditorFashionWeapon.ObjectNameList_0_Knife     ); LittleEnvironmentCreator.instance.SwitchToEnvironment("环境——斩裂刀"); break;
                case 1: FashionWeaponGroupDatasItems.UpdateItems(_rexEditorFashionWeapon.ObjectNameList_1_Hammer    ); LittleEnvironmentCreator.instance.SwitchToEnvironment("环境——锤子");break;
                case 2: FashionWeaponGroupDatasItems.UpdateItems(_rexEditorFashionWeapon.ObjectNameList_2_DualBlade ); LittleEnvironmentCreator.instance.SwitchToEnvironment("环境——双刀");break;
                case 3: FashionWeaponGroupDatasItems.UpdateItems(_rexEditorFashionWeapon.ObjectNameList_3_KallaGun  ); LittleEnvironmentCreator.instance.SwitchToEnvironment("环境——塑能枪");break;
                case 4: FashionWeaponGroupDatasItems.UpdateItems(_rexEditorFashionWeapon.ObjectNameList_4_Spear     ); LittleEnvironmentCreator.instance.SwitchToEnvironment("环境——饲灵枪");break;
                case 5: FashionWeaponGroupDatasItems.UpdateItems(_rexEditorFashionWeapon.ObjectNameList_5_Bow       ); LittleEnvironmentCreator.instance.SwitchToEnvironment("环境——弓箭");break;
            }
            FashionWeaponGroupDatasRoot.SetSiblingIndex(index + 2);
            
            for (int numIndex = 0; numIndex < FashionWeaponGroupItems.Count; numIndex++)
            {
                FashionWeaponGroupItems[numIndex].ShowApply(index);
            }
            
            FashionWeaponGroupRoot.GetComponent<RectTransform>()?.ForceUpdateRectTransforms();
            FashionWeaponGroupRoot.GetComponent<ContentSizeFitter>()?.SetLayoutVertical();
            _rexEditorFashionWeapon.ApplySubStrategy(index);
        }

        private void clickFashionWeaponGroupDataBtn(ObjectStringPath data, int index)
        {
            _rexEditorFashionWeapon.LoadObject(data.FilePath);
            
            for (int numIndex = 0; numIndex < FashionWeaponGroupDatasItems.Count; numIndex++)
            {
                FashionWeaponGroupDatasItems[numIndex].ShowApply(index);
            }
        }
    }
}