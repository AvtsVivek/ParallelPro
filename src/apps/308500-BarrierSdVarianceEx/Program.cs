// create the array of bank accounts


var dogBreedList = new List<DogBreed>()
{
    new DogBreed() { BreedName = "German Shepherd" },
        new DogBreed() { BreedName = "Labrador" },
            new DogBreed() { BreedName = "Golden retriever" },
                new DogBreed() { BreedName = "Boxer" },
                    new DogBreed() { BreedName = "Tibertan mastiff" },
};

// create the total balance counter
int totalShoulderHeight = 0;

// create the barrier
var barrier = new Barrier(dogBreedList.Count, (myBarrier) => {
    // zero the height
    totalShoulderHeight = 0;
    // sum the heights totals
    dogBreedList.ForEach(dogBreed => totalShoulderHeight += dogBreed.ShoulderHeight);

    // write out the balance
    Console.WriteLine("Total balance: {0}", totalShoulderHeight);
});

// define the tasks array

var taskList = new List<Task>();

// loop to create the tasks
foreach (var dogBreed in dogBreedList)
{
    var task = new Task((stateObj) => {

        // create a typed reference to the account
        var dogBreed = (DogBreed)stateObj!;

        // start of phase
        Random random = new ();

        dogBreed.ShoulderHeight = random.Next(150, 600);
        
        // end of phase

        // tell the user that this task has has completed the phase
        Console.WriteLine("Task {0}, phase {1} ended",
            Task.CurrentId, barrier.CurrentPhaseNumber);

        // signal the barrier
        barrier.SignalAndWait();

        // start of phase
        // alter the balance of this Task's account using the total balance 
        // deduct 10% of the difference from the total balance
        dogBreed.ShoulderHeight -= (totalShoulderHeight - dogBreed.ShoulderHeight) / 10;
        // end of phase

        // tell the user that this task has has completed the phase
        Console.WriteLine("Task {0}, phase {1} ended",
            Task.CurrentId, barrier.CurrentPhaseNumber);

        // signal the barrier
        barrier.SignalAndWait();
    },
    dogBreed);
    
    taskList.Add(task);
}

// start the task
taskList.ForEach(task => task.Start());

// wait for all of the tasks to complete
Task.WaitAll(taskList.ToArray());

// wait for input before exiting
Console.WriteLine("Press enter to finish");
Console.ReadLine();