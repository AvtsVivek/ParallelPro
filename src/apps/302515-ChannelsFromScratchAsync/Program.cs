using System.Collections.Concurrent;
using System.Runtime.InteropServices;


class Program
{
    static async Task Main()
    {
        var channel = new MyChannel<int>();
        _ = Task.Run(async delegate 
        {
            for (int i = 0; ; i++) 
            {
                await Task.Delay(1000);
                channel.Write(i);
            }
        });

        while (true)
        {
            Console.WriteLine(await channel.ReadAsync());
        }
    }
}

class MyChannel<T>
{
    // This is the backing store.
    // Unbounded
    private readonly ConcurrentQueue<T> _queue = new ();
    // The Sychronization to co-ordinate between the two players. 
    // semaphoreSlim is like a box of keys. 
    // If you want to use the bathroom, then you have to use a key.
    // if there is no key in the box, you have to wait for the key to go to use the bath. 
    // So here we creating a SemaphoreSlim, we are creating a box with 0 keys in it.
    private readonly SemaphoreSlim _semaphoreSlim = new (0);



    public void Write(T item)
    {
        _queue.Enqueue(item);
        // Release is putting key into the box.
        _semaphoreSlim.Release();
    }
    public async Task<T> ReadAsync()
    {
        // First i have to wait for a key.
        // As soon as _semaphoreSlim.Release(); is called, the following wait will return.
        await _semaphoreSlim.WaitAsync();
        // I now have a key in my hand at this point in time.
        bool v = _queue.TryDequeue(out T? item);
        return item!;
    }
}