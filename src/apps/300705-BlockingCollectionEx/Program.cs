// create the blocking collection
using System.Collections.Concurrent;

var blockingCollection = new BlockingCollection<Deposit>();

// create and start the producers, which will generate
// deposits and place them into the collection
var producerTaskList = new List<Task>();

for (int i = 0; i < 3; i++)
{
    var producerTask = Task.Factory.StartNew(() => {
        // create a series of deposits
        for (int j = 0; j < 20; j++)
        {
            // create the transfer
            var deposit = new Deposit { Amount = 100 };
            // place the transfer in the collection
            blockingCollection.Add(deposit);
        }
    });
    producerTaskList.Add(producerTask);
};

// create a many to one continuation that will signal
// the end of production to the consumer
Task.Factory.ContinueWhenAll(producerTaskList.ToArray(), antecedents => {
    // signal that production has ended
    Console.WriteLine("Signalling production end");
    blockingCollection.CompleteAdding();
});

// create a bank account
var bankAccount = new BankAccount();

// create the consumer, which will update
// the balance based on the deposits
var consumerTask = Task.Factory.StartNew(() => {
    while (!blockingCollection.IsCompleted)
    {
        Deposit deposit;
        // try to take the next item 
        if (blockingCollection.TryTake(out deposit!))
        {
            // update the balance with the transfer amount
            bankAccount.Balance += deposit.Amount;
        }
    }
    // print out the final balance
    Console.WriteLine("Final Balance: {0}", bankAccount.Balance);
});

// wait for the consumer to finish
consumerTask.Wait();

// wait for input before exiting
Console.WriteLine("Press enter to finish");
Console.ReadLine();