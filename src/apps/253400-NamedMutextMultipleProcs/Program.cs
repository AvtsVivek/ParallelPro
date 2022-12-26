using System.Diagnostics;
using System.Security.Principal;
using System.Threading;

class SimpleClass
{
    public int Counter { get; set; } = default!;
}
class Program
{
    private static void Main(string[] args)
    {

        // declare the name we will use for the mutex
        var mutexName = "myApressMutex";

        // declare the mutext
        Mutex namedMutext;

        try
        {
            // test to see if the named mutex already exists
            namedMutext = Mutex.OpenExisting(mutexName);
        }
        catch (WaitHandleCannotBeOpenedException)
        {
            // the mutext does not exist - we must create it
            namedMutext = new Mutex(false, mutexName);
        }

        // create the task
        var task = new Task(() => {
            while (true)
            {
                // acquire the mutex
                Console.WriteLine("Waiting to acquire Mutex");
                namedMutext.WaitOne();
                Console.WriteLine("Acquired Mutex - press enter to release");
                Console.ReadLine();
                namedMutext.ReleaseMutex();
                Console.WriteLine("Released Mutex");
            }
        });

        // start the task
        task.Start();

        // wait for the task to complete
        task.Wait();
    }
}
