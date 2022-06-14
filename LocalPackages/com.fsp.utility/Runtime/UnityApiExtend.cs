using UnityEngine;

// 这边是对unity的方法扩展
public static class UnityApiExtend 
{
    #region String

    public static bool IsNullOrEmptyEx(this string str)
    {
        return string.IsNullOrEmpty(str);
    }

    #endregion


    #region AnimationCurve

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
            else if(includeLast)
            {
                keyvalue += addtiveValue;
            }

            targetAC.AddKey(new Keyframe(
                sourceAC[i].time, keyvalue,
                sourceAC[i].inTangent, sourceAC[i].outTangent,
                sourceAC[i].inWeight,  sourceAC[i].outWeight));
        }
        targetAC.postWrapMode = sourceAC.postWrapMode;
        targetAC.preWrapMode  = sourceAC.preWrapMode;
        return targetAC;
    }

    #endregion
}
