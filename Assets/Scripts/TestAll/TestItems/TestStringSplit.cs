using System;
using UnityEngine;

namespace fsp.testall.TestItems
{
    // 结论：主要是这个接口：XXXXX.Split(XXXXX, StringSplitOptions.RemoveEmptyEntries)
    public class TestStringSplit : TestItemBase
    {
        public string Test5String = "A;;B;;C;D;E";
        public string Test5ResultString = "";
        public char[] Test5StringSplit = new char[1] {';'};

        public override void TestFunc0()
        {
            if (!TestBool0) return;
            Test5ResultString = string.Join(";", Test5String.Split(Test5StringSplit, StringSplitOptions.RemoveEmptyEntries));
            Debug.Log(Test5ResultString);
            TestBool0 = false;
        }
    }
}