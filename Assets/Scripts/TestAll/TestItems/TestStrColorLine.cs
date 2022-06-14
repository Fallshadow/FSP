using UnityEngine;

namespace fsp.testall.TestItems
{
    // 换行会中断掉颜色的衔接所以要把每个行字符串都放上颜色
    public class TestStrColorLine : TestItemBase
    {
        public string Test2Str0 = "测试数据";
        public string Test2Str1 = "测试数据";
        public Color Test2Color0 = Color.white;
        
        public override void TestFunc0()
        {
            if (!TestBool0) return;
            string colorCode = ColorUtility.ToHtmlStringRGB(Test2Color0);
            Debug.Log(@$"<color=#{colorCode}>{Test2Str0}
                {Test2Str1}</color>");
        }
    }
}