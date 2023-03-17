using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncInLoopBodies
{
    internal class SyncViaLock
    {
        public double Calulate()
        {
            // create the shared data value
            double total = 0;
            // create a lock object
            object lockObj = new object();
            // perform a parallel loop
            Parallel.For(0, 100000, item => {
                // get the lock on the shared value
                lock (lockObj)
                {
                    // add the square of the current
                    // value to the running total
                    total += Math.Pow(item, 2);
                }
            });

            return total;
        }
    }
}
