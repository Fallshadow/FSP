using System;
using System.Globalization;
using UnityEditor;
using UnityEngine;

namespace fsp.shake
{
    public partial class PositionShakeConfigEditor
    {
        private bool onInspectorGUIShowCreate = false;
        private PositionShakeType tempCreateShakeType = PositionShakeType.NONE;
        private string tempCreateShakeName = "新的震动";
        
        private void createPositionShakeListData(string dataName)
        {
            PositionShakeConfig createData = new PositionShakeConfig();
            createData.ShakeConfigName = dataName;
            configSO.ShakeConfigDatas.Add(createData);
        }

        private void OnInspectorGUICreate()
        {
            EditorGUILayout.Space();
            onInspectorGUIShowCreate = EditorGUILayout.BeginFoldoutHeaderGroup(onInspectorGUIShowCreate, "[ 创 建 配 置 ]");
            if (!onInspectorGUIShowCreate)
            {
                EditorGUILayout.EndFoldoutHeaderGroup();
                return;
            }

            EditorGUILayout.BeginHorizontal(GUI.skin.box);
            EditorGUILayout.BeginVertical();
            PopupWindowUtility.Show(GUILayoutUtility.GetRect(EditorStyles.popup.fixedWidth, EditorStyles.popup.fixedHeight), 
                "震动类型选择：",tempCreateShakeType,  i => { tempCreateShakeType = (PositionShakeType)i; });
            tempCreateShakeName = EditorGUILayout.TextField("震动名称：", tempCreateShakeName);
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            
            if (GUILayout.Button("New"))
            {
                Undo.RecordObject(configSO, "Add Shake Config");
                PositionShakeConfig newConfigData = new PositionShakeConfig();
                newConfigData.ShakeConfigName = checkCreateDataName(tempCreateShakeName);
                newConfigData.PSType = tempCreateShakeType;
                configSO.ShakeConfigDatas.Add(newConfigData);
                curSelectPositionShakeIndex = configSO.ShakeConfigDatas.Count - 1;
                reloadPositionShakeListData();
            }
            
            if (GUILayout.Button("Clone") && configSO.ShakeConfigDatas.Count > 0)
            {
                Undo.RecordObject(configSO, "Add Shake Config");
                PositionShakeConfig cloneData = (PositionShakeConfig) curConfigItem.Clone();
                cloneData.ShakeConfigName += DateTime.Now.ToString(CultureInfo.CurrentCulture);
                configSO.ShakeConfigDatas.Add(cloneData);
                curSelectPositionShakeIndex = configSO.ShakeConfigDatas.Count - 1;
                reloadPositionShakeListData();
            }

            if (GUILayout.Button("Delete"))
            {
                Undo.RecordObject(configSO, "Remove Shake Config");
                configSO.ShakeConfigDatas.RemoveAt(curSelectPositionShakeIndex);
                curSelectPositionShakeIndex = 0;
                reloadPositionShakeListData();
            }

            if (GUILayout.Button("Save"))
            {
                EditorUtility.SetDirty(configSO);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        private string checkCreateDataName(string checkName)
        {
            foreach (var itemName in positionShakeNameList)
            {
                if (String.Equals(itemName, checkName, StringComparison.Ordinal))
                {
                    checkName += DateTime.Now.ToString(CultureInfo.CurrentCulture);
                    break;
                }
            }

            return checkName;
        }
    }
}