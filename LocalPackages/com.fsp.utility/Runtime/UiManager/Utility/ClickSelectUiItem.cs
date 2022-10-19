using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace fsp.ui.utility
{
    public enum ClickSelectUiItemType
    {
        CSUIT_NONE = -1,
        CSUIT_DEFAULT = 0,
        CSUIT_DEFAULT_1 = 1,
    }

    public class ClickSelectUiItem : UiItem
    {
        [SerializeField] private ClickSelectUiItemType mType = ClickSelectUiItemType.CSUIT_NONE;
        [SerializeField] protected GameObject selectIconGo = null;
        [SerializeField] private GameObject unSelectIconGo = null;
        [Header("是否总是响应：若开启则每次点击都会执行具体click的响应内容")]
        public bool AlwaysResp = false;
        protected UiButton mBtn = null;
        public bool IsClicked = false;
        public Action OnClickShowCallBack = null;
        public Action UnClickShowCallBack = null;

        protected virtual void Awake()
        {
            evt.EventManager.instance.Register<ClickSelectUiItemType, ClickSelectUiItem>(evt.EventGroup.UI, (short)evt.UiEvent.CLICK_SELECT_UIITEM, onTrigger);
        }

        protected virtual void Start()
        {
            mBtn = gameObject.GetComponent<UiButton>();
            mBtn.onClick.RemoveListener(onClick);
            mBtn.onClick.AddListener(onClick);
        }

        protected virtual void OnDestroy()
        {
            mBtn?.onClick.RemoveListener(onClick);
            evt.EventManager.instance.Unregister<ClickSelectUiItemType, ClickSelectUiItem>(evt.EventGroup.UI, (short)evt.UiEvent.CLICK_SELECT_UIITEM, onTrigger);
        }

        protected void SetMType(ClickSelectUiItemType type)
        {
            mType = type;
        }

        public void SetSelImg(bool enable)
        {
            if(selectIconGo != null)
            {
                selectIconGo.SetActive(enable);
            }
            if(unSelectIconGo != null)
            {
                unSelectIconGo.SetActive(!enable);
            }
        }

        protected void SetBtnResp(bool isCanClick)
        {
            if(mBtn == null)
            {
                Start();
            }

            mBtn.interactable = isCanClick;
        }

        public virtual void DoClick()
        {
            onClick();
        }

        public virtual void DoUnclick()
        {
            unclick();
        }

        protected void onClick()
        {
            if(IsClicked && !AlwaysResp)
            {
                return;
            }
            onClickShow();
            IsClicked = true;
            evt.EventManager.instance.Send<ClickSelectUiItemType, ClickSelectUiItem>(evt.EventGroup.UI, (short)evt.UiEvent.CLICK_SELECT_UIITEM, mType, this);
            onRealClcik();
        }

        protected void unclick()
        {
            if(!IsClicked && !AlwaysResp)
            {
                return;
            }
            unClickShow();
            IsClicked = false;
        }

        protected virtual void onClickShow()
        {
            SetSelImg(true);
            OnClickShowCallBack?.Invoke();
        }

        protected virtual void unClickShow()
        {
            SetSelImg(false);
            UnClickShowCallBack?.Invoke();
        }

        protected virtual void onRealClcik()
        {

        }

        protected virtual void onTrigger(ClickSelectUiItemType type, ClickSelectUiItem emitter)
        {
            if(type == mType && emitter != this)
            {
                unclick();
            }
        }
    }
}