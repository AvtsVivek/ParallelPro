using System.Collections.Concurrent;
using System.Security.Principal;
using System.Threading.Tasks;

class SimpleClass
{
    public double Number { get; set; }
}

class TaskContinuation
{
    static void Main(string[] args)
    {
        var rootTask = new Task<SimpleClass>(() => {
            // create a new simple object
            var simpleObject = new SimpleClass();
            Console.WriteLine("Here we go...Type a number");
            var numberString = Console.ReadLine();
            var number = double.Parse(numberString!);            
            simpleObject.Number = number;
            // return the simple object
            return simpleObject;
        });

        var firstStageContinuationTask2 = rootTask.ContinueWith<double>((Task<SimpleClass> antecedent) => {
            var numberTwice = (2 * antecedent.Result.Number);
            return numberTwice;
        });

        var firstStageContinuationTask3 = rootTask.ContinueWith<double>((Task<SimpleClass> antecedent) => {
            var numberThrice = (3 * antecedent.Result.Number);
            return numberThrice;
        });

        var firstStageContinuationTask4 = rootTask.ContinueWith<double>((Task<SimpleClass> antecedent) => {
            var numberFourTimes = (4 * antecedent.Result.Number);
            return numberFourTimes;
        });

        var secondStageContinuationTask2 = firstStageContinuationTask2.ContinueWith<double>((Task<double> firstStageContinuationTask2Innder) => {
            var finalResult = Math.Pow(firstStageContinuationTask2Innder.Result, 2 + 1);
            return finalResult;
        });

        var secondStageContinuationTask3 = firstStageContinuationTask3.ContinueWith<double>((Task<double> firstStageContinuationTask3Inner) => {
            var finalResult = Math.Pow(firstStageContinuationTask3Inner.Result, 3 + 1);
            return finalResult;
        });

        var secondStageContinuationTask4 = firstStageContinuationTask4.ContinueWith<double>((Task<double> secondStageContinuationTask4Inner) => {
            var finalResult = Math.Pow(secondStageContinuationTask4Inner.Result, 4 + 1);
            return finalResult;
        });

        try
        {
            // start the task
            rootTask.Start();
            // Wait for it to complete
            Task.WaitAll(rootTask);
        }
        catch (AggregateException exception)
        {
            Console.WriteLine(exception.Message);
            Console.WriteLine("Looks like you have not typed a proper number.");
            return; 
        }

        Console.WriteLine($"The give number is {rootTask.Result.Number} ");

        Console.WriteLine($"The first stage number firstStageContinuationTask2 is {firstStageContinuationTask2.Result}");
        Console.WriteLine($"The first stage number firstStageContinuationTask3 is {firstStageContinuationTask3.Result}");
        Console.WriteLine($"The first stage number firstStageContinuationTask4 is {firstStageContinuationTask4.Result}");

        Console.WriteLine($"The second stage number secondStageContinuationTask2 is {secondStageContinuationTask2.Result}");
        Console.WriteLine($"The second stage number secondStageContinuationTask3 is {secondStageContinuationTask3.Result}");
        Console.WriteLine($"The second stage number secondStageContinuationTask4 is {secondStageContinuationTask4.Result}");

        Console.WriteLine("Press enter to finish");
        Console.ReadLine();
    }
}