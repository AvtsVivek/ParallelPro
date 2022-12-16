using System;

namespace ConcurrencyPlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            var runner = new DemoRunner();
            runner.Run();

            // Uncomment if you want to see the console output
            // Console.WriteLine("Press any key to end the program.");
            // Console.ReadLine();
        }
    }
}
