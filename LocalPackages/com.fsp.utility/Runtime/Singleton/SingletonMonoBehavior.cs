using System;
using UnityEngine;

public abstract class SingletonMonoBehavior<T> : MonoBehaviour where T : SingletonMonoBehavior<T>
{
    public static T instance => s_instance;
    private static T s_instance = null;

    protected virtual void Awake()
    {
        if (s_instance == null)
        {
            s_instance = this as T;
            s_instance.init();
        }
        else
        {
            if (Application.isPlaying)
            {
                Destroy(this);
            }
        }
    }

    protected virtual void OnDestroy()
    {
        s_instance = null;
    }

    protected virtual void init() { } 
}