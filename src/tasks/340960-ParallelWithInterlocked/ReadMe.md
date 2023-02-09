
- The total variable is shared between the iterations of the loop and, since the iterations may be performed concurrently, there is the potential
for a data race.
 
- To avoid the data race, we use the System.Threading.Interlocked class to safely add the current iteration index to the total. Because there are 100 steps in the parallel loop, the Interlocked class will be called 100 times, and as you know, synchronization has an overhead—so we incur that overhead 100 times.