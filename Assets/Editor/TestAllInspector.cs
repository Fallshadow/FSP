using System;
using System.Collections.Generic;
using UnityEditor;
using fsp.testall;
using fsp.testall.TestItems;

[CustomEditor(typeof(TestAll))]
public class TestAllInspector : Editor
{
    private TestAll parent;

    private List<bool> storeBools = new List<bool>();
    
    private void OnEnable()
    {
        parent = target as TestAll;
    }

    public override void OnInspectorGUI()
    {
        for (int index = 0; index < parent.TestItems.Count; index++)
        {
            TestItemBase itemBase = parent.TestItems[index];
            if(itemBase.TestItemType == TestItemType.TIT_NONE) continue;
            while(index >= storeBools.Count) storeBools.Add(false);
            string title = getTitle(itemBase.TestItemType);
            storeBools[index] = EditorGUILayout.BeginFoldoutHeaderGroup(storeBools[index], title);
            if (storeBools[index])
            {
                showInspector(itemBase);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }

    private static string getTitle(TestItemType testItemType)
    {
        string result = "None";
        switch (testItemType)
        {
            case TestItemType.TIT_NONE:
                break;
            case TestItemType.TIT_StringToHash:
                result = "测试StringToHash效率";
                break;
            case TestItemType.TIT_Str_Color_Line:
                result = "测试str和color log换行";
                break;
            case TestItemType.TIT_Enum_Long:
                result = "测试Enum的long";
                break;
            case TestItemType.TIT_Assert_GC:
                result = "测试Assert GC";
                break;
            case TestItemType.TIT_Str_Split:
                result = "测试String Split";
                break;
            case TestItemType.TIT_AnimationCurveAdditive:
                result = "测试 AnimationCurve Additive";
                break;
            default:
                break;
        }

        return result;
    }

    private static void showInspector(TestItemBase itemBase)
    {
        TestItemType testItemType = itemBase.TestItemType;
        switch (testItemType)
        {
            case TestItemType.TIT_NONE:
                break;
            case TestItemType.TIT_StringToHash:
                TestStringToHash testStringToHash = itemBase as TestStringToHash;
                if (testStringToHash == null) return;
                testStringToHash.StringToHashTime = EditorGUILayout.IntField("每隔多久进行一次字符检测切换", testStringToHash.StringToHashTime);
                testStringToHash.StringToHashCount = EditorGUILayout.IntField("同时进行字符检测的个数", testStringToHash.StringToHashCount);
                testStringToHash.TestBool0 = EditorGUILayout.Toggle("Update里进行两个的stringtohash", testStringToHash.TestBool0);
                testStringToHash.TestBool1 = EditorGUILayout.Toggle("Update里进行stringtohash好的两个的int比较", testStringToHash.TestBool1);
                testStringToHash.TestBool2 = EditorGUILayout.Toggle("Update里进行两个的string Ordinal 比较", testStringToHash.TestBool2);
                break;
            case TestItemType.TIT_Str_Color_Line:
                TestStrColorLine testStrColorLine = itemBase as TestStrColorLine;
                if (testStrColorLine == null) return;
                testStrColorLine.TestBool0 = EditorGUILayout.Toggle("Update里自由配置str和color", testStrColorLine.TestBool0);
                testStrColorLine.Test2Str0 = EditorGUILayout.TextField("Update里str", testStrColorLine.Test2Str0);
                testStrColorLine.Test2Str1 = EditorGUILayout.TextField("Update里str", testStrColorLine.Test2Str1);
                testStrColorLine.Test2Color0 = EditorGUILayout.ColorField("Update里color", testStrColorLine.Test2Color0);
                break;
            case TestItemType.TIT_Enum_Long:
                TestEnumLong testEnumLong = itemBase as TestEnumLong;
                if (testEnumLong == null) return;
                testEnumLong.TestBool0 = EditorGUILayout.Toggle("Update里EnumLong", testEnumLong.TestBool0);
                EditorGUILayout.LabelField($"Update里EnumLong0:{testEnumLong.Test3MyEnumLong0}");
                EditorGUILayout.LabelField($"Update里EnumLong1:{testEnumLong.Test3MyEnumLong1}");
                break;
            case TestItemType.TIT_Assert_GC:
                TestAssertGC testAssertGC = itemBase as TestAssertGC;
                if (testAssertGC == null) return;
                testAssertGC.TestBool0 = EditorGUILayout.Toggle("Assert",   testAssertGC.TestBool0);
                testAssertGC.TestBool1 = EditorGUILayout.Toggle("BOOL Log", testAssertGC.TestBool1);
                testAssertGC.TestBool2 = EditorGUILayout.Toggle("Log",      testAssertGC.TestBool2);
                break;
            case TestItemType.TIT_Str_Split:
                TestStringSplit testStringSplit = itemBase as TestStringSplit;
                if (testStringSplit == null) return;
                testStringSplit.TestBool0 = EditorGUILayout.Toggle("Test5StringSplit", testStringSplit.TestBool0);
                testStringSplit.Test5String = EditorGUILayout.TextField("输入;间隔字符串", testStringSplit.Test5String);
                EditorGUILayout.TextField("字符串结果", testStringSplit.Test5ResultString);
                break;
            case TestItemType.TIT_AnimationCurveAdditive:
                TestAnimationCurvetAdditive testAnimationCurvetAdditive = itemBase as TestAnimationCurvetAdditive;
                if (testAnimationCurvetAdditive == null) return;
                testAnimationCurvetAdditive.TestBool0 = EditorGUILayout.Toggle("Copy And Add Once", testAnimationCurvetAdditive.TestBool0);
                testAnimationCurvetAdditive.additiveValue = EditorGUILayout.FloatField("Additive Value", testAnimationCurvetAdditive.additiveValue);
                testAnimationCurvetAdditive.sourceAC = EditorGUILayout.CurveField("Source Curve", testAnimationCurvetAdditive.sourceAC);
                EditorGUILayout.CurveField("Result Curve", testAnimationCurvetAdditive.targetAC);
                break;
            default:
                break;
        }
    }
}
