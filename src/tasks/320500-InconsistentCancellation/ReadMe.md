
- Cancellation tokens are used to create antecedent Tasks but not continuations. The continuations are still scheduled, which causes unexpected results and usually leads to unhandled exceptions.

- Either check the status of the antecedent using the Task.Status property, or create antecedents using tokens from the same CancellationTokenSource as the antecedent (in which case the continuation is not performed).

- The following example creates a Task that waits until the CancellationToken it has been created with is cancelled. Three continuations are created

- The first indicates the problem of not using a cancellation token and not checking the antecedent status; the Result property of the antecedent is read, which cause the continuation Task to throw an unhandled exception. The second continuation uses the same cancellation token source as its antecedent, so it isn’t executed when the antecedent is cancelled. 

- The third continuation checks the status of the antecedent and reads the Result property only if the antecedent has not been cancelled.