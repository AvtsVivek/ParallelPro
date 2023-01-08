
# Child Tasks, attached.

- Attached child tasks have a special relationship with their parents. 
- There are three parts to the relationship:
  - The parent Task waits for attached child Tasks to complete before it completes.
  - The parent Task throws any exceptions thrown by attached child Tasks.
  - The status of the parent Task depends on the status of attached child Tasks.

- Specify the AttachedToParent value from the System.Threading.Tasks.TaskCreationOptions enumeration as a constructor argument. 
- This establishes the relationship with the parent Task
- The Wait() call on the parent Task will not return until the parent and all of its attached children have finished
- 