using System.Diagnostics;
using System.Security.Principal;
using System.Threading;

class SimpleClass
{
    public int Counter { get; set; } = default!;
}
class Program
{
    private static void Main(string[] args)
    {
        // create the bank account instance
        var simpleObject1 = new SimpleClass();
        var simpleObject2 = new SimpleClass();


        // create the mutex
        var mutex1 = new Mutex();
        var mutex2 = new Mutex();

        // create a new task to update the first account
        var task1 = new Task(() => {
            // enter a loop for 1000 balance updates
            for (int j = 0; j < 1000; j++)
            {
                // acquire the lock for the account
                bool lockAcquired = mutex1.WaitOne();
                try
                {
                    // update the balance
                    simpleObject1.Counter++;
                }
                finally
                {
                    if (lockAcquired) mutex1.ReleaseMutex();
                }
            }
        });

        // create a new task to update the second account
        var task2 = new Task(() => {
            // enter a loop for 1000 balance updates
            for (int j = 0; j < 1000; j++)
            {
                // acquire the lock for the account
                bool lockAcquired = mutex2.WaitOne(); ;
                try
                {
                    // update the balance
                    simpleObject2.Counter += 2;
                }
                finally
                {
                    if (lockAcquired) mutex2.ReleaseMutex();
                }
            }
        });

        // create a new task to update the first account
        Task task3 = new Task(() => {
            // enter a loop for 1000 balance updates
            for (int j = 0; j < 1000; j++)
            {
                // acquire the locks for both accounts
                // Taking the lock like this ensures that no one else is doing anything(adding or substracting)
                // with both the accounts at this point in time.
                bool lockAcquired = Mutex.WaitAll(new WaitHandle[] { mutex1, mutex2 });
                try
                {
                    // simulate a transfer between accounts
                    simpleObject1.Counter++;
                    simpleObject2.Counter--;
                }
                finally
                {
                    if (lockAcquired)
                    {
                        // There is no release all, so manually you have to do like the following.
                        mutex1.ReleaseMutex();
                        mutex2.ReleaseMutex();
                    }
                }
            }
        });

        // start the tasks
        task1.Start();
        task2.Start();
        task3.Start();

        // wait for the tasks to complete
        Task.WaitAll(task1, task2, task3);

        // write out the counter value
        Console.WriteLine("Account1 balance {0}, Account2 balance: {1}", simpleObject1.Counter, simpleObject2.Counter);

        // wait for input before exiting
        Console.WriteLine("Press enter to finish");
        Console.ReadLine();
    }
}
