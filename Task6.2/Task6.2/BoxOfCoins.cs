using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;

namespace BoxOfCoins
{
    public class BoxOfCoins
    {

        public static int Solve(int[] boxes)
        {
            //get sum
            int sum = 0;
            for(int i = 0; i < boxes.Length; i++)
            {
                sum = sum + boxes[i];
            }

            //get alex max value by going first
            int maxAlex = Solver(boxes, boxes.Length);

            //decrease to get max cindy can get
            int maxCindy = sum - maxAlex;

            return maxAlex - maxCindy;
        }
        
        public static int Solver(int[] boxes, int n)
        {
            //Create table of dimension N*N to store values.
            int[ , ] table = new int[n,n];

            // loop for length of the subarray, go from 1 to N
            for (int len = 1; len <= n; len++)
            {
                // loop for i, j
                for (int i = 0; i <= n - len; i++)
                {
                    int j = i + len - 1;
                    int a, b, c;

                    // Opponent picks jth coin.
                    if (i + 1 < n && j - 1 >= 0)
                    {
                        a = table[i + 1, j - 1];
                    }
                    else
                    {
                        a = 0;
                    }

                    // Opponent picks (i+1)th coin.
                    if (i + 2 < n)
                    {
                        b = table[i + 2, j];
                    }
                    else
                    {
                        b = 0;
                    }

                    // Opponent picks (j-1)th coin.
                    if (j - 2 >= 0)
                    {
                        c = table[i, j - 2];
                    }
                    else
                    {
                        c = 0;
                    }

                    //Fill array with values depend on maximum value whether you pick i or j coin.
                    table[i, j] = Math.Max((boxes[i] + Math.Min(a, b)), (boxes[j] + Math.Min(a, c)));
                }
            }
            //Return max value in this place
            return table[0, n - 1];

        }
    }

}