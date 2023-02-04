using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {

        var dataItems = new List<string>() { "an", "apple", "a", "day",
                    "keeps", "the", "doctor", "away" };

        Parallel.ForEach(dataItems, (string item, ParallelLoopState parallelLoopState) => {

            if (item.Contains("k"))
            {
                Console.WriteLine("Hit: {0}", item);
                parallelLoopState.Stop();
            }
            else
            {
                Console.WriteLine("Miss: {0}", item);
            }

        });

        // wait for input before exiting
        Console.WriteLine("Press enter to run the same loop with break instead of stop");
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

        });


        // wait for input before exiting
        Console.WriteLine("Press enter to run the same loop to print task id");
        Console.ReadLine();

        Parallel.ForEach(dataItems, (string item, ParallelLoopState parallelLoopState) => {

            if (item.Contains("k"))
            {
                Console.WriteLine("Hit: {0}", item);
                parallelLoopState.Stop();
            }
            else
            {
                Console.WriteLine("Miss: {0}", item);
            }

            Console.WriteLine("Task ID {0} processing: state is {1}", Task.CurrentId, parallelLoopState.ToString());
        });


        // wait for input before exiting
        Console.WriteLine("Press enter to finish");
        Console.ReadLine();

    }
}
