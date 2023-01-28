using System.Threading.Channels;

class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("We expect one and only argument.");
            Console.WriteLine($"The number of args are {args.Length}.");
            Console.WriteLine($"Ensure there is only one argument.");
            Console.WriteLine("So exiting...");
            return; // we excpect only one arg
        }

        var start = DateTimeOffset.Now;
        Console.Clear();
        var ids = await CustomerReader.GetIdsAsync();
        Console.WriteLine(ids.ToDelimitedString(","));

        switch (args[0])
        {
            case "RunSequentially":
                {
                    // Option 1 = Run Sequentially
                    await RunSequentially(ids);
                }
                break;
            case "RunWithContinuation":
                {
                    // Option 2 = Run With Continuation
                    await RunWithContinuation(ids);
                }
                break;
            case "RunWithChannel":
                {
                    // Option 3 = Run With Channel
                    await RunWithChannel(ids);
                }
                break;
            default:
                {
                    Console.WriteLine("In correct command line arg.");
                    Console.WriteLine("Exiting. ....");
                }
                break;
        }

        var elapsed = DateTimeOffset.Now - start;
        Console.WriteLine($"\nTotal time: {elapsed}");

        Console.ReadLine();
    }

    // Option 1
    static async Task RunSequentially(List<int> ids)
    {
        foreach (var id in ids)
        {
            var person = await CustomerReader.GetCustomerAsync(id);
            DisplayPerson(person);
        }
    }

    // Option 2
    static async Task RunWithContinuation(List<int> ids)
    {
        var allTasks = new List<Task>();

        foreach (var id in ids)
        {
            Task<Customer> currentTask = CustomerReader.GetCustomerAsync(id);

            Task continuation = currentTask.ContinueWith(t =>
            {
                var person = t.Result;
                lock (allTasks)
                {
                    DisplayPerson(person);
                }
            });

            allTasks.Add(continuation);
        }
        await Task.WhenAll(allTasks);
    }

    // Option 3
    static async Task RunWithChannel(List<int> ids)
    {
        var channel = Channel.CreateBounded<Customer>(10);

        var consumer = ShowData(channel.Reader);
        var producer = ProduceData(ids, channel.Writer);

        await producer;
        await consumer;
    }

    static async Task ShowData(ChannelReader<Customer> reader)
    {
        await foreach (var person in reader.ReadAllAsync())
        {
            DisplayPerson(person);
        }
    }

    static async Task ProduceData(List<int> ids, ChannelWriter<Customer> writer)
    {
        var allTasks = new List<Task>();
        foreach (int id in ids)
        {
            var currentTask = FetchRecord(id, writer);
            allTasks.Add(currentTask);
        }
        await Task.WhenAll(allTasks);
        writer.Complete();
    }

    static async Task FetchRecord(int id, ChannelWriter<Customer> writer)
    {
        var person = await CustomerReader.GetCustomerAsync(id);
        await writer.WriteAsync(person);
    }

    static void DisplayPerson(Customer person)
    {
        Console.WriteLine("--------------");
        Console.WriteLine($"{person.ID}: {person}");
        Console.WriteLine($"{person.StartDate:D}");
        Console.WriteLine($"Rating: {new string('*', person.Rating)}");
    }
}
