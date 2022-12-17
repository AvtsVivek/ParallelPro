// Interrupt a sleeping thread.

using ThreadInterrupt;

ThreadUtils.PrintThreadDetails();

var sleepingThread = new Thread(ThreadUtils.SleepIndefinitely);
sleepingThread.Name = "Sleeping";
sleepingThread.Start();
Console.WriteLine("Main thread will now sleep for 2 sec");
Thread.Sleep(2000);
Console.WriteLine("Main thread just woke up");
sleepingThread.Interrupt();

