
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

        var allowedCommandLineArgs = new List<string>() { "AsParallel", "AsRegular"};

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
            //var results = from item in sourceData.AsParallel() 
            //              where item % 2 == 0 select item;

            var results = sourceData.AsParallel()
            .Where(item => item % 2 == 0)
            .Select(item => item);

            foreach (int item in results)
                Console.WriteLine("Item {0}", item);
        }

        if (args[0] == "AsRegular")
        {
            var results = sourceData
            .Where(item => item % 2 == 0)
            .Select(item => item);

            foreach (int item in results)
                Console.WriteLine("Item {0}", item);
        }


        // wait for input before exiting
        //Console.WriteLine("Press enter to finish");
        //Console.ReadLine();
    }
}
