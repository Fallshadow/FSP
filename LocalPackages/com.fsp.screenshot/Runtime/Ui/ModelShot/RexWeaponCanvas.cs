using System.Collections.Generic;
using fsp.LittleSceneEnvironment;
using fsp.ObjectStylingDesigne;
using fsp.ui.utility;
using UnityEngine;
using UnityEngine.UI;

namespace fsp.modelshot.ui
{
    [BindingResource_ModelShot(UiAssetIndex.RexWeaponCanvas)]
    public class RexWeaponCanvas : CaptureScreenShotCanvasBase
    {
        [SerializeField] private Transform weaponGroupRoot = null;
        [SerializeField] private ModelViewerStringPathUiItem weaponGroupPrefab = null;
        private UiItemList<ObjectStringPath, ModelViewerStringPathUiItem> weaponGroupItems = null;
        private readonly List<ObjectStringPath> weaponGroupDatas = new List<ObjectStringPath>();
        private int curWeaponGroupIndex = -1;
        
        [SerializeField] private Transform weaponGroupDatasRoot = null;
        [SerializeField] private ModelViewerStringPathUiItem weaponGroupDatasPrefab = null;
        private UiItemList<ObjectStringPath, ModelViewerStringPathUiItem> weaponGroupDatasItems = null;
        
        private ObjectStylingStrategyRexEditorWeapon _rexEditorWeapon = null;

        public override void Initialize()
        {
            base.Initialize();
            _rexEditorWeapon = (ObjectStylingStrategyRexEditorWeapon)ObjectStylingDesigner.instance.CreateOrGetStrategy(ObjectStylingType.RexEditor_Weapon_Library);
            initWeaponGroup();
            initWeaponGroupDatas();
        }

        public override void Release()
        {
            base.Release();
            ObjectStylingDesigner.instance.ReleaseStrategy(ObjectStylingType.RexEditor_Weapon_Library);
        }

        protected override void onShow()
        {
            base.onShow();
            weaponGroupItems.UpdateItems(weaponGroupDatas);
        }

        private void initWeaponGroup()
        {
            weaponGroupItems = new UiItemList<ObjectStringPath, ModelViewerStringPathUiItem>(
                weaponGroupPrefab,
                weaponGroupRoot,
                item =>
                {
                    item.GetButton(0).onClick.AddListener(() => { clickWeaponGroupBtn(item.Data, item.Index); });
                },
                (index, data, item) => { item.UpdateItem(index, data); }
            );
            weaponGroupDatas.Clear();
            foreach (var subStrategyName in _rexEditorWeapon.SubStrategyNames)
            {
                weaponGroupDatas.Add(new ObjectStringPath()
                {
                    FilterName = subStrategyName,
                    FilePath = ""
                });
            }
        }

        private void initWeaponGroupDatas()
        {
            weaponGroupDatasItems = new UiItemList<ObjectStringPath, ModelViewerStringPathUiItem>(
                weaponGroupDatasPrefab,
                weaponGroupDatasRoot,
                item =>
                {
                    item.GetButton(0).onClick.AddListener(() => { clickWeaponGroupDataBtn(item.Data, item.Index); });
                },
                (index, data, item) => { item.UpdateItem(index, data); }
            );
        }

        private void clickWeaponGroupBtn(ObjectStringPath data, int index)
        {
            weaponGroupDatasRoot.SetActive(curWeaponGroupIndex != index);
            curWeaponGroupIndex = curWeaponGroupIndex != index ? index : -1;
            // 显示group下的所有物件按钮
            switch (index)
            {
                case 0: weaponGroupDatasItems.UpdateItems(_rexEditorWeapon.ObjectNameList_0_Knife     ); LittleEnvironmentCreator.instance.SwitchToEnvironment("环境——斩裂刀"); break;
                case 1: weaponGroupDatasItems.UpdateItems(_rexEditorWeapon.ObjectNameList_1_Hammer    ); LittleEnvironmentCreator.instance.SwitchToEnvironment("环境——锤子");break;
                case 2: weaponGroupDatasItems.UpdateItems(_rexEditorWeapon.ObjectNameList_2_DualBlade ); LittleEnvironmentCreator.instance.SwitchToEnvironment("环境——双刀");break;
                case 3: weaponGroupDatasItems.UpdateItems(_rexEditorWeapon.ObjectNameList_3_KallaGun  ); LittleEnvironmentCreator.instance.SwitchToEnvironment("环境——塑能枪");break;
                case 4: weaponGroupDatasItems.UpdateItems(_rexEditorWeapon.ObjectNameList_4_Spear     ); LittleEnvironmentCreator.instance.SwitchToEnvironment("环境——饲灵枪");break;
                case 5: weaponGroupDatasItems.UpdateItems(_rexEditorWeapon.ObjectNameList_5_Bow       ); LittleEnvironmentCreator.instance.SwitchToEnvironment("环境——弓箭");break;
            }
            weaponGroupDatasRoot.SetSiblingIndex(index + 2);
            
            for (int numIndex = 0; numIndex < weaponGroupItems.Count; numIndex++)
            {
                weaponGroupItems[numIndex].ShowApply(index);
            }
            
            weaponGroupRoot.GetComponent<RectTransform>()?.ForceUpdateRectTransforms();
            weaponGroupRoot.GetComponent<ContentSizeFitter>()?.SetLayoutVertical();
            _rexEditorWeapon.ApplySubStrategy(index);
        }

        private void clickWeaponGroupDataBtn(ObjectStringPath data, int index)
        {
            _rexEditorWeapon.LoadObject(data.FilePath);
            
            for (int numIndex = 0; numIndex < weaponGroupDatasItems.Count; numIndex++)
            {
                weaponGroupDatasItems[numIndex].ShowApply(index);
            }
        }
    }
}