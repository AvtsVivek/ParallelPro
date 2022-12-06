using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConcurrentBagEx
{
    public class CbTrials
    {
        private ConcurrentBag<int> concurrentBag = new ConcurrentBag<int>();

        private List<Task> addNumberToConcurrentBagTaskList = new List<Task>();

        public void AddTasks()
        {
            Console.WriteLine("Start of AddTasks method");

            for (int i = 0; i < 500; i++)
            {
                var numberToAdd = i;
                
                addNumberToConcurrentBagTaskList.Add(Task.Run(() => {

                    concurrentBag.Add(numberToAdd);

                    Console.WriteLine($"A new task is added to list, which will add {numberToAdd} to concurrent bag ");

                }));
            }

            Console.WriteLine("End of AddTasks method");
        }

        public void RunAllTasks()
        {
            Console.WriteLine("Start of RunAllTasks");

            Task.WaitAll(addNumberToConcurrentBagTaskList.ToArray());

            Console.WriteLine("All tasks done ");
        }

        public void PrintAllItemsInConcurrentBag()
        {
            Console.WriteLine($"The number of items in concurrent bag is {concurrentBag.Count}");

            foreach (var item in concurrentBag)
                Console.WriteLine(item);
            
        }
    }
}
