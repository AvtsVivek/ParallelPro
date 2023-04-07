
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

        var allowedCommandLineArgs = new List<string>() { "AsParallel", "AsSequential" };

        if (!allowedCommandLineArgs.Contains(args[0]))
        {
            Console.WriteLine($"The command line args must be one among ... ");
            allowedCommandLineArgs.ForEach(allowedArg => Console.WriteLine(allowedArg));
            return;
        }

        Console.WriteLine(args[0]);

        // create some source data
        int[] sourceData = new int[10];

        for (int i = 0; i < sourceData.Length; i++)
            sourceData[i] = i;

        // preserve order with the AsOrdered() method
        IEnumerable<double> results =
            from item in sourceData.AsParallel().AsOrdered()
            select Math.Pow(item, 2);

        // enumerate the results of the parallel query
        foreach (double d in results)
            Console.WriteLine("Parallel result: {0}", d);

        // Wait for input before exiting
        // Console.WriteLine("Press enter to finish");
        // Console.ReadLine();
    }
}
