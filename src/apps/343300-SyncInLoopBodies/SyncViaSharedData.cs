using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncInLoopBodies
{
    internal class SyncViaSharedData
    {
        public double Calulate()
        {
            // create the shared data value
            double total = 0;
            // perform a parallel loop
            Parallel.For(
            0,
            100000,
            item => {
                // add the square of the current
                // value to the running total
                total += Math.Pow(item, 2);
            });
            return total;
        }
    }
}
