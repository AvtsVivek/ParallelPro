
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
        int[] sourceData = new int[100000];
        for (int i = 0; i < sourceData.Length; i++)
            sourceData[i] = i;
        

        var myList = new List<KeyValuePair<int, double>>();

        if (args[0] == "AsSequential")
        {
            // define a filtering query using keywords
            //IEnumerable<double> results1
            //    = from item in sourceData
            //      where item % 2 == 0
            //      select Math.Pow(item, 2);

            // The above and below are same. 
            // The above is query syntax while the below is fluent syntax. 
            // I prefer the fluent syntax.


            IEnumerable<double> results1
                = sourceData
                .Where(item => item % 2 == 0)
                .Select(item => Math.Pow(item, 2));

            // enumerate the results
            foreach (var d in results1)
                Console.WriteLine("Result: {0}", d);
        }

        if (args[0] == "AsParallel")
        {
            //IEnumerable<double> results1
            //= from item in sourceData.AsParallel()
            //  where item % 2 == 0
            //  select Math.Pow(item, 2);

            // The above and below are same. 
            // The above is query syntax while the below is fluent syntax. 
            // I prefer the fluent syntax.

            // define a filtering query using extension methods
            IEnumerable<double> results2
                = sourceData.AsParallel()
                .Where(item => item % 2 == 0)
                .Select(item => Math.Pow(item, 2));

            // enumerate the results
            foreach (var d in results2)
                Console.WriteLine("Result: {0}", d);

        }


        // wait for input before exiting
        //Console.WriteLine("Press enter to finish");
        //Console.ReadLine();
    }
}
