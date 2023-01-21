

- A frequently used optimization in C# is to reuse objects. This is done to avoid the overhead of creating and subsequently disposing of new instances. No issues arise when this technique is applied to a consumer taking items from a BlockingCollection, but it cannot be safely applied to a producer. 

- Doing so adds the same instance to the collection again and again, and each time the producer changes the fields of the object, the change applies to every reference currently unprocessed in the collection.

- 