using System.Collections.Concurrent;
using System.Collections;

namespace ProducerConsumerCollection;

public class SafeStack<T> : IProducerConsumerCollection<T>
{
    // Used for enforcing thread-safety
    private object m_lockObject = new object();

    // We'll use a regular old Stack for our core operations
    private Stack<T> m_sequentialStack = null!;

    //
    // Constructors
    //
    public SafeStack()
    {
        m_sequentialStack = new Stack<T>();
    }

    public SafeStack(IEnumerable<T> collection)
    {
        m_sequentialStack = new Stack<T>(collection);
    }

    //
    // Safe Push/Pop support
    //
    public void Push(T item)
    {
        lock (m_lockObject) m_sequentialStack.Push(item);
    }

    public bool TryPop(out T item)
    {
        bool rval = true;
        lock (m_lockObject)
        {
            if (m_sequentialStack.Count == 0) { item = default!; rval = false; }
            else
            {
                item = m_sequentialStack.Pop();
            }
        }
        return rval;
    }

    //
    // IProducerConsumerCollection(T) support
    //
    public bool TryTake(out T item)
    {
        return TryPop(out item);
    }

    public bool TryAdd(T item)
    {
        Push(item);
        return true; // Push doesn't fail
    }

    public T[] ToArray()
    {
        T[] rval = null!;
        lock (m_lockObject) rval = m_sequentialStack.ToArray();
        return rval;
    }

    public void CopyTo(T[] array, int index)
    {
        lock (m_lockObject) m_sequentialStack.CopyTo(array, index);
    }

    //
    // Support for IEnumerable(T)
    //
    public IEnumerator<T> GetEnumerator()
    {
        // The performance here will be unfortunate for large stacks,
        // but thread-safety is effectively implemented.
        Stack<T> stackCopy = null!;
        lock (m_lockObject) stackCopy = new Stack<T>(m_sequentialStack);
        return stackCopy.GetEnumerator();
    }

    //
    // Support for IEnumerable
    //
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable<T>)this).GetEnumerator();
    }

    //
    // Support for ICollection
    //
    public bool IsSynchronized
    {
        get { return true; }
    }

    public object SyncRoot
    {
        get { return m_lockObject; }
    }

    public int Count
    {
        get { return m_sequentialStack.Count; }
    }

    public void CopyTo(Array array, int index)
    {
        lock (m_lockObject) ((ICollection)m_sequentialStack).CopyTo(array, index);
    }
}