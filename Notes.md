- Background Thread and Foreground Thread
  - A managed thread created as a foreground thread is not the UI thread or the main thread. 
  - Foreground threads are threads that will prevent the managed process from terminating if they are running. 
  - If an application is terminated, any running background threads will be stopped so that the process can shut down.
  - By default, newly created threads are foreground threads

- 