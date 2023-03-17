using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncInLoopBodies
{
    internal class SyncViaTls
    {
        public double Calulate()
        {
            int[] nums = Enumerable.Range(0, 100000).ToArray();
            double total = 0;

            // Use type parameter to make subtotal a long, not an int
            Parallel.For<double>(0, nums.Length, () => 0, (j, loop, subtotal) =>
            {
                // subtotal += nums[j];
                subtotal += Math.Pow(nums[j], 2);
                return subtotal;
            },
            (x) => SyncViaTls.Add(ref total, x)
            );

            return total;
        }
        public static double Add(ref double location1, double value)
        {
            double newCurrentValue = location1; // non-volatile read, so may be stale
            while (true)
            {
                double currentValue = newCurrentValue;
                double newValue = currentValue + value;
                newCurrentValue = Interlocked.CompareExchange(ref location1, newValue, currentValue);
                if (newCurrentValue.Equals(currentValue)) // see "Update" below
                    return newValue;
            }
        }
    }
}
