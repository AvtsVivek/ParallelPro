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

        // create the new escalation policy
        TaskScheduler.UnobservedTaskException += (object? sender, UnobservedTaskExceptionEventArgs eventArgs) => {
                // mark the exception as being handled
                eventArgs.SetObserved();
                // get the aggregate exception and process the contents
                ((AggregateException)eventArgs.Exception).Handle(ex => {
                    // write the type of the exception to the console
                    Console.WriteLine("Exception type: {0}", ex.GetType());
                    return true;
                });
            };

        // create tasks that will throw an exception
        var task1 = new Task(() => {
            if (PrintThreadInfo)
            {
                var threadInfo = PrintThreadDetails("From Main Method");
                Console.WriteLine(threadInfo);
            }
            throw new NullReferenceException();
        });

        var task2 = new Task(() => {
            if (PrintThreadInfo)
            {
                var threadInfo = PrintThreadDetails("From Main Method");
                Console.WriteLine(threadInfo);
            }
            throw new ArgumentOutOfRangeException();
        });

        // start the tasks
        task1.Start(); task2.Start();

        // wait for the tasks to complete - but do so
        // without calling any of the trigger members
        // so that the exceptions remain unhandled
        while (!task1.IsCompleted || !task2.IsCompleted)
        {
            Thread.Sleep(500);
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
