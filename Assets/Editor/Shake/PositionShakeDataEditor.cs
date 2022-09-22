using System.Collections.Generic;
using fsp.debug;
using fsp.editor;
using UnityEditor;
using UnityEngine;

namespace fsp.shake
{
    [CustomEditor(typeof(PositionShakeListSO))]
    public partial class PositionShakeConfigEditor : Editor
    {
        private PositionShakeListSO configSO = null;
        private PositionShakeConfig curConfigItem = null;
        private int curSelectPositionShakeIndex = 0;
        private List<string> positionShakeNameList = new List<string>();
        private List<int> test = new List<int>();
        
        void OnEnable()
        {
            configSO = target as PositionShakeListSO;
            initPositionShakeListData();
            reloadPositionShakeListData();
        }

        private void initPositionShakeListData()
        {
            if (configSO.ShakeConfigDatas.Count == 0)
            {
                createPositionShakeListData(Time.time.ToString());
            }
        }

        private void reloadPositionShakeListData()
        {
            positionShakeNameList.Clear();
            for (int i = 0; i < configSO.ShakeConfigDatas.Count; i++)
            {
                positionShakeNameList.Add(configSO.ShakeConfigDatas[i].ShakeConfigName);
            }
        }

        public override void OnInspectorGUI()
        {
            var curRect = GUILayoutUtility.GetRect(EditorStyles.popup.fixedWidth, EditorStyles.popup.fixedHeight);
            PopupWindowUtility.Show(curRect, "Config选择:", positionShakeNameList, curSelectPositionShakeIndex, i => { curSelectPositionShakeIndex = i; }, true);
            
            OnInspectorGUICreate();
            OnInspectorGUIBaseConfig();
            
            curConfigItem = configSO.ShakeConfigDatas[curSelectPositionShakeIndex];
        }
    }
}