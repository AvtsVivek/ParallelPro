using ChannelsCustomReadWrite;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;


class Program
{
    static void Main()
    {
        var channel = new CustomChannel<string>();

        var produceTaskOne = Producer(channel, 3);
        var produceTaskTwo = Producer(channel, 3);
        var produceTaskThree = Producer(channel, 3);
        var consumerTask = Consumer(channel);

        Task.WaitAll(produceTaskOne, produceTaskTwo, produceTaskThree, consumerTask);

        // You can run the above with three producers or below with one producer.
        //var produceTaskOne = Producer(channel, 50);
        //var consumerTask = Consumer(channel);
        //Task.WaitAll(produceTaskOne, consumerTask);
    }

    private static async Task Producer(IWrite<string> writer, int numberOfItemToProduce) {

        for (int i = 0; i < numberOfItemToProduce; i++)
        {
            writer.Push(i.ToString());
            await Task.Delay(100);
            // https://stackoverflow.com/a/38530225/1977871
        }
        writer.Complete();

        // return Task.CompletedTask;
    }

    private static async Task Consumer(IRead<string> reader)
    {
        while (!reader.IsComplete())
        {
            var item = await reader.ReadAsync();
            Console.WriteLine($"comsumer: {item}");
        }
        Console.WriteLine("Done from consumer side.");
    }
}

