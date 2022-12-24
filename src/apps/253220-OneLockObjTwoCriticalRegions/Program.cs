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
        var incrementTaskList = new List<Task>();
        var decrementTaskList = new List<Task>();

        // create the lock object
        var lockObj = new object();

        for (var i = 0; i < 5; i++)
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
                        simpleObject.Counter++; // = simpleObject.Counter + 1;
                    }
                }
            });

            incrementTaskList.Add(task);
        }

        // Start all of the tasks
        foreach (var task in incrementTaskList)
            task.Start();

        for (var i = 0; i < 5; i++)
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
                        simpleObject.Counter = simpleObject.Counter - 2;
                    }
                }
            });

            decrementTaskList.Add(task);
        }

        // Start all of the tasks
        foreach (var task in decrementTaskList)
            task.Start();


        // wait for all of the tasks to complete
        Task.WaitAll(incrementTaskList.ToArray());
        Task.WaitAll(decrementTaskList.ToArray());

        // write out the counter value
        Console.WriteLine("Expected value: -5000");
        Console.WriteLine("Balance: {0}", simpleObject.Counter);

        // wait for input before exiting
        Console.WriteLine("Press enter to finish");
        Console.ReadLine();
    }
}
