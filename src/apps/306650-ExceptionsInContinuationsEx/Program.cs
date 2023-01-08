class ExceptionsInContinuationsEx
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
            // write out a message
            Console.WriteLine("Third generation task");
        });

        // start the first gen task
        gen1Task.Start();

        // wait for the last task in the chain to complete
        gen3Task.Wait();

        // wait for input before exiting
        Console.WriteLine("Press enter to finish");
        Console.ReadLine();

        Console.WriteLine("Done ... ");
    }
}