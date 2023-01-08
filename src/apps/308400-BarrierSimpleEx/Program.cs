// create the array of bank accounts


//var dogBreedList = new List<DogBreed>()
//{
//    new DogBreed() { BreedName = "German Shepherd" },
//        new DogBreed() { BreedName = "Labrador" },
//            new DogBreed() { BreedName = "Golden retriever" },
//                new DogBreed() { BreedName = "Boxer" },
//                    new DogBreed() { BreedName = "Tibertan mastiff" },
//};

// create the total balance counter
// int totalShoulderHeight = 0;

// define the tasks array

var taskCount = 5;

var taskList = new List<Task>();

// create the barrier
var barrier = new Barrier(taskCount, (myBarrier) => {
    Console.WriteLine($"Post-Phase action: phase={myBarrier.CurrentPhaseNumber}");
});

// loop to create the tasks

for (int i = 0; i < taskCount; i++)
{
    var task = new Task(() => {
        // signal the barrier
        Console.WriteLine("First signal...");
        barrier.SignalAndWait();
        Console.WriteLine("Second signal...");
        barrier.SignalAndWait();
        Console.WriteLine("Third signal...");
        barrier.SignalAndWait();
        Console.WriteLine("Fourth signal...");
        barrier.SignalAndWait();
        Console.WriteLine("Fifth signal...");
        barrier.SignalAndWait();
    });
    taskList.Add(task);
}

// start the task
taskList.ForEach(task => task.Start());

// wait for all of the tasks to complete
Task.WaitAll(taskList.ToArray());

// wait for input before exiting
Console.WriteLine("Press enter to finish");
Console.ReadLine();