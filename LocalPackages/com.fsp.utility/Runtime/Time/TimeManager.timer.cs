using System;
using System.Collections.Generic;
using fsp.debug;

namespace fsp.time
{
    public partial class TimeManager : SingletonMonoBehavior<TimeManager>
    {
        private LinkedList<Timer> timerLink = new LinkedList<Timer>();
        private LinkedListNode<Timer> tempCurrentNode = null;

        #region Timer Control Function

        public Timer AddUpdateTimer(bool isUnique, string name, UpdateTimer.OnUpdateDelegate callback, bool autoStart = true, bool byServer = false)
        {
            if (isUnique)
            {
                Timer existTimer = GetUniqueTimer(name);
                if (existTimer != null)
                    return existTimer;
            }

            UpdateTimer updateTimer = new UpdateTimer(name, isUnique, callback, byServer);
            if (!autoStart)
            {
                updateTimer.Pause();
            }

            return addTimer(updateTimer);
        }

        public CycleTimer AddCycleTimer(bool isUnique, string name, float cycle, System.Action callback, bool autoStart = true, bool byServer = false)
        {
            if (isUnique)
            {
                Timer existTimer = GetUniqueTimer(name);
                if (existTimer != null)
                    return (CycleTimer)existTimer;
            }

            CycleTimer cycleTimer = new CycleTimer(name, isUnique, cycle, callback, byServer);
            if (!autoStart)
            {
                cycleTimer.Pause();
            }

            return (CycleTimer)addTimer(cycleTimer);
        }

        public Timer AddCountDownTimer(bool isUnique, string name, float countDownTime, System.Action callback, bool autoStart = true, bool autoRemove = true, bool byServer = false)
        {
            if (isUnique)
            {
                Timer existTimer = GetUniqueTimer(name);
                if (existTimer != null)
                {
                    return existTimer;
                }
            }
            //
            CountDownTimer countDownTimer = new CountDownTimer(name, isUnique, countDownTime, callback, autoRemove, byServer);
            if (!autoStart)
            {
                countDownTimer.Pause();
            }

            return addTimer(countDownTimer);
        }

        public Timer AddFullTimer(bool isUnique, string name, bool autoStart = true,
            bool autoRemove = true, FullTimer.OnUpdateDelegate onUpdateDelegate = null,
            float cycle = -1, System.Action onCycleDelegate = null,
            float countDownTime = -1, System.Action onFinishDelegate = null, bool byServer = false)
        {
            if (isUnique)
            {
                Timer existTimer = GetUniqueTimer(name);
                if (existTimer != null)
                    return existTimer;
            }

            FullTimer fullTimer = new FullTimer(name, isUnique, byServer);
            fullTimer.autoRemove = autoRemove;
            if (onUpdateDelegate != null)
            {
                fullTimer.onUpdateDelegate += onUpdateDelegate;
            }

            if (cycle != -1)
            {
                fullTimer.Cycle = cycle;
                fullTimer.onCycleDelegate += onCycleDelegate;
            }

            if (countDownTime != -1)
            {
                fullTimer.CountDownTime = countDownTime;
                fullTimer.onFinishDelegate += onFinishDelegate;
            }

            if (!autoStart)
            {
                fullTimer.Pause();
            }

            return addTimer(fullTimer);
        }

        
        public void AddDelayTimer(System.Action delayCallback)
        {
            DelayTimer timer = new DelayTimer(null,false,delayCallback);
            addTimer(timer);
            timer.canReset = false;
        }

        public Timer GetUniqueTimer(string name)
        {
            tempCurrentNode = timerLink.First;
            while (tempCurrentNode != null)
            {
                if (tempCurrentNode.Value.IsUnique && tempCurrentNode.Value.Name == name)
                    return tempCurrentNode.Value;

                tempCurrentNode = tempCurrentNode.Next;
            }

            return null;
        }

        public Timer GetTimer(string name)
        {
            tempCurrentNode = timerLink.First;
            while (tempCurrentNode != null)
            {
                if (tempCurrentNode.Value.Name != null && tempCurrentNode.Value.Name.Equals(name, StringComparison.Ordinal))
                    return tempCurrentNode.Value;

                tempCurrentNode = tempCurrentNode.Next;
            }

            return null;
        }


        public void RemoveTimer(Timer timer)
        {
            if (timer != null)
            {
                timerLink.Remove(timer);
            }
        }

        /// <summary>
        /// All same name will remove!
        /// </summary>
        /// <param name="name"></param>
        public void RemoveTimer(string name)
        {
            tempCurrentNode = timerLink.First;
            while (tempCurrentNode != null)
            {
                LinkedListNode<Timer> nextNode = tempCurrentNode.Next;

                if (tempCurrentNode.Value.Name == name)
                {
                    timerLink.Remove(tempCurrentNode);
                }

                tempCurrentNode = nextNode;
            }
        }

        #endregion

        #region Private Function

        private Timer addTimer(Timer data)
        {
            if (!timerLink.Contains(data))
            {
                timerLink.AddLast(data);
                return data;
            }

            debug.PrintSystem.LogError("[TIME] Arealy have same timer!  =>" + data.Name);
            return null;
        }

        #endregion
    }
}