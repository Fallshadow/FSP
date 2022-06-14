using System;
using UnityEditor;
using fsp.testall;

[CustomEditor(typeof(TestAll))]
public class TestAllInspector : Editor
{
    private TestAll parent;
    private static bool showTest1StringToHash = false;
    private static bool showTest2StrColor = false;
    private static bool showTest3EnumLong = false;
    private static bool showTest4Assert = false;
    private static bool showTest5StringSplit = false;
    
    private void OnEnable()
    {
        parent = target as TestAll;
    }

    public override void OnInspectorGUI()
    {
        showTest1StringToHash = EditorGUILayout.BeginFoldoutHeaderGroup(showTest1StringToHash, "测试StringToHash效率");
        if (showTest1StringToHash)
        {
            parent.StringToHashTime = EditorGUILayout.IntField("每隔多久进行一次字符检测切换", parent.StringToHashTime);
            parent.StringToHashCount = EditorGUILayout.IntField("同时进行字符检测的个数", parent.StringToHashCount);
            parent.TestStringToHash1 = EditorGUILayout.Toggle("Update里进行两个的stringtohash", parent.TestStringToHash1);
            parent.TestStringToHash2 = EditorGUILayout.Toggle("Update里进行stringtohash好的两个的int比较", parent.TestStringToHash2);
            parent.TestStringToHash3 = EditorGUILayout.Toggle("Update里进行两个的string Ordinal 比较", parent.TestStringToHash3);
        } 
        EditorGUILayout.EndFoldoutHeaderGroup();
        
        showTest2StrColor = EditorGUILayout.BeginFoldoutHeaderGroup(showTest2StrColor, "测试str和color log换行");
        if (showTest2StrColor)
        {
            parent.Test2StrColor0 = EditorGUILayout.Toggle("Update里自由配置str和color", parent.Test2StrColor0);
            parent.Test2Str0 = EditorGUILayout.TextField("Update里str", parent.Test2Str0);
            parent.Test2Str1 = EditorGUILayout.TextField("Update里str", parent.Test2Str1);
            parent.Test2Color0 = EditorGUILayout.ColorField("Update里color", parent.Test2Color0);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        
        showTest3EnumLong = EditorGUILayout.BeginFoldoutHeaderGroup(showTest3EnumLong, "测试Enum的long");
        if (showTest3EnumLong)
        {
            parent.RunTest3EnumLong0 = EditorGUILayout.Toggle("Update里EnumLong", parent.RunTest3EnumLong0);
            EditorGUILayout.LabelField($"Update里EnumLong0:{parent.Test3MyEnumLong0}");
            EditorGUILayout.LabelField($"Update里EnumLong1:{parent.Test3MyEnumLong1}");
            // parent.Test3MyEnum0 = (Test3EnumLong0)EditorGUILayout.EnumFlagsField($"Update里Enum0:",parent.Test3MyEnum0);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        
        showTest4Assert = EditorGUILayout.BeginFoldoutHeaderGroup(showTest4Assert, "测试Assert GC");
        if (showTest4Assert)
        {
            parent.RunTest4Assert0 = EditorGUILayout.Toggle("Assert", parent.RunTest4Assert0);
            parent.RunTest4Assert1 = EditorGUILayout.Toggle("BOOL Log", parent.RunTest4Assert1);
            parent.RunTest4Assert2 = EditorGUILayout.Toggle("Log", parent.RunTest4Assert2);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        
        showTest5StringSplit = EditorGUILayout.BeginFoldoutHeaderGroup(showTest5StringSplit, "测试String Split");
        if (showTest5StringSplit)
        {
            parent.RunTest5String0 = EditorGUILayout.Toggle("Test5StringSplit", parent.RunTest5String0);
            parent.Test5String = EditorGUILayout.TextField("输入;间隔字符串", parent.Test5String);
            EditorGUILayout.TextField("字符串结果", parent.Test5ResultString);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }
}
