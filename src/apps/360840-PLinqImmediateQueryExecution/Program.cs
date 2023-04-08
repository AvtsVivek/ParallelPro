
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Dynamic;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        IEnumerable<double> results = new List<double>();

        // create some source data
        int[] sourceData = new int[10];
        for (int i = 0; i < sourceData.Length; i++)
            sourceData[i] = i;
        

        if (args[0] == "AsParallel")
        {

            Console.WriteLine("Defining PLINQ query");
            // define the query
            results = sourceData.AsParallel().Select(item =>
            {
                Console.WriteLine("Processing item {0}", item);
                return Math.Pow(item, 2);
            }).ToArray();
        }

        if (args[0] == "AsSequential")
        {

            Console.WriteLine("Defining regular query");
            // define the query
            results = sourceData.Select(item =>
            {
                Console.WriteLine("Processing item {0}", item);
                return Math.Pow(item, 2);
            }).ToArray();
        }

        Console.WriteLine("Waiting...");
        Thread.Sleep(5000);

        // sum the results - this will trigger
        // execution of the query
        Console.WriteLine("Accessing results");
        double total = 0;
        foreach (double d in results)
        {
            total += d;
        }
        Console.WriteLine("Total {0}", total);

        // wait for input before exiting
        // Console.WriteLine("Press enter to finish");
        // Console.ReadLine();
    }
}
