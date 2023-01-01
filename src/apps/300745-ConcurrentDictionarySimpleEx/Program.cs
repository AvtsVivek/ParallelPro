using System.Collections.Concurrent;
using System.Security.Principal;
using System.Threading.Tasks;

class SimpleClass
{
    public int Counter { get; set; }
}

class Program
{
    private static void Main(string[] args)
    {

        // create the bank account instance
        SimpleClass simpleObject = new();

        // create a shared dictionary
        ConcurrentDictionary<object, int> sharedConcurrentDictionary = new();

        // create an array of tasks
        var taskList = new List<Task<int>>();

        // create tasks to process the list
        for (var i = 0; i < 10; i++)
        {
            // put the initial value into the dictionary
            sharedConcurrentDictionary.TryAdd(i, simpleObject.Counter);
            // create the new task
            var task = new Task<int>((keyObj) =>
            {
                // define variables for use in the loop
                int currentValue;
                bool gotValue;

                // enter a loop for 1000 balance updates
                for (int j = 0; j < 1000; j++)
                {
                    // get the current value from the dictionary
                    gotValue = sharedConcurrentDictionary.TryGetValue(keyObj!, out currentValue);
                    // increment the value and update the dictionary entry
                    sharedConcurrentDictionary.TryUpdate(keyObj!, currentValue + 1, currentValue);
                }

                // define the final result
                int result;
                // get our result from the dictionary
                gotValue = sharedConcurrentDictionary.TryGetValue(keyObj!, out result);
                // return the result value if we got one
                if (gotValue)
                {
                    return result;
                }
                else
                {
                    // there was no result available - we have encountered a problem
                    throw new Exception(String.Format("No data item available for key {0}", keyObj));
                }
            }, i);

            taskList.Add(task);
        }

        // wait for all of the tasks to complete
        // Start all of the tasks
        taskList.ForEach(task => task.Start());

        // wait for the tasks to complete
        Task.WaitAll(taskList.ToArray());

        // update the balance of the account using the task results
        for (var i = 0; i < taskList.Count; i++)
            simpleObject.Counter += taskList[i].Result;

        // write out the counter value
        Console.WriteLine("Expected value {0}, Counter: {1}", 10000, simpleObject.Counter);

        // wait for input before exiting
        Console.WriteLine("Press enter to finish");
        Console.ReadLine();

        Console.WriteLine("Done. ..");
    }
}
