using System.Collections.Concurrent;
using System.Security.Principal;
using System.Threading.Tasks;


class SimpleClass
{
    public int Counter { get; set; }
}

class Multiple_Locks
{

    static void Main(string[] args)
    {

        // create the bank account instance
        var simpleObject = new SimpleClass();

        // create two lock objects
        var lock1 = new object();
        var lock2 = new object();

        // create an array of tasks
        // Task[] tasks = new Task[10];
        // create an array of tasks
        var taskList = new List<Task>();

        // create five tasks that use the first lock object
        for (int i = 0; i < 5; i++)
        {
            // create a new task
            var task = new Task(() => {
                // enter a loop for 1000 balance updates
                for (int j = 0; j < 100000; j++)
                {
                    lock (lock1)
                    {
                        // update the balance
                        simpleObject.Counter++;
                    }
                }
            });

            taskList.Add(task);
        }

        // create five tasks that use the second lock object
        for (var i = 5; i < 10; i++)
        {
            // create a new task
            var task = new Task(() => {
                // enter a loop for 1000 balance updates
                for (int j = 0; j < 100000; j++)
                {
                    lock (lock2)
                    {
                        // update the balance
                        simpleObject.Counter++;
                    }
                }
            });

            taskList.Add(task);
        }

        // start the tasks
        foreach (var task in taskList)
            task.Start();
        

        // wait for all of the tasks to complete
        Task.WaitAll(taskList.ToArray());

        // write out the counter value
        Console.WriteLine("Expected value {0}, Balance: {1}", 100000, simpleObject.Counter);

        if(simpleObject.Counter == 1000000)
            Console.WriteLine("Data race did not happen. ");
        else
            Console.WriteLine("Data race Happened. Expected value {0}, Balance: {1}", 100000, simpleObject.Counter);

        // wait for input before exiting
        Console.WriteLine("Press enter to finish");
        Console.ReadLine();
    }
}