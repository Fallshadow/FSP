using System;
using UnityEditor;

[CustomEditor(typeof(TestAll))]
public class TestAllInspector : Editor
{
    private TestAll parent;
    private static bool showStringToHash = false;
    
    private void OnEnable()
    {
        parent = target as TestAll;
    }

    public override void OnInspectorGUI()
    {
        showStringToHash = EditorGUILayout.BeginFoldoutHeaderGroup(showStringToHash, "测试StringToHash效率");
        if (showStringToHash)
        {
            parent.StringToHashTime = EditorGUILayout.IntField("每隔多久进行一次字符检测切换", parent.StringToHashTime);
            parent.StringToHashCount = EditorGUILayout.IntField("同时进行字符检测的个数", parent.StringToHashCount);
            parent.TestStringToHash1 = EditorGUILayout.Toggle("Update里进行两个的stringtohash", parent.TestStringToHash1);
            parent.TestStringToHash2 = EditorGUILayout.Toggle("Update里进行stringtohash好的两个的int比较", parent.TestStringToHash2);
            parent.TestStringToHash3 = EditorGUILayout.Toggle("Update里进行两个的string Ordinal 比较", parent.TestStringToHash3);
        }
    }
}
