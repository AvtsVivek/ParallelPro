
var threadInfo = PrintThreadDetails("From Main Method");

Console.WriteLine(threadInfo);

Task.Factory.StartNew(() => {

    var threadInfo = PrintThreadDetails("From Task");

    Console.WriteLine(threadInfo);

    Console.WriteLine("Hello World");
});
// wait for input before exiting
Console.WriteLine("Main method complete. Press enter to finish.");
Console.ReadLine();

string PrintThreadDetails(string contextInfo)
{
    var finalString = string.Empty;

    finalString = finalString + Environment.NewLine;

    finalString = $"The context information is: {contextInfo}";

    finalString = finalString + Environment.NewLine + $"The current thread id is {Thread.CurrentThread.ManagedThreadId}.";

    var backgroundString = Thread.CurrentThread.IsBackground ? "a background thread" : "NOT a background thread";

    finalString = finalString + Environment.NewLine + $"This thread is {backgroundString}";

    var threadPoolInfoString = Thread.CurrentThread.IsThreadPoolThread ? "a thread pool thread" : "NOT a thread pool thread";

    finalString = finalString + Environment.NewLine + $"This thread is {threadPoolInfoString}";

    finalString = finalString + Environment.NewLine;

    return finalString;
}