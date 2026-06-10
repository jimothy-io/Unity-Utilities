using System;

namespace Jimothy.Utilities.Timers
{
    public abstract class Timer
    {
        protected float InitialTime;
        protected float Time;
        public bool IsRunning { get; private set; }
        public abstract float Progress { get; }

        public Action TimerStarted = () => { };
        public Action TimerStopped = () => { };

        protected Timer(float value)
        {
            InitialTime = value;
            IsRunning = false;
        }

        public void Start()
        {
            Time = InitialTime;
            if (!IsRunning)
            {
                IsRunning = true;
                TimerStarted.Invoke();
            }
        }

        public void Stop()
        {
            if (IsRunning)
            {
                IsRunning = false;
                TimerStopped.Invoke();
            }
        }

        public void Resume() => IsRunning = true;

        public void Pause() => IsRunning = false;

        public float GetTime() => Time;

        public abstract void Tick(float deltaTime);
    }
}