using System;
using System.Collections.Generic;
using System.Text;

namespace Vector
{
    public class SelectionSort : ISorter
    {
        public void Sort<K>(K[] sequence, IComparer<K> comparer) where K : IComparable<K>
        {
            if (comparer == null) comparer = Comparer<K>.Default;

            //implement Selection Sort
            K temp;
            for (int i = 0; i < sequence.Length; i++)
            {
                int min = i;
                for (int j = i + 1; j < sequence.Length; j++)
                {
                    if (comparer.Compare(sequence[j], sequence[min]) < 0)
                    {
                        min = j;
                    }
                }
                if (min != i)
                {
                    temp = sequence[min];
                    sequence[min] = sequence[i];
                    sequence[i] = temp;
                }
            }
        }
    }
}
