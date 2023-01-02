using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Threading.Channels;

namespace OrderablePartitionerDemo
{
    class Program
    {
        static async Task Main()
        {
            var boundedChannel = Channel.CreateBounded<int>(10);

            _ = Task.Run(async delegate 
            {
                for (int i = 0; i < 10 ; i++) 
                {
                    await Task.Delay(100);
                    await boundedChannel.Writer.WriteAsync(i);
                }
                boundedChannel.Writer.Complete();
            });

            // So here, once the Complete() is called, the following loop will exit.
            //await foreach (var item in boundedChannel.Reader.ReadAllAsync())
            //{
            //    Console.WriteLine(item);
            //}

            // We can use the above loop or below loop. Any one will work.

            while (await boundedChannel.Reader.WaitToReadAsync())
            {
                Console.WriteLine(await boundedChannel.Reader.ReadAsync());
            }

            // Then the following done is written.
            Console.WriteLine("Done!!");
        }
    }    
}