using System.Collections.Generic;
using fsp.editor;
using fsp.eutility;
using fsp.modelshot.Game.ObjectStylingDesigne;
using UnityEditor;
using UnityEngine;

namespace fsp.modelshot.editor.ObjectStylingDesigne
{
    [CustomEditor(typeof(ObjectStylingStrategySO))]
    public class ObjectStylingStrategySOInspector : Editor
    {
        private ObjectStylingStrategySO config;

        private bool objectPathStructsFoldOut = false;
        private bool stringFoldOut = false;
        
        void OnEnable()
        {
            config = target as ObjectStylingStrategySO;
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("保存"))
            {
                EditorUtility.SetDirty(config);
                AssetDatabase.SaveAssets();
            }
            
            using (new GUILabelWidth(120))
            {
                EUtility.ListTEditorShowNewT(config.ObjectPathStructs, "方案列表",ref objectPathStructsFoldOut, () =>
                {
                    foreach (var item in config.ObjectPathStructs)
                    {
                        using (new GUISubFeild(0))
                        {
                            var rectPopUp = EditorGUILayout.GetControlRect(false, 18f, EditorStyles.popup);
                            PopupWindowUtility.Show(rectPopUp, "方案ID枚举", item.IdType, (intP) => { item.IdType = (ObjectStylingType) intP;});
                            item.CreatePlanName = EditorGUILayout.TextField("方案名称" ,item.CreatePlanName);
                            item.ResourceFolderAssetsPath = EditorGUILayout.TextField("方案文件夹" ,item.ResourceFolderAssetsPath);
                            item.Postion = EditorGUILayout.Vector3Field("方案骨架初始化位置", item.Postion);
                            EUtility.ListTEditorShowDefaultT(item.FileSuffixStrings, "后缀列表", ref stringFoldOut, () =>
                            {
                                using (new GUISubFeild(1))
                                {
                                    if (item.FileSuffixStrings == null) item.FileSuffixStrings = new List<string>();
                                    for (int index = 0; index < item.FileSuffixStrings.Count; index++)
                                    {
                                        item.FileSuffixStrings[index] ??= "";
                                        item.FileSuffixStrings[index] = EditorGUILayout.TextField(item.FileSuffixStrings[index]);
                                    }
                                }
                            }, 1);
                        }
                    }
                }, 0);
            }
        }
    }
}