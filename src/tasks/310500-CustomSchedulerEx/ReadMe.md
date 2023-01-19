

- Demos the use of the AutoResetEvent class. 
- The constructor requires you to specify whether the event is initially set. We create three worker Tasks, each of which calls the WaitOne() method of the AutoResetEvent. 
- A fourth Task, the supervisor, sets the event every 500 milliseconds. 
- Each time the event is set, one waiting worker Task is released. 
- If you run the program, you will see long sequences where a given worker Task is never released, or seems to be the one constantly being released—the AutoResetEvent class makes no guarantees about which waiting Task will be released when the event is set, and you should be careful not to make assumptions about the order in which workers are released when using this class.