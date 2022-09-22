using UnityEngine;

namespace fsp
{
    public abstract class SingletonMonoBehaviorNoDestroy<T> : MonoBehaviour where T : SingletonMonoBehaviorNoDestroy<T>
    {
        public static T instance => s_instance;
        private static T s_instance = null;

        protected virtual void Awake()
        {
            if (s_instance == null)
            {
                DontDestroyOnLoad(gameObject);
                s_instance = this as T;
                init();
            }
            else
            {
                Destroy(this);
            }
        }

        protected virtual void init()
        {
        }
    }
}