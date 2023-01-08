using System.ComponentModel.DataAnnotations;

class DetachedChildTaskEx
{
    static void Main(string[] args)
    {
        // create the parent task
        var parentTask = new Task(() => {
            // create the first child task
            var childTask = new Task(() => {
                // write out a message and wait
                Console.WriteLine("Child task running");
                Thread.Sleep(1000);
                Console.WriteLine("Child task finished");
                throw new Exception();
            });
            Console.WriteLine("Starting child task...");
            childTask.Start();
        });

        // start the parent task
        parentTask.Start();
        // wait for the parent task
        Console.WriteLine("Waiting for parent task");
        parentTask.Wait();
        Console.WriteLine("Parent task finished");
        // wait for input before exiting
        Console.WriteLine("Press enter to finish");
        Console.ReadLine();

        Console.WriteLine("Done ... ");
    }
}