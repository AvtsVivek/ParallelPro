
# Continuation Tasks

- One of the most useful features of the TPL is the flexible way you can coordinate what groups of Tasks do to build complex parallel programs. 
- In this chapter, we’ll look at the main approaches to coordination.

- The first two are task continuations and child tasks. 

- Continuations let you create chains of Tasks that are executed one after the other. 

- Child tasks are run in the body of another Task and let you break down work into smaller pieces in order to increase concurrency. This example demonstrates a simple task continuation.


