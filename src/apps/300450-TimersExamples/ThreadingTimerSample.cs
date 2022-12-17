using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimersExamples
{
    internal class ThreadingTimerSample
    {
        private System.Threading.Timer? _timer;

        public void StartTimer()
        {
            if (_timer == null)
            {
                InitializeTimer();
            }
        }

        public async Task DisposeTimerAsync()
        {
            if (_timer != null)
            {
                await _timer.DisposeAsync();
            }
        }

        private void InitializeTimer()
        {
            var updater = new MessageUpdater();

            _timer = new System.Threading.Timer(
            callback: new TimerCallback(TimerFired),
            state: updater,
            dueTime: 500,
            period: 1000);
        }

        private void TimerFired(object? state)
        {
            int messageCount = CheckForNewMessageCount();

            if (messageCount > 0 && state is MessageUpdater updater)
            {
                updater.Update(messageCount);
            }
        }

        private int CheckForNewMessageCount()
        {
            // Generate a random number of messages to return
            return new Random().Next(100);
        }
    }

    internal class MessageUpdater
    {
        internal void Update(int messageCount)
        {
            var debugMessage = $"You have {messageCount} new messages!";

            debugMessage = debugMessage + Environment.NewLine + $"The current thread id is {Thread.CurrentThread.ManagedThreadId}.";

            var backgroundString = Thread.CurrentThread.IsBackground ? "a background thread" : "NOT a background thread";

            backgroundString = "This is " + backgroundString;

            debugMessage = debugMessage + Environment.NewLine + backgroundString;

            var threadPoolInfoString = Thread.CurrentThread.IsThreadPoolThread ? "a thread pool thread" : "NOT a thread pool thread";

            threadPoolInfoString = "This is " + threadPoolInfoString;

            debugMessage = debugMessage + Environment.NewLine + threadPoolInfoString;

            Debug.WriteLine(debugMessage);
        }
    }
}