using System.Diagnostics;

class Program
{
    //private static string _webUrl = string.Empty;
    private static int _maxDegreeOfParallelism = -1;
    static void Main(string[] args)
    {
        if(!ReadCommandLineArgsAndGetReady(args))
            return;

        // create a ParallelOptions instance
        // and set the max concurrency to 1
        var options = new ParallelOptions() { MaxDegreeOfParallelism = _maxDegreeOfParallelism };

        // perform a parallel for loop
        Parallel.For(0, 10, options, index => {
            Console.WriteLine("For Index {0} started", index);
            Thread.Sleep(500);
            Console.WriteLine("For Index {0} finished", index);
        });

        // create an array of ints to process
        int[] dataElements = new int[] { 0, 2, 4, 6, 8 };

        // perform a parallel foreach loop
        Parallel.ForEach(dataElements, options, index => {
            Console.WriteLine("ForEach Index {0} started", index);
            Thread.Sleep(500);
            Console.WriteLine("ForEach Index {0} finished", index);
        });

        // wait for input before exiting
        Console.WriteLine("Press enter to finish");
        Console.ReadLine();

    }

    private static bool ReadCommandLineArgsAndGetReady(string[] args)
    {
        var maxDegreeOfParallelism = string.Empty;

        if (args.Length == 1)
        {
            maxDegreeOfParallelism = args[0];
        }
        else
        {
            Console.WriteLine("In suffecients params");
            return false;
        }

        if (!int.TryParse(maxDegreeOfParallelism, out _maxDegreeOfParallelism))
        {
            Console.WriteLine("maxDegreeOfParallelism is invalid. It should be an integer");
            return false;
        }

        return true;
    }
}
