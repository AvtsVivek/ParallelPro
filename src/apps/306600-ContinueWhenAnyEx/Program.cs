//class SimpleClass
//{
//    public int Counter { get; set; }
//}
class ContinueWhenAnyEx
{
    static void Main(string[] args)
    {
        // create an array of tasks
        // Task<int>[] tasks = new Task<int>[10];
        var antecedentTaskList /*antecedentTaskList*/ = new List<Task<int>>();

        // create a cancellation token source
        CancellationTokenSource cancellationTokenSource = new ();

        var cancellationToken = cancellationTokenSource.Token;

        // create the random number generator
        var rndom = new Random();

        for (int i = 0; i < 10; i++)
        {
            // create a new task
            var task = new Task<int>(() => {
                // define the variable for the sleep interval
                int sleepInterval;
                // acquire exclusive access to the random
                // number generator and get a random value
                lock (rndom)
                {
                    sleepInterval = rndom.Next(500, 2000);
                }
                // put the task thread to sleep for the interval
                cancellationToken.WaitHandle.WaitOne(sleepInterval);
                // check to see the current task has been cancelled
                cancellationToken.ThrowIfCancellationRequested();
                // return the sleep interval as the result
                return sleepInterval;
            }, cancellationToken);
            antecedentTaskList.Add(task);
        }

        // set up a when-any multi-task continuation
        var continuationTask = Task.Factory.ContinueWhenAny<int>(antecedentTaskList.ToArray(),
            (Task<int> antecedent) => {
                // write out a message using the antecedent result
                Console.WriteLine("The first task slept for {0} milliseconds",
                    antecedent.Result);
            });

        // start the atecedent tasks
        foreach (var antecedentTask in antecedentTaskList)
            antecedentTask.Start();
        

        // wait for the contination task to complete
        continuationTask.Wait();

        // cancel the remaining tasks
        cancellationTokenSource.Cancel();

        // wait for input before exiting
        Console.WriteLine("Press enter to finish");
        Console.ReadLine();
        Console.WriteLine("Done ... ");
    }
}