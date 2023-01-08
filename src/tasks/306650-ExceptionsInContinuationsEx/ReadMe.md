- Throwing exceptions in continuations chains

- There are no special features for propagating exceptions through a continuation chain. 
- Any exceptions thrown by any Task in a continuation chain must be processed, or they will be treated as unhandled exceptions when the finalizer for the Task is performed. 
- See the previous chapter for details of processing Task exceptions. Listing 4-8 illustrates the problem with exceptions in chains.
- 