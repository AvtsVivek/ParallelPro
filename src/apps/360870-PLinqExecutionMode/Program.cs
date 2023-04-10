
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Dynamic;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Atleast one command line arg expected. Exiting...");
            return;
        }

        if (args.Length > 1)
        {
            Console.WriteLine("Only one command line arg expected. Exiting...");
            return;
        }

        var allowedCommandLineArgs = new List<string>() { "ForceParallelism", "Default", "AsSequential" };

        if (!allowedCommandLineArgs.Contains(args[0]))
        {
            Console.WriteLine($"The command line args must be one among ... ");
            allowedCommandLineArgs.ForEach(allowedArg => Console.WriteLine(allowedArg));
            return;
        }

        Console.WriteLine(args[0]);

        var parallelExecutionMode = ParallelExecutionMode.Default;

        if (args[0] == "ForceParallelism")
            parallelExecutionMode = ParallelExecutionMode.ForceParallelism;

        Console.WriteLine(parallelExecutionMode);

        // create some source data
        int[] sourceData = new int[10];
        for (int i = 0; i < sourceData.Length; i++)
            sourceData[i] = i;

        IEnumerable<double> results;

        if (args[0] == "AsSequential")
        {
            // define the query and force parallelism
            results = sourceData
                .Where(item => item % 2 == 0)
                .Select(item => Math.Pow(item, 2));
        }
        else
        {
            // define the query and force parallelism
            results = sourceData
                .AsParallel()
                .WithExecutionMode(parallelExecutionMode)
                .Where(item => item % 2 == 0)
                .Select(item => Math.Pow(item, 2));
        }

        // enumerate the results
        foreach (double d in results)
            Console.WriteLine("Result {0}", d);

        // wait for input before exiting
        // Console.WriteLine("Press enter to finish");
        // Console.ReadLine();
    }
}
