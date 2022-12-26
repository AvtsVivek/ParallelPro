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

        // create the reader-writer lock
        var readerWriterLockSlim = new ReaderWriterLockSlim();

        // create an array of tasks
        var taskList = new List<Task>();

        for (int i = 0; i < 5; i++)
        {
            if (PrintThreadInfo)
            {
                var threadInfo = PrintThreadDetails("From Task Method");
                Console.WriteLine(threadInfo);
            }
            // create a new task
            var task = new Task(() => {
                while (true)
                {
                    // acqure the read lock
                    readerWriterLockSlim.EnterReadLock();
                    // we now have the lock
                    Console.WriteLine($"Read lock acquired - count: {readerWriterLockSlim.CurrentReadCount}. The current thread id is {Thread.CurrentThread.ManagedThreadId}");
                    // wait - this simulates a read operation
                    Task.Delay(1000).Wait();
                    // release the read lock
                    readerWriterLockSlim.ExitReadLock();
                    Console.WriteLine($"Read lock released - count: {readerWriterLockSlim.CurrentReadCount}. The current thread id is {Thread.CurrentThread.ManagedThreadId}");
                }
            });

            taskList.Add(task);
        }

        // Start all of the tasks
        taskList.ForEach(task => task.Start());

        // prompt the user
        Console.WriteLine("Press enter to acquire write lock");
        // wait for the user to press enter 
        Console.ReadLine();

        // acquire the write lock
        Console.WriteLine("Requesting write lock");
        readerWriterLockSlim.EnterWriteLock();

        Console.WriteLine("Write lock acquired");
        Console.WriteLine("Press enter to release write lock");
        // wait for the user to press enter 
        Console.ReadLine();
        // release the write lock
        readerWriterLockSlim.ExitWriteLock();

        // wait for input before exiting
        Console.WriteLine("Press enter to finish");
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
