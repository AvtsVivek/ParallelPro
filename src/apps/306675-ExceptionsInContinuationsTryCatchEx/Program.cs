using System.ComponentModel.DataAnnotations;

class ExceptionsInContinuationsTryCatchEx
{
    static void Main(string[] args)
    {

        // create a first generation task
        var gen1Task = new Task(() => {
            // write out a message
            Console.WriteLine("First generation task");
        });

        // create a second generation task
        var gen2Task = gen1Task.ContinueWith(antecedent => {
            // write out a message
            Console.WriteLine("Second generation task - throws exception");
            throw new Exception();
        });

        // create a third generation task
        var gen3Task = gen2Task.ContinueWith(antecedent => {

            // check to see if the antecedent threw an exception
            if (antecedent.Status == TaskStatus.Faulted)
            {
                var ex = antecedent.Exception!;
                Console.WriteLine($"The type of exception {ex.GetType().FullName}");
                var innerEx = ex.InnerException!;
                Console.WriteLine($"The type of inner exception {innerEx.GetType().FullName}");
                // get and rethrow the antecedent exception
                throw antecedent.Exception!.InnerException!;
            }
            // write out a message
            Console.WriteLine("Third generation task");
        });

        // start the first gen task
        gen1Task.Start();

        try
        {
            // wait for the last task in the chain to complete
            gen3Task.Wait();
        }
        catch (AggregateException ex)
        {
            ex.Handle(inner => {
                Console.WriteLine("Handled exception of type: {0}", inner.GetType());
                return true;
            });
        }

        // wait for input before exiting
        Console.WriteLine("Press enter to finish");
        Console.ReadLine();

        Console.WriteLine("Done ... ");
    }
}