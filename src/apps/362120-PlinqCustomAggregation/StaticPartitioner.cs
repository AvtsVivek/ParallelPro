using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StaticPartitioner<T> : Partitioner<T>
{
    private T[] Data;

    public StaticPartitioner(T[] data)
    {
        Data = data;
    }

    public override bool SupportsDynamicPartitions
    {
        get
        {
            return false;
        }
    }

    public override IList<IEnumerator<T>> GetPartitions(int partitionCount)
    {
        // create the list to hold the enumerators
        var list = new List<IEnumerator<T>>();
        // determine how many items per enumerator
        int itemsPerEnum = Data.Length / partitionCount;
        // process each but the last partition
        for (int i = 0; i < partitionCount - 1; i++)
        {
            list.Add(CreateEnum(i * itemsPerEnum, (i + 1) * itemsPerEnum));
        }
        
        // handle the last, potentially irregularly sized, partition
        list.Add(CreateEnum((partitionCount - 1) * itemsPerEnum, Data.Length));

        // return the list as the result
        return list;
    }

    private IEnumerator<T> CreateEnum(int startIndex, int endIndex)
    {
        int index = startIndex;
        while (index < endIndex)
        {
            yield return Data[index++];
        }
    }
}

