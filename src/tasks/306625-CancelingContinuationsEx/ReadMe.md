# Cancellation of Continuation Tasks

- Demos canceling continuations. An antecedent Task is created and waits using a CancellationToken wait handle. When the user presses the enter key, the CancellationTokenSource, and therefore the antecedent Task, are cancelled.

- Each of the three continuation Tasks behaves in a different way. The neverScheduled Task has been created with the same CancellationToken as the antecedent and so is never scheduled to be run. 
- The second Task, called badSelective, is created using the OnlyOnCanceled value from the TaskContinuationOptions enumeration. 

- But, it is created using the same CancellationToken as the antecedent, so the options and the token can never be in a state where the Task will be scheduled. 

- Tasks that rely on the OnlyOnCanceled value **should not share a CancellationToken with their antecedent**. 

- The final Task, named continuation, shows a selective continuation that will run properly when the antecedent is cancelled. 

- What is the TaskScheduler.Current sitting over there? Its needed by the overload. If you dont have it, then its a coompilation error.