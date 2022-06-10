using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace fsp.testall
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

    public class TestAll : MonoBehaviour
    {
        void Update()
        {
            #region 测试：stringToHash 结论：10W条Animator.StringToHash 要26ms 是很慢的 是不可以在update调用的

            RunStringToHash();
            RunStringToHash2();
            RunStringToHash3();

            #endregion

            #region 测试2：str 换行和 颜色的兼容 结论：换行会中断掉颜色的衔接所以要把每个行字符串都放上颜色

            RunStr0();

            #endregion

            // ToStudy 未解之谜！
            #region 测试3:Enum long 到底可以不可以 到底4字节还是8字节 结论：非常遗憾的问题是，longEnum确实是8字节但是Inspector就是报错！

            RunTest3EnumLong();

            #endregion
            
            #region 测试4：Assert有原生的GC？ 结论：Debug.Log Debug.Assert 都是有GC的 1.3Kb

            RunTest4AssertFunc0();
            RunTest4AssertFunc1();
            RunTest4AssertFunc2();

            #endregion
        }

        #region 测试stringToHash

        public bool TestStringToHash1 = false;
        public bool TestStringToHash2 = false;
        public bool TestStringToHash3 = false;


        private string[] stringToHashArray = new string[]
        {
            "霸体",
            "格挡",
            "灵能飞弹",
            "强击锤蓄力",
            "格挡无敌",
            "临时霸体",
            "爆燃进击",
            "霸体减伤",
            "飞行蓄力",
            "命中加武装强化",
            "刃镰形态",
            "战枪形态",
            "御伞形态",
            "幻影旋灭",
        };

        private int[] stringToHashArray2 = new int[]
        {
            Animator.StringToHash("霸体"),
            Animator.StringToHash("格挡"),
            Animator.StringToHash("灵能飞弹"),
            Animator.StringToHash("强击锤蓄力"),
            Animator.StringToHash("格挡无敌"),
            Animator.StringToHash("临时霸体"),
            Animator.StringToHash("爆燃进击"),
            Animator.StringToHash("霸体减伤"),
            Animator.StringToHash("飞行蓄力"),
            Animator.StringToHash("命中加武装强化"),
            Animator.StringToHash("刃镰形态"),
            Animator.StringToHash("战枪形态"),
            Animator.StringToHash("御伞形态"),
            Animator.StringToHash("幻影旋灭"),
        };

        public int StringToHashTime = 10;
        private float stringToHashTimer = 0;
        public int StringToHashCount = 1;
        private string stringToHashTestString = "对比的字符串";
        private int stringToHashTestString2 = Animator.StringToHash("对比的字符串");

        private int stringToHashLength = 0;
        private int stringToHashCurIndex = 0;
        private List<int> stringToHashList = new List<int>();

        private void RunStringToHash()
        {
            if (!TestStringToHash1) return;
            if (stringToHashTimer == 0)
            {
                stringToHashList.Clear();
                for (int index = 0; index < StringToHashCount; index++)
                {
                    stringToHashList.Add(Random.Range(0, stringToHashArray.Length));
                }
            }

            stringToHashTimer += Time.deltaTime;
            for (int index = 0; index < StringToHashCount; index++)
            {
                int hash = Animator.StringToHash(stringToHashArray[stringToHashList[index]]);
                int compareHash = Animator.StringToHash(stringToHashTestString);
            }

            if (stringToHashTimer > StringToHashTime)
            {
                stringToHashTimer = 0;
            }
        }

        private void RunStringToHash2()
        {
            if (!TestStringToHash2) return;

            if (stringToHashTimer == 0)
            {
                stringToHashList.Clear();
                for (int index = 0; index < StringToHashCount; index++)
                {
                    stringToHashList.Add(Random.Range(0, stringToHashArray2.Length));
                }
            }

            stringToHashTimer += Time.deltaTime;
            for (int index = 0; index < StringToHashCount; index++)
            {
                int hash = stringToHashArray2[stringToHashList[index]];
                int compareHash = stringToHashTestString2;
            }

            if (stringToHashTimer > StringToHashTime)
            {
                stringToHashTimer = 0;
            }
        }

        private void RunStringToHash3()
        {
            if (!TestStringToHash3) return;

            if (stringToHashTimer == 0)
            {
                stringToHashList.Clear();
                for (int index = 0; index < StringToHashCount; index++)
                {
                    stringToHashList.Add(Random.Range(0, stringToHashArray.Length));
                }
            }

            stringToHashTimer += Time.deltaTime;
            for (int index = 0; index < StringToHashCount; index++)
            {
                bool isSame = String.Equals(stringToHashArray[stringToHashList[index]], stringToHashTestString, StringComparison.Ordinal);
            }

            if (stringToHashTimer > StringToHashTime)
            {
                stringToHashTimer = 0;
            }
        }

        #endregion
        
        #region 测试2:str 换行和 颜色的兼容

        public bool Test2StrColor0 = false;
        public string Test2Str0 = "测试数据";
        public string Test2Str1 = "测试数据";
        public Color Test2Color0 = Color.white;

        public void RunStr0()
        {
            if (!Test2StrColor0) return;
            string colorCode = ColorUtility.ToHtmlStringRGB(Test2Color0);
            Debug.Log(@$"<color=#{colorCode}>{Test2Str0}
                {Test2Str1}</color>");
        }

        #endregion
        
        #region 测试3:Enum long 到底可以不可以 到底4字节还是8字节

        public bool RunTest3EnumLong0 = false;

        public ulong Test3MyEnumLong0 = (ulong) Test3EnumLong0.Item32;
        public ulong Test3MyEnumLong1 = (ulong) Test3EnumLong0.Item64;
        // public Test3EnumLong0 Test3MyEnum0 = Test3EnumLong0.Item32;
        public void RunTest3EnumLong()
        {
            if (!RunTest3EnumLong0) return;
            Debug.Log($"32: {(ulong) Test3EnumLong0.Item32} 64: {(ulong) Test3EnumLong0.Item64}");
            Debug.Log($"Enum Test3EnumLong0 SizeOf: {sizeof(Test3EnumLong0)}");
            Debug.Log($"ulong Test3MyEnumLong0 sizeof: {sizeof(ulong)}");
            Debug.Log($"Enum Test3EnumLong1 SizeOf: {sizeof(Test3EnumLong1)}");
            RunTest3EnumLong0 = false;
        }

        #endregion
        
        #region 测试4：Assert有原生的GC？ 

        public bool RunTest4Assert0 = false;
        public bool RunTest4Assert1 = false;
        public bool RunTest4Assert2 = false;
        private int[] Test4List = new int[2] {10, 20};
        
        public void RunTest4AssertFunc0()
        {
            if (!RunTest4Assert0) return;
            Debug.Assert(Array.IndexOf(Test4List, 1) != -1);
        }
        
        public void RunTest4AssertFunc1()
        {
            if (!RunTest4Assert1) return;
            if (Array.IndexOf(Test4List, 1) != -1)
            {
                Debug.Log(1);
            }
        }
        
        public void RunTest4AssertFunc2()
        {
            if (!RunTest4Assert2) return;
            Debug.Log(1);
        }
        
        #endregion
    }
}