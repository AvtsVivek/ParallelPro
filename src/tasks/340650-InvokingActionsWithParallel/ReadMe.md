

# Deadlocked Task Scheduler

- Custom task schedulers are often written with no means of expanding the number of threads available. This causes deadlock when there are more Task dependencies than free threads.

- Task schedulers should either allow inline execution or be able to add to the set of regular execution threads to cope with complex waiting relationships.

- The following demonstrates a custom task scheduler that maintains a fixed number of threads and does not allow inline execution. The code that tests the scheduler will cause a deadlock, because there are more waiting Tasks than there are working threads.

