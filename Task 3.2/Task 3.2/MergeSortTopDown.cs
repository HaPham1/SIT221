using System;
using System.Collections.Generic;
using System.Text;

namespace Vector
{
    public class MergeSortTopDown : ISorter
    {
        public void Sort<K>(K[] sequence, IComparer<K> comparer) where K : IComparable<K>
        {
            if (comparer == null) comparer = Comparer<K>.Default;
            mergeSort(sequence, comparer);
        }

        //Merge content of 2 array into 1 array with desired length
        public void Merge<K> (K[] S1, K[]S2, K[] S, IComparer<K> comp)
        {
            int i = 0;
            int j = 0;
            while (i + j < S.Length)
            {
                if (j == S2.Length || (i < S1.Length && comp.Compare(S1[i], S2[j]) < 0))
                {
                    S[i + j] = S1[i++]; //Copy element of S1 while incrementing i
                }
                else
                {
                    S[i + j] = S2[j++]; //Copy element of S2 while incrementing j
                }
            }
        }

        public void mergeSort<K>(K[] S, IComparer<K> comp)
        {
            int n = S.Length;
            if (n < 2) return;

            //get the middle
            int mid = n / 2;

            //K[] S1 = CopyOfRange(S, 0, mid);
            //K[] S2 = CopyOfRange(S, mid, n);

            K[] S1 = new K[mid];
            K[] S2 = new K[n - mid];
            Array.Copy(S, 0, S1, 0, mid);       //Copy first half
            Array.Copy(S, mid, S2, 0, n - mid); //Cope 2nd half

            //Sort 2 half then merge
            mergeSort(S1, comp);
            mergeSort(S2, comp);
            Merge(S1, S2, S, comp);
        }

        //public K[] CopyOfRange<K> (K[] data, int start, int end)
        //{
        //    int length = end - start;
        //    K[] result = new K[length];
        //    Array.Copy(data, start, result, 0, length);
        //    return result;
        //}
    }
}
