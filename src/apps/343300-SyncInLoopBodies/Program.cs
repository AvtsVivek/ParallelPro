using SyncInLoopBodies;
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

        var allowedCommandLineArgs = new List<string>() { "ViaTls", "ViaLock", "ViaShared" };

        if (!allowedCommandLineArgs.Contains(args[0]))
        {
            Console.WriteLine($"The command line args must be one among ... ");
            allowedCommandLineArgs.ForEach(allowedArg => Console.WriteLine(allowedArg));
            return;
        }

        double total = 0;
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        Console.WriteLine(args[0]);

        if (args[0] == "ViaTls")
        {
            var calculator = new SyncViaTls();
            total = calculator.Calulate();
        }
        
        if(args[0] == "ViaShared")
        {
            var calculator = new SyncViaSharedData();
            total = calculator.Calulate();
        }

        if (args[0] == "ViaLock")
        {
            var calculator = new SyncViaLock();
            total = calculator.Calulate();
        }


        stopwatch.Stop();

        Console.WriteLine($"Total is {total}");

        Console.WriteLine($"Time elapsed is {stopwatch.ElapsedMilliseconds}");

        // wait for input before exiting
        // Console.WriteLine("Press enter to finish");
        // Console.ReadLine();
    }
}
