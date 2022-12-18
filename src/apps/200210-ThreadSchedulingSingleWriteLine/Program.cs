Console.WriteLine("Hello, World!");

var bgThreadOne = new Thread(MethodOne);
// The following is to set the thread as background, but its not necessary for this example.
// bgThreadOne.IsBackground = true;
bgThreadOne.Start();

var bgThreadTwo = new Thread(MethodTwo);
bgThreadTwo.Start();

Thread.Sleep(2000);
Console.WriteLine("Exiting .... ");


void MethodOne()
{
    var localMessage = string.Empty;
    localMessage = localMessage + Environment.NewLine + "Method 1 - line 1";
    Thread.Sleep(100);
    localMessage = localMessage + Environment.NewLine + "Method 1 - line 2";
    Thread.Sleep(100);
    localMessage = localMessage + Environment.NewLine + "Method 1 - line 3";
    Thread.Sleep(100);
    localMessage = localMessage + Environment.NewLine + "Method 1 - line 4";
    Thread.Sleep(100);
    localMessage = localMessage + Environment.NewLine + "Method 1 - line 5";
    Thread.Sleep(100);
    localMessage = localMessage + Environment.NewLine + "Method 1 - line 6";
    Thread.Sleep(100);
    localMessage = localMessage + Environment.NewLine + "Method 1 - line 7";
    Thread.Sleep(100);
    localMessage = localMessage + Environment.NewLine + "Method 1 - line 8";
    Thread.Sleep(100);
    localMessage = localMessage + Environment.NewLine + "Method 1 - line 9";
    Thread.Sleep(100);
    localMessage = localMessage + Environment.NewLine + "Method 1 - line 10";
    Thread.Sleep(100);
    localMessage = localMessage + Environment.NewLine + "Method 1 - line 11";
    Thread.Sleep(100);
    localMessage = localMessage + Environment.NewLine + "Method 1 - line 12";
    Thread.Sleep(100);
    Console.WriteLine(localMessage);
}

void MethodTwo()
{
    var localMessage = string.Empty;
    localMessage = localMessage + Environment.NewLine + "Method 2 - line 1";
    Thread.Sleep(100);
    localMessage = localMessage + Environment.NewLine + "Method 2 - line 2";
    Thread.Sleep(100);
    localMessage = localMessage + Environment.NewLine + "Method 2 - line 3";
    Thread.Sleep(100);
    localMessage = localMessage + Environment.NewLine + "Method 2 - line 4";
    Thread.Sleep(100);
    localMessage = localMessage + Environment.NewLine + "Method 2 - line 5";
    Thread.Sleep(100);
    localMessage = localMessage + Environment.NewLine + "Method 2 - line 6";
    Thread.Sleep(100);
    localMessage = localMessage + Environment.NewLine + "Method 2 - line 7";
    Thread.Sleep(100);
    localMessage = localMessage + Environment.NewLine + "Method 2 - line 8";
    Thread.Sleep(100);
    localMessage = localMessage + Environment.NewLine + "Method 2 - line 9";
    Thread.Sleep(100);
    localMessage = localMessage + Environment.NewLine + "Method 2 - line 10";
    Thread.Sleep(100);
    localMessage = localMessage + Environment.NewLine + "Method 2 - line 11";
    Thread.Sleep(100);
    localMessage = localMessage + Environment.NewLine + "Method 2 - line 12";
    Thread.Sleep(100);
    Console.WriteLine(localMessage);
}