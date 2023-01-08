- Throwing exceptions in continuations chains

- There are no special features for propagating exceptions through a continuation chain. 

- Any exceptions thrown by any Task in a continuation chain must be processed, or they will be treated as unhandled exceptions when the finalizer for the Task is performed. 

- This demos illustrates the problem with exceptions in chains.

- The second-generation Task throws an exception, but the third-generation Task still runs. 
- The main thread waits for the last Task in the chain to complete and prompts the user to press the Enter key. 
- The program then exits, causing the finalizer to be called, at which point the exception from the secondgeneration continuation is propagated.