using System;
using System.Collections.Generic;
using fsp.debug;
using UnityEngine;

namespace fsp.ui
{
    public class UiManageStrategy
    {
        public RectTransform MainRoot => fullScreenRoot;

        private RectTransform fullScreenRoot = null;
        
        private LinkedList<UiBase> fullScreenCavases = new LinkedList<UiBase>();
        
        public UiManageStrategy(RectTransform[] roots)
        {
            fullScreenRoot = roots[0];
        }
        
        public void OpenUi(UiBase ui, Action completeCb, int showPage = 0)
        {
            switch (ui.OpenType)
            {
                case UiOpenType.UOT_FULL_SCREEN:
                {
                    openFullScreenCanvas(ui, completeCb, showPage);
                    break;
                }
            }
        }
        
        private void openFullScreenCanvas(UiBase ui, Action completeCb, int showPage)
        {
            if (fullScreenCavases.Contains(ui))
            {
                PrintSystem.LogWarning($"[MainCanvasRoot] UI has already open. UI: {ui.name}");
                completeCb?.Invoke();
                return;
            }

            LinkedListNode<UiBase> lastNode = fullScreenCavases.Last;
            if (lastNode == null || lastNode.Value.State != UiState.US_SHOW)
            {
                fullScreenCavases.AddLast(ui);
                ui.transform.SetParent(fullScreenRoot);
                ui.transform.SetAsFirstSibling();
                ui.Open(CloseFullScreenCanvas, completeCb, showPage);

                return;
            }

            lastNode.Value.Hide(() => openFullScreenCanvas(ui, completeCb, showPage));
        }
        
        private void CloseFullScreenCanvas(UiBase ui)
        {
            if (fullScreenCavases.Count == 0) return;
            
            bool isOpenLast = (ui == fullScreenCavases.Last.Value);
            fullScreenCavases.Remove(ui);
            if (fullScreenCavases.Count == 0 || !isOpenLast)
            {
                return;
            }

            fullScreenCavases.Last.Value.transform.SetAsFirstSibling();
            fullScreenCavases.Last.Value.Show();
        }
        
        public UiBase CreateUi(UiBase uiPrefab)
        {
            UiBase ui;
            switch (uiPrefab.OpenType)
            {
                case UiOpenType.UOT_FULL_SCREEN:
                case UiOpenType.UOT_COMMON:
                    {
                        ui = UnityEngine.Object.Instantiate(uiPrefab, fullScreenRoot);
                        break;
                    }
                default:
                    {
                        return null;
                    }
            }
            return ui;
        }
        
        public void Clear(UiBase remainOne = null)
        {
            fullScreenCavases.Clear();
        }
    }
}