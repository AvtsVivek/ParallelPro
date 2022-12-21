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

        var cancellationTokenSource = new CancellationTokenSource();

        // create a task that throws an exception
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
            // wait until we are cancelled
            cancellationTokenSource.Token.WaitHandle.WaitOne(-1);
            throw new OperationCanceledException();
        }, cancellationTokenSource.Token);

        // start the tasks
        task1.Start();
        task2.Start();

        // cancel the token
        cancellationTokenSource.Cancel();

        // wait for the tasks, ignoring the exceptions

        try
        {
            Task.WaitAll(task1, task2);
        }
        catch (AggregateException)
        {
            // ignore exceptions
        }

        // write out the details of the task exception
        Console.WriteLine("Task 1 completed: {0}", task1.IsCompleted);
        Console.WriteLine("Task 1 faulted: {0}", task1.IsFaulted);
        Console.WriteLine("Task 1 cancelled: {0}", task1.IsCanceled);
        Console.WriteLine(task1.Exception);

        // write out the details of the task exception
        Console.WriteLine("Task 2 completed: {0}", task2.IsCompleted);
        Console.WriteLine("Task 2 faulted: {0}", task2.IsFaulted);
        Console.WriteLine("Task 2 cancelled: {0}", task2.IsCanceled);
        Console.WriteLine(task2.Exception);

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
