

https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.blockingcollection-1

- The blocking collection by itself is not a collection. Its a wrapper to an undrelying collection. The underlying collection must impliment IProducerConsumerCollection.

- by default, the underlying collection is 