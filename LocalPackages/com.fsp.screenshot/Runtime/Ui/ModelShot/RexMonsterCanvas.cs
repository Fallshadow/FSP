using System.Collections.Generic;
using fsp.LittleSceneEnvironment;
using fsp.ObjectStylingDesigne;
using fsp.ui.utility;
using UnityEngine;
using UnityEngine.UI;

namespace fsp.modelshot.ui
{
    [BindingResource_ModelShot(UiAssetIndex.RexMonsterCanvas)]
    public class RexMonsterCanvas : CaptureScreenShotCanvasBase
    {
        [SerializeField] private Transform FashionMonsterGroupRoot = null;
        [SerializeField] private ModelViewerStringPathUiItem FashionMonsterGroupPrefab = null;
        private UiItemList<ObjectStringPath, ModelViewerStringPathUiItem> FashionMonsterGroupItems = null;
        private readonly List<ObjectStringPath> FashionMonsterGroupDatas = new List<ObjectStringPath>();
        private int curFashionMonsterGroupIndex = -1;

        [SerializeField] private Transform FashionMonsterGroupDatasRoot = null;
        [SerializeField] private ModelViewerStringPathUiItem FashionMonsterGroupDatasPrefab = null;
        private UiItemList<ObjectStringPath, ModelViewerStringPathUiItem> FashionMonsterGroupDatasItems = null;

        private ObjectStylingStrategyRexEditorMonster _rexEditorFashionMonster = null;

        public override void Initialize()
        {
            base.Initialize();
            _rexEditorFashionMonster = (ObjectStylingStrategyRexEditorMonster) ObjectStylingDesigner.instance.CreateOrGetStrategy(ObjectStylingType.RexEditor_Monster_Library);
            DragArea.OnDragHandler += _rexEditorFashionMonster.RotateZeroTransY;
            initFashionMonsterGroup();
            initFashionMonsterGroupDatas();
        }

        public override void Release()
        {
            base.Release();
            DragArea.OnDragHandler -= _rexEditorFashionMonster.RotateZeroTransY;
            ObjectStylingDesigner.instance.ReleaseStrategy(ObjectStylingType.RexEditor_Monster_Library);
        }

        protected override void onShow()
        {
            base.onShow();
            FashionMonsterGroupItems.UpdateItems(FashionMonsterGroupDatas);
        }

        private void initFashionMonsterGroup()
        {
            FashionMonsterGroupItems = new UiItemList<ObjectStringPath, ModelViewerStringPathUiItem>(
                FashionMonsterGroupPrefab,
                FashionMonsterGroupRoot,
                item => { item.GetButton(0).onClick.AddListener(() => { clickFashionMonsterGroupBtn(item.Data, item.Index); }); },
                (index, data, item) => { item.UpdateItem(index, data); }
            );
            FashionMonsterGroupDatas.Clear();
            foreach (var subStrategyName in _rexEditorFashionMonster.SubStrategyNames)
            {
                FashionMonsterGroupDatas.Add(new ObjectStringPath()
                {
                    FilterName = subStrategyName,
                    FilePath = ""
                });
            }
        }

        private void initFashionMonsterGroupDatas()
        {
            FashionMonsterGroupDatasItems = new UiItemList<ObjectStringPath, ModelViewerStringPathUiItem>(
                FashionMonsterGroupDatasPrefab,
                FashionMonsterGroupDatasRoot,
                item => { item.GetButton(0).onClick.AddListener(() => { clickFashionMonsterGroupDataBtn(item.Data, item.Index); }); },
                (index, data, item) => { item.UpdateItem(index, data); }
            );
        }

        private void clickFashionMonsterGroupBtn(ObjectStringPath data, int index)
        {
            FashionMonsterGroupDatasRoot.SetActive(curFashionMonsterGroupIndex != index);
            curFashionMonsterGroupIndex = curFashionMonsterGroupIndex != index ? index : -1;
            LittleEnvironmentCreator.instance.SwitchToEnvironment("环境——怪物");
            // 显示group下的所有物件按钮
            switch (index)
            {
                case 0: FashionMonsterGroupDatasItems.UpdateItems(_rexEditorFashionMonster.ObjectNameList_0_Boss );break;
                case 1: FashionMonsterGroupDatasItems.UpdateItems(_rexEditorFashionMonster.ObjectNameList_1_NPC   );break;
            }

            FashionMonsterGroupDatasRoot.SetSiblingIndex(index + 2);

            for (int numIndex = 0; numIndex < FashionMonsterGroupItems.Count; numIndex++)
            {
                FashionMonsterGroupItems[numIndex].ShowApply(index);
            }

            FashionMonsterGroupRoot.GetComponent<RectTransform>()?.ForceUpdateRectTransforms();
            FashionMonsterGroupRoot.GetComponent<ContentSizeFitter>()?.SetLayoutVertical();
        }

        private void clickFashionMonsterGroupDataBtn(ObjectStringPath data, int index)
        {
            _rexEditorFashionMonster.ApplySubStrategy(0);
            _rexEditorFashionMonster.LoadObject(data.FilePath);
            int _index = data.FilterName.IndexOf('_') + 1;
            string MonsterId = data.FilterName.Substring(_index, data.FilterName.Length - _index - 7);
            string MonsterActionPath = "";
            if (data.FilePath.Contains("Boss"))
            {
                MonsterActionPath = Application.dataPath + $"/ResourceRex/Character/Monster/Boss/Mob_{MonsterId}/Animations/Clips";
            }
            if (data.FilePath.Contains("NPC_Monster"))
            {
                MonsterActionPath = Application.dataPath + $"/ResourceRex/Character/Monster/NPC_Monster/Mob_{MonsterId}/Animations/Clips";
            }
            
            commonFuncMono.ActionPanel.ClearClips();
            commonFuncMono.ActionPanel.LoadFolder(MonsterActionPath);
            commonFuncMono.ActionPanel.SetDisplayGO(_rexEditorFashionMonster.Objects[0]);
            
            for (int numIndex = 0; numIndex < FashionMonsterGroupDatasItems.Count; numIndex++)
            {
                FashionMonsterGroupDatasItems[numIndex].ShowApply(index);
            }
        }
    }
}