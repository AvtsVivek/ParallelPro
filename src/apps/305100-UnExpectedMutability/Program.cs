using System.Collections.Concurrent;
using System.Security.Principal;
using System.Threading.Tasks;


class MyReferenceData
{
    public double PI = 3.14;
}

class MyImmutableType
{
    public readonly MyReferenceData refData = new MyReferenceData();
    public readonly int circleSize = 1;
}

class UnExpectedImmutability
{

    static void Main(string[] args)
    {

        // create a new instance of the immutable type
        var immutableObject = new MyImmutableType();

        // create a cancellation token source
        var cancellationTokenSource = new CancellationTokenSource();

        // create a task that will calculate the circumference
        // of a 1 unit circle and check the result
        var task1 = new Task(() => {
            while (true)
            {
                // perform the calculation
                double circ = 2 * immutableObject.refData.PI * immutableObject.circleSize;
                Console.WriteLine("Circumference: {0}", circ);
                // check for the mutation
                if (circ == 4)
                {
                    // the mutation has occurred - break
                    // out of the loop
                    Console.WriteLine("Mutation detected");
                    break;
                }
                // sleep for a moment
                cancellationTokenSource.Token.WaitHandle.WaitOne(250);
            }
        }, cancellationTokenSource.Token);

        // start the task
        task1.Start();

        // wait to let the task start work
        Thread.Sleep(1000);

        // perform the mutation
        immutableObject.refData.PI = 2;

        // join the task
        task1.Wait();

        // wait for input before exiting
        Console.WriteLine("Press enter to finish");
        Console.ReadLine();
    }
}