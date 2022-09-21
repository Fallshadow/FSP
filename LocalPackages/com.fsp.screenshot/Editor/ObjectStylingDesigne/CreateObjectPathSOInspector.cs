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
            EUtility.ListTEditorShowNewT(config.ObjectPathStructs, "方案列表",ref objectPathStructsFoldOut, () =>
            {
                foreach (var item in config.ObjectPathStructs)
                {
                    using (new GUIHorizontalGroup(true))
                    {
                        item.CreatePlanName = EditorGUILayout.TextField(item.CreatePlanName);
                        item.ResourceFolderAssetsPath = EditorGUILayout.TextField(item.ResourceFolderAssetsPath);
                    }

                    EUtility.ListTEditorShowDefaultT(item.FileSuffixStrings, "后缀列表", ref stringFoldOut, () =>
                    {
                        for (int index = 0; index < item.FileSuffixStrings.Count; index++)
                        {
                            item.FileSuffixStrings[index] ??= "";
                            item.FileSuffixStrings[index] = EditorGUILayout.TextField(item.FileSuffixStrings[index]);
                        }
                    });
                }
            });
        }
    }
}