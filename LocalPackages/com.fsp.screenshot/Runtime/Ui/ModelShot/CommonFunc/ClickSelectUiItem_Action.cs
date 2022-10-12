using System;
using UnityEngine;
using fsp.ui.utility;

namespace fsp.modelshot.ui
{
    public class ClickSelectUiItem_Action : ClickSelectUiItem
    {
        public Action<int, AnimationClip> ClickCallBack;
        private AnimationClip actionData;
        
        private const int text_ActionName = 0;
        
        public void SetActionInfo(int index, AnimationClip data)
        {
            Index = index;
            actionData = data;
            GetText(text_ActionName).text = data.name;
        }

        protected override void onRealClcik()
        {
            base.onRealClcik();
            ClickCallBack?.Invoke(Index, actionData);
        }
    }
}