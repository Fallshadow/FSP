using System;
using UnityEngine;
using System.Collections.Generic;

namespace fsp.time
{
    public partial class TimeManager
    {      
        public static ulong ClientLocalTimeStamp { get { return Convert.ToUInt64(DateTimeOffset.Now.ToLocalTime().ToUnixTimeSeconds()); } }

        public ulong ServerTime
        {
            get { return 0; }
        }
        
        ulong lastServerTime = 0u;

        public ulong ServerElasedTime { get; private set; } = 0u;

        public void Reset(bool bForce = false)
        {
            tempCurrentNode = timerLink.First;
            while (tempCurrentNode != null)
            {
                LinkedListNode<Timer> nextNode = tempCurrentNode.Next;

                if (tempCurrentNode.Value.isGlobal == false && (bForce == true || tempCurrentNode.Value.canReset))
                {
                    tempCurrentNode.Value.Reset();
                    timerLink.Remove(tempCurrentNode);
                }
                tempCurrentNode = nextNode;
            }
            
            fixedTempCurrentNode = fixedTimerLink.First;
            while (fixedTempCurrentNode != null)
            {
                LinkedListNode<Timer> nextNode = fixedTempCurrentNode.Next;

                if (fixedTempCurrentNode.Value.isGlobal == false && (bForce == true || fixedTempCurrentNode.Value.canReset))
                {
                    fixedTempCurrentNode.Value.Reset();
                    fixedTimerLink.Remove(fixedTempCurrentNode);
                }

                fixedTempCurrentNode = nextNode;
            }
        }
        private void Update()
        {
            tempCurrentNode = timerLink.First;
            ServerElasedTime = ServerTime - lastServerTime;

            while (tempCurrentNode != null)
            {
                LinkedListNode<Timer> nextNode = tempCurrentNode.Next;
                tempCurrentNode.Value.Process(Time.deltaTime, ServerElasedTime);
                tempCurrentNode = nextNode;
            }

            lastServerTime = ServerTime;
        }
    }
}