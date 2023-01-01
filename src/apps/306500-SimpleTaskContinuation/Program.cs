using System.Collections.Concurrent;
using System.Security.Principal;
using System.Threading.Tasks;

class SimpleClass
{
    public int Counter { get; set; }
}

class TaskContinuation
{
    static void Main(string[] args)
    {

        var task = new Task<SimpleClass>(() => {
            // create a new simple object
            var simpleObject = new SimpleClass();
            // enter a loop
            for (var i = 0; i < 1000; i++)
            {
                // increment the counter
                simpleObject.Counter++;
            }
            // return the simple object
            return simpleObject;
        });

        task.ContinueWith((Task<SimpleClass> antecedent) => {
            Console.WriteLine("Final Balance: {0}", antecedent.Result.Counter);
        });

        // start the task
        task.Start();

        // wait for input before exiting
        Console.WriteLine("Press enter to finish");
        Console.ReadLine();
    }
}