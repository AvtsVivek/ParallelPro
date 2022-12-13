namespace OrderablePartitionerDemo
{

    class Program
    {
        static void Main()
        {
            //
            // First a fairly simple visual test
            //
            var someCollection = new string[] { "four", "score", "and", "twenty", "years", "ago" };
            var someOrderablePartitioner = new SingleElementOrderablePartitioner<string>(someCollection);

            Parallel.ForEach(someOrderablePartitioner, (item, state, index) =>
            {
                Console.WriteLine("ForEach: item = {0}, index = {1}, thread id = {2}", item, index, Environment.CurrentManagedThreadId);
            });

            //
            // Now a test of static partitioning, using 2 partitions and 2 tasks
            //
            var staticPartitioner = someOrderablePartitioner.GetOrderablePartitions(2);

            // staticAction will consume the shared enumerable
            
            int partitionerListIndex = 0;

            void staticAction()
            {
                int myIndex = Interlocked.Increment(ref partitionerListIndex) - 1;
                var enumerator = staticPartitioner[myIndex];
                while (enumerator.MoveNext())
                    Console.WriteLine("Static partitioning: item = {0}, index = {1}, thread id = {2}",
                        enumerator.Current.Value, enumerator.Current.Key, Environment.CurrentManagedThreadId);
                enumerator.Dispose();
            }

            // Now launch two of them
            Parallel.Invoke(staticAction, staticAction);

            //
            // Now a more rigorous test of dynamic partitioning (used by Parallel.ForEach)
            //
            Console.WriteLine("OrderablePartitioner test: testing for index mismatches");
            var src = Enumerable.Range(0, 100000).ToList();
            var myOP = new SingleElementOrderablePartitioner<int>(src);

            int counter = 0;
            bool mismatch = false;

            Parallel.ForEach(myOP, (item, state, index) =>
            {
                if (item != index) mismatch = true;
                Interlocked.Increment(ref counter);
            });

            if (mismatch) Console.WriteLine("OrderablePartitioner Test: index mismatch detected");

            Console.WriteLine("OrderablePartitioner test: counter = {0}, should be 100000", counter);
        }
    }
}