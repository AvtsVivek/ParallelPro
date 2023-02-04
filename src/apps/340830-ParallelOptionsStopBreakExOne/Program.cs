using System;
using System.Diagnostics;
using System.Reflection;

class Program
{
    static void Main(string[] args)
    {

        if (args.Length != 1)
        {
            Console.WriteLine("Only 1 command line arg is expected. Returning....");
            return;
        }

        var validInputArgList = new List<string>() { "UseBreak", "UseStop" };

        if (!validInputArgList.Contains(args[0]))
        {
            Console.WriteLine("The command line arg must be on of UseBreak, UseStop and nothing else. Returning....");
            return;
        }

        Console.WriteLine($"The command line arg is {args[0]}");

        var dataItems = new List<string>() { "an", "apple", "a", "day",
                    "keeps", "the", "doctor", "away" };

        Parallel.ForEach(dataItems, (string item, ParallelLoopState parallelLoopState) => {

            if (item.Contains("k"))
            {
                if (args[0] == "UseBreak")
                {
                    Console.WriteLine("Hit using Break: {0}", item);
                    parallelLoopState.Break();
                }
                else if (args[0] == "UseStop")
                {
                    Console.WriteLine("Hit using Stop: {0}", item);
                    parallelLoopState.Stop();
                }
            }
            else
            {
                Console.WriteLine("Miss : {0}", item);
            }
        });


        // wait for input before exiting
        Console.WriteLine("Press enter to run the same loop to print task id and break");
        Console.ReadLine();

        Parallel.ForEach(dataItems, (string item, ParallelLoopState parallelLoopState) => {

            if (item.Contains("k"))
            {
                Console.WriteLine("Hit: {0}", item);
                parallelLoopState.Break();
            }
            else
            {
                Console.WriteLine("Miss: {0}", item);
            }

            Console.WriteLine("Task ID {0} processing: state is {1}", Task.CurrentId, parallelLoopState.ToString());
        });


        // wait for input before exiting
        // Console.WriteLine("Press enter to finish");
        // Console.ReadLine();

    }
}
