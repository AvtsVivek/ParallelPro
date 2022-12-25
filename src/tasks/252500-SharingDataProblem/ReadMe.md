- The example creates 10 Tasks, each of which increments the Counter property 1,000 times.
We then wait for all of the Tasks to complete and print out the value of Counter value. If there are ten Tasks and each
of them increments counter 1000 times, the final value should be 10K (10 × 1000).

- What happens in practice is different.
Expected value 10000, Actual value: 8840

- Why is this so...?

- This is caused by a data race. Whenever we have two or more Tasks performing operations that update a shared piece data, there is the potential for a race.

- We can imageine this to be of three step process.
  - Read the current counter value from the object.
  - Calculate the new value.
  - Update the counter with the new value.

- So while a first task is reading and then calculting the new value, a different, second, task would read and do the same, but before the first task would update. The second should have read after the first task has updated. 

- There are four broad kinds of solution to shared data problems:
  - Sequential execution: We stop parallelizing the work.
  - Immutability: We stop Tasks being able to modify the data. 
  - Isolation: We stop sharing the data.
  - Synchronization: We coordinate the actions of the Tasks so that they take turns instead of competing.

- The first two are not useful. In the party and cake context, sequential execution means that you get the cake to
yourself(only one thread, or task), immutability means that you can look at the cake but can’t eat it.

- Executing in Isolation, Isolation solves the shared data problem by giving everyone their own piece of data. You don’t need to
share if everyone at the party gets their own cake.

- Finally, Synchronizing Execution. If sequential execution means being the only person at the party, immutability is having a plastic cake(you cannot cut), and isolation is giving everyone their own cake, then synchronization is having many guests and one cake and asking one person to ensure everyone gets a piece of the cake by having turns and play nice.