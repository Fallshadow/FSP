using UnityEngine;

namespace fsp.time
{
    [System.Serializable]
    public class DelayTimer : Timer
    {
        public System.Action delayDelegate;

        bool execute = false;

        public DelayTimer(string name, bool isUnique, System.Action onCycleDelegate, bool byServer = false) : base(name, isUnique, byServer)
        {
            this.delayDelegate = onCycleDelegate;
            execute = false;
        }

        public override void Process(float deltaTime, ulong serverDeltaTime)
        {
            if (pause)
            {
                return;
            }
            
            base.Process(deltaTime, serverDeltaTime);


            if(!execute)
            {
                execute = true;
                return;
            }

            delayDelegate?.Invoke();
            TimeManager.instance.RemoveTimer(this);
        }

        public override void Reset()
        {
            execute = false;
            base.Reset();
        }
    }
}