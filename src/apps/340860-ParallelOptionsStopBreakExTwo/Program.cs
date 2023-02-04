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

        var res = Parallel.For(0, 100, (int index, ParallelLoopState loopState) => {
            // calculate the square of the index
            double sqr = Math.Pow(index, 2);
            // if the square value is > 100 then break
            
            if (sqr > 100)
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
            else
            {
                // write out the value
                Console.WriteLine("Square value of {0} is {1}", index, sqr);
            }

        });

        // wait for input before exiting
        //Console.WriteLine("Press enter to finish");
        //Console.ReadLine();
    }
}
