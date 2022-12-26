class Program
{
    private static void Main(string[] args)
    {
        // create the reader-writer lock
        var readerWriterLockSlim = new ReaderWriterLockSlim();

        // create a new task
        var task = new Task(() => {
            // acqure the read lock
            readerWriterLockSlim.EnterReadLock();
            // we now have the lock
            Console.WriteLine($"Read lock acquired - count: {readerWriterLockSlim.CurrentReadCount}. The current thread id is {Thread.CurrentThread.ManagedThreadId}");

            if (true)
            {
                try
                {
                    // acquire write lock
                    readerWriterLockSlim.EnterWriteLock();
                    // ...perform write operations ...
                    // release the write lock
                    readerWriterLockSlim.ExitWriteLock();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    // throw;
                }
            }

            readerWriterLockSlim.ExitReadLock();
            Console.WriteLine($"Read lock released - count: {readerWriterLockSlim.CurrentReadCount}. The current thread id is {Thread.CurrentThread.ManagedThreadId}");
        });

        task.Start();

        Console.WriteLine("Press enter to finish");
        Console.ReadLine();
        Console.WriteLine("Done. ..");
    }
}
