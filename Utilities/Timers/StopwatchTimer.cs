namespace Jimothy.Utilities.Timers
{
    public class StopwatchTimer : Timer
    {
        public StopwatchTimer() : base(0f)
        {
        }
        
        public override float Progress => -1f;

        public override void Tick(float deltaTime)
        {
            if (!IsRunning) return;
            
            Time += deltaTime;
        }
        
        public void Reset() => Time = 0f;
    }
}