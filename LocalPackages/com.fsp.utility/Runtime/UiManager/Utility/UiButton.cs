using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace fsp.ui.utility
{
    public class UiButton : Button
    {
        public static string NoSoundId = "Play_ui_no_sound";
        public static string CommonEnableSoundId = "Play_ui_common_btn";
        public static string CommonDiableSoundId = "Play_ui_error";

        public string SoundId
        {
            get { return soundId; }
            set { soundId = value; }
        }

        public int UiAudioId
        {
            get { return uiAudioId; }
            set { uiAudioId = value; }
        }

        /// <summary>
        /// 按鈕是否為灰態(還是可以點擊)。
        /// </summary>
        public bool IsAvailable
        {
            get
            {
                return isAvailable;
            }
            set
            {
                setAvailable(value);
                isAvailable = value;
            }
        }

        public bool DoBaseTransition = true;

        [SerializeField] protected int uiAudioId;
        [SerializeField] protected string soundId = null;
        [SerializeField] protected Graphic glowBg = null;
        [SerializeField] protected Material glowMat = null;

        private bool isAvailable;
        public Action PointerUpAction = null;
        public Action PointerDownAction = null;

#if UNITY_EDITOR
        protected override void Reset()
        {
            base.Reset();
            Navigation nav = new Navigation
            {
                mode = Navigation.Mode.None,
            };
            navigation = nav;
        }
#endif

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            PlaySound();
            base.OnPointerClick(eventData);
        }

        string GetGameObjectPath(GameObject obj)
        {
            string path = @"\" + obj.name;
            while (obj.transform.parent != null)
            {
                obj = obj.transform.parent.gameObject;
                path = @"\" + obj.name + path;
            }
            return path;
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            PointerDownAction?.Invoke();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            PointerUpAction?.Invoke();
        }

        public virtual void PlaySound()
        {
            // 若有設置audioId則優先
            if (uiAudioId > 0)
            {
                // TODO:音效系统
                // act.audio.AudioManager.instance.PostPlayEvent(uiAudioId);
                return;
            }

            if (soundId == NoSoundId)
            {
                return;
            }

            if (interactable == false)
            {
                // TODO:音效系统
                // act.audio.AudioManager.instance.PostPlayEvent(CommonDiableSoundId);
                return;
            }

            /* 通用按钮点击音效，除了已配置的ui音效以及如图所示技能按钮不通用，其他按钮通用此event（ps：通用点击优先级最低，如果任意一个指定按钮播放音效可以干掉通用点击音效） */
            if (string.IsNullOrEmpty(soundId))
            {
                // TODO:音效系统
                // act.audio.AudioManager.instance.PostPlayEvent(CommonEnableSoundId);
            }
            else
            {
                // TODO:音效系统
                // act.audio.AudioManager.instance.PostPlayEvent(soundId);
            }
        }

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            if (targetGraphic == null)
            {
                return;
            }

            if (!isAvailable)
            {
                return;
            }

            switch (transition)
            {
                case Transition.ColorTint:
                {
                    if (state != SelectionState.Disabled && targetGraphic.material == UiStyleHelperSO.Instance.GrayscaleMat)
                    {
                        targetGraphic.material = null;
                    }
                    else if (state == SelectionState.Disabled && targetGraphic.material == null)
                    {
                        targetGraphic.material = UiStyleHelperSO.Instance.GrayscaleMat;
                    }
                    break;
                }
            }

            if (glowBg != null)
            {
                glowBg.material = (state == SelectionState.Disabled) ? UiStyleHelperSO.Instance.GrayscaleGlowMat : glowMat;
            }

            if (DoBaseTransition)
            {
                base.DoStateTransition(state, instant);
            }
        }

        private void setAvailable(bool isAvailable)
        {
            if (currentSelectionState == SelectionState.Disabled)
            {
                return;
            }
            
            switch (transition)
            {
                case Transition.ColorTint:
                {
                    if (image == null) break;
                    image.color = isAvailable ? colors.highlightedColor : colors.disabledColor;
                    break;
                }
                case Transition.SpriteSwap:
                {
                    if (image == null) break;
                    image.overrideSprite = isAvailable ? spriteState.highlightedSprite : spriteState.disabledSprite;
                    break;
                }
            }
            
            if (glowBg != null)
            {
                glowBg.material = isAvailable ? glowMat : UiStyleHelperSO.Instance.GrayscaleGlowMat;
            }
        }
    }
}