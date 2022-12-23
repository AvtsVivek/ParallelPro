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

        var counter = 0;

        // create an list of tasks
        var tasks = new List<Task>();

        for (int i = 0; i < 10; i++)
            // create a new task
            tasks.Add(new Task(() => {

                // enter a loop for 1000 increments
                for (int j = 0; j < 1000; j++)
                {
                    // increment the counters
                    counter++;
                    simpleObject.Counter++;
                }
            }));
        

        // Start all of the tasks
        foreach (var task in tasks)
            task.Start();
        

        // wait for all of the tasks to complete
        Task.WaitAll(tasks.ToArray());

        // write out the counter value
        Console.WriteLine("Expected value {0}, Counter value: {1}", 10000, simpleObject.Counter);
        Console.WriteLine("Expected value {0}, Counter value: {1}", 10000, counter);
        // wait for input before exiting
        Console.WriteLine("Press enter to finish");
        Console.ReadLine();
    }
}
