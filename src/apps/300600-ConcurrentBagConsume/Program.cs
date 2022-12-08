// See https://aka.ms/new-console-template for more information
using ConcurrentBagConsume;

Console.WriteLine("Hello, World!");

var cbTrials = new CbTrials();

cbTrials.AddTasksToAddNumbers();

cbTrials.EnsureAllTasksAreCompleted();

cbTrials.AddTasksToRemoveNumbers();

cbTrials.CheckForItemsInBag();

