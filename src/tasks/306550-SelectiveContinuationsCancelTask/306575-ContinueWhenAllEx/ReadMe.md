# Continuation Tasks

- This demos what continuation tasks are run if the antecedent is cancelled. 

![TaskContinuation](./images/20SelectiveContinuations20.svg)

- Only the following ContinuationTasks are not executed. Except these two all others are executed.

```cs
var firstStageContinuationTaskNotOnCanceled = firstStageTask.ContinueWith((Task firstStageTask) => {
    Console.WriteLine("Continuation ...NotOnCanceled"); // This is not executed.
}, TaskContinuationOptions.NotOnCanceled);

var firstStageContinuationTaskOnlyOnRanToCompletion = firstStageTask.ContinueWith((Task firstStageTask) => {
    Console.WriteLine("Continuation ...OnlyOnRanToCompletion"); // This is not executed.
}, TaskContinuationOptions.OnlyOnRanToCompletion);
```

