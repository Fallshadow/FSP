using UnityEditor;
using UnityEngine;

namespace fsp.shake
{
    public partial class PositionShakeConfigEditor
    {
        private bool onInspectorGUIShowBaseConfig = false;
        private void OnInspectorGUIBaseConfig()
        {
            EditorGUILayout.Space();
            onInspectorGUIShowBaseConfig = EditorGUILayout.BeginFoldoutHeaderGroup(onInspectorGUIShowBaseConfig, "[ 基 础 配 置 ]");
            if (!onInspectorGUIShowBaseConfig)
            {
                EditorGUILayout.EndFoldoutHeaderGroup();
                return;
            }
            
            EditorGUILayout.BeginHorizontal(GUI.skin.box);
            EditorGUILayout.BeginVertical();
            EditorGUI.BeginChangeCheck();
            curConfigItem.ShakeConfigName = EditorGUILayout.TextField("震动名称：", curConfigItem.ShakeConfigName);
            if (EditorGUI.EndChangeCheck())
            {
                curConfigItem.ShakeConfigName = checkCreateDataName(curConfigItem.ShakeConfigName);
                reloadPositionShakeListData();
            }
            curConfigItem.ApplyRandomShake = EditorGUILayout.Toggle("应用随机震动：", curConfigItem.ApplyRandomShake);
            curConfigItem.DelayShakeTime = EditorGUILayout.FloatField("延迟震动开始时间：", curConfigItem.DelayShakeTime);
            EditorGUI.BeginChangeCheck();
            float newTotalShakeTime = EditorGUILayout.FloatField("震动总时长：", curConfigItem.TotalShakeTime);
            if (EditorGUI.EndChangeCheck()) curConfigItem.ChangeCurveTotalTimeScale(newTotalShakeTime, curConfigItem.TotalShakeTime);
            curConfigItem.TotalShakeTime = newTotalShakeTime;
            
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }
    }
}