class SimpleClass
{
    public int Counter { get; set; }
}
class ContinueWhenAllEx
{

    static void Main(string[] args)
    {
        // create the simple objecjt instance
        SimpleClass simpleObject = new ();

        // create an array of tasks
        // Task<int>[] tasks = new Task<int>[10];

        var antecedentTaskList = new List<Task<int>>();

        for (int i = 0; i < 10; i++)
        {
            // create a new task
            var antecedentTask = new Task<int>((stateObject) => {

                // get the state object
                int isolatedBalance = (int)stateObject!;

                // enter a loop for 1000 balance updates
                for (int j = 0; j < 1000; j++)
                {
                    // update the balance
                    isolatedBalance++;
                }

                // return the updated balance
                return isolatedBalance;

            }, simpleObject.Counter);

            antecedentTaskList.Add(antecedentTask);
        }

        // set up a multi-task continuation
        var continuation = Task.Factory.ContinueWhenAll<int>(antecedentTaskList.ToArray(), antecedents => {

            // run through and sum the individual balances
            foreach (Task<int> antecedent in antecedents)
                simpleObject.Counter += antecedent.Result;
            
        });

        // start the atecedent tasks
        foreach (Task<int> task in antecedentTaskList)
            task.Start();
        

        // wait for the contination task to complete
        continuation.Wait();

        // write out the counter value
        Console.WriteLine("Expected value {0}, Counter: {1}", 10000, simpleObject.Counter);

        // wait for input before exiting
        Console.WriteLine("Press enter to finish");
        Console.ReadLine();
        Console.WriteLine("Done ... ");
    }
}