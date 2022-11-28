using System;
using System.Collections.Generic;
using System.Text;

namespace Vector
{
    public class RandomizedQuickSort : ISorter
    {
        public void Sort<K>(K[] sequence, IComparer<K> comparer) where K : IComparable<K>
        {
            if (comparer == null) comparer = Comparer<K>.Default;
            QuickSort(sequence, comparer, 0, sequence.Length - 1);
        }

        public void QuickSort<K> (K[] S, IComparer<K> comparer, int a, int b)
        {
            if (a >= b) return;
            int left = a;
            int right = b - 1;

            // Create a random number and choose that as pivot
            Random rand = new Random();
            int ranNum = rand.Next(a, b);
            K pivot = S[ranNum];

            //Swap pivot and final element in array
            K temp;
            temp = S[b]; S[b] = S[ranNum]; S[ranNum] = temp;


            while (left <= right)
            {
                //scan left
                while (left <= right && comparer.Compare(S[left], pivot) < 0) left++;
                //scan right
                while (left <= right && comparer.Compare(S[right], pivot) > 0) right--;
                //left right not cross yet
                if (left <= right)
                {
                    //swap and shrink range
                    temp = S[left]; S[left] = S[right]; S[right] = temp;
                    left++; right--;
                }
            }

            //put pivot in correct position
            temp = S[left]; S[left] = S[b]; S[b] = temp;

            //Recursively calls for 2 half.
            QuickSort(S, comparer, a, left - 1);
            QuickSort(S, comparer, left + 1, b);
        }
    }
}
