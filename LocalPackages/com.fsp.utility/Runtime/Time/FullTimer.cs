namespace fsp.time
{
    /// <summary>
    /// Timer + UpdateTimer + CycleTimer
    /// </summary>
    [System.Serializable]
    public class FullTimer : UpdateTimer
    {
        /// <summary>
        /// -1 -> roop, 
        /// >0 -> count down 
        /// </summary>
        public float CountDownTime = -1f;

        public float Cycle = 1;
        public float CycleElasedTime = 0f;

        public System.Action onCycleDelegate;
        public System.Action onFinishDelegate;

        public bool autoRemove = true;

        public FullTimer(string name, bool isUnique, bool byServer = false) : base(name, isUnique, null, byServer){}

        public override void Process(float deltaTime, ulong serverDeltaTime)
        {
            if (pause)
                return;

            base.Process(deltaTime, serverDeltaTime);
            CycleElasedTime += deltaTime;
            if (CycleElasedTime >= Cycle)
            {
                onCycleDelegate?.Invoke();
                CycleElasedTime -= Cycle;
            }

            if (CountDownTime > 0 && GetElasedTime() >= CountDownTime)
            {
                if (autoRemove)
                {
                    TimeManager.instance.RemoveTimer(this);
                }

                onFinishDelegate?.Invoke();
            }
        }

        public override void Reset()
        {
            base.Reset();
            CycleElasedTime = 0;
        }

        public override bool IsPlaying()
        {
            if (pause)
                return false;
            return GetElasedTime() <= CountDownTime;
        }
        
        public void UpdateCountDown(float cd)
        {
            CountDownTime = cd;
            onCycleDelegate?.Invoke();
        }

        public float GetRemainTime()
        {
            float result = CountDownTime - GetElasedTime();
            return result < 0 ? 0 : result;
        }
    }
}