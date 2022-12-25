- Executing in Isolation. 

- Isolation solves the shared data problem by giving everyone their own piece of data. You don’t need to share if everyone at the party gets their own cake.

- We can provide a Task with isolated data by using the constructor overload that takes a state object.

- Each Task is given the current counter value(simpleObject.Counter whose value is 0) as a state object when it is created. The data is isolated because each Task only modifies its own version of the counter. When all of the Tasks have completed, we read the
results and combine them to accurately update the counter value.

- So the simpleObject.Counter value changes only in the end after all of the tasks are executed completely and successifully.

- There is no data race in here because the Tasks are only concerned with their own local version of the counter. 

- This is an example of isolation by convention: the code is written so that the Tasks works in isolation but the isolation is not enforced by the .NET runtime; we the developers have to take care to ensure that Tasks don’t share data by mistake.



