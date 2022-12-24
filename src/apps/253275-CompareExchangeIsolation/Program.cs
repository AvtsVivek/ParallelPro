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

        for (var i = 0; i < 50; i++)
        {
            // create a new task
            var task = new Task(() =>
            {
                // get a local copy of the shared data
                var startBalance = simpleObject.Counter;
                // create a local working copy of the shared data
                var localBalance = startBalance;

                // enter a loop for 1000 increments
                for (var j = 0; j < 1000; j++)
                {
                    // update the counter
                    // Interlocked.Increment(ref simpleObject.Counter);
                    localBalance++;
                }

                // check to see if the shared data has changed since we started
                // and if not, then update with our local value
                var sharedData = Interlocked.CompareExchange(ref simpleObject.Counter, localBalance, startBalance);

                if (sharedData == startBalance)
                    Console.WriteLine("Shared data updated OK");
                else
                    Console.WriteLine("Shared data changed");
                

            });

            taskList.Add(task);
        }

        // Start all of the tasks
        foreach (var task in taskList)
            task.Start();

        // wait for all of the tasks to complete
        Task.WaitAll(taskList.ToArray());

        // write out the counter value
        Console.WriteLine("Expected value {0}, Balance: {1}", 50000, simpleObject.Counter);

        // wait for input before exiting
        Console.WriteLine("Press enter to finish");
        Console.ReadLine();
    }
}
