# Data Race with collections

- This example demos data races that can happen with regular dot net collections.

- One of the most common ways of sharing data is through collection classes. Often, you will want to use Tasks to parallel process the contents of a collection or use a collection to gather the results produced by Tasks. 

- Sharing a collection between Tasks creates the same kinds of data races as sharing other types. This example demonstrates a collection data race. A System.Collections.Generic.Queue<int> with 1,000 items is processed by ten Tasks. 

- While there are still items in the collection, the Tasks remove the first item and increment a counter, and the counter is synchronized using interlocked operations.

- Running the example gives you will get the following message.
 
```
Exception type System.InvalidOperationException from System.Private.CoreLib
Exception message is Queue empty.
Exception type System.InvalidOperationException from System.Private.CoreLib
Exception message is Queue empty.
Items processed: 1093
Press enter to finish
```

- In the above you see **Items processed: 1093**. You may get a different number.
- The number should be 1000, but its 1093. Why is that? Its due to data race.

- The example gives rise to two kinds of data race. The first is where the counter value exceeds 1,000, which happens because the steps in the Queue. So you see 1093 above. Dequeue() method are not synchronized so Tasks are reading the same value several times from the head of the queue. 

- The second is a System.InvalidOperationException, thrown when calls to Queue.Dequeue() are made when the queue is
empty; this happens because the check to see if there are items left in the queue (sharedQueue.Count > 0) and the request to take an item from the queue (sharedQueue.Dequeue()) are not protected in a critical region.