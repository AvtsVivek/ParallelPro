namespace ParallelExamples
{
    internal class ParallelInvokeExample
    {
        internal void DoWorkInParallel()
        {
            Parallel.Invoke(
                DoComplexWork,
                () => {
                    Console.WriteLine($"Hello from lambda expression. Thread id: {Environment.CurrentManagedThreadId}");
                },
                new Action(() =>
                {
                    Console.WriteLine($"Hello from Action. Thread id: {Environment.CurrentManagedThreadId}");
                }),
                delegate ()
                {
                    Console.WriteLine($"Hello from delegate. Thread id: {Environment.CurrentManagedThreadId}");
                }
            );
        }

        private void DoComplexWork()
        {
            Console.WriteLine($"Hello from DoComplexWork method. Thread id: {Environment.CurrentManagedThreadId}");
        }
    }
}