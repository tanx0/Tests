using System;
using System.Linq;

namespace ReactAppTest
{
    public class Utils
    {
        /// <summary>
        /// index of the array where the sum of integers at the index on the left is equal to the sum of integers on the right
        /// Assumption: length of the array is between 0 and 10000
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static int? FindMiddleIndex(int[] array)
        {
            if (array.Length <= 1)
                return null;
            for (int i = 1; i < array.Length - 1; i++)
            {
                if (IsMiddle(array, i))
                    return i;
            }
            return null;
        }

        private static bool IsMiddle(int[] array, int index)
        {
            var length = array.Length;
            var leftSegment = new ArraySegment<int>(array, 0, index);
            var rightSegment = new ArraySegment<int>(array, index + 1, length - index - 1);
            return leftSegment.Sum() == rightSegment.Sum();
        }

    }
}
