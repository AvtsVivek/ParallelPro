
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

        var sourceData = new int[100];

        for (int i = 0; i < sourceData.Length; i++)
            sourceData[i] = i;

        if (args[0] == "AsParallel")
        {          

            // define a parallel linq query
            IEnumerable<double> results2 =
                from item in sourceData.AsParallel()
                select Math.Pow(item, 2);

            // enumerate the results of the parallel query
            foreach (double d in results2)
                Console.WriteLine("Parallel result: {0}", d);
            
        }

        if (args[0] == "AsSequential")
        {
            IEnumerable<double> results1 = from item in sourceData 
                                           select Math.Pow(item, 2);

            // enumerate the results of the sequential query
            foreach (double d in results1)
                Console.WriteLine("Sequential result: {0}", d);
        }


        // wait for input before exiting
        //Console.WriteLine("Press enter to finish");
        //Console.ReadLine();
    }
}
