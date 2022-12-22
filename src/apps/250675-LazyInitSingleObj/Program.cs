using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Here we go...");
        var lazyOrder = new Lazy<Order>();
        Console.WriteLine("lazy order created. But its value is not.");
        Console.WriteLine($"{lazyOrder}");
        Console.WriteLine("But the order object is not yet created.");
        Console.WriteLine("We are now accessing the Value property of the layOrder object. So now the order object is created.");
        var order = lazyOrder.Value;
        Console.WriteLine($"The order id is {order.Id}");
    }
}

public class Order 
{
    public int Id { get; set; }
    public Order()
    {
        Console.WriteLine("From the order class ctor ");
        Debugger.Break();
        Console.WriteLine("Order object is now created.");
        Id = 10;
    }
}