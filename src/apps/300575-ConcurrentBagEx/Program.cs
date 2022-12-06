// See https://aka.ms/new-console-template for more information
using ConcurrentBagEx;

Console.WriteLine("Hello, World!");

var cbTrials = new CbTrials();

cbTrials.AddTasks();

cbTrials.RunAllTasks();

cbTrials.PrintAllItemsInConcurrentBag();
