namespace fsp.testall
{
    public enum TestItemType : int
    {
        TIT_NONE = 0,
        TIT_StringToHash = 1,       // 测试：stringToHash效率
        TIT_Str_Color_Line = 2,     // 测试：字符换行和颜色
        TIT_Enum_Long = 3,          // 测试：Long 枚举之谜
        TIT_Assert_GC = 4,          // 测试：Assert GC
        TIT_Str_Split = 5,          // 测试：String.Split可以筛除空字符？
        
        
        TIT_AnimationCurveAdditive = 10000,         // 测试：动画曲线additive扩展方法生效测试
    }
}