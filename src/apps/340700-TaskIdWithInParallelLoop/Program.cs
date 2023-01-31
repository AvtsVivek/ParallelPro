class TaskIdWithInParallelLoop
{
    private static void Main(string[] args)
    {
        Parallel.For(0, 10, index => {
            Console.WriteLine("Task ID {0} processing index: {1}", Task.CurrentId, index);
        });

        // Introduces parallelState.
        Parallel.For(3, 6, (index, parallelState) => {
            Console.WriteLine("Task ID {0} processing index: {1}, state is {2}", Task.CurrentId, index, parallelState.ToString());
        });

        // wait for input before exiting
        Console.WriteLine("Press enter to finish");
        Console.ReadLine();

    }
}