
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

        var sourceData = new List<int>(100);

        for (int i = 0; i < 100; i++)
            sourceData.Add(i);

        var myList = new List<KeyValuePair<int, double>>();

        if (args[0] == "AsSequential")
        {
            sourceData.ForEach(item => myList.Add(new KeyValuePair<int, double>(item, Math.Pow(item, 2))) );

            foreach (var d in myList)
                Console.WriteLine("Sequential result: {0}, {1}", d.Key, d.Value);
        }

        if (args[0] == "AsParallel")
        {

            Parallel.ForEach(sourceData, item => {
                Console.WriteLine(item);
                myList.Add(new KeyValuePair<int, double>(item, Math.Pow(item, 2)));
            });

            foreach (var d in myList)
                Console.WriteLine("Parallel result: {0}, {1}", d.Key, d.Value);

        }


        // wait for input before exiting
        //Console.WriteLine("Press enter to finish");
        //Console.ReadLine();
    }
}
