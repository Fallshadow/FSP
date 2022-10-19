using UnityEngine;

namespace fsp.time
{
    [System.Serializable]
    public class CycleTimer : Timer
    {
        public float Cycle = 1;

        public System.Action onCycleDelegate;

        float cycleElasedTime = 0f;

        public CycleTimer(string name, bool isUnique, float cycle, System.Action onCycleDelegate, bool byServer = false) : base(name, isUnique, byServer)
        {
            Cycle = cycle;
            this.onCycleDelegate = onCycleDelegate;
        }

        public override void Process(float deltaTime, ulong serverDeltaTime)
        {
            if (pause)
            {
                return;
            }

            base.Process(deltaTime, serverDeltaTime);

            cycleElasedTime += deltaTime;

            if (Mathf.Abs(cycleElasedTime) >= Cycle)
            {
                cycleElasedTime = 0;
                onCycleDelegate?.Invoke();
            }
        }

        public override void Reset()
        {
            base.Reset();
            cycleElasedTime = 0;
        }
    }
}