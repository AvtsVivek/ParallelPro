using System.Diagnostics;
using System.Threading.Tasks;

class Program
{
    private static void Main(string[] args)
    {
        // create the reader-writer lock
        var readerWriterLockSlim = new ReaderWriterLockSlim();

        // create a cancellation token source
        var cancellationTokenSource = new CancellationTokenSource();

        // create some shared data
        int sharedData = 0;

        // create an array of tasks
        var readerTaskList = new List<Task>();

        for (int i = 0; i < 5; i++)
        {
            // create a new task
            var task = new Task(() => {
                while (true)
                {
                    // acqure the read lock
                    readerWriterLockSlim.EnterReadLock();
                    // we now have the lock
                    Console.WriteLine("Read lock acquired - count: {0}",
                        readerWriterLockSlim.CurrentReadCount);

                    // read the shared data
                    Console.WriteLine("Shared data value {0}", sharedData);

                    // wait - slow things down to make the example clear
                    cancellationTokenSource.Token.WaitHandle.WaitOne(1000);

                    // release the read lock
                    readerWriterLockSlim.ExitReadLock();
                    Console.WriteLine("Read lock released - count {0}",
                        readerWriterLockSlim.CurrentReadCount);

                    // check for cancellation
                    cancellationTokenSource.Token.ThrowIfCancellationRequested();
                }
            }, cancellationTokenSource.Token);
            // start the new task
            readerTaskList.Add(task);
        }

        // Start all of the tasks
        readerTaskList.ForEach(task => task.Start());

        var writerTaskList = new List<Task>();

        for (int i = 0; i < 2; i++)
        {
            var task = new Task(() => {
                while (true)
                {
                    // acquire the upgradeable lock
                    readerWriterLockSlim.EnterUpgradeableReadLock();

                    // simulate a branch that will require a write 
                    if (true)
                    {
                        // acquire the write lock
                        readerWriterLockSlim.EnterWriteLock();
                        // print out a message with the details of the lock
                        Console.WriteLine("Write Lock acquired - waiting readers {0}, writers {1}, upgraders {2}",
                            readerWriterLockSlim.WaitingReadCount, readerWriterLockSlim.WaitingWriteCount,
                            readerWriterLockSlim.WaitingUpgradeCount);

                        // modify the shared data
                        sharedData++;

                        // wait - slow down the example to make things clear
                        cancellationTokenSource.Token.WaitHandle.WaitOne(1000);
                        // release the write lock
                        readerWriterLockSlim.ExitWriteLock();
                    }

                    // release the upgradable lock
                    readerWriterLockSlim.ExitUpgradeableReadLock();

                    // check for cancellation
                    cancellationTokenSource.Token.ThrowIfCancellationRequested();
                }

            }, cancellationTokenSource.Token);
            // start the new task
            // writerTasks[i].Start();
            writerTaskList.Add(task);
        }

        // Start all of the tasks
        writerTaskList.ForEach(task => task.Start());

        // prompt the user
        Console.WriteLine("Press enter to cancel tasks");
        // wait for the user to press enter 
        Console.ReadLine();

        // cancel the tasks
        cancellationTokenSource.Cancel();

        try
        {
            // wait for the tasks to complete
            Task.WaitAll(readerTaskList.ToArray());
        }
        catch (AggregateException agex)
        {
            agex.Handle(ex => true);
        }

        // wait for input before exiting
        Console.WriteLine("Press enter to finish");
        Console.ReadLine();
    }
}
