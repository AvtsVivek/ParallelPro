
class Program
{
    private static void Main(string[] args)
    {

        Tuple<int, int> commandLineInput = null!;

        if (!TryReadCommandLineArgs(args, out commandLineInput))
            return;

        var participantCount = commandLineInput.Item1;
        var taskCount = commandLineInput.Item2;

        var taskList = new List<Task>();

        // create the barrier
        var barrier = new Barrier(participantCount, (myBarrier) =>
        {
            Console.WriteLine($"Post-Phase action: phase={myBarrier.CurrentPhaseNumber}");
        });

        // loop to create the tasks

        for (int i = 0; i < taskCount; i++)
        {
            var task = new Task(() =>
            {
                // signal the barrier
                Console.WriteLine("First signal...");
                barrier.SignalAndWait();
                Console.WriteLine("Second signal...");
                barrier.SignalAndWait();
                Console.WriteLine("Third signal...");
                barrier.SignalAndWait();
                Console.WriteLine("Fourth signal...");
                barrier.SignalAndWait();
                Console.WriteLine("Fifth signal...");
                barrier.SignalAndWait();
                Console.WriteLine("Sixth signal...");
                barrier.SignalAndWait();
                Console.WriteLine("Seventh signal...");
                barrier.SignalAndWait();
            });
            taskList.Add(task);
        }

        // start the task
        taskList.ForEach(task => task.Start());

        // wait for all of the tasks to complete
        Task.WaitAll(taskList.ToArray());

        // Exiting...
        Console.WriteLine("Exiting...");
    }
    private static bool TryReadCommandLineArgs(string[] args, out Tuple<int, int> commandLineInput)
    {
        var participantCount = 0;
        var taskCount = 0;
        commandLineInput = null!;

        if (args.Length != 2)
        {
            Console.WriteLine("There must be 2 int inputs.");
            return false;
        }

        if (!int.TryParse(args[0], out participantCount))
        {
            Console.WriteLine("The first arg must be an +ve integer greater than 0");
            return false;
        }

        if (!int.TryParse(args[1], out taskCount))
        {
            Console.WriteLine("The second arg must be an integer");
            return false;
        }

        if (participantCount == 0)
        {
            Console.WriteLine("The first arg must be an +ve integer greater than 0");
            return false;
        }

        if (taskCount == 0)
        {
            Console.WriteLine("The second arg must be an +ve integer greater than 0");
            return false;
        }

        commandLineInput = new Tuple<int, int>(participantCount, taskCount);

        return true;
    }
}