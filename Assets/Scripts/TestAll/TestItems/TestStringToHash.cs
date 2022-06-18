using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace fsp.testall.TestItems
{
    // 结果是
    // 方法0 update里进行stringtohash是非常慢的
    // 方法1 stringtohash好的int比较很快 基本就是从array中去值的消耗
    // 方法2 直接比较字符  在他们中间
    public class TestStringToHash : TestItemBase
    {
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

        public int StringToHashTime = 1;
        private float stringToHashTimer = 0;
        public int StringToHashCount = 100;
        private string stringToHashTestString = "对比的字符串";
        private int stringToHashTestString2 = Animator.StringToHash("对比的字符串");
        private List<int> stringToHashList = new List<int>();

        public override void TestFunc0()
        {
            if (!TestBool0) return;
            if (stringToHashTimer == 0)
            {
                stringToHashList.Clear();
                for (int index = 0; index < StringToHashCount; index++)
                {
                    stringToHashList.Add(Random.Range(0, stringToHashArray.Length));
                }
            }

            stringToHashTimer += Time.deltaTime;
            if (stringToHashTimer > StringToHashTime)
            {
                stringToHashTimer = 0;
            }
            
            for (int index = 0; index < StringToHashCount; index++)
            {
                int hash = Animator.StringToHash(stringToHashArray[stringToHashList[index]]);
                int compareHash = Animator.StringToHash(stringToHashTestString);
            }
        }

        public override void TestFunc1()
        {
            if (!TestBool1) return;
            
            if (stringToHashTimer == 0)
            {
                stringToHashList.Clear();
                for (int index = 0; index < StringToHashCount; index++)
                {
                    stringToHashList.Add(Random.Range(0, stringToHashArray2.Length));
                }
            }

            stringToHashTimer += Time.deltaTime;
            if (stringToHashTimer > StringToHashTime)
            {
                stringToHashTimer = 0;
            }
            
            for (int index = 0; index < StringToHashCount; index++)
            {
                int hash = stringToHashArray2[stringToHashList[index]];
                int compareHash = stringToHashTestString2;
            }
        }

        public override void TestFunc2()
        {
            if (!TestBool2) return;
            
            if (stringToHashTimer == 0)
            {
                stringToHashList.Clear();
                for (int index = 0; index < StringToHashCount; index++)
                {
                    stringToHashList.Add(Random.Range(0, stringToHashArray.Length));
                }
            }

            stringToHashTimer += Time.deltaTime;
            if (stringToHashTimer > StringToHashTime)
            {
                stringToHashTimer = 0;
            }
            
            for (int index = 0; index < StringToHashCount; index++)
            {
                bool isSame = String.Equals(stringToHashArray[stringToHashList[index]], stringToHashTestString, StringComparison.Ordinal);
            }
        }
    }
}