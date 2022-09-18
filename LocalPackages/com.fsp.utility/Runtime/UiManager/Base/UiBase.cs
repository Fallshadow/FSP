using System;
using System.Collections.Generic;
using fsp.debug;
using UnityEngine;

namespace fsp.ui
{
    [DisallowMultipleComponent]
    public abstract class UiBase : MonoBehaviour
    {
        public virtual UiOpenType OpenType => curentUiOpenType;
        private UiOpenType curentUiOpenType = UiOpenType.UOT_COMMON;
        
        public UiState State { get; protected set; }
        
        public virtual bool IsAlwaysVisible => false;
        
        protected Action<UiBase> onCloseCompleteHandler = null;
        public bool IsOpen => onCloseCompleteHandler != null;

        // Animation
        protected bool isTransiting = false;
        protected HashSet<IUiAnimation> playingAnimations = null;
        protected Action onAnimationCompleteCallback = null;
        protected string uiAnimationSuffix = string.Empty;
        protected IUiAnimation[] uiAnimations = null;
        protected int playingAnimationCount = 0;
        
        public virtual void Initialize()
        {
            
        }

        public virtual void Release()
        {

        }
        
        public virtual void OnCreate()
        {
            setAnimations();
            SetVisible(false);
            State = UiState.US_HIDE;
            Initialize();
        }
        
        protected void setAnimations()
        {
            uiAnimations = GetComponentsInChildren<IUiAnimation>();
            playingAnimations = new HashSet<IUiAnimation>();
            for (int i = 0; i < uiAnimations.Length; ++i)
            {
                uiAnimations[i].Initialize(onAnimationComplete);
            }
        }
        
        protected void onAnimationComplete(object sender)
        {
            if (--playingAnimationCount > 0)
            {
                return;
            }

            playingAnimations.Clear();
            onTransitionComplete();
        }

        
        public void Open(Action<UiBase> closeCompleteCb, Action openCompleteCb = null, int showPage = 0)
        {
            if (onCloseCompleteHandler != null)
            {
                debug.PrintSystem.LogWarning($"[UserInterfaceBase] UI has already open. Name: {gameObject.name}");
                openCompleteCb?.Invoke();
                return;
            }

            onCloseCompleteHandler = closeCompleteCb;
            onOpen();
            Show(openCompleteCb);
        }
        
        protected virtual void onOpen()
        {
            
        }
        
        protected virtual void onShow()
        {
            
        }
        
        protected virtual void onShowComplete() 
        {

        }
        
        protected virtual void onHideComplete()
        {
            
        }
        
        public virtual void Show(Action showCompleteCb = null)
        {
            if (State != UiState.US_HIDE)
            {
                PrintSystem.LogWarning($"[UserInterfaceBase] Show at wrong state. Name: {gameObject.name}, State: {State}");
                showCompleteCb?.Invoke();
                return;
            }

            if (isTransiting)
            {
                StopAnimations();
            }

            State = UiState.US_SHOW;
            onAnimationCompleteCallback += showCompleteCb;
            SetVisible(true);
            onShow();
            playAnimations(UiAnimationClip.UAC_SHOW);
        }
        
        public void StopAnimations()
        {
            onAnimationCompleteCallback = null;
            foreach (IUiAnimation clip in playingAnimations)
            {
                clip.Stop();
            }
            playingAnimations.Clear();
        }
        
        protected void playAnimations(UiAnimationClip animationClip)
        {
            isTransiting = true;
            // Check number of clips.
            for (int i = 0; i < uiAnimations.Length; ++i)
            {
                if (uiAnimations[i].HasClip(animationClip, uiAnimationSuffix))
                {
                    playingAnimations.Add(uiAnimations[i]);
                }
            }

            if (playingAnimations.Count == 0)
            {
                onTransitionComplete();
                return;
            }

            // Play clips.
            playingAnimationCount = playingAnimations.Count;
            foreach (IUiAnimation clip in playingAnimations)
            {
                clip.Play(animationClip, uiAnimationSuffix);
            }
        }
        
        protected virtual void onTransitionComplete()
        {
            isTransiting = false;
            switch (State)
            {
                case UiState.US_SHOW:
                {
                    onShowComplete();
                    break;
                }
                case UiState.US_HIDE:
                {
                    SetVisible(false);
                    onHideComplete();
                    break;
                }
            }

            if (onAnimationCompleteCallback != null)
            {
                onAnimationCompleteCallback();
                onAnimationCompleteCallback = null;
            }
        }
        
        public virtual void SetVisible(bool isVisible)
        {
            if (isVisible)
            {
                gameObject.SetActive(true);
                return;
            }

            if(!IsAlwaysVisible)
            {
                gameObject.SetActive(false);
            }
        }
        
        public virtual void Hide(Action hideCompleteCb = null)
        {
            if (State != UiState.US_SHOW)
            {
                PrintSystem.LogWarning($"[UserInterfaceBase] Hide at wrong state. Name: {gameObject.name}, State: {State}");
                hideCompleteCb?.Invoke();
                return;
            }

            if (isTransiting)
            {
                StopAnimations();
            }

            State = UiState.US_HIDE;
            onAnimationCompleteCallback += hideCompleteCb;
            onHide();
            playAnimations(UiAnimationClip.UAC_HIDE);
        }
        
        protected virtual void onHide()
        {

        }
        
        public void SetOpenType(UiOpenType uiOpenType)
        {
            curentUiOpenType = uiOpenType;
        }
        
        public virtual void OnRuin() // NOTE: 因為OnDestroy被MonoBehaviour使用了
        {
            switch (State)
            {
                case UiState.US_SHOW:
                {
                    if (isTransiting)
                    {
                        StopAnimations();
                        onShowComplete();
                    }

                    if (IsOpen)
                    {
                        onClose();
                        onHide();
                        onHideComplete();
                    }
                    else
                    {
                        onHide();
                        onHideComplete();
                    }
                    break;
                }
                case UiState.US_HIDE:
                {
                    if (isTransiting)
                    {
                        StopAnimations();
                        onHideComplete();
                    }

                    if (IsOpen)
                    {
                        onClose();
                    }
                    break;
                }
                default:
                {
                    return;
                }
            }

            Release();
        }
        
        protected virtual void onClose()
        {

        }

        
        protected virtual void Reset()
        {
            gameObject.layer = UiManager.VisibleUiLayer;
        }
    }
}