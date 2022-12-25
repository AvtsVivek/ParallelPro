using System.Diagnostics;
using System.Security.Principal;
using System.Threading;

class SimpleClass
{
    public int Counter = 0;
}
class Program
{
    private static void Main(string[] args)
    {
        // create the bank account instance
        var simpleObject = new SimpleClass();

        // create the spinlock
        var spinlock = new SpinLock();

        // create an list of tasks
        var taskList = new List<Task>();

        for (var i = 0; i < 50; i++)
        {
            // create a new task
            var task = new Task(() =>
            {
                // get a local copy of the shared data
                var startCounter = simpleObject.Counter;
                // create a local working copy of the shared data
                var localCounter = startCounter;

                // enter a loop for 1000 increments
                for (var j = 0; j < 1000; j++)
                {
                    var lockAcquired = false;
                    try
                    {
                        spinlock.Enter(ref lockAcquired);
                        // update the counter
                        simpleObject.Counter = simpleObject.Counter + 1;
                    }
                    finally
                    {
                        if (lockAcquired) spinlock.Exit();
                    }
                }
            });

            taskList.Add(task);
        }

        // Start all of the tasks
        foreach (var task in taskList)
            task.Start();

        // wait for all of the tasks to complete
        Task.WaitAll(taskList.ToArray());

        // write out the counter value
        Console.WriteLine("Expected value {0}, Counter: {1}", 50000, simpleObject.Counter);

        // wait for input before exiting
        Console.WriteLine("Press enter to finish");
        Console.ReadLine();
    }
}
