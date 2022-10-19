namespace fsp.time
{
    public class CountDownTimer : Timer
    {
        private float countDownTime = 0f;

        private readonly System.Action onFinishDelegate;

        private readonly bool autoRemove = true;

        public CountDownTimer(string name, bool isUnique, float countDownTime, System.Action onFinishDelegate, bool autoRemove, bool byServer = false) : base(name, isUnique, byServer)
        {
            this.countDownTime = countDownTime;
            this.onFinishDelegate = onFinishDelegate;
            this.autoRemove = autoRemove;
        }

        public override void Process(float deltaTime, ulong serverDeltaTime)
        {
            if (pause)
            {
                return;
            }

            base.Process(deltaTime, serverDeltaTime);
            if (GetElasedTime() >= countDownTime)
            {
                if (autoRemove)
                {
                    TimeManager.instance.RemoveTimer(this);
                }
                else
                {
                    pause = true;
                }
                onFinishDelegate?.Invoke();

            }
        }

        public void Execute()
        {
            fsp.time.TimeManager.instance.RemoveTimer(this);
            onFinishDelegate?.Invoke();
        }

        public override bool IsPlaying()
        {
            if (pause)
            {
                return false;
            }

            return GetElasedTime() <= countDownTime;
        }

        public void UpdateCountDown(float cd)
        {
            countDownTime = cd;
        }

        public float GetRemainTime()
        {
            return countDownTime - GetElasedTime();
        }
    }
}