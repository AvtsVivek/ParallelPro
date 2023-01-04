using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChannelsCustomReadWrite
{
    public interface IRead<T>
    {
        Task<T> ReadAsync(); // This is an async operation
        bool IsComplete();
    }
    public interface IWrite<T>
    {
        void Push(T item); // This is a sync operation
        void Complete();
    }
    public class CustomChannel<T> : IRead<T>, IWrite<T>
    {
        private bool _isComplete;
        private ConcurrentQueue<T> _queue = new ConcurrentQueue<T>();
        private SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(0);

        public void Push(T item)
        {
            _queue.Enqueue(item);
            _semaphoreSlim.Release();
        }

        public async Task<T> ReadAsync()
        {
            await _semaphoreSlim.WaitAsync();

            if (_queue.TryDequeue(out var item))
                return item;
            else
                return default(T)!;
        }
        public void Complete()
        {
            _isComplete = true;
            // Just in case,
            // Sometimes, ReadAsync is still waiting, while producers are done pushing.
            // when its complete, just do a release.
            // If we dont have the release here, its someitmes waits adn gets stuck.
            _semaphoreSlim.Release();
        }

        public bool IsComplete()
        {
            return _isComplete && _queue.IsEmpty;
        }
    }
}
