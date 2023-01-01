using System.Collections.Concurrent;
using System.Security.Principal;
using System.Threading.Tasks;

class Multiple_Locks
{
    static void Main(string[] args)
    {

        // create two lock objects
        var lock1 = new object();
        var lock2 = new object();

        // create a task that acquires lock 1
        // and then lock 2
        var task1 = new Task(() => {
            lock (lock1)
            {
                Console.WriteLine("Task 1 acquired lock 1");
                Thread.Sleep(500);
                lock (lock2)
                {
                    Console.WriteLine("Task 1 acquired lock 2");
                }
            }
        });

        // create a task that acquires lock 2
        // and then lock 1
        var task2 = new Task(() => {
            lock (lock2)
            {
                Console.WriteLine("Task 2 acquired lock 2");
                Thread.Sleep(500);
                lock (lock1)
                {
                    Console.WriteLine("Task 2 acquired lock 1");
                }
            }
        });

        // start the tasks
        task1.Start();
        task2.Start();

        // wait for input before exiting
        Console.WriteLine("Press enter to finish");
        Console.ReadLine();
    }
}