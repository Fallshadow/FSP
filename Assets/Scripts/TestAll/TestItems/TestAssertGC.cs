using System;
using UnityEngine;

namespace fsp.testall.TestItems
{
    // 结论：Debug.Log Debug.Assert 都是有GC的 1.3Kb
    public class TestAssertGC : TestItemBase
    {
        private int[] Test4List = new int[2] {10, 20};

        public override void TestFunc0()
        {
            if (!TestBool0) return;
            Debug.Assert(Array.IndexOf(Test4List, 1) != -1);
        }

        public override void TestFunc1()
        {
            if (!TestBool1) return;
            if (Array.IndexOf(Test4List, 1) != -1)
            {
                Debug.Log(1);
            }
        }

        public override void TestFunc2()
        {
            if (!TestBool2) return;
            Debug.Log(1);
        }
    }
}