using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelExamples
{
    internal class ThreadUtils
    {
        internal static string PrintThreadDetails(string contextInfo)
        {
            var finalString = string.Empty;

            finalString = $"The context information is: {contextInfo}";

            finalString = finalString + Environment.NewLine + $"The current thread id is {Thread.CurrentThread.ManagedThreadId}.";

            var backgroundString = Thread.CurrentThread.IsBackground ? "a background thread" : "NOT a background thread";

            finalString = finalString + Environment.NewLine + $"This thread is {backgroundString}";

            var threadPoolInfoString = Thread.CurrentThread.IsThreadPoolThread ? "a thread pool thread" : "NOT a thread pool thread";

            finalString = finalString + Environment.NewLine + $"This thread is {threadPoolInfoString}";

            return finalString;
        }
    }
}
