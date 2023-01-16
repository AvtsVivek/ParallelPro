
using System.Threading;

class SimpleClass
{
    public int Counter { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        // create object list
        var simpleObjectList = new List<SimpleClass>();

        for (int i = 0; i < 5; i++)
            simpleObjectList.Add(new SimpleClass());
        
        // create the total balance counter
        var totalCounts = 0;

        // create the barrier
        var barrier = new Barrier(5, (myBarrier) =>
        {
            // zero the balance
            totalCounts = 0;

            // sum the account totals
            foreach (var simpleObject in simpleObjectList)
                totalCounts += simpleObject.Counter;
            
            // write out the total number of counts.
            Console.WriteLine("Total number of counts: {0}", totalCounts);
        });

        // define the tasks array
        var taskList = new List<Task>();

        // loop to create the tasks
        for (var i = 0; i < 5; i++)
        {
            var task = new Task((stateObj) =>
            {

                // create a typed reference to the simpleObject
                SimpleClass simpleObject = (SimpleClass)stateObj!;

                // start of phase
                Random random = new ();
                for (int j = 0; j < 1000; j++)
                {
                    simpleObject.Counter += random.Next(1, 100);
                }
                // end of phase

                // tell the user that this task has has completed the phase
                Console.WriteLine("Task {0}, phase {1} ended",
                    Task.CurrentId, barrier.CurrentPhaseNumber);

                // signal the barrier
                barrier.SignalAndWait();

                // start of phase
                // alter the count of this Task's object using the total cout
                // deduct 10% of the difference from the total count
                simpleObject.Counter -= (totalCounts - simpleObject.Counter) / 10;
                // end of phase

                // tell the user that this task has has completed the phase
                Console.WriteLine("Task {0}, phase {1} ended", Task.CurrentId, barrier.CurrentPhaseNumber);

                // signal the barrier
                barrier.SignalAndWait();
            }, simpleObjectList[i]);
            taskList.Add(task);
        }

        taskList.ForEach(task => task.Start());

        // wait for all of the tasks to complete
        Task.WaitAll(taskList.ToArray());

        // wait for input before exiting
        Console.WriteLine("Press enter to finish");
        Console.ReadLine();
    }
}