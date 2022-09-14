using System;
using System.Collections.Generic;
using System.Reflection;

namespace fsp.evt
{
    // Note: 事件的簽章會以第一個註冊的函式為準。
    // Note: 如果Delegate被清空之後註冊一個不同簽章的函式，不會導致exception，但會使原來發送的事件接收不到。
    public class EventHandler
    {
        private readonly Dictionary<int, Delegate> callbackDict = new Dictionary<int, Delegate>(521);

        public virtual void Clear()
        {
            callbackDict.Clear();
        }

        public void Register(int id, Action callback)
        {
            callbackDict.TryGetValue(id, out Delegate del);
            if (del == null)
            {
                callbackDict[id] = callback;
                return;
            }

            if (del is Action callbacks)
            {
                checkDupAction(callbacks, callback);
                callbackDict[id] = callbacks + callback;
            }
            else
            {
                logErrorRegisterError(del.Method, callback.Method);
            }
        }

        public void Register<T>(int id, Action<T> callback)
        {
            callbackDict.TryGetValue(id, out Delegate del);
            if (del == null)
            {
                callbackDict[id] = callback;
                return;
            }

            if (del is Action<T> callbacks)
            {
                checkDupAction(callbacks, callback);
                callbackDict[id] = callbacks + callback;
            }
            else
            {
                logErrorRegisterError(del.Method, callback.Method);
            }
        }

        public void Register<T1, T2>(int id, Action<T1, T2> callback)
        {
            callbackDict.TryGetValue(id, out Delegate del);
            if (del == null)
            {
                callbackDict[id] = callback;
                return;
            }

            if (del is Action<T1, T2> callbacks)
            {
                checkDupAction(callbacks, callback);
                callbackDict[id] = callbacks + callback;
            }
            else
            {
                logErrorRegisterError(del.Method, callback.Method);
            }
        }

        public void Register<T1, T2, T3>(int id, Action<T1, T2, T3> callback)
        {
            callbackDict.TryGetValue(id, out Delegate del);
            if (del == null)
            {
                callbackDict[id] = callback;
                return;
            }

            if (del is Action<T1, T2, T3> callbacks)
            {
                checkDupAction(callbacks, callback);
                callbackDict[id] = callbacks + callback;
            }
            else
            {
                logErrorRegisterError(del.Method, callback.Method);
            }
        }

        public void Register<T1, T2, T3, T4>(int id, Action<T1, T2, T3, T4> callback)
        {
            callbackDict.TryGetValue(id, out Delegate del);
            if (del == null)
            {
                callbackDict[id] = callback;
                return;
            }

            if (del is Action<T1, T2, T3, T4> callbacks)
            {
                checkDupAction(callbacks, callback);
                callbackDict[id] = callbacks + callback;
            }
            else
            {
                logErrorRegisterError(del.Method, callback.Method);
            }
        }

        public void Register<T1, T2, T3, T4, T5>(int id, Action<T1, T2, T3, T4, T5> callback)
        {
            callbackDict.TryGetValue(id, out Delegate del);
            if (del == null)
            {
                callbackDict[id] = callback;
                return;
            }

            if (del is Action<T1, T2, T3, T4, T5> callbacks)
            {
                checkDupAction(callbacks, callback);
                callbackDict[id] = callbacks + callback;
            }
            else
            {
                logErrorRegisterError(del.Method, callback.Method);
            }
        }

        public void Unregister(int id, Action callback)
        {
            callbackDict.TryGetValue(id, out Delegate del);
            if (del is Action callbacks)
            {
                callbackDict[id] = callbacks - callback;
            }
        }

        public void Unregister<T>(int id, Action<T> callback)
        {
            callbackDict.TryGetValue(id, out Delegate del);
            if (del is Action<T> callbacks)
            {
                callbackDict[id] = callbacks - callback;
            }
        }

        public void Unregister<T1, T2>(int id, Action<T1, T2> callback)
        {
            callbackDict.TryGetValue(id, out Delegate del);
            if (del is Action<T1, T2> callbacks)
            {
                callbackDict[id] = callbacks - callback;
            }
        }

        public void Unregister<T1, T2, T3>(int id, Action<T1, T2, T3> callback)
        {
            callbackDict.TryGetValue(id, out Delegate del);
            if (del is Action<T1, T2, T3> callbacks)
            {
                callbackDict[id] = callbacks - callback;
            }
        }

        public void Unregister<T1, T2, T3, T4>(int id, Action<T1, T2, T3, T4> callback)
        {
            callbackDict.TryGetValue(id, out Delegate del);
            if (del is Action<T1, T2, T3, T4> callbacks)
            {
                callbackDict[id] = callbacks - callback;
            }
        }

        public void Unregister<T1, T2, T3, T4, T5>(int id, Action<T1, T2, T3, T4, T5> callback)
        {
            callbackDict.TryGetValue(id, out Delegate del);
            if (del is Action<T1, T2, T3, T4, T5> callbacks)
            {
                callbackDict[id] = callbacks - callback;
            }
        }

        public virtual void Send(int id)
        {
            callbackDict.TryGetValue(id, out Delegate del);
            if (del == null)
            {
                return;
            }

            Action action = del as Action;
            //debug.PrintSystem.Assert(action != null, $"Action cast error, need: {del.Method}, pass: ()");
            if (action == null) //避免GC (Assert會有原生的GC)
            {
                debug.PrintSystem.LogError($"Action cast error, need: {del.Method}, pass: ()");
            }
            action?.Invoke();
        }

        public virtual void Send<T>(int id, T arg)
        {
            callbackDict.TryGetValue(id, out Delegate del);
            if (del == null)
            {
                return;
            }

            Action<T> action = del as Action<T>;
            //debug.PrintSystem.Assert(action != null, $"Action cast error, need: {del.Method}, pass: (${arg?.GetType()})");
            if (action == null) //避免GC (Assert會有原生的GC)
            {
                debug.PrintSystem.LogError($"Action cast error, need: {del.Method}, pass: (${arg?.GetType()})");
            }
            action?.Invoke(arg);
        }

        public virtual void Send<T1, T2>(int id, T1 arg1, T2 arg2)
        {
            callbackDict.TryGetValue(id, out Delegate del);
            if (del == null)
            {
                return;
            }

            Action<T1, T2> action = del as Action<T1, T2>;
            //debug.PrintSystem.Assert(action != null, $"Action cast error, need: {del.Method}, pass: (${arg1?.GetType()}, ${arg2?.GetType()})");
            if (action == null) //避免GC (Assert會有原生的GC)
            {
                debug.PrintSystem.LogError($"Action cast error, need: {del.Method}, pass: (${arg1?.GetType()}, ${arg2?.GetType()})");
            }
            action?.Invoke(arg1, arg2);
        }

        public virtual void Send<T1, T2, T3>(int id, T1 arg1, T2 arg2, T3 arg3)
        {
            callbackDict.TryGetValue(id, out Delegate del);
            if (del == null)
            {
                return;
            }

            Action<T1, T2, T3> action = del as Action<T1, T2, T3>;
            //debug.PrintSystem.Assert(action != null, $"Action cast error, need: {del.Method}, pass: (${arg1?.GetType()}, ${arg2?.GetType()}, ${arg3?.GetType()})");
            if (action == null) //避免GC (Assert會有原生的GC)
            {
                debug.PrintSystem.LogError($"Action cast error, need: {del.Method}, pass: (${arg1?.GetType()}, ${arg2?.GetType()}, ${arg3?.GetType()})");
            }
            action?.Invoke(arg1, arg2, arg3);
        }

        public virtual void Send<T1, T2, T3, T4>(int id, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            callbackDict.TryGetValue(id, out Delegate del);
            if (del == null)
            {
                return;
            }

            Action<T1, T2, T3, T4> action = del as Action<T1, T2, T3, T4>;
            //debug.PrintSystem.Assert(action != null, $"Action cast error, need: {del.Method}, pass: (${arg1?.GetType()}, ${arg2?.GetType()}, ${arg3?.GetType()}, ${arg4?.GetType()})");
            if (action == null) //避免GC (Assert會有原生的GC)
            {
                debug.PrintSystem.LogError($"Action cast error, need: {del.Method}, pass: (${arg1?.GetType()}, ${arg2?.GetType()}, ${arg3?.GetType()}, ${arg4?.GetType()})");
            }
            action?.Invoke(arg1, arg2, arg3, arg4);
        }

        public virtual void Send<T1, T2, T3, T4, T5>(int id, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            callbackDict.TryGetValue(id, out Delegate del);
            if (del == null)
            {
                return;
            }

            Action<T1, T2, T3, T4, T5> action = del as Action<T1, T2, T3, T4, T5>;
            //debug.PrintSystem.Assert(action != null, $"Action cast error, need: {del.Method}, pass: (${arg1?.GetType()}, ${arg2?.GetType()}, ${arg3?.GetType()}, ${arg4?.GetType()}, ${arg5?.GetType()})");
            if (action == null) //避免GC (Assert會有原生的GC)
            {
                debug.PrintSystem.LogError($"Action cast error, need: {del.Method}, pass: (${arg1?.GetType()}, ${arg2?.GetType()}, ${arg3?.GetType()}, ${arg4?.GetType()}, ${arg5?.GetType()})");
            }
            action?.Invoke(arg1, arg2, arg3, arg4, arg5);
        }

        private static void logErrorRegisterError(MethodInfo delMethodInfo, MethodInfo actionMethodInfo)
        {
            debug.PrintSystem.LogError($"[EventHandler] Cannot register different types of callback functions in the same Event ID, need: {delMethodInfo}, pass: {actionMethodInfo}");
        }

        [System.Diagnostics.Conditional("PRINT_SYSTEM_DEBUG")]
        private static void checkDupAction(Action callbacks, Action action)
        {
            Delegate[] list = callbacks.GetInvocationList();
            //debug.PrintSystem.Assert(Array.IndexOf(list, action) == -1, $"Can't register same event, {action.Method}");
            if (Array.IndexOf(list, action) != -1) //避免GC (Assert會有原生的GC)
            {
                debug.PrintSystem.LogError($"Can't register same event, {action.Method}");
            }
        }
        
        [System.Diagnostics.Conditional("PRINT_SYSTEM_DEBUG")]
        private static void checkDupAction<T>(Action<T> callbacks, Action<T> action)
        {
            Delegate[] list = callbacks.GetInvocationList();
            //debug.PrintSystem.Assert(Array.IndexOf(list, action) == -1, $"Can't register same event, {action.Method}");
            if (Array.IndexOf(list, action) != -1) //避免GC (Assert會有原生的GC)
            {
                debug.PrintSystem.LogError($"Can't register same event, {action.Method}");
            }
        }
        
        [System.Diagnostics.Conditional("PRINT_SYSTEM_DEBUG")]
        private static void checkDupAction<T1, T2>(Action<T1, T2> callbacks, Action<T1, T2> action)
        {
            Delegate[] list = callbacks.GetInvocationList();
            //debug.PrintSystem.Assert(Array.IndexOf(list, action) == -1, $"Can't register same event, {action.Method}");
            if (Array.IndexOf(list, action) != -1) //避免GC (Assert會有原生的GC)
            {
                debug.PrintSystem.LogError($"Can't register same event, {action.Method}");
            }
        }
        
        [System.Diagnostics.Conditional("PRINT_SYSTEM_DEBUG")]
        private static void checkDupAction<T1, T2, T3>(Action<T1, T2, T3> callbacks, Action<T1, T2, T3> action)
        {
            Delegate[] list = callbacks.GetInvocationList();
            //debug.PrintSystem.Assert(Array.IndexOf(list, action) == -1, $"Can't register same event, {action.Method}");
            if (Array.IndexOf(list, action) != -1) //避免GC (Assert會有原生的GC)
            {
                debug.PrintSystem.LogError($"Can't register same event, {action.Method}");
            }
        }
        
        [System.Diagnostics.Conditional("PRINT_SYSTEM_DEBUG")]
        private static void checkDupAction<T1, T2, T3, T4>(Action<T1, T2, T3, T4> callbacks, Action<T1, T2, T3, T4> action)
        {
            Delegate[] list = callbacks.GetInvocationList();
            //debug.PrintSystem.Assert(Array.IndexOf(list, action) == -1, $"Can't register same event, {action.Method}");
            if (Array.IndexOf(list, action) != -1) //避免GC (Assert會有原生的GC)
            {
                debug.PrintSystem.LogError($"Can't register same event, {action.Method}");
            }
        }
        
        [System.Diagnostics.Conditional("PRINT_SYSTEM_DEBUG")]
        private static void checkDupAction<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> callbacks, Action<T1, T2, T3, T4, T5> action)
        {
            Delegate[] list = callbacks.GetInvocationList();
            //debug.PrintSystem.Assert(Array.IndexOf(list, action) == -1, $"Can't register same event, {action.Method}");
            if (Array.IndexOf(list, action) != -1) //避免GC (Assert會有原生的GC)
            {
                debug.PrintSystem.LogError($"Can't register same event, {action.Method}");
            }
        }
    }
}