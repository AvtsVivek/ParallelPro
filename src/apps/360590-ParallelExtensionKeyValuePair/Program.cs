
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

        // var sourceData = new int[100];

        Console.WriteLine("Here we go...");

        var sourceData = new List<int>(100);

        for (int i = 0; i < 100; i++)
            sourceData.Add(i);

        var myList = new List<KeyValuePair<int, double>>();

        if (args[0] == "AsSequential")
        {
            

            // define a sequential linq query
            // IEnumerable<double> results1 = sourceData.Select(item => Math.Pow(item, 2));

            // define a sequential linq query
            // IEnumerable<double> results11 =
            sourceData.ForEach(item => myList.Add(new KeyValuePair<int, double>(item, Math.Pow(item, 2))) );

            // enumerate the results of the sequential query
            //foreach (double d in results1)
            //    Console.WriteLine("Sequential result: {0}", d);

            //foreach (double d in results1)
            //    Console.WriteLine("Sequential result: {0}", d);

            foreach (var d in myList)
                Console.WriteLine("Sequential result: {0}, {1}", d.Key, d.Value);
        }

        if (args[0] == "AsParallel")
        {
            //// define a parallel linq query
            // var results2 = sourceData.AsParallel()
            //  .Select(item => Math.Pow(item, 2));

            //// enumerate the results of the parallel query
            //foreach (var d in results2)
            //    Console.WriteLine("Parallel result: {0}", d);

            // define a sequential linq query
            // IEnumerable<double> results11 =

            Parallel.ForEach(sourceData, item => {
                Console.WriteLine(item);
                myList.Add(new KeyValuePair<int, double>(item, Math.Pow(item, 2)));
            });

            // sourceData.AsParallel().ForEach(item => myList.Add(new KeyValuePair<int, double>(item, Math.Pow(item, 2))));

            // enumerate the results of the sequential query
            //foreach (double d in results1)
            //    Console.WriteLine("Sequential result: {0}", d);

            //foreach (double d in results1)
            //    Console.WriteLine("Sequential result: {0}", d);

            //foreach (var d in myList)
            //    Console.WriteLine("Sequential result: {0}, {1}", d.Key, d.Value);

            foreach (var d in myList)
                Console.WriteLine("Parallel result: {0}, {1}", d.Key, d.Value);

        }


        // wait for input before exiting
        //Console.WriteLine("Press enter to finish");
        //Console.ReadLine();
    }
}
