﻿namespace fsp.time
{
    [System.Serializable]
    public class UpdateTimer : Timer
    {
        public delegate void OnUpdateDelegate(float delta);

        public OnUpdateDelegate onUpdateDelegate;

        public UpdateTimer(string name, bool isUnique, OnUpdateDelegate onUpdateDelegate, bool byServer = false) : base(name, isUnique, byServer)
        {
            this.onUpdateDelegate += onUpdateDelegate;
        }

        public override void Process(float deltaTime, ulong serverDeltaTime)
        {
            if (pause)
                return;

            base.Process(deltaTime, serverDeltaTime);

            onUpdateDelegate?.Invoke(deltaTime);
        }
    }
}