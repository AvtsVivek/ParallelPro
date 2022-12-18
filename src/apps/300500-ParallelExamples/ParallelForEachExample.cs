namespace ParallelExamples
{
    internal class ParallelForEachExample
    {
        internal void ExecuteParallelForEach(IList<int> numbers)
        {
            Parallel.ForEach(numbers, number =>
            {
                var threadInfo = ThreadUtils.PrintThreadDetails("ParallelForEachExample - ExecuteParallelForEach");

                var timeContainsNumber = DateTime.Now.ToLongTimeString().Contains(number.ToString());

                var stringMessage = string.Empty;
                
                if (timeContainsNumber)
                    stringMessage = $"The current time contains number {number}. Thread id: {Environment.CurrentManagedThreadId}";
                else
                    stringMessage = $"The current time does not contain number {number}. Thread id: {Environment.CurrentManagedThreadId}";

                Console.WriteLine(threadInfo + Environment.NewLine + stringMessage);
            });
        }
    }
}