using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search_and_SortDataAnalyser.Sorting_Algorithms
{
    internal class Bubble_Sort
    {
        public int lastSortCount = 0;
        public int lastSortCountD = 0;

        public int[] arrayClone;

        public void ClearValues()
        {
            lastSortCount = 0;
            lastSortCountD = 0;
        }

        public int[] BubbleSortArray(int[] array, string order)
        {
            arrayClone = array;

            Console.WriteLine(order + "   lsc: " + lastSortCount + "   lscD: " + lastSortCountD);

            for (int i = 0; i < arrayClone.Length - 1; i++)  // For each element in the array
            {
                for (int j = 0; j < (arrayClone.Length - 1) - i; j++)  // When j is < array length - current element index
                {
                    if (order == "A")
                    {
                        if (arrayClone[j + 1] < arrayClone[j])  // If the element in front of the element at index j in the array is SMALLER (then swap)
                        {
                            lastSortCount++;  // Iterate the counter

                            int buffer = arrayClone[j];

                            arrayClone[j] = arrayClone[j + 1];
                            arrayClone[j + 1] = buffer;
                        }
                    }
                    else if (order == "D")
                    {
                        if (arrayClone[j + 1] > arrayClone[j])  // If the element in front of the element at index j in the array is LARGER (then swap)
                        {
                            lastSortCountD++;  // Iterate the counter

                            int buffer = arrayClone[j];

                            arrayClone[j] = arrayClone[j + 1];
                            arrayClone[j + 1] = buffer;
                        }
                    }
                }
            }

            Console.WriteLine(order + "   lsc: " + lastSortCount + "   lscD: " + lastSortCountD);

            return arrayClone;
        }
    }
}
