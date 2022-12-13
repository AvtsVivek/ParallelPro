namespace BlockingCollectionDemo;

class BlockingCollectionDemo
{
    static async Task Main()
    {
        //await AddTakeDemo.BC_AddTakeCompleteAdding();
        //TryTakeDemo.BC_TryTake();
        // FromToAnyDemo.BC_FromToAny();
        await ConsumingEnumerableDemo.BC_GetConsumingEnumerable();
        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }
}
