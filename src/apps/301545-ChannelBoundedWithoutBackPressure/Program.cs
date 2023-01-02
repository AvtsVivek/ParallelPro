using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Threading.Channels;

namespace OrderablePartitionerDemo
{
    class Program
    {
        static async Task Main()
        {
            var boundedChannel = Channel.CreateBounded<int>(1);

            _ = Task.Run(async delegate 
            {
                for (int i = 0; ; i++) 
                {
                    await Task.Delay(1000);
                    // Even thoought we are trying to put an element every one second, 
                    // and that an element is being pulled out every two seconds,
                    // there is no back pressure being built up. 
                    // Back pressure builds up, if there are more and more items being put into 
                    // the system than the nubmer of itmes being retrived.
                    // Note that the This is bounded channel with only one elment(see the ctor).
                    // Even though we are trying to put an element every one second, the channel is bounded at 1 element.
                    // So it has to wait till its empty again. And this happens eveery two seconds.
                    // Sincec it is limited to 1 element, there is no back pressure.
                    await boundedChannel.Writer.WriteAsync(i);
                }
            });

            while (true)
            {
                await Task.Delay(2000);
                Console.WriteLine(await boundedChannel.Reader.ReadAsync());
            }
        }
    }

    
}