- Task Dependency Deadlock

- If two or more Tasks depend on each other to complete, none can move forward without the others, so a
deadlock (the condition where the Tasks involved cannot progress) occurs.

- Solution

- The only way to avoid this problem is to ensure that your Tasks do not depend on one another. This
requires careful attention when writing your Task bodies and thorough testing. You can also use the
debugging features of Visual Studio 2010 to help detect deadlocks (see Chapter 7 for details).

- Example

- In the following example, two Tasks depend upon one another, and each requires the result of the other
to generate its own result. When the program is run, both Tasks are started by the main application
thread and deadlock. Because the main thread waits for the Tasks to finish, the whole program seizes up
and never completes.

