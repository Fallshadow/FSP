using System;
using UnityEngine;
using UnityEngine.UI;

namespace fsp.ui.utility
{
    public class BtnChangeTextInput : MonoBehaviour
    {
        public Button ChangeModeBtn = null;
        public Text ModeName = null;
        
        private string mode1Name;
        private string mode2Name;
        private string curName;
        private string defName;
        private Action<string> changeModeCallBack = null;
        
        private void Start()
        {
            ChangeModeBtn = transform.GetComponentInChildren<Button>();
            ModeName = transform.GetComponentInChildren<Text>();
            ChangeModeBtn.onClick.AddListener(() =>
            {
                curName = string.Equals(curName, mode1Name, StringComparison.Ordinal) ? mode2Name : mode1Name;
                ModeName.text = curName;
                changeModeCallBack?.Invoke(curName);
            });
        }

        public void Init(string mode1NameP, string mode2NameP, Action<string> btnCallBack, string curNameP)
        {
            this.mode1Name = mode1NameP;
            this.mode2Name = mode2NameP;
            this.curName = curNameP;
            this.defName = curNameP;
            changeModeCallBack = btnCallBack;
        }

        private void Reset()
        {
            if (ChangeModeBtn == null)
            {
                ChangeModeBtn = transform.GetComponentInChildren<Button>();
            }
            
            if (ModeName == null)
            {
                ModeName = transform.GetComponentInChildren<Text>();
            }
        }

        public void SetValue(string value)
        {
            ModeName.text = value;
        }

        public string GetValue()
        {
            return ModeName.text;
        }
        
        public void ResetToDefault()
        {
            ModeName.text = defName;
        }
    }
}