using System;

namespace fsp.evt
{
    // 传统游戏开发中的事件系统
    public class EventManager : Singleton<EventManager>
    {
        // 使用两个short进行拼接，组成消息的唯一ID
        private static int combineId(short hi, short lo)
        {
            return hi << 16 | (ushort)lo;
        }

        private readonly EventHandler eventHandler = new EventHandler();

        #region Register 0 1 2 3 4 5参数
        
        public void Register(EventGroup groupId, short eventId, Action callback)
        {
            eventHandler.Register(combineId((short)groupId, eventId), callback);
        }
        
        public void Register<T>(EventGroup groupId, short eventId, Action<T> callback)
        {
            eventHandler.Register(combineId((short)groupId, eventId), callback);
        }

        public void Register<T1, T2>(EventGroup groupId, short eventId, Action<T1, T2> callback)
        {
            eventHandler.Register(combineId((short)groupId, eventId), callback);
        }
        
        public void Register<T1, T2, T3>(EventGroup groupId, short eventId, Action<T1, T2, T3> callback)
        {
            eventHandler.Register(combineId((short)groupId, eventId), callback);
        }

        public void Register<T1, T2, T3,T4>(EventGroup groupId, short eventId, Action<T1, T2, T3,T4> callback)
        {
            eventHandler.Register(combineId((short)groupId, eventId), callback);
        }

        public void Register<T1, T2, T3, T4, T5>(EventGroup groupId, short eventId, Action<T1, T2, T3, T4, T5> callback)
        {
            eventHandler.Register(combineId((short)groupId, eventId), callback);
        }
        
        #endregion
        
        #region Unregister 0 1 2 3 4 5参数
        
        public void Unregister(EventGroup groupId, short eventId, Action callback)
        {
            eventHandler.Unregister(combineId((short)groupId, eventId), callback);
        }
        
        public void Unregister<T>(EventGroup groupId, short eventId, Action<T> callback)
        {
            eventHandler.Unregister(combineId((short)groupId, eventId), callback);
        }
        
        public void Unregister<T1, T2>(EventGroup groupId, short eventId, Action<T1, T2> callback)
        {
            eventHandler.Unregister(combineId((short)groupId, eventId), callback);
        }
        
        public void Unregister<T1, T2, T3>(EventGroup groupId, short eventId, Action<T1, T2, T3> callback)
        {
            eventHandler.Unregister(combineId((short)groupId, eventId), callback);
        }

        public void Unregister<T1, T2, T3,T4>(EventGroup groupId, short eventId, Action<T1, T2, T3,T4> callback)
        {
            eventHandler.Unregister(combineId((short)groupId, eventId), callback);
        }

        public void Unregister<T1, T2, T3, T4, T5>(EventGroup groupId, short eventId, Action<T1, T2, T3, T4, T5> callback)
        {
            eventHandler.Unregister(combineId((short)groupId, eventId), callback);
        }
        
        #endregion

        #region Send 0 1 2 3 4 5参数

        public void Send(EventGroup groupId, short eventId)
        {
            eventHandler.Send(combineId((short)groupId, eventId));
        }
        
        public void Send<T>(EventGroup groupId, short eventId, T arg1)
        {
            eventHandler.Send(combineId((short)groupId, eventId), arg1);
        }
        
        public void Send<T1, T2>(EventGroup groupId, short eventId, T1 arg1, T2 agr2)
        {
            eventHandler.Send(combineId((short)groupId, eventId), arg1, agr2);
        }
        
        public void Send<T1, T2, T3>(EventGroup groupId, short eventId, T1 arg1, T2 agr2, T3 agr3)
        {
            eventHandler.Send(combineId((short)groupId, eventId), arg1, agr2, agr3);
        }

        public void Send<T1, T2, T3,T4>(EventGroup groupId, short eventId, T1 arg1, T2 agr2, T3 agr3,T4 arg4)
        {
            eventHandler.Send(combineId((short)groupId, eventId), arg1, agr2, agr3,arg4);
        }

        public void Send<T1, T2, T3, T4, T5>(EventGroup groupId, short eventId, T1 arg1, T2 agr2, T3 agr3, T4 arg4, T5 arg5)
        {
            eventHandler.Send(combineId((short)groupId, eventId), arg1, agr2, agr3, arg4, arg5);
        }
        
        #endregion
    }
}