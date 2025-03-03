using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search_and_SortDataAnalyser
{
    internal class BubbleSort
    {
        public int lastSortCount = 0;

        public int[] BubbleSortArray(int[] array, string order)
        {
            int[] arrayClone = array;

            for (int i = 0; i < arrayClone.Length - 1; i++)
            {
                for (int j = 0; j < (arrayClone.Length - 1) - i; j++)
                {
                    if (arrayClone[j + 1] < arrayClone[j])
                    {
                        lastSortCount++;  // Iterate the counter

                        int buffer = arrayClone[j];

                        if (order == "A")
                        {
                            arrayClone[j] = arrayClone[j + 1];
                            arrayClone[j + 1] = buffer;
                        }
                        else if (order == "D")
                        {
                            arrayClone[j] = arrayClone[j - 1];
                            arrayClone[j - 1] = buffer;
                        }
                    }
                }
            }

            return arrayClone;
        }
    }
}
