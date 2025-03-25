using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search_and_SortDataAnalyser.Sorting_Algorithms
{
    internal class Bubble_Sort
    {
        public int count_A = 0;
        public int count_D = 0;

        private int[] arrayClone;

        public Bubble_Sort() { ClearValues(); }

        private void ClearValues()
        {
            count_A = 0;
            count_D = 0;
        }

        public int[] BubbleSortArray(int[] array, string order)
        {
            arrayClone = array;
            
            //Console.WriteLine(order + "\n   lscA: " + count_A + "   lscD: " + count_D);

            for (int i = 0; i < arrayClone.Length; i++)  // For each element in the array
            {
                for (int j = 0; j < (arrayClone.Length) - i; j++)  // When j is < array length - current element index
                {
                    if (order == "A" && !(j + 1 > arrayClone.Length - 1))
                    {
                        if (arrayClone[j + 1] < arrayClone[j])  // If the element in front of the element at index j in the array is SMALLER (then swap)
                        {
                            count_A++;  // Iterate the counter

                            int buffer = arrayClone[j];

                            arrayClone[j] = arrayClone[j + 1];
                            arrayClone[j + 1] = buffer;
                        }
                    }
                    else if (order == "D" && !(j + 1 > arrayClone.Length - 1))
                    {
                        if (arrayClone[j + 1] > arrayClone[j])  // If the element in front of the element at index j in the array is LARGER (then swap)
                        {
                            count_D++;  // Iterate the counter

                            int buffer = arrayClone[j];

                            arrayClone[j] = arrayClone[j + 1];
                            arrayClone[j + 1] = buffer;
                        }
                    }
                }
            }

            return arrayClone;
        }
    }
}
