using SemaphoreSlimIntro;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;

namespace OrderablePartitionerDemo
{

    class Program
    {
        private static string exampleNumber = string.Empty;
        static async Task Main(string[] args)
        {
            if (args.Length == 1)
                exampleNumber = args[0];
            else
            {
                Console.WriteLine("No example ran.");
                return;
            }

            switch (exampleNumber)
            {
                case "RunZeroCountExample":
                    {
                        await RunZeroCountExample();
                        return;
                    }
                case "RunOneCountExample":
                    {
                        await RunOneCountExample();
                        return;
                    }
                default:
                    {
                        Console.WriteLine("No example ran.");
                        break;
                    }
            }
        }

        private static async Task RunZeroCountExample()
        {
            // semaphoreSlim is like a box of keys. Keys are for a room like conf or bath room. 
            // If you want to use the room, then you need a key.
            // If there is no key in the box, you have to wait for the key to go use the room. 
            // So here we creating a SemaphoreSlim, we are creating a box with 0 keys in it.
            // Which means we will not be able to access the resource, or do the work. 
            var examples = new SemaphoreSlimExamples(0);
            await examples.ZeroCountExample();
        }
        private static async Task RunOneCountExample()
        {
            var examples = new SemaphoreSlimExamples(1);
            await examples.OneCountExample();
        }
    }
}