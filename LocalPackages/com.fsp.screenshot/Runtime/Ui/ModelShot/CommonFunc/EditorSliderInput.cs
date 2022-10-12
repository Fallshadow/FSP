using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace fsp.modelshot.ui
{
    public class EditorSliderInput : MonoBehaviour
    {
        public Slider MSlider = null;
        public InputField MInputField = null;
        private Action<float> silderCallBack = null;
        private Action<string> inputCallBack = null;
        private float MDefValue = 0;
        private void Start()
        {
            
        }

        public void Init(float min,float max,Action<float> floatMethod,Action<string> stringMethod,float defValue)
        {
            MDefValue = defValue;
            MSlider.minValue = min;
            MSlider.maxValue = max;
            silderCallBack = floatMethod;
            inputCallBack = stringMethod;
            ResetToDefault();
            
            MSlider = transform.GetComponentInChildren<Slider>();
            MInputField = transform.GetComponentInChildren<InputField>();
            MSlider.onValueChanged.RemoveAllListeners();
            MSlider.onValueChanged.AddListener(value =>
            {
                MInputField.text = value.ToString(CultureInfo.CurrentCulture);
                silderCallBack?.Invoke(value);
            });
            MInputField.onValueChanged.RemoveAllListeners();
            MInputField.onValueChanged.AddListener(value =>
            {
                inputCallBack?.Invoke(value);
            });
        }

        public void ResetToDefault()
        {
            MSlider.value = MDefValue;
            MInputField.text = MDefValue.ToString(CultureInfo.CurrentCulture);
        }

        private void Reset()
        {
            if (MSlider == null)
            {
                MSlider = transform.GetComponentInChildren<Slider>();
            }

            if (MInputField == null)
            {
                MInputField = transform.GetComponentInChildren<InputField>();
            }
        }

        public void SetValue(float value)
        {
            MInputField.text = value.ToString(CultureInfo.CurrentCulture);
            MSlider.value = value;
        }

        public float GetValue()
        {
            if (MInputField.text == "")
            {
                return 0;
            }
            return float.Parse(MInputField.text);
        }
        
    }
}