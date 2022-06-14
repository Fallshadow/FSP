using System;
using UnityEngine;

namespace fsp.testall.TestItems
{
    [Flags]
    public enum Test3EnumLong0 : ulong
    {
        Item32 = (ulong) 1 << 31,
        Item64 = (ulong) 1 << 63,
    }
    
    public enum Test3EnumLong1
    {
        Item32 = 1 << 31,
    }
    // ToStudy 未解之谜！
    // 结论：非常遗憾的问题是，longEnum确实是8字节但是Inspector就是报错！
    public class TestEnumLong : TestItemBase
    {
        public ulong Test3MyEnumLong0 = (ulong) Test3EnumLong0.Item32;
        public ulong Test3MyEnumLong1 = (ulong) Test3EnumLong0.Item64;

        public override void TestFunc0()
        {
            if (!TestBool0) return;
            Debug.Log($"32: {(ulong) Test3EnumLong0.Item32} 64: {(ulong) Test3EnumLong0.Item64}");
            Debug.Log($"Enum Test3EnumLong0 SizeOf: {sizeof(Test3EnumLong0)}");
            Debug.Log($"ulong Test3MyEnumLong0 sizeof: {sizeof(ulong)}");
            Debug.Log($"Enum Test3EnumLong1 SizeOf: {sizeof(Test3EnumLong1)}");
            TestBool0 = false;
        }
    }
}