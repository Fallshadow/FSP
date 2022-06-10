
// 这边是对unity的方法扩展
public static class UnityApiExtend 
{
    #region String

    public static bool IsNullOrEmptyEx(this string str)
    {
        return string.IsNullOrEmpty(str);
    }

    #endregion

}
