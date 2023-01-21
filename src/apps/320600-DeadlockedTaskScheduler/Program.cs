class Test_Deadlocked_Task_Scheduler
{
    static void Main(string[] args)
    {
        // create the scheduler
        var scheduler = new Deadlocked_Task_Scheduler(5);

        // create a token source
        var tokenSource = new CancellationTokenSource();

        var tasks = new Task[6];

        for (int i = 0; i < tasks.Length; i++)
        {
            tasks[i] = Task.Factory.StartNew((object stateObj) => {
                int index = (int)stateObj;
                if (index < tasks.Length - 1)
                {
                    Console.WriteLine("Task {0} waiting for {1}", index, index + 1);
                    tasks[index + 1].Wait();
                }
                Console.WriteLine("Task {0} complete", index);
            }, i, tokenSource.Token, TaskCreationOptions.None, scheduler);
        }

        Task.WaitAll(tasks);
        Console.WriteLine("All tasks complete");

        // wait for input before exiting
        Console.WriteLine("Press enter to finish");
        Console.ReadLine();
    }
}