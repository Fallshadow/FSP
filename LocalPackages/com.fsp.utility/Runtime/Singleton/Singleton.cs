using System;

namespace fsp
{
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
                    // 这个是通过反射创建实例，是一个全新的实例，所以mono那边不可以用，mono要用unity创建好的
                    s_instance = Activator.CreateInstance<T>();
                    s_instance.init();
                }

                return s_instance;
            }
        }

        protected virtual void init()
        {
        }
    }
}