using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadInterrupt
{
    internal class ThreadUtils
    {
        internal static void SleepIndefinitely()
        {
            Console.WriteLine("Thread '{0}' about to sleep indefinitely.",
                              Thread.CurrentThread.Name);

            PrintThreadDetails();

            try
            {
                Thread.Sleep(Timeout.Infinite);
            }
            catch (ThreadInterruptedException)
            {
                Console.WriteLine("Thread '{0}' awoken.",
                                  Thread.CurrentThread.Name);
            }
            finally
            {
                Console.WriteLine("Thread '{0}' executing finally block.",
                                  Thread.CurrentThread.Name);
            }
            Console.WriteLine("Thread '{0} finishing normal execution.",
                              Thread.CurrentThread.Name);
            Console.WriteLine();
        }

        internal static void PrintThreadDetails()
        {
            Console.ForegroundColor= ConsoleColor.Green;
            Console.WriteLine($"The current thread id is {Thread.CurrentThread.ManagedThreadId}.");

            var backgroundString = Thread.CurrentThread.IsBackground ? "a background thread" : "NOT a background thread";

            Console.WriteLine($"This thread is {backgroundString}");

            var threadPoolInfoString = Thread.CurrentThread.IsThreadPoolThread ? "a thread pool thread" : "NOT a thread pool thread";

            Console.WriteLine($"This thread is {threadPoolInfoString}");
            Console.ForegroundColor = ConsoleColor.White;
        }

    }
}
