using System;
using UnityEngine;

namespace fsp.shake
{
    // 位移震动，这里主要用 振幅 + 频率 获取实际位移曲线
    // 可以用于震屏，震屏实际上就是位移相机
    [Serializable]
    public class PositionShakeConfig : ICloneable
    {
        public PositionShakeType positionShakeType = PositionShakeType.NONE;
        public string ShakeConfigName;
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
            newClone.FrequencyXCurve = newClone.FrequencyXCurve.GetDeepCloneCurve();
            newClone.FrequencyYCurve = newClone.FrequencyYCurve.GetDeepCloneCurve();
            newClone.FrequencyZCurve = newClone.FrequencyZCurve.GetDeepCloneCurve();
            newClone.AmplitudeXCurve = newClone.AmplitudeXCurve.GetDeepCloneCurve();
            newClone.AmplitudeYCurve = newClone.AmplitudeYCurve.GetDeepCloneCurve();
            newClone.AmplitudeZCurve = newClone.AmplitudeZCurve.GetDeepCloneCurve();
            return newClone;
        }
    }
}

