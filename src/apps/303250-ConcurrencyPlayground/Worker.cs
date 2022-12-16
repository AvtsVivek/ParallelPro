using System.Threading;

namespace ConcurrencyPlayground
{
    public class Worker
    {
        public void DoWork(int numLoops)
        {
            while (numLoops >= 0)
            {
                numLoops--;
            }
        }
    }
}
