using System.Collections.Concurrent;
using System.Security.Principal;
using System.Threading.Tasks;

class Multiple_Locks
{
    static void Main(string[] args)
    {

        // create the synch primitive
        Mutex mutex = new ();

        // create a cancellation token source
        CancellationTokenSource cancellationTokenSource = new ();

        // create a task that acquires and releases the mutex
        var task1 = new Task(() => {
            while (true)
            {
                mutex.WaitOne();
                Console.WriteLine("Task 1 acquired mutex");
                // wait for 500ms
                cancellationTokenSource.Token.WaitHandle.WaitOne(500);
                // exit the mutex
                mutex.ReleaseMutex();
                Console.WriteLine("Task 1 released mutex");
            }
        }, cancellationTokenSource.Token);

        // create a task that acquires and then abandons the mutex
        var task2 = new Task(() => {
            // wait for 2 seconds to let the other task run
            cancellationTokenSource.Token.WaitHandle.WaitOne(2000);
            // acquire the mutex
            mutex.WaitOne();
            Console.WriteLine("Task 2 acquired mutex");
            // abandon the mutex
            throw new Exception("Abandoning Mutex");
        }, cancellationTokenSource.Token);

        // start the tasks
        task1.Start();
        task2.Start();

        // put the main thread to sleep 
        cancellationTokenSource.Token.WaitHandle.WaitOne(3000);

        // wait for task 2 
        try
        {
            task2.Wait();
        }
        catch (AggregateException ex)
        {
            ex.Handle((inner) => {
                Console.WriteLine(inner);
                return true;
            });
        }

        // wait for input before exiting
        Console.WriteLine("Press enter to finish");
        Console.ReadLine();
    }
}