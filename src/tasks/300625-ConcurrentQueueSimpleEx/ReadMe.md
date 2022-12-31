
- The ConcurrentQueue class implements a first in, first out (FIFO) queue, which means that when you take items from the queue, you get them in the same order in which they were added. 

- To place an item into a ConcurrentQueue, you call the Enqueue() method. 

- To take the first item in the queue, you call TryDequeue() and to get the first item in the queue without taking it, you call TryPeek().

- TryDequeue() and TryPeek() take a parameter of the collection type, modified by the out keyword and return a bool result. 

- If the result is true, the parameter will contain the data item. If it is false, no data item could be obtained. 

- The shows how to use the concurrentQueue class to resolve the data race. 

- The TryDequeue() and TryPeek() methods force you to code for possible failure, that is, for the eventuality that no data item is available. The example checks to see if there are items in the queue by reading the Count property. We can’t assume that there will still be items by the time our call to TryDequeue() is executed, so we have to check the bool result from TryDequeue() to see if we received data to process. 

