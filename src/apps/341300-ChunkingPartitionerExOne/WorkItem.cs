namespace ChunkingPartitionerExOne
{
    class WorkItem
    {
        public int WorkDuration
        {
            get;
            set;
        }

        public void PerformWork()
        {
            // simulate work by sleeping
            Thread.Sleep(WorkDuration);
        }
    }
}
