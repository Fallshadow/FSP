using System;
using System.Collections.Generic;
using fsp.debug;
using fsp.testall.TestItems;

namespace fsp.testall
{
    public static class TestItemFactory
    {
        private static Dictionary<TestItemType, Type> _types = new Dictionary<TestItemType, Type>();

        static TestItemFactory()
        {
            _types.Add(TestItemType.TIT_NONE, typeof(TestItemBase));
            _types.Add(TestItemType.TIT_StringToHash, typeof(TestStringToHash));
            _types.Add(TestItemType.TIT_Str_Color_Line, typeof(TestStrColorLine));
            _types.Add(TestItemType.TIT_Enum_Long, typeof(TestEnumLong));
            _types.Add(TestItemType.TIT_Assert_GC, typeof(TestAssertGC));
            _types.Add(TestItemType.TIT_Str_Split, typeof(TestStringSplit));
            
            
            
            _types.Add(TestItemType.TIT_AnimationCurveAdditive, typeof(TestAnimationCurvetAdditive));
        }
        
        public static T GetTestItem<T>(TestItemType testItemType) where T : TestItemBase
        {
            object obj = Activator.CreateInstance(_types[testItemType]);
            if (!(obj is T testItemBase))
            {
                PrintSystem.LogError($"[TestAll] {typeof(T)} 转换失败");
                return null;
            }

            testItemBase.TestItemType = testItemType;
            return testItemBase;
        }
    }
}