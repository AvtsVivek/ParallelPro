﻿using System.Threading;

class SelectiveContinuations
{

    static void Main(string[] args)
    {

        // create the cancellation token source
        var cancellationTokenSource = new CancellationTokenSource();

        // create the cancellation token
        var cancellationToken = cancellationTokenSource.Token;

        var commandArgs = ReadCommandLineArgsAndGetReady(args);

        if (!commandArgs.Item1)
        {
            Console.WriteLine("Exiting the program...");
            return;
        }

        var firstStageTask = new Task(() => {


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


            var commandLineArg = commandArgs.Item2;

            Console.WriteLine($"From the firstStageTask ... {commandLineArg}");

            switch (commandLineArg)
            {
                case "JustRun":
                    {

                    }
                    break;
                case "ThrowException":
                    {
                        throw new Exception("Throwing exception ....");
                    }
                default:
                    {
                        Console.WriteLine($"The command line arg {commandLineArg} is not expected.");
                    }
                    break;
            }

        }, cancellationToken);

        var firstStageContinuationTaskNone = firstStageTask.ContinueWith((Task firstStageTask) => {
            Console.WriteLine("Continuation ...None");
        }, TaskContinuationOptions.None);

        var firstStageContinuationTaskPreferFairness = firstStageTask.ContinueWith((Task firstStageTask) => {
            Console.WriteLine("Continuation ...PreferFairness");
        }, TaskContinuationOptions.PreferFairness);

        var firstStageContinuationTaskLongRunning = firstStageTask.ContinueWith((Task firstStageTask) => {
            Console.WriteLine("Continuation ...LongRunning");
        }, TaskContinuationOptions.LongRunning);

        var firstStageContinuationTaskAttachedToParent = firstStageTask.ContinueWith((Task firstStageTask) => {
            Console.WriteLine("Continuation ...AttachedToParent");
        }, TaskContinuationOptions.AttachedToParent);

        var firstStageContinuationTaskDenyChildAttach = firstStageTask.ContinueWith((Task firstStageTask) => {
            Console.WriteLine("Continuation ...DenyChildAttach");
        }, TaskContinuationOptions.DenyChildAttach);

        var firstStageContinuationTaskHideScheduler = firstStageTask.ContinueWith((Task firstStageTask) => {
            Console.WriteLine("Continuation ...HideScheduler");
        }, TaskContinuationOptions.HideScheduler);

        var firstStageContinuationTaskLazyCancellation = firstStageTask.ContinueWith((Task firstStageTask) => {
            Console.WriteLine("Continuation ...LazyCancellation");
        }, TaskContinuationOptions.LazyCancellation);

        var firstStageContinuationTaskRunContinuationsAsynchronously = firstStageTask.ContinueWith((Task firstStageTask) =>
        {
            Console.WriteLine("Continuation ...RunContinuationsAsynchronously");
        }, TaskContinuationOptions.RunContinuationsAsynchronously);

        var firstStageContinuationTaskNotOnRanToCompletion = firstStageTask.ContinueWith((Task firstStageTask) => {
            Console.WriteLine("Continuation ...NotOnRanToCompletion");
        }, TaskContinuationOptions.NotOnRanToCompletion);

        var firstStageContinuationTaskNotOnFaulted = firstStageTask.ContinueWith((Task firstStageTask) => {
            Console.WriteLine("Continuation ...NotOnFaulted");
        }, TaskContinuationOptions.NotOnFaulted);

        var firstStageContinuationTaskOnlyOnCanceled = firstStageTask.ContinueWith((Task firstStageTask) => {
            Console.WriteLine("Continuation ...OnlyOnCanceled");
        }, TaskContinuationOptions.OnlyOnCanceled);

        var firstStageContinuationTaskNotOnCanceled = firstStageTask.ContinueWith((Task firstStageTask) => {
            Console.WriteLine("Continuation ...NotOnCanceled");
        }, TaskContinuationOptions.NotOnCanceled);

        var firstStageContinuationTaskOnlyOnFaulted = firstStageTask.ContinueWith((Task firstStageTask) => {
            Console.WriteLine("Continuation ...OnlyOnFaulted");
        }, TaskContinuationOptions.OnlyOnFaulted);

        var firstStageContinuationTaskOnlyOnRanToCompletion = firstStageTask.ContinueWith((Task firstStageTask) => {
            Console.WriteLine("Continuation ...OnlyOnRanToCompletion");
        }, TaskContinuationOptions.OnlyOnRanToCompletion);

        var firstStageContinuationTaskExecuteSynchronously = firstStageTask.ContinueWith((Task firstStageTask) => {
            Console.WriteLine("Continuation ...ExecuteSynchronously");
        }, TaskContinuationOptions.ExecuteSynchronously);



        try
        {
            // start the task
            firstStageTask.Start();

            // Wait for all of the tasks to be completed.
            Task.WaitAll(firstStageTask);
        }
        catch (AggregateException exception)
        {
            Console.WriteLine(exception.Message);
            Console.WriteLine("Exception cought...");
            return; 
        }

        Console.WriteLine("Done ... ");
    }

    private static Tuple<bool, string> ReadCommandLineArgsAndGetReady(string[] args)
    {
        var returnObject = new Tuple<bool, string>(false, string.Empty);

        if (args.Length != 1)
        {
            Console.WriteLine("There should be only one command line arg.");
            return returnObject;
        }

        if (string.IsNullOrWhiteSpace(args[0]))
        {
            Console.WriteLine($"The command line arg {args[0]} is not expected.");
            return returnObject;
        }

        returnObject = new Tuple<bool, string>(true, args[0]);

        return returnObject;
    }
}