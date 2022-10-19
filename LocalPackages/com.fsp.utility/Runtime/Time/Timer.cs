using System;

namespace fsp.time
{
    [System.Serializable]
    public class Timer
    {
        public string Name = "";
        public ulong serverElasedTime { get; private set; } = 0u;
        public float ElasedTime { get; private set; } = 0f;
        public bool canReset = true;
        public bool isGlobal = false;   //全局Timer,  只注册一次， 不会被Reset Force清理掉
        public Action cbReset;
        public string GroupName;

        public bool IsUnique { get; private set; }
        public bool ByServer { get; protected set; } = false;
        protected bool pause { get; set; } = false;

        public Timer(string name, bool isUnique, bool byServer)
        {
            Name = name;
            IsUnique = isUnique;
            ByServer = byServer;
        }

        public virtual void Pause()
        {
            pause = true;
        }

        public virtual void Play()
        {
            Reset();
            pause = false;
        }

        public virtual void Continue()
        {
            pause = false;
        }

        public virtual void Reset()
        {
            ElasedTime = 0;
            serverElasedTime = 0;
            cbReset?.Invoke();
        }

        public virtual bool IsPlaying()
        {
            return !pause;
        }

        public virtual void Process(float deltaTime, ulong serverDeltaTime)
        {
            if (pause)
                return;
            
            ElasedTime += deltaTime;

            this.serverElasedTime += serverDeltaTime;
        }

        public float GetElasedTime()
        {
            if (!ByServer)
            {
                return ElasedTime;
            }
            else
            {
                return serverElasedTime / 1000f;
            }
        }
    }
}