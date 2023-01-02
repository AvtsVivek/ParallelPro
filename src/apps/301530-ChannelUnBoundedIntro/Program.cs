using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Threading.Channels;

namespace OrderablePartitionerDemo
{
    class Program
    {
        static async Task Main()
        {
            var unboundedChannel = Channel.CreateUnbounded<int>();
            _ = Task.Run(async delegate 
            {
                for (int i = 0; ; i++) 
                {
                    await Task.Delay(1000);
                    unboundedChannel.Writer.TryWrite(i);
                    // You can use the following as well. 
                    // await unboundedChannel.Writer.WriteAsync(i);
                }
            });

            while (true)
            {
                Console.WriteLine(await unboundedChannel.Reader.ReadAsync());
            }
        }
    }

    
}