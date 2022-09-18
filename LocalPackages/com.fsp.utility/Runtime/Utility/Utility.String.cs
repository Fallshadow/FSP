using System;
using UnityEngine;

namespace fsp.utility
{
    // 静态通用方法——字符串相关处理
    public static partial class Utility
    {
        // 换行会中断掉颜色的衔接所以要把每个行字符串都放上颜色
        public static string GetColoredString(string title, string str, Color color)
        {
            string colorCode = ColorUtility.ToHtmlStringRGB(color);
            string[] segmentedStr = str.Split('\n');
            segmentedStr[0] = $"{title} {segmentedStr[0]}";
            for (int i = 0; i < segmentedStr.Length; i++)
            {
                segmentedStr[i] = $"<color=#{colorCode}>{segmentedStr[i]}</color>";
            }

            // prevent unneccesary cost.
            return (segmentedStr.Length == 1) ? segmentedStr[0] : String.Join("\n", segmentedStr);
        }
    }
}