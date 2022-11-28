using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;

namespace CoinRepresentation
{
	/// <summary>
	/// Class <c>Coin Representation</c> contains a Dictionary
	/// and a method for solving the problem of representing a value Z,
	/// a long integer between 1 and 10^18 using special coins of value 
	/// 2^0, 2^1, ..., 2^k, with each value having exactly 2 coins;
	/// </summary>
	/// <example> For example:
	/// Input: Z = 1, return 1 as only possible combination is {1}.
	/// Input Z = 2, return 2 as possible combinations are {1,1}, {2}.
	/// Input Z = 4, return 3 as possible combinations are {1,1,2}, {2,2}, {4}.
	/// </example>
    public class CoinRepresentation
    {
		/// <summary>
		/// This Dictionary <c>my_dict</c> is created to store
		/// key-value pairs generated within the method; <see cref="Solve"/>
		/// </summary>
		private static Dictionary<long, long> my_dict = new Dictionary<long,long>();

		/// <summary>
		/// This method give the total number of unique representations for value <paramref name="sum"/> using the coins provided.
		/// </summary>
		/// <remarks>
		/// It's a Dynamic Programming approach utilizing Recursion and Memoization using Dictionary.
		/// Larger value is computed using smaller ones and stored in Dictionary for later uses.
		/// Value can be return directly from Dictionary if previously computed.
		/// Sum is restricted within the range of 1 and 10^18, however sum = 0 is also put down as base case to compute sum = 2
		/// 3 Main Cases:
		/// Base case return 1 for <paramref name="sum"/> with value 0 or 1;
		/// For even value of <paramref name="sum"/> we add number of combinations for sum/2 and sum/2 - 1;
		/// For odd value of <paramref name="sum"/> we have same number of combinations as (sum - 1) / 2;
		/// <example> For example:
		/// sum = 0 result in 1;
		/// sum = 1 result in 1;
		/// sum = 2 result in 1 + 1 = 2 ( equal to case 0 + 1);
		/// sum = 3 result in 1 (same as 1);
		/// </example>
		/// </remarks>
		/// <param name="sum"> is the sum target value</param>
		/// <returns>
		/// A long integer number that present the total number of unique combinations of <paramref name="sum"/>
		/// using the special coins.
		/// </returns>
		/// <seealso href="https://www.youtube.com/watch?v=OQ5jsbhAv_M&list=PLZES21J5RvsHOeSW9Vrvo0EEc2juNe3tX&index=2"/>
		public static long Solve(long sum)
		{
			// Base cases
			if (sum == 0 || sum == 1)
			{
				return 1;
			}
			// If already recorded in dictionary, just return the value with specified key
			if (my_dict.ContainsKey(sum))
			{
				return my_dict[sum];
			}
			// If not, add it to the dictionary and return
			else
            {
				// Handle case of even number
				if (sum % 2 is 0)
				{
					my_dict.Add(sum, Solve(sum / 2) + Solve(sum / 2 - 1));
				}
				// Handle case of odd number
				else
				{
					my_dict.Add(sum, Solve((sum - 1) / 2));
				}
				return my_dict[sum];
			}


		}

	}
}