- Using Break or Stop

- When you use Break, then you will see that all of the squares of 1 to 10 are printed.
- But when you use Stop instead, then you will see that all of the squares of 1 to 10 are ***NOT*** printed.


- When, as in this example, Break() is called more than once, the Parallel class ensures that all of the items prior to the lowest index where Break() was called are processed. In the example, Break() was called on indices greate then 10. In this case, the lowest break occurred at index 11; therefore, all items prior to 11 will be processed. 

- The three ParallelLoopState properties IsStopped, LowestBreakIteration, and ShouldExitCurrentIteration allow you to improve the responsiveness of a parallel loop by checking the overall state of the loop from within your iteration. Checking these values, especially in iterations that will take a long time to complete, will reduce the amount of unwanted processing after Break() or Stop() has been called.

- 