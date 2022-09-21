using fsp.eutility;
using fsp.modelshot.Game.ObjectStylingDesigne;
using UnityEditor;
using UnityEngine;

namespace fsp.modelshot.editor.ObjectStylingDesigne
{
    [CustomEditor(typeof(CreateObjectPathSO))]
    public class CreateObjectPathSOInspector : Editor
    {
        private CreateObjectPathSO config;

        private bool objectPathStructsFoldOut = false;
        private bool stringFoldOut = false;
        
        void OnEnable()
        {
            config = target as CreateObjectPathSO;
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("保存"))
            {
                EditorUtility.SetDirty(config);
                AssetDatabase.SaveAssets();
            }

            using (new GUILabelWidth(70))
            {
                EUtility.ListTEditorShowNewT(config.ObjectPathStructs, "方案列表",ref objectPathStructsFoldOut, () =>
                {
                    foreach (var item in config.ObjectPathStructs)
                    {
                        using (new GUISubFeild(0))
                        {
                            item.CreatePlanName = EditorGUILayout.TextField("方案名称" ,item.CreatePlanName);
                            item.ResourceFolderAssetsPath = EditorGUILayout.TextField("方案文件夹" ,item.ResourceFolderAssetsPath);

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
                        }
                    }
                }, 0);
            }
        }
    }
}