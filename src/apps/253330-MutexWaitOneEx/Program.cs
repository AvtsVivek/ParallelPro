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
        var simpleObject = new SimpleClass();

        // create the mutex
        var mutex = new Mutex();

        // create an list of tasks
        var taskList = new List<Task>();

        for (var i = 0; i < 50; i++)
        {
            // create a new task
            var task = new Task(() =>
            {
                // enter a loop for 1000 increments
                for (var j = 0; j < 1000; j++)
                {
                    bool lockAcquired = mutex.WaitOne();
                    try
                    {
                        // update the counter
                        simpleObject.Counter = simpleObject.Counter + 1;
                    }
                    finally
                    {
                        // release the mutext
                        if (lockAcquired) mutex.ReleaseMutex();
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
        Console.WriteLine("Expected value {0}, counter: {1}", 50000, simpleObject.Counter);

        // wait for input before exiting
        Console.WriteLine("Press enter to finish");
        Console.ReadLine();
    }
}
