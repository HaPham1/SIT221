using System;
using System.Collections.Generic;
using System.Text;

namespace Vector
{
    public class InsertionSort : ISorter
    {
        public void Sort<K>(K[] sequence, IComparer<K> comparer) where K : IComparable<K>
        {
            if (comparer == null) comparer = Comparer<K>.Default;

            //implement Insertion Sort
            K temp;
            int j;
            for (int i = 1; i < sequence.Length; i++)
            {
                j = i;
                while ((j > 0) && (comparer.Compare(sequence[j - 1], sequence[j]) > 0))
                {
                    temp = sequence[j - 1];
                    sequence[j - 1] = sequence[j];
                    sequence[j] = temp;
                    j--;
                }
            }
        }
    }
}
