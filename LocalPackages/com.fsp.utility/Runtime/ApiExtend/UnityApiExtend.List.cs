using System.Collections.Generic;

public static partial class UnityApiExtend
{
    public static bool IsNullOrEmpty<T>(this List<T> list)
    {
        return list == null || list.Count == 0;
    }
}