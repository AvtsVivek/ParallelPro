namespace ParallelExamples
{
    internal class ParallelInvokeExample
    {
        internal void DoWorkInParallel()
        {
            Parallel.Invoke(
                DoComplexWork,
                () => {
                    var localMessage = $"Hello from lambda expression. Thread id: {Environment.CurrentManagedThreadId}";
                    var threadInfo = ThreadUtils.PrintThreadDetails("ParallelInvokeExample - lambda expression");
                    var finalMessage = threadInfo + Environment.NewLine + localMessage;
                    finalMessage = finalMessage + Environment.NewLine;
                    Console.WriteLine(finalMessage);
                },
                new Action(() =>
                {
                    var localMessage = $"Hello from Action. Thread id: {Environment.CurrentManagedThreadId}";
                    var threadInfo = ThreadUtils.PrintThreadDetails("ParallelInvokeExample - Action");
                    var finalMessage = threadInfo + Environment.NewLine + localMessage;
                    finalMessage = finalMessage + Environment.NewLine;
                    Console.WriteLine(finalMessage);
                }),
                delegate ()
                {
                    var localMessage = $"Hello from delegate. Thread id: {Environment.CurrentManagedThreadId}";
                    var threadInfo = ThreadUtils.PrintThreadDetails("ParallelInvokeExample - Delegate");
                    var finalMessage = threadInfo + Environment.NewLine + localMessage;
                    finalMessage = finalMessage + Environment.NewLine;
                    Console.WriteLine(finalMessage);
                }
            );
        }

        private void DoComplexWork()
        {
            var localMessage = $"Hello from DoComplexWork method. Thread id: {Environment.CurrentManagedThreadId}";
            var threadInfo = ThreadUtils.PrintThreadDetails("ParallelInvokeExample - DoComplexWork");
            var finalMessage = threadInfo + Environment.NewLine + localMessage;
            finalMessage = finalMessage + Environment.NewLine;
            Console.WriteLine(finalMessage);
        }
    }
}