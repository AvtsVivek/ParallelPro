using System.Threading.Tasks;

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

        // create the task
        var task = new Task(() => {

            if (PrintThreadInfo)
            {
                var threadInfo = PrintThreadDetails("From Task Method");
                Console.WriteLine(threadInfo);
            }

            for (int i = 0; i < int.MaxValue; i++)
            {
                Thread.Sleep(100);
                if (cancellationToken.IsCancellationRequested)
                {
                    Console.WriteLine("Task cancel detected");
                    throw new OperationCanceledException(cancellationToken);
                }
                else
                    Console.WriteLine("Int value {0}", i);

            }
        });

        // wait for input before we start the task
        Console.WriteLine("Press enter to start task");
        Console.WriteLine("Press enter again to cancel task");
        Console.ReadLine();




        try
        {
            // start the task
            task.Start();

            // read a line from the console.
            Console.ReadLine();

            // cancel the task
            Console.WriteLine("Cancelling task");

            cancellationTokenSource.Cancel();

            Task.WaitAll(task);

            // The following line is not getting executed. 
            // Thats probably because the exception is being raised at the previous step. 
            // So to know the status, we need to use the finally block. 
            Console.WriteLine($"Task status in the try block ... {task.Status}");

        }
        catch (AggregateException ex)
        {
            // enumerate the exceptions that have been aggregated
            foreach (Exception inner in ex.InnerExceptions)
            {
                Console.WriteLine("Exception type {0} from {1}",
                    inner.GetType(), inner.Source);
            }
        }
        finally
        {
            Console.WriteLine($"Task status in the finally block ... {task.Status}");
        }

        Console.WriteLine($"Task status after the try catch finally block ... {task.Status}");




        // wait for input before exiting
        Console.WriteLine(task.Status);
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
