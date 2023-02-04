using System;
using System.Diagnostics;

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

        // run a parallel loop in which one of
        // the iterations calls Stop()
        ParallelLoopResult loopResult =
            Parallel.For(0, 10, (int index, ParallelLoopState loopState) => {
                if (index == 2)
                {

                    if (args[0] == "UseBreak")
                    {
                        Console.WriteLine("Breaking on index {0}", index);
                        loopState.Break();
                    }
                    else if (args[0] == "UseStop")
                    {
                        Console.WriteLine("Stopping on index {0}", index);
                        loopState.Stop();
                    }
                }
            });

        // get the details from the loop result
        Console.WriteLine("Loop Result");
        Console.WriteLine("IsCompleted: {0}", loopResult.IsCompleted);
        Console.WriteLine("BreakValue: {0}", loopResult.LowestBreakIteration.HasValue);


        // wait for input before exiting
        //Console.WriteLine("Press enter to finish");
        //Console.ReadLine();
    }
}
