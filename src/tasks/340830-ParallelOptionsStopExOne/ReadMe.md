# Parallel Loop state class. 

- Sequential loops don't always run to completion, and the same holds true for parallel loops. Say we have a collection of strings, and want to find one that contains the letter k. We might write a sequential loop as follows:

```cs
List<string> dataItems = new List<string>() { "an", "apple", "a", "day", "keeps", "the", "doctor", "away" };

foreach (string item in dataItems) 
{
    if (item.Contains("k")) { 
        Console.WriteLine("Hit: {0}", item);
        break;
    } 
    else 
        Console.WriteLine("Miss: {0}", item);    
}
```
- This loop works through the collection item by item until it reaches the word keeps, at which point the break keyword terminates the loop. You can’t use break with parallel loops, but you can get the same effect using the ParallelLoopState class.

- So what we need as follows.

```cs
Parallel.ForEach(dataItems, (string item, ParallelLoopState parallelLoopState) => {
    if (item.Contains("k"))
    {
        Console.WriteLine("Hit: {0}", item);
        parallelLoopState.Stop();
    }
    else
    {
        Console.WriteLine("Miss: {0}", item);
    }
});
```

- The instance of ParallelLoopState is created automatically by the Parallel class and passed to your Action.
- The Stop() method is useful when you are looking for a specific result, as in the sequential loop where we looked for a word containing the letter k. 
- When you call Stop(), the Parallel class doesn’t stop any new iterations. However, iterations that are already running may continue to be performed
- Tasks are used to process more than one iteration value, so some of your data items may continue to be processed even after you have called Stop().

- loopState.Break() means complete all iterations on all threads that are prior to the current iteration on the current thread, and then exit the loop
- loopState.Stop() means stop all iterations as soon as convenient
- So we dont see much diff between stop and break.

- https://stackoverflow.com/a/8818668/1977871

```cs
Parallel.For(0, maximum_operations, options, (a, loopState) =>
    {
        // do work

        // cancellationToken.Cancel() should be called externally
        if(token.IsCancellationRequested)
        {
            // cancellation requested - perform cleanup work if necessary

            // then call
            loopState.Break();
            // or
            loopState.Stop();
        }
    });
```

- 

 

