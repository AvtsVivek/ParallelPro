using System.Security.Principal;

class SimpleClass
{
    public int Counter { get; set; }
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

        for (var i = 0; i < 10; i++)
        {
            // create a new task
            var task = new Task(() =>
            {
                // enter a loop for 1000 increments
                for (int j = 0; j < 1000; j++)
                {
                    lock (lockObj)
                    {
                        // update the balance
                        simpleObject.Counter = simpleObject.Counter + 1;
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
        Console.WriteLine("Expected value {0}, Counter value: {1}", 10000, simpleObject.Counter);
        // wait for input before exiting
        Console.WriteLine("Press enter to finish");
        Console.ReadLine();
    }
}
