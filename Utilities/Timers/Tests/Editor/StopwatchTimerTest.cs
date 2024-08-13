using Jimothy.Utilities.Timers;
using NUnit.Framework;

namespace Utilities.Timers.Tests
{
    public class StopwatchTimerTest
    {
        private const float AcceptableDelta = 0.01f;
    
        [Test]
        public void StopwatchTimer_IsRunning_WhenStarted()
        {
            var timer = new StopwatchTimer();
        
            timer.Start();
            timer.Tick(1f);
        
            Assert.IsTrue(timer.IsRunning);
        }
    
        [Test]
        public void StopwatchTimer_IsNotRunning_WhenStopped()
        {
            var timer = new StopwatchTimer();
        
            timer.Start();
            timer.Tick(1f);
            timer.Stop();
        
            Assert.IsFalse(timer.IsRunning);
        }
    
        [Test]
        public void StopwatchTimer_TimeIs10_When10SecondsPassed()
        {
            var timer = new StopwatchTimer();
        
            timer.Start();
            timer.Tick(10f);
        
            Assert.AreEqual(10f, timer.GetTime(), AcceptableDelta);
        }

        [Test]
        public void StopwatchTimer_IsStopped_AfterStopping()
        {
            var timer = new StopwatchTimer();
        
            timer.Start();
            timer.Tick(3f);
            timer.Stop();
        
            Assert.IsFalse(timer.IsRunning);
        }

        [Test]
        public void StopwatchTimer_IsReset_AfterResetting()
        {
            var timer = new StopwatchTimer();
        
            timer.Start();
            timer.Tick(3f);
            timer.Reset();
            timer.Tick(1f);
        
            Assert.AreEqual(1f, timer.GetTime(), AcceptableDelta);
        }

        [Test]
        public void StopwatchTimer_ProgressIsNegativeOne_Always()
        {
            var timer = new StopwatchTimer();
        
            Assert.AreEqual(-1f, timer.Progress, AcceptableDelta);
        
            timer.Start();
            Assert.AreEqual(-1f, timer.Progress, AcceptableDelta);

            timer.Tick(1f);
            Assert.AreEqual(-1f, timer.Progress, AcceptableDelta);

            timer.Stop();
            Assert.AreEqual(-1f, timer.Progress, AcceptableDelta);
        }

        [Test]
        public void StopwatchTimer_IsNotRunning_WhenPaused()
        {
            var timer = new StopwatchTimer();
        
            timer.Start();
            timer.Tick(3f);
            timer.Pause();
        
            Assert.IsFalse(timer.IsRunning);
        }
    
        [Test]
        public void StopwatchTimer_TimeDoesntTick_WhenPaused()
        {
            var timer = new StopwatchTimer();
        
            timer.Start();
            timer.Tick(3f);
            timer.Pause();
            timer.Tick(1f);
        
            Assert.AreEqual(3f, timer.GetTime(), AcceptableDelta);
        }
    
        [Test]
        public void StopwatchTimer_TimeResumes_WhenResumed()
        {
            var timer = new StopwatchTimer();
        
            timer.Start();
            timer.Tick(3f);
            timer.Pause();
            timer.Tick(1f);
            timer.Resume();
            timer.Tick(3f);
        
            Assert.AreEqual(6f, timer.GetTime(), AcceptableDelta);
        }
    }
}