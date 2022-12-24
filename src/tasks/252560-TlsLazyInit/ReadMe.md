
- ThreadLocal provides an overloaded constructor so you can supply a factory delegate that will
initialize the isolated data value. This factory delegate is lazily initialized, meaning that it will not be
called until the Task calls the ThreadLocal.Value property for the first time.

- The factory delegate is not called until the first time that the variable is accessed

- 