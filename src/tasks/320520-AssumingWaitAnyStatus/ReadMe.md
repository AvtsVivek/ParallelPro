- Here we go ....

- The Task.WaitAny() method triggers the continuation Task when one of the antecedents completes, but completion can include cancellation or an exception being thrown. A common problem arises when a continuation Task doesn’t check the state of the antecedent.

- This problem is a variation on the previous one, and the solution is similar. Check to see if the antecedent that has triggered the continuation has completed properly, and use common cancellation token sources.

- The following example ensures that a cancelled Task is the antecedent to a many-to-one continuation that reads the Result property without checking the antecedent status. This results in an unhandled exception.

- 