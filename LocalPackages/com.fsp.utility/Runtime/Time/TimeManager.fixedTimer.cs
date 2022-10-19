using System.Collections.Generic;
using UnityEngine;

namespace fsp.time
{
    public partial class TimeManager : SingletonMonoBehavior<TimeManager>
    {
        private LinkedList<Timer> fixedTimerLink = new LinkedList<Timer>();
        private LinkedListNode<Timer> fixedTempCurrentNode = null;
        
        private void FixedUpdate()
        {
            fixedTempCurrentNode = fixedTimerLink.First;
            ServerElasedTime = ServerTime - lastServerTime;

            while (fixedTempCurrentNode != null)
            {
                LinkedListNode<Timer> nextNode = fixedTempCurrentNode.Next;
                fixedTempCurrentNode.Value.Process(Time.fixedDeltaTime, ServerElasedTime);
                fixedTempCurrentNode = nextNode;
            }
        }
        
        public Timer AddFixedUpdateTimer(bool isUnique, string timerName, FixedUpdateTimer.OnUpdateDelegate callback, bool autoStart = true, bool byServer = false)
        {
            if (isUnique)
            {
                Timer existTimer = GetUniqueFixedTimer(timerName);
                if (existTimer != null)
                    return existTimer;
            }

            FixedUpdateTimer fixedUpdateTimer = new FixedUpdateTimer(timerName, isUnique, callback, byServer);
            if (!autoStart)
            {
                fixedUpdateTimer.Pause();
            }

            return addFixedTimer(fixedUpdateTimer);
        }

        public Timer GetUniqueFixedTimer(string timerName)
        {
            fixedTempCurrentNode = fixedTimerLink.First;
            while (fixedTempCurrentNode != null)
            {
                if (fixedTempCurrentNode.Value.IsUnique && fixedTempCurrentNode.Value.Name == timerName)
                    return fixedTempCurrentNode.Value;

                fixedTempCurrentNode = fixedTempCurrentNode.Next;
            }

            return null;
        }

        public Timer GetFixedTimer(string timerName)
        {
            fixedTempCurrentNode = fixedTimerLink.First;
            while (fixedTempCurrentNode != null)
            {
                if (fixedTempCurrentNode.Value.Name == timerName)
                    return fixedTempCurrentNode.Value;

                fixedTempCurrentNode = fixedTempCurrentNode.Next;
            }

            return null;
        }


        public void RemoveFixedTimer(Timer timer)
        {
            if (timer != null)
            {
                fixedTimerLink.Remove(timer);
            }
        }

        /// <summary>
        /// All same name will remove!
        /// </summary>
        /// <param name="timerName"></param>
        public void RemoveFixedTimer(string timerName)
        {
            fixedTempCurrentNode = fixedTimerLink.First;
            while (fixedTempCurrentNode != null)
            {
                LinkedListNode<Timer> nextNode = fixedTempCurrentNode.Next;

                if (fixedTempCurrentNode.Value.Name == timerName)
                    fixedTimerLink.Remove(fixedTempCurrentNode);

                fixedTempCurrentNode = nextNode;
            }
        }
        
        private Timer addFixedTimer(Timer data)
        {
            if (!fixedTimerLink.Contains(data))
            {
                fixedTimerLink.AddLast(data);
                return data;
            }

            debug.PrintSystem.LogError("[TIME] Arealy have same timer!");
            return null;
        }
    }
}