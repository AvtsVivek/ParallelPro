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

        // create the cancellation token source
        var cancellationTokenSource = new CancellationTokenSource();
        // create the cancellation token
        var cancellationToken = cancellationTokenSource.Token;

        // create the tasks
        var task1 = new Task(() => {
            
            if (PrintThreadInfo)
            {
                var threadInfo = PrintThreadDetails("From Main Method");
                Console.WriteLine(threadInfo);
            }

            for (int i = 0; i < 5; i++)
            {
                // check for task cancellation
                cancellationToken.ThrowIfCancellationRequested();
                // print out a message
                Console.WriteLine("Task 1 - Int value {0}", i);
                // put the task to sleep for 1 second
                cancellationToken.WaitHandle.WaitOne(1000);
            }
            Console.WriteLine("Task 1 complete");
        }, cancellationToken);

        Task task2 = new Task(() => {
            Console.WriteLine("Task 2 complete");
        }, cancellationToken);

        // start the tasks
        task1.Start();
        task2.Start();

        // wait for the tasks
        Console.WriteLine("Waiting for tasks to complete.");
        Task.WaitAll(task1, task2);
        Console.WriteLine("Tasks Completed.");


        // wait for input before exiting
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
