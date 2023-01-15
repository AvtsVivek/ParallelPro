// create the array of dog breeds


using System.Numerics;

var dogBreedList = new List<DogBreed>()
{
    new DogBreed() { BreedName = "German Shepherd" },
        new DogBreed() { BreedName = "Labrador" },
            new DogBreed() { BreedName = "Golden retriever" },
                new DogBreed() { BreedName = "Boxer" },
                    new DogBreed() { BreedName = "Tibertan mastiff" },
};

// create the total balance counter
double totalShoulderHeight = 0;
int breedCount = 0;
double meanOfHeight = 0;
List<double> differenceFromMeanList = new ();
double variance = 0;
object lockObj = new object();

// create the barrier
var barrier = new Barrier(dogBreedList.Count, (myBarrier) => {

    if (myBarrier.CurrentPhaseNumber == 0)
    {
        Console.WriteLine($"{myBarrier.CurrentPhaseNumber} zero");

        // sum the heights totals
        dogBreedList.ForEach(dogBreed => totalShoulderHeight += dogBreed.ShoulderHeight);

        // write out the balance
        Console.WriteLine("Total balance: {0}", totalShoulderHeight);
    }

    if (myBarrier.CurrentPhaseNumber == 1)
    {
        Console.WriteLine($"{myBarrier.CurrentPhaseNumber} zero");

        // sum the heights totals
        dogBreedList.ForEach(dogBreed => Console.WriteLine($"BreadName: {dogBreed.BreedName}, Height: {dogBreed.ShoulderHeight}"));

        dogBreedList.ForEach(dogBreed => Console.WriteLine($"{dogBreed.ShoulderHeight}"));

        // write out the balance
        Console.WriteLine("Total balance: {0}", totalShoulderHeight);
    }

    if (myBarrier.CurrentPhaseNumber == 2)
    {
        // Now calculate the average.
        // Now that the count is ready, we can calculate the average.

        meanOfHeight = totalShoulderHeight / breedCount;

        Console.WriteLine($"The mean is {meanOfHeight}");
    }

    if (myBarrier.CurrentPhaseNumber == 3)
    {
        // Now calculate the average.
        // Now that the count is ready, we can calculate the average.

        double sumOfDifferenceFromMean = 0;

        differenceFromMeanList.ForEach(diff => Console.WriteLine($"Squares {diff}"));

        differenceFromMeanList.ForEach(diff => sumOfDifferenceFromMean = sumOfDifferenceFromMean + diff);

        Console.WriteLine($"Sum of squares {sumOfDifferenceFromMean}");

        variance = sumOfDifferenceFromMean / (breedCount);

        Console.WriteLine($"The variance square is {variance}");
    }

    if (myBarrier.CurrentPhaseNumber == 4)
    {
        // Now calculate the average.
        // Now that the count is ready, we can calculate the average.

        var standardDeviation = Math.Sqrt(variance);

        Console.WriteLine($"The standard Deviation {standardDeviation}");
    }
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
        Console.WriteLine("Task {0}, phase {1} ended", Task.CurrentId, barrier.CurrentPhaseNumber);

        // signal the barrier
        barrier.SignalAndWait();

        // Start of next phase

        // In this phase, there is nothing to do. We already got the heights. 
        // So move on...

        // tell the user that this task has has completed the phase
        Console.WriteLine("Task {0}, phase {1} ended", Task.CurrentId, barrier.CurrentPhaseNumber);
        // end of phase
        // signal the barrier
        barrier.SignalAndWait();

        // start of phase

        // We nned to get the count of the dog breed. Its dogBreedList.Count. But pretend that its not available. 
        // Lets say we need to evaluate this.
        Interlocked.Increment(ref breedCount);
        // end of phase

        // tell the user that this task has has completed the phase
        Console.WriteLine("Task {0}, phase {1} ended", Task.CurrentId, barrier.CurrentPhaseNumber);
        // signal the barrier. Now the count is ready.
        barrier.SignalAndWait();

        // Start of phase Variance.

        double power = 2.0;
        lock (lockObj)
        {
            
            differenceFromMeanList.Add(Math.Pow((meanOfHeight - dogBreed.ShoulderHeight), power));
        }
        // End of phase.
        barrier.SignalAndWait();

        // Start of standard devaition phase
        // Ther is nothing to do here.
        // Simply wait.
        barrier.SignalAndWait();
    }, dogBreed);
    
    taskList.Add(task);
}

// start the task
taskList.ForEach(task => task.Start());

// wait for all of the tasks to complete
Task.WaitAll(taskList.ToArray());

// wait for input before exiting

Console.WriteLine("Exiting ...");

// Console.WriteLine("Press enter to finish");
// Console.ReadLine();