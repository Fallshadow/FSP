using System;
using UnityEngine;

namespace fsp.shake
{
    // 位移震动，这里主要用 振幅 + 频率 获取实际位移曲线
    // 可以用于震屏，震屏实际上就是位移相机
    [Serializable]
    public class PositionShakeConfig : ICloneable
    {
        public PositionShakeType PSType = PositionShakeType.NONE;
        public string ShakeConfigName;

        public bool ApplyRandomShake = false;
        public float DelayShakeTime = 0f;
        public float TotalShakeTime = 0f;
        
        public AnimationCurve FrequencyXCurve;
        public AnimationCurve FrequencyYCurve;
        public AnimationCurve FrequencyZCurve;
        public AnimationCurve AmplitudeXCurve;
        public AnimationCurve AmplitudeYCurve;
        public AnimationCurve AmplitudeZCurve;
        
        public object Clone()
        {
            // 先用浅拷贝创建新克隆
            PositionShakeConfig newClone = (PositionShakeConfig)MemberwiseClone();
            // 然后依次对新克隆创建深拷贝字段
            newClone.FrequencyXCurve = this.FrequencyXCurve.GetDeepCloneCurve();
            newClone.FrequencyYCurve = this.FrequencyYCurve.GetDeepCloneCurve();
            newClone.FrequencyZCurve = this.FrequencyZCurve.GetDeepCloneCurve();
            newClone.AmplitudeXCurve = this.AmplitudeXCurve.GetDeepCloneCurve();
            newClone.AmplitudeYCurve = this.AmplitudeYCurve.GetDeepCloneCurve();
            newClone.AmplitudeZCurve = this.AmplitudeZCurve.GetDeepCloneCurve();
            return newClone;
        }

        public void ChangeCurveTotalTimeScale(float newTotalTime, float prevTotalTime)
        {
            FrequencyXCurve = FrequencyXCurve.ChangeCurveTotalTimeScale(newTotalTime, prevTotalTime);
            FrequencyYCurve = FrequencyYCurve.ChangeCurveTotalTimeScale(newTotalTime, prevTotalTime);
            FrequencyZCurve = FrequencyZCurve.ChangeCurveTotalTimeScale(newTotalTime, prevTotalTime);
            AmplitudeXCurve = AmplitudeXCurve.ChangeCurveTotalTimeScale(newTotalTime, prevTotalTime);
            AmplitudeYCurve = AmplitudeYCurve.ChangeCurveTotalTimeScale(newTotalTime, prevTotalTime);
            AmplitudeZCurve = AmplitudeZCurve.ChangeCurveTotalTimeScale(newTotalTime, prevTotalTime);
        }
    }
}

