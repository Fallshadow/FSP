using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace fsp.modelshot.ui
{
    public class ItemActionPlay : MonoBehaviour
    {
        [Header("配置项")] public EditorSliderInput ActionPlayProcess = null;
        public Button PreFrame = null;
        public Button PlayOrPause = null;
        public Button NextFrame = null;
        public GameObject PlaySprite = null;
        public GameObject PauseSprite = null;
        public InputField PlaySpeedInputText = null;
        public Toggle FixPos = null;
        public Toggle AllGo = null;

        [Header("观察项")] public AnimationClip CurAnimationClip;
        public GameObject CurGameObject;
        public List<GameObject> CurGameObjects = new List<GameObject>();
        public SelectModelActionPanel parent;
        public bool isPlaying = false;
        public float allFrameCount = 0;
        public float playSpeed = 2;

        public void Init()
        {
            PreFrame.onClick.AddListener(PreFrameFunc);
            PlayOrPause.onClick.AddListener(PlayOrPauseFunc);
            NextFrame.onClick.AddListener(NextFrameFunc);
            PlaySpeedInputText.onValueChanged.AddListener(SpeedChangeFunc);
        }

        public void Init(SelectModelActionPanel parent)
        {
            this.parent = parent;
            PreFrame.onClick.AddListener(PreFrameFunc);
            PlayOrPause.onClick.AddListener(PlayOrPauseFunc);
            NextFrame.onClick.AddListener(NextFrameFunc);
            PlaySpeedInputText.onValueChanged.AddListener(SpeedChangeFunc);
            AllGo.onValueChanged.AddListener(allGoChange);
        }

        private void allGoChange(bool allGo)
        {
            if (allGo)
            {
                CurGameObjects.Clear();
                foreach (var item in parent.parent.ModelPanel._freeScreenShot.Objects)
                {
                    CurGameObjects.Add(item);
                }
            }
        }

        public void ResetAcitonPlay(AnimationClip animationClip, GameObject gameObject)
        {
            SetPlay(false);
            CurAnimationClip = animationClip;
            allFrameCount = CurAnimationClip.length * animationClip.frameRate;
            CurGameObject = gameObject;
            PlaySpeedInputText.text = "1";
            ActionPlayProcess.MInputField.text = "0";
            ActionPlayProcess.Init(0, CurAnimationClip.length * animationClip.frameRate,
                value => { SampleAnimation(value / animationClip.frameRate); },
                value => { SampleAnimation(float.Parse(value) / animationClip.frameRate); },
                0);
        }

        private void SampleAnimation(float value)
        {
            if (FixPos.isOn)
            {
                if (AllGo.isOn)
                {
                    foreach (var item in CurGameObjects)
                    {
                        Vector3 curPos = item.transform.position;
                        CurAnimationClip.SampleAnimation(item, value);
                        item.transform.position = curPos;
                    }
                }
                else
                {
                    Vector3 curPos = CurGameObject.transform.position;
                    CurAnimationClip.SampleAnimation(CurGameObject, value);
                    CurGameObject.transform.position = curPos;
                }
            }
            else
            {
                if (AllGo.isOn)
                {
                    foreach (var item in CurGameObjects)
                    {
                        CurAnimationClip.SampleAnimation(item, value);
                    }
                }
                else
                {
                    CurAnimationClip.SampleAnimation(CurGameObject, value);
                }
            }
        }

        private void FixedUpdate()
        {
            if (isPlaying)
            {
                ActionPlayProcess.SetValue(GetTargetFrame(ActionPlayProcess.GetValue() + (float) 1 / 2 * playSpeed));
            }
        }

        public void SetPlay(bool setPlay)
        {
            isPlaying = setPlay;
            PlaySprite.SetActive(!isPlaying);
            PauseSprite.SetActive(isPlaying);
        }

        public void PlayOrPauseFunc()
        {
            SetPlay(!isPlaying);
        }

        public void PreFrameFunc()
        {
            ActionPlayProcess.SetValue(GetTargetFrame(ActionPlayProcess.GetValue() - 1));
        }

        public void NextFrameFunc()
        {
            ActionPlayProcess.SetValue(GetTargetFrame(ActionPlayProcess.GetValue() + 1));
        }

        public void SpeedChangeFunc(string speedText)
        {
            playSpeed = float.Parse(speedText);
        }

        public float GetTargetFrame(float curFrame)
        {
            float tagetFrame = 0;
            tagetFrame = curFrame > allFrameCount ? curFrame - allFrameCount : curFrame;
            tagetFrame = curFrame < 0 ? curFrame + allFrameCount : tagetFrame;
            return tagetFrame;
        }
    }
}