class SelectiveContinuationsCancelTask
{

    static void Main(string[] args)
    {

        // create the cancellation token source
        var cancellationTokenSource = new CancellationTokenSource();

        // create the cancellation token
        var cancellationToken = cancellationTokenSource.Token;

        var firstStageTask = new Task(() =>
        {

            for (int i = 0; i < int.MaxValue; i++)
            {
                Thread.Sleep(100);
                if (cancellationToken.IsCancellationRequested)
                {
                    Console.WriteLine("Task cancel detected");
                    throw new OperationCanceledException(cancellationToken);
                }
                else
                    Console.WriteLine("Int value {0}", i);

            }

        }, cancellationToken);

        var firstStageContinuationTaskNone = firstStageTask.ContinueWith((firstStageTask) =>
        {
            Console.WriteLine("Continuation ...None");
        }, TaskContinuationOptions.None);

        var firstStageContinuationTaskPreferFairness = firstStageTask.ContinueWith((firstStageTask) =>
        {
            Console.WriteLine("Continuation ...PreferFairness");
        }, TaskContinuationOptions.PreferFairness);

        var firstStageContinuationTaskLongRunning = firstStageTask.ContinueWith((firstStageTask) =>
        {
            Console.WriteLine("Continuation ...LongRunning");
        }, TaskContinuationOptions.LongRunning);

        var firstStageContinuationTaskAttachedToParent = firstStageTask.ContinueWith((firstStageTask) =>
        {
            Console.WriteLine("Continuation ...AttachedToParent");
        }, TaskContinuationOptions.AttachedToParent);

        var firstStageContinuationTaskDenyChildAttach = firstStageTask.ContinueWith((firstStageTask) =>
        {
            Console.WriteLine("Continuation ...DenyChildAttach");
        }, TaskContinuationOptions.DenyChildAttach);

        var firstStageContinuationTaskHideScheduler = firstStageTask.ContinueWith((firstStageTask) =>
        {
            Console.WriteLine("Continuation ...HideScheduler");
        }, TaskContinuationOptions.HideScheduler);

        var firstStageContinuationTaskLazyCancellation = firstStageTask.ContinueWith((firstStageTask) =>
        {
            Console.WriteLine("Continuation ...LazyCancellation");
        }, TaskContinuationOptions.LazyCancellation);

        var firstStageContinuationTaskRunContinuationsAsynchronously = firstStageTask.ContinueWith((firstStageTask) =>
        {
            Console.WriteLine("Continuation ...RunContinuationsAsynchronously");
        }, TaskContinuationOptions.RunContinuationsAsynchronously);

        var firstStageContinuationTaskNotOnRanToCompletion = firstStageTask.ContinueWith((firstStageTask) =>
        {
            Console.WriteLine("Continuation ...NotOnRanToCompletion");
        }, TaskContinuationOptions.NotOnRanToCompletion);

        var firstStageContinuationTaskNotOnFaulted = firstStageTask.ContinueWith((firstStageTask) =>
        {
            Console.WriteLine("Continuation ...NotOnFaulted");
        }, TaskContinuationOptions.NotOnFaulted);

        var firstStageContinuationTaskOnlyOnCanceled = firstStageTask.ContinueWith((firstStageTask) =>
        {
            Console.WriteLine("Continuation ...OnlyOnCanceled");
        }, TaskContinuationOptions.OnlyOnCanceled);

        var firstStageContinuationTaskNotOnCanceled = firstStageTask.ContinueWith((firstStageTask) =>
        {
            Console.WriteLine("Continuation ...NotOnCanceled"); // This is not executed.
        }, TaskContinuationOptions.NotOnCanceled);

        var firstStageContinuationTaskOnlyOnFaulted = firstStageTask.ContinueWith((firstStageTask) =>
        {
            Console.WriteLine("Continuation ...OnlyOnFaulted");
        }, TaskContinuationOptions.OnlyOnFaulted);

        var firstStageContinuationTaskOnlyOnRanToCompletion = firstStageTask.ContinueWith((firstStageTask) =>
        {
            Console.WriteLine("Continuation ...OnlyOnRanToCompletion"); // This is not executed.
        }, TaskContinuationOptions.OnlyOnRanToCompletion);

        var firstStageContinuationTaskExecuteSynchronously = firstStageTask.ContinueWith((firstStageTask) =>
        {
            Console.WriteLine("Continuation ...ExecuteSynchronously");
        }, TaskContinuationOptions.ExecuteSynchronously);



        try
        {
            // start the task
            firstStageTask.Start();

            // read a line from the console.
            Console.ReadLine();

            // cancel the task
            Console.WriteLine("Cancelling task");

            cancellationTokenSource.Cancel();

            Task.WaitAll(firstStageTask);

            // The following line is not getting executed. 
            // Thats probably because the exception is being raised at the previous step. 
            // So to know the status, we need to use the finally block. 
            Console.WriteLine($"Task status in the try block, main thread ... {firstStageTask.Status}");
        }
        catch (AggregateException exception)
        {
            Console.WriteLine(exception.Message);
            Console.WriteLine("Exception cought...");

            // enumerate the exceptions that have been aggregated
            foreach (Exception inner in exception.InnerExceptions)
            {
                Console.WriteLine("Exception type {0} from {1}",
                    inner.GetType(), inner.Source);
            }

            return;
        }
        finally
        {
            Console.WriteLine($"Task status in the finally block, main thread ... {firstStageTask.Status}");
        }

        Console.WriteLine($"Task status after the try catch finally block, main thread ... {firstStageTask.Status}");

        Console.WriteLine("Done ... ");
    }
}