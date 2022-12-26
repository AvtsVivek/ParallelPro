class Program
{
    private static void Main(string[] args)
    {
        // create the reader-writer lock
        var readerWriterLockSlim = new ReaderWriterLockSlim();

        // create a cancellation token source
        var cancellationTokenSource = new CancellationTokenSource();

        // create the cancellation token
        var cancellationToken = cancellationTokenSource.Token;

        // create an array of tasks
        var taskList = new List<Task>();

        for (int i = 0; i < 5; i++)
        {
            // create a new task
            var task = new Task(() => {
                // acqure the read lock
                readerWriterLockSlim.EnterReadLock();
                // we now have the lock
                Console.WriteLine($"Read lock acquired - count: {readerWriterLockSlim.CurrentReadCount}. The current thread id is {Thread.CurrentThread.ManagedThreadId}");
                // wait - this simulates a read operation
                cancellationToken.WaitHandle.WaitOne();
                // release the read lock
                readerWriterLockSlim.ExitReadLock();
                Console.WriteLine($"Read lock released - count: {readerWriterLockSlim.CurrentReadCount}. The current thread id is {Thread.CurrentThread.ManagedThreadId}");
            });

            taskList.Add(task);
        }

        // Start all of the tasks
        taskList.ForEach(task => task.Start());

        Console.WriteLine("Press any key to cancel");
        Console.ReadLine();
        Console.WriteLine("Cancelling ...");
        cancellationTokenSource.Cancel();

        // wait for input before exiting
        Console.WriteLine("Press enter to finish");
        Console.ReadLine();
        Console.WriteLine("Done. ..");
    }
}
