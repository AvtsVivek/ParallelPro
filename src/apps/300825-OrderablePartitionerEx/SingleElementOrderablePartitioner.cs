﻿using System.Collections;
using System.Collections.Concurrent;

namespace OrderablePartitionerDemo
{
    // Simple partitioner that will extract one (index,item) pair at a time,
    // in a thread-safe fashion, from the underlying collection.
    class SingleElementOrderablePartitioner<T> : OrderablePartitioner<T>
    {
        // The collection being wrapped by this Partitioner
        private readonly IEnumerable<T> m_referenceEnumerable;

        // Class used to wrap m_index for the purpose of sharing access to it
        // between an InternalEnumerable and multiple InternalEnumerators
        private class Shared<U>
        {
            internal U Value;

            public Shared(U item)
            {
                Value = item;
            }
        }

        // Internal class that serves as a shared enumerable for the
        // underlying collection.
        private class InternalEnumerable : IEnumerable<KeyValuePair<long, T>>, IDisposable
        {
            private readonly IEnumerator<T> m_reader;
            private bool m_disposed = false;
            private readonly Shared<long> m_index = null!;

            // These two are used to implement Dispose() when static partitioning is being performed
            int m_activeEnumerators;
            private readonly bool m_downcountEnumerators;

            // "downcountEnumerators" will be true for static partitioning, false for
            // dynamic partitioning.
            public InternalEnumerable(IEnumerator<T> reader, bool downcountEnumerators)
            {
                m_reader = reader;
                m_index = new Shared<long>(0);
                m_activeEnumerators = 0;
                m_downcountEnumerators = downcountEnumerators;
            }

            public IEnumerator<KeyValuePair<long, T>> GetEnumerator()
            {
                if (m_disposed)
                    throw new ObjectDisposedException("InternalEnumerable: Can't call GetEnumerator() after disposing");

                // For static partitioning, keep track of the number of active enumerators.
                if (m_downcountEnumerators) Interlocked.Increment(ref m_activeEnumerators);

                return new InternalEnumerator(m_reader, this, m_index);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable<KeyValuePair<long, T>>)this).GetEnumerator();
            }

            public void Dispose()
            {
                if (!m_disposed)
                {
                    // Only dispose the source enumerator if you are doing dynamic partitioning
                    if (!m_downcountEnumerators)
                    {
                        m_reader.Dispose();
                    }
                    m_disposed = true;
                }
            }

            // Called from Dispose() method of spawned InternalEnumerator.  During
            // static partitioning, the source enumerator will be automatically
            // disposed once all requested InternalEnumerators have been disposed.
            public void DisposeEnumerator()
            {
                if (m_downcountEnumerators)
                {
                    if (Interlocked.Decrement(ref m_activeEnumerators) == 0)
                    {
                        m_reader.Dispose();
                    }
                }
            }
        }

        // Internal class that serves as a shared enumerator for
        // the underlying collection.
        private class InternalEnumerator : IEnumerator<KeyValuePair<long, T>>
        {
            private KeyValuePair<long, T> m_current;
            private readonly IEnumerator<T> m_source;
            private readonly InternalEnumerable m_controllingEnumerable;
            private readonly Shared<long> m_index = null!;
            bool m_disposed = false;

            public InternalEnumerator(IEnumerator<T> source, InternalEnumerable controllingEnumerable, Shared<long> index)
            {
                m_source = source;
                // m_current = default(KeyValuePair<long, T>);
                m_current = default;
                m_controllingEnumerable = controllingEnumerable;
                m_index = index;
            }

            object IEnumerator.Current
            {
                get { return m_current; }
            }

            KeyValuePair<long, T> IEnumerator<KeyValuePair<long, T>>.Current
            {
                get { return m_current; }
            }

            void IEnumerator.Reset()
            {
                throw new NotSupportedException("Reset() not supported");
            }

            // This method is the crux of this class.  Under lock, it calls
            // MoveNext() on the underlying enumerator, grabs Current and index,
            // and increments the index.
            bool IEnumerator.MoveNext()
            {
                bool rval = false;
                lock (m_source)
                {
                    rval = m_source.MoveNext();
                    if (rval)
                    {
                        m_current = new KeyValuePair<long, T>(m_index.Value, m_source.Current);
                        m_index.Value++;
                    }
                    else
                    {
                        //m_current = default(KeyValuePair<long, T>);
                    }
                }
                return rval;
            }

            void IDisposable.Dispose()
            {
                if (!m_disposed)
                {
                    // Delegate to parent enumerable's DisposeEnumerator() method
                    m_controllingEnumerable.DisposeEnumerator();
                    m_disposed = true;
                }
            }
        }

        // Constructor just grabs the collection to wrap
        public SingleElementOrderablePartitioner(IEnumerable<T> enumerable)
            : base(true, true, true)
        {
            // Verify that the source IEnumerable is not null
            //if (enumerable == null)
            //    throw new ArgumentNullException("enumerable");

            // m_referenceEnumerable = enumerable;

            m_referenceEnumerable = enumerable ?? throw new ArgumentNullException(nameof(enumerable));
        }

        // Produces a list of "numPartitions" IEnumerators that can each be
        // used to traverse the underlying collection in a thread-safe manner.
        // This will return a static number of enumerators, as opposed to
        // GetOrderableDynamicPartitions(), the result of which can be used to produce
        // any number of enumerators.
        public override IList<IEnumerator<KeyValuePair<long, T>>> GetOrderablePartitions(int numPartitions)
        {
            if (numPartitions < 1)
                throw new ArgumentOutOfRangeException(nameof(numPartitions));

            var list = new List<IEnumerator<KeyValuePair<long, T>>>(numPartitions);

            // Since we are doing static partitioning, create an InternalEnumerable with reference
            // counting of spawned InternalEnumerators turned on.  Once all of the spawned enumerators
            // are disposed, dynamicPartitions will be disposed.
            var dynamicPartitions = new InternalEnumerable(m_referenceEnumerable.GetEnumerator(), true);
            for (int i = 0; i < numPartitions; i++)
                list.Add(dynamicPartitions.GetEnumerator());

            return list;
        }

        // Returns an instance of our internal Enumerable class.  GetEnumerator()
        // can then be called on that (multiple times) to produce shared enumerators.
        public override IEnumerable<KeyValuePair<long, T>> GetOrderableDynamicPartitions()
        {
            // Since we are doing dynamic partitioning, create an InternalEnumerable with reference
            // counting of spawned InternalEnumerators turned off.  This returned InternalEnumerable
            // will need to be explicitly disposed.
            return new InternalEnumerable(m_referenceEnumerable.GetEnumerator(), false);
        }

        // Must be set to true if GetDynamicPartitions() is supported.
        public override bool SupportsDynamicPartitions
        {
            get { return true; }
        }
    }
}