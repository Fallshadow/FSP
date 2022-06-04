using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestAll : MonoBehaviour
{
    void Start()
    {
        
    }
    
    void Update()
    {
        #region 结论：10W条Animator.StringToHash 要26ms 是很慢的 是不可以在update调用的

        RunStringToHash();
        RunStringToHash2();
        RunStringToHash3();

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

}
