
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
            Console.WriteLine("Two command line args expected. Exiting...");
            return;
        }

        if (args.Length == 1)
        {
            Console.WriteLine("Two command line args expected. Exiting...");
            return;
        }

        if (args.Length > 2)
        {
            Console.WriteLine("Only two command line args expected. Exiting...");
            return;
        }

        var allowedFirstCommandLineArgs = new List<string>() { "AsSequential", "AsParallel" };

        if (!allowedFirstCommandLineArgs.Contains(args[0]))
        {
            Console.WriteLine($"The command line args must be one among ... ");
            allowedFirstCommandLineArgs.ForEach(allowedArg => Console.WriteLine(allowedArg));
            return;
        }

        Console.WriteLine(args[0]);



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
            Console.WriteLine("As sequential added to parallelism....");
            // define the query and force parallelism
            results = sourceData
                .AsParallel()
                .WithDegreeOfParallelism(2)
                .Where(item => item % 2 == 0)
                .AsSequential()
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
