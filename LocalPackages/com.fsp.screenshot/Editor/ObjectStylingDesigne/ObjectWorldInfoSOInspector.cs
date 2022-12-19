using System.Collections.Generic;
using fsp.eutility;
using UnityEditor;
using UnityEngine;

namespace fsp.ObjectStylingDesigne
{
    [CustomEditor(typeof(ObjectWorldInfoSO))]
    public class ObjectWorldInfoSOInspector : Editor
    {
        private ObjectWorldInfoSO config;
        private bool worldInfoFoldOut = false;
        private List<bool> objectInfoFoldOuts = new List<bool>();
        
        void OnEnable()
        {
            config = target as ObjectWorldInfoSO;
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
                EUtility.ListTEditorShowNewT(config.WorldInfos, "方案列表",ref worldInfoFoldOut, () =>
                {
                    for (int configIndex = 0; configIndex < config.WorldInfos.Count; configIndex++)
                    {
                        ObjectWorldInfo item = config.WorldInfos[configIndex];
                        objectInfoFoldOuts.Expand(configIndex);
                        bool objectInfoFoldOut = objectInfoFoldOuts[configIndex];
                        using (new GUISubFeild(0))
                        {
                            item.WorldInfoName = EditorGUILayout.TextField("方案名称", item.WorldInfoName);
                            EUtility.ListTEditorShowNewT(item.CInfos, "物体详细信息", ref objectInfoFoldOut, () =>
                            {
                                using (new GUISubFeild(1))
                                {
                                    for (int osIndex = 0; osIndex < item.CInfos.Count; osIndex++)
                                    {
                                        EditorGUILayout.LabelField($"第{osIndex}个");
                                        ObjectStylingWorldTransInfo oSWorldTransInfo = item.CInfos[osIndex];
                                        oSWorldTransInfo.Position = EditorGUILayout.Vector3Field("相对Pos", oSWorldTransInfo.Position);
                                        oSWorldTransInfo.Rotation = EditorGUILayout.Vector3Field("相对Rot", oSWorldTransInfo.Rotation);
                                        oSWorldTransInfo.Scale = EditorGUILayout.Vector3Field("相对Scale", oSWorldTransInfo.Scale);
                                        oSWorldTransInfo.SkeletonLayer = EditorGUILayout.IntField("层级", oSWorldTransInfo.SkeletonLayer);
                                        oSWorldTransInfo.IsScaleObj = EditorGUILayout.Toggle("是否要相对缩放", oSWorldTransInfo.IsScaleObj);
                                    }
                                }
                            }, 1);
                        }
                        objectInfoFoldOuts[configIndex] = objectInfoFoldOut;
                    }
                }, 0);
            }
        }
    }
}