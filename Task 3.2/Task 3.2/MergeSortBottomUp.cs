using System;
using System.Collections.Generic;
using System.Text;

namespace Vector
{
    public class MergeSortBottomUp : ISorter
    {
        public void Sort<K>(K[] sequence, IComparer<K> comparer) where K : IComparable<K>
        {
            if (comparer == null) comparer = Comparer<K>.Default;
            mergeSortBottomUp(sequence, comparer);
        }

        public void Merge<K>(K[] input, K[] output, IComparer<K> comp, int start, int inc)
        {
            int end1 = Math.Min(start + inc, input.Length);             //last of 1
            int end2 = Math.Min(start + 2 * inc, input.Length);         //last of 2
            int x = start;                                              //1st of 1
            int y = start + inc;                                        //1st of 2
            int z = start;                                              //1st of output

            //Go through both list and put into next output through compare.
            while (x < end1 && y < end2)
                if (comp.Compare(input[x], input[y]) < 0)
                {
                    output[z++] = input[x++];
                }
                else
                {
                    output[z++] = input[y++];
                }

            //copy remaining
            if (x < end1) Array.Copy(input, x, output, z, end1 - x);
            else if (y < end2) Array.Copy(input, y, output, z, end2 - y);
        }

        public void mergeSortBottomUp<K> (K[] sequence, IComparer<K> comparer)
        {
            int n = sequence.Length;
            K[] src = sequence;
            K[] dest = new K[n];
            K[] temp;
            
            //merge sort
            for (int i = 1; i < n; i *= 2)
            {
                for (int j = 0; j < n; j += 2 * i)
                {
                    Merge(src, dest, comparer, j, i);
                }
                temp = src; src = dest; dest = temp;
            }
            if (sequence != src)
            {
                Array.Copy(src, 0, sequence, 0, n); //additional copy to reset to original
            }
        }
    }
}
