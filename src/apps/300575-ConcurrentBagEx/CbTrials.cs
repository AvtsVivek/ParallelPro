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
        public ConcurrentBag<int> cb = new ConcurrentBag<int>();
        public List<Task> addTaskList = new List<Task>();

        public void AddTasks()
        {
            Console.WriteLine("Start of AddTasks method");

            for (int i = 0; i < 500; i++)
            {
                var numberToAdd = i;
                
                addTaskList.Add(Task.Run(() => {
                    cb.Add(numberToAdd);
                    Console.WriteLine($"A new task is added to list, while the {numberToAdd} to ");
                }));
            }

            Console.WriteLine("End of AddTasks method");
        }

        public void RunAllTasks()
        {
            Console.WriteLine("Start of RunAllTasks");
            Task.WaitAll(addTaskList.ToArray());
            Console.WriteLine("All tasks done ");
        }
    }
}
