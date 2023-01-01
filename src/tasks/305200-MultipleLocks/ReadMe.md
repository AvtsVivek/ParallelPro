
# Multiple Locks

- In a multiple lock scenario, multiple critical sections modify the same shared data, and each has its own lock or synchronization primitive. 

- Access to each critical region is synchronized, but there is no overall coordination which means that a data race can still occur.

- Ensure that every Task that enters the critical region uses the same reference to acquire the synchronization lock.

- The following example shows two locks being used to synchronize access to critical regions that access the same shared data, in this case, the counter of our simple object example. 

- Ten Tasks are created in two groups of five: members of the first group uses one lock to synchronize their balance updates, and the second group uses the other lock.

- 