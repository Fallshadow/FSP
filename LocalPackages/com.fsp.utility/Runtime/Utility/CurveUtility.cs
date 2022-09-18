using System.Collections.Generic;
using UnityEngine;

namespace fsp.utility
{
    // ToStudy:数学  振幅频率 如何合成一个曲线
    public class CurveUtility
    {
        public static AnimationCurve BuildCurve(float totalTime, AnimationCurve freqCurve, AnimationCurve amplitudeCurve)
        {
            if (freqCurve == null || amplitudeCurve == null)
            {
                return null;
            }

            AnimationCurve curve = new AnimationCurve();
            float timrRatio = totalTime;
            float curTime = 0;
            float tmpCurTime = 0;
            float tmpElapsedTime = 0;

            float buildStepDegree = 15f;
            float stepDegree = 30f;
            float startDegree = 0f;
            float endDegree = stepDegree;
            float stepRatio = (stepDegree / 360f);

            float curFreq = Mathf.Abs(freqCurve.Evaluate(0));
            float curElapsedTime = (1 / curFreq) * stepRatio;

            List<KeyValuePair<float, float>> keyList = new List<KeyValuePair<float, float>>();
            BuildSinWave(startDegree, endDegree, buildStepDegree, curFreq, keyList);
            for (int i = 0; i < keyList.Count; i++)
            {
                tmpCurTime = keyList[i].Key * timrRatio;
                curve.AddKey(tmpCurTime, keyList[i].Value * amplitudeCurve.Evaluate(tmpCurTime));
            }

            curTime += curElapsedTime;
            startDegree = endDegree + buildStepDegree;
            endDegree += stepDegree;

            while (curTime < 1)
            {
                keyList.Clear();
                curFreq = Mathf.Abs(freqCurve.Evaluate(curTime * timrRatio));
                BuildSinWave(startDegree, endDegree, buildStepDegree, curFreq, keyList);
                tmpElapsedTime = getWaveElapsedTime(startDegree - buildStepDegree, curFreq);
                for (int i = 0; i < keyList.Count; i++)
                {
                    tmpCurTime = (curTime + keyList[i].Key - tmpElapsedTime) * timrRatio;
                    curve.AddKey(tmpCurTime, keyList[i].Value * amplitudeCurve.Evaluate(tmpCurTime));
                }

                curElapsedTime = (1 / curFreq) * stepRatio;
                curTime += curElapsedTime;
                startDegree = endDegree + buildStepDegree;
                endDegree += stepDegree;
                if (startDegree > 360.0f)
                {
                    startDegree -= 360.0f;
                }

                if (endDegree > 360.0f)
                {
                    endDegree -= 360.0f;
                }
            }

            return curve;
        }

        /// <summary>
        /// 以 sin 曲線創建關鍵點, 以1秒為基準
        /// </summary>
        /// <param name="startDegree">起始角度</param>
        /// <param name="endDegree">結束角度</param>
        /// <param name="stepDegree">遞增角度</param>
        /// <param name="frequency"></param>
        public static void BuildSinWave(float startDegree, float endDegree, float stepDegree, float frequency, List<KeyValuePair<float, float>> keyList)
        {
            if (keyList == null)
            {
                keyList = new List<KeyValuePair<float, float>>();
            }

            float time = 0;
            float sin = 0;
            float PIx2 = Mathf.PI * 2f;
            for (float i = startDegree; i < endDegree; i += stepDegree)
            {
                time = getWaveElapsedTime(i, frequency);
                sin = Mathf.Sin(time * PIx2 * frequency);

                KeyValuePair<float, float> keyValue = new KeyValuePair<float, float>(time, sin);
                keyList.Add(keyValue);
            }

            // last point
            time = getWaveElapsedTime(endDegree, frequency);
            sin = Mathf.Sin(time * PIx2 * frequency);
            KeyValuePair<float, float> lastKeyValue = new KeyValuePair<float, float>(time, sin);
            keyList.Add(lastKeyValue);
        }

        private static float getWaveElapsedTime(float degree, float frequency)
        {
            if (frequency <= 0)
            {
                return 0;
            }

            float result = (degree * Mathf.Deg2Rad) / (Mathf.PI * 2f * frequency);
            return result;
        }
    }
}