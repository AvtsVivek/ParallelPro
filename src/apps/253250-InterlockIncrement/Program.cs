using System.Diagnostics;
using System.Security.Principal;

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

        // create an list of tasks
        var taskList = new List<Task>();

        // create the lock object
        var lockObj = new object();

        for (var i = 0; i < 50; i++)
        {
            // create a new task
            var task = new Task(() =>
            {
                // enter a loop for 1000 increments
                for (int j = 0; j < 1000; j++)
                {
                    lock (lockObj)
                    {
                        // update the counter
                        Interlocked.Increment(ref simpleObject.Counter);
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
        Console.WriteLine("Expected value {0}, Balance: {1}",
            50000, simpleObject.Counter);

        // wait for input before exiting
        Console.WriteLine("Press enter to finish");
        Console.ReadLine();
    }
}
