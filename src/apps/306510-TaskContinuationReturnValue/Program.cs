using System.Collections.Concurrent;
using System.Security.Principal;
using System.Threading.Tasks;

class Product
{
    public int Price { get; set; }
}

class TaskContinuation
{
    static void Main(string[] args)
    {
        // We are calling here antecedentTask and continuationTask.
        // Better would be with the suffix Task and so antecedent and continuation.

        var antecedentTask = new Task<Product>(() => {
            // create a new simple object
            var simpleProduct = new Product();
            // enter a loop, do some calculation to calculate the price.
            for (var i = 0; i < 1000; i++)
            {
                // increment the counter
                simpleProduct.Price++;
            }
            // return the simple object
            return simpleProduct;
        });

        var continuationTask = antecedentTask.ContinueWith<double>((Task<Product> antecedent) => {
            Console.WriteLine("The cost price is: {0}", antecedent.Result.Price);
            // Now add 10 Percent GST(good and service tax)
            return antecedent.Result.Price * 1.1;
        });

        // start the task
        antecedentTask.Start();

        Console.WriteLine($"Final price of the product is {continuationTask.Result}");

        // wait for input before exiting
        Console.WriteLine("Press enter to finish");
        Console.ReadLine();
    }
}