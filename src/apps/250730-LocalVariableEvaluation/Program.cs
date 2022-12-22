using System.Threading.Tasks;
using System.Threading;

class Program
{
    private static bool PrintThreadInfo = false;
    private static void Main(string[] args)
    {
        if (args.Length == 1)
            bool.TryParse(args[0], out PrintThreadInfo);

        Console.WriteLine($"PrintThreadInfo var is {PrintThreadInfo}");

        if (PrintThreadInfo)
        {
            var threadInfo = PrintThreadDetails("From Main Method");
            Console.WriteLine(threadInfo);
        }

        // create and start the "bad" tasks
        for (int i = 0; i < 5; i++)
        {
            Task.Factory.StartNew(() =>
            {
                if (PrintThreadInfo)
                {
                    var threadInfo = PrintThreadDetails("From Main Method");
                    Console.WriteLine(threadInfo);
                }
                // write out a message that uses the loop counter
                Console.WriteLine("Task {0} has counter value: {1}",
                Task.CurrentId, i);
            });
        }

        // create and start the "good" tasks
        for (int i = 0; i < 5; i++)
        {
            Task.Factory.StartNew((stateObj) =>
            {
                if (PrintThreadInfo)
                {
                    var threadInfo = PrintThreadDetails("From Main Method");
                    Console.WriteLine(threadInfo);
                }
                // cast the state object to an int
                int loopValue = (int)stateObj!;

                // write out a message that uses the loop counter
                Console.WriteLine("Task {0} has counter value: {1}", Task.CurrentId, loopValue);
            }, i);
        }

        // wait for input before exiting
        Console.WriteLine("Main method complete. Press enter to finish.");
        Console.ReadLine();
    }

    private static string PrintThreadDetails(string contextInfo)
    {
        var finalString = string.Empty;

        finalString = finalString + Environment.NewLine;

        finalString = $"The context information is: {contextInfo}";

        finalString = finalString + Environment.NewLine + $"The current thread id is {Thread.CurrentThread.ManagedThreadId}.";

        var backgroundString = Thread.CurrentThread.IsBackground ? "a background thread" : "NOT a background thread";

        finalString = finalString + Environment.NewLine + $"This thread is {backgroundString}";

        var threadPoolInfoString = Thread.CurrentThread.IsThreadPoolThread ? "a thread pool thread" : "NOT a thread pool thread";

        finalString = finalString + Environment.NewLine + $"This thread is {threadPoolInfoString}";

        finalString = finalString + Environment.NewLine;

        return finalString;
    }
}
