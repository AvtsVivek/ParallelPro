# Continuation Tasks

Creating Many-to-One and Any-To-One Continuations

- The Earlier continuations were one-to-one or one-to-many; 
- One antecedent has one or more continuations. 
- This demos ContinueWhenAll() methods of the System.Threading.Tasks.TaskFactory class.
- Obtain an instance of TaskFactory through the static Task.Factory property.
- The ContinueWhenAll() and ContinueWhenAny() methods both take an array of Tasks argument. 
- ContinueWhenAll() schedules a continuation to be performed when all of the Tasks in the array have completed, whereas ContineWhenAny() schedules a continuation to be performed when any single Task in the array has completed. 
- This demo demonstrates a simple multitask continuation applied to the Isolation example from the previous chapter.
- 