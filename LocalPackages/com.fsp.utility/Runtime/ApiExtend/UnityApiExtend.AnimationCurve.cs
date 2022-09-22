using UnityEngine;

namespace fsp
{
    public static partial class UnityApiExtend
    {
        public static AnimationCurve GetDeepCloneCurve(this AnimationCurve refCurve)
        {
            AnimationCurve result = new AnimationCurve
            {
                keys = refCurve.keys, postWrapMode = refCurve.postWrapMode, preWrapMode = refCurve.preWrapMode
            };
            return result;
        }

        /// <summary>
        /// 按某一时间比例缩放曲线
        /// </summary>
        /// <param name="refCurve">本曲线</param>
        /// <param name="newTotalTime">现在总时间</param>
        /// <param name="prevTotalTime">过去总时间</param>
        /// <returns></returns>
        public static AnimationCurve ChangeCurveTotalTimeScale(this AnimationCurve refCurve, float newTotalTime, float prevTotalTime)
        {
            Keyframe[] keys = refCurve.keys;

            AnimationCurve result = new AnimationCurve();
            float ratio = 1;
            for (int i = 0; i < keys.Length; i++)
            {
                ratio = refCurve[i].time / prevTotalTime;
                keys[i].time = ratio * newTotalTime;
            }

            result.keys = keys;
            result.postWrapMode = refCurve.postWrapMode;
            result.preWrapMode = refCurve.preWrapMode;
            return result;
        }

        // 将sourceAC的设置全全复制给targetAC 唯一不同在于targetAC会比sourceAC整体浮动addtiveValue（除了最后一个点 / 全部）
        public static AnimationCurve AddtiveAnimationCurve(this AnimationCurve sourceAC, float addtiveValue, bool includeLast = true)
        {
            AnimationCurve targetAC = new AnimationCurve();
            for (int i = 0, count = sourceAC.length; i < count; i++)
            {
                float keyvalue = sourceAC[i].value;

                if (i < sourceAC.length - 1)
                {
                    keyvalue += addtiveValue;
                }
                else if (includeLast)
                {
                    keyvalue += addtiveValue;
                }

                targetAC.AddKey(new Keyframe(
                    sourceAC[i].time, keyvalue,
                    sourceAC[i].inTangent, sourceAC[i].outTangent,
                    sourceAC[i].inWeight, sourceAC[i].outWeight));
            }

            targetAC.postWrapMode = sourceAC.postWrapMode;
            targetAC.preWrapMode = sourceAC.preWrapMode;
            return targetAC;
        }
    }
}