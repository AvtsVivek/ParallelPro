namespace People.Service;
public class CustomerService : ICustomerService
{
    public List<Customer> GetCustomers()
    {
        var p = new List<Customer>()
        {
            new Customer(1, "John", "Koenig", new DateTime(1975, 10, 17), 6, ""),
            new Customer(2, "Dylan", "Hunt", new DateTime(2000, 10, 2), 8, ""),
            new Customer(3, "Leela", "Turanga", new DateTime(1999, 3, 28), 8, "{1} {0}"),
            new Customer(4, "John", "Crichton", new DateTime(1999, 3, 19), 7, ""),
            new Customer(5, "Dave", "Lister", new DateTime(1988, 2, 15), 9, ""),
            new Customer(6, "Laura", "Roslin", new DateTime(2003, 12, 8), 6, ""),
            new Customer(7, "John", "Sheridan", new DateTime(1994, 1, 26), 6, ""),
            new Customer(8, "Dante", "Montana", new DateTime(2000, 11, 1), 5, ""),
            new Customer(9, "Isaac", "Gampu", new DateTime(1977, 9, 10), 4, ""),
        };
        return p;
    }
}
