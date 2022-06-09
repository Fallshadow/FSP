using System;

public class Singleton<T> where T : Singleton<T>
{
    // 后续继承可以直接操作这个s_instance
    protected static T s_instance;

    public static T instance
    {
        get
        {
            if (s_instance == null)
            {
                // ToStudy:
                // 使用与指定参数最匹配的构造函数创建指定类型的实例。
                s_instance = Activator.CreateInstance<T>();
                s_instance.init();
            }
            return s_instance;
        }
    }

    protected virtual void init() { }
}