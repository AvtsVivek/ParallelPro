# Synchronization Introduction

- Synchronization means making Tasks take turns, avoiding races by serializing access to shared data, usually only allowing one Task access to the shared data at any given time

- The two key elements to understanding synchronization are 
  - The critical region. In party terms, critical regions are the cakes or the things that we want to control access to in order to avoid problems.
  - The synchronization primitive. These play the role of the great aunt, or the way in which we enforce order and politeness.

- We can imagine an ATM machine inside of a cabin and a security guard outside who will allow only one person inside.

- A critical region is one or more C# statements that we want to serialize access to so we can avoid a data race. For the counter example, the critical region is the statement that increments the
counter, as shown in the following fragment:

```cs
for (int j = 0; j < 1000; j++) {
    // update the balance
    simpleObject.Counter++; // <-- critical region
}
```

- A **synchronization primitive** is a special kind of data type that is used to coordinate Tasks’ access to critical regions and, therefore, to shared data. A Task arrives at a point in its work where they need to access a critical region. The Task checks with the synchronization primitive to see if the critical section is already in use. If the critical section is free, then it proceeds to execute the code statements. If there is another Task already using the critical section, then the newly arrived Task is asked to wait. When the Task that is using the critical section has finished, it tells the synchronization primitive that it is done, so that another Task can be allowed to proceed.

- Acquire or take the lock. When a primitive grants access to a critical section, the Task is said to **acquire or take the lock**. 
- Release or exit the lock: When a Task notifies the primitive that is leaving the critical section, it is said to **release or exit the lock**.

- There are three kinds of synchronization primitives in the .NET Framework:
  - Lightweight primitives
  - Heavyweight (classic) primitives
  - Wait handles

- 