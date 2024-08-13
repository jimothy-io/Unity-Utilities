namespace Jimothy.Utilities.Timers
{
    public class CountdownTimer : Timer
    {
        public CountdownTimer(float value) : base(value)
        {
        }

        public override float Progress => 1 - (Time / InitialTime);

        public override void Tick(float deltaTime)
        {
            if (!IsRunning) return;

            if (Time > 0f)
            {
                Time -= deltaTime;
            }
            if (Time <= 0f)
            {
                Time = 0f;
                Stop();
            }
        }
        
        public bool IsFinished => Time <= 0f;

        public void Reset() => Time = InitialTime;
        
        public void Reset(float value)
        {
            InitialTime = value;
            Reset();
        }
    }
}