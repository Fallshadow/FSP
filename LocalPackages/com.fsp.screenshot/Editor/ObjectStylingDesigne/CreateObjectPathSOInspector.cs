﻿using System.Collections.Generic;
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
        private List<bool> stringFoldOuts = new List<bool>();
        private List<bool> objectInfoFoldOuts = new List<bool>();
        
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
                    
                    for (int configIndex = 0; configIndex < config.ObjectPathStructs.Count; configIndex++)
                    {
                        ObjectStylingStrategyInfo item = config.ObjectPathStructs[configIndex];
                        stringFoldOuts.Expand(configIndex);
                        objectInfoFoldOuts.Expand(configIndex);
                        bool stringFoldOut = stringFoldOuts[configIndex];
                        bool objectInfoFoldOut = objectInfoFoldOuts[configIndex];
                        using (new GUISubFeild(0))
                        {
                            var rectPopUp = EditorGUILayout.GetControlRect(false, 18f, EditorStyles.popup);
                            PopupWindowUtility.Show(rectPopUp, "方案ID枚举", item.IdType, (intP) => { item.IdType = (ObjectStylingType) intP; });
                            item.CreatePlanName = EditorGUILayout.TextField("方案名称", item.CreatePlanName);
                            item.ResourceFolderAssetsPath = EditorGUILayout.TextField("方案文件夹", item.ResourceFolderAssetsPath);
                            item.Postion = EditorGUILayout.Vector3Field("方案骨架初始化位置", item.Postion);
                            item.MaxLayer = EditorGUILayout.IntField("方案骨架总层数", item.MaxLayer);
                            EUtility.ListTEditorShowDefaultT(item.FileSuffixStrings, "后缀列表", ref stringFoldOut, () =>
                            {
                                using (new GUISubFeild(1))
                                {
                                    for (int index = 0; index < item.FileSuffixStrings.Count; index++)
                                    {
                                        item.FileSuffixStrings[index] ??= "";
                                        item.FileSuffixStrings[index] = EditorGUILayout.TextField(item.FileSuffixStrings[index]);
                                    }
                                }
                            }, 1);
                            
                            EUtility.ListTEditorShowNewT(item.ObjectInfos, "物体详细信息", ref objectInfoFoldOut, () =>
                            {
                                using (new GUISubFeild(1))
                                {
                                    for (int osIndex = 0; osIndex < item.ObjectInfos.Count; osIndex++)
                                    {
                                        EditorGUILayout.LabelField($"第{osIndex}个");
                                        ObjectStylingWorldTransInfo oSWorldTransInfo = item.ObjectInfos[osIndex];
                                        oSWorldTransInfo.Position = EditorGUILayout.Vector3Field("相对Pos", oSWorldTransInfo.Position);
                                        oSWorldTransInfo.Rotation = EditorGUILayout.Vector3Field("相对Rot", oSWorldTransInfo.Rotation);
                                        oSWorldTransInfo.Scale = EditorGUILayout.Vector3Field("相对Scale", oSWorldTransInfo.Scale);
                                        oSWorldTransInfo.SkeletonLayer = EditorGUILayout.IntField("层级", oSWorldTransInfo.SkeletonLayer);
                                    }
                                }
                            }, 1);
                        }

                        stringFoldOuts[configIndex] = stringFoldOut;
                        objectInfoFoldOuts[configIndex] = objectInfoFoldOut;
                    }
                }, 0);
            }
        }
    }
}