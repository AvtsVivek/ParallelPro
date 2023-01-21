

- A common problem when consuming items from a BlockingCollection is to enter a loop while the IsCompleted property returns false and call the blocking Take() method to remove items from the collection. 
- This creates a potential race condition, such that:
• No items waiting to be processed.
• The consumer checks the IsCompleted property and gets false.
• The producer calls the CompleteAdding() method.
• The consumer calls Take().

- This sequence causes an exception to be thrown. Between the call to IsCompleted and Take(), the producer has completed the collection and attempts to Take() from a completed collection result in an exception.

- 