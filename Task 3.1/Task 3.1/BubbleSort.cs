using System;
using System.Collections.Generic;
using System.Text;

namespace Vector
{
    public class BubbleSort : ISorter
    {
        public void Sort<K>(K[] sequence, IComparer<K> comparer) where K : IComparable<K>
        {
            if (comparer == null) comparer = Comparer<K>.Default;

            //Implement bubble sort
            K temp;
            for (int j = 0; j <= sequence.Length - 2; j++)
            {
                int swap = 0;
                for (int i = 0; i <= sequence.Length - 2; i++)
                {
                    if (comparer.Compare(sequence[i], sequence[i + 1]) > 0)
                    {
                        temp = sequence[i + 1];
                        sequence[i + 1] = sequence[i];
                        sequence[i] = temp;
                        swap++;
                    }
                }
                if (swap == 0)
                {
                    break;
                }
            }
        }
    }
}
