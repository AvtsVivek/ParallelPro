
Synchronization in Loop Bodies

A synchronization primitive is used to coordinate access to a shared variable, typically a result.

Performance is significantly reduced because the tasks performing the loop body must wait to acquire
the primitive on each loop body iteration.

Solution

Use TLS instead of a synchronization primitive 