using Jimothy.Utilities.Timers;
using NUnit.Framework;

namespace Utilities.Timers.Tests
{
    public class CountdownTimerTest
    {
        private const float AcceptableDelta = 0.01f;

        [Test]
        public void CountdownTimer_IsRunning_WhenStarted()
        {
            var timer = new CountdownTimer(10f);

            timer.Start();
            timer.Tick(1f);

            Assert.IsTrue(timer.IsRunning);
        }

        [Test]
        public void CountdownTimer_IsNotRunning_WhenStopped()
        {
            var timer = new CountdownTimer(10f);

            timer.Start();
            timer.Tick(1f);
            timer.Stop();

            Assert.IsFalse(timer.IsRunning);
        }

        [Test]
        public void CountdownTimer_TimeIs9_When1SecondPassed()
        {
            var timer = new CountdownTimer(10f);

            timer.Start();
            timer.Tick(1f);

            Assert.AreEqual(9f, timer.GetTime(), AcceptableDelta);
        }

        [Test]
        public void CountdownTimer_TimeIs0_When10SecondsPassed()
        {
            var timer = new CountdownTimer(10f);

            timer.Start();
            timer.Tick(10f);

            Assert.AreEqual(0f, timer.GetTime(), AcceptableDelta);
        }

        [Test]
        public void CountdownTimer_TimeIs0_When11SecondsPassed()
        {
            var timer = new CountdownTimer(10f);

            timer.Start();
            timer.Tick(11f);

            Assert.AreEqual(0f, timer.GetTime(), AcceptableDelta);
        }

        [Test]
        public void CountdownTimer_IsFinished_WhenTimeIs0()
        {
            var timer = new CountdownTimer(10f);

            timer.Start();
            timer.Tick(10f);

            Assert.IsTrue(timer.IsFinished);
            Assert.IsFalse(timer.IsRunning);
        }

        [Test]
        public void CountdownTimer_ProgressIs0point5_When5SecondsPassed()
        {
            var timer = new CountdownTimer(10f);

            timer.Start();
            timer.Tick(5f);

            Assert.AreEqual(0.5f, timer.Progress, AcceptableDelta);
        }

        [Test]
        public void CountdownTimer_ProgressIs1_When10SecondsPassed()
        {
            var timer = new CountdownTimer(10f);

            timer.Start();
            timer.Tick(10f);

            Assert.AreEqual(1f, timer.Progress, AcceptableDelta);
        }

        [Test]
        public void CountdownTimer_ProgressIs0point3_When3SecondsPassed()
        {
            var timer = new CountdownTimer(10f);

            timer.Start();
            timer.Tick(3f);

            Assert.AreEqual(0.3f, timer.Progress, AcceptableDelta);
        }

        [Test]
        public void CountdownTimer_ResetTimeIs10_WhenReset()
        {
            var timer = new CountdownTimer(10f);

            timer.Start();
            timer.Tick(3f);
            timer.Reset();

            Assert.AreEqual(10f, timer.GetTime(), AcceptableDelta);
        }

        [Test]
        public void CountdownTimer_ResetTimeIs5_WhenResetWith5()
        {
            var timer = new CountdownTimer(10f);

            timer.Start();
            timer.Tick(3f);
            timer.Reset(5f);

            Assert.AreEqual(5f, timer.GetTime(), AcceptableDelta);
        }

        [Test]
        public void CountdownTimer_KeepsCounting_AfterReset()
        {
            var timer = new CountdownTimer(10f);

            timer.Start();
            timer.Tick(3f);
            timer.Reset();
            timer.Tick(1f);

            Assert.AreEqual(9f, timer.GetTime(), AcceptableDelta);
        }

        [Test]
        public void CountdownTimer_IsNotRunning_WhenPaused()
        {
            var timer = new CountdownTimer(10f);

            timer.Start();
            timer.Tick(3f);
            timer.Pause();

            Assert.IsFalse(timer.IsRunning);
        }

        [Test]
        public void CountdownTimer_IsRunning_WhenResumed()
        {
            var timer = new CountdownTimer(10f);

            timer.Start();
            timer.Tick(3f);
            timer.Pause();
            timer.Tick(3f);
            timer.Resume();

            Assert.IsTrue(timer.IsRunning);
        }

        [Test]
        public void CountdownTimer_TimeResumes_WhenResumed()
        {
            var timer = new CountdownTimer(10f);

            timer.Start();
            timer.Tick(3f);
            timer.Pause();
            timer.Tick(3f);
            timer.Resume();
            timer.Tick(3f);

            Assert.AreEqual(4f, timer.GetTime(), AcceptableDelta);
            Assert.AreEqual(0.6f, timer.Progress, AcceptableDelta);
        }
    }
}
