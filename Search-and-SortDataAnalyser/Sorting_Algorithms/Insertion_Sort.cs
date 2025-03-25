using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search_and_SortDataAnalyser.Sorting_Algorithms
{
    internal class Insertion_Sort
    {
        public int count_A = 1;
        public int count_D = 1;

        private int[] arrayClone;

        public Insertion_Sort() { ClearValues(); }

        private void ClearValues()
        {
            count_A = 1;
            count_D = 1;
        }

        public int[] InsertionSortArray(int[] array, string order)
        {
            arrayClone = array;

            //Console.WriteLine(order + "\n   lscA: " + count_A + "   lscD: " + count_D);

            int numSorted = 1; // number of values in place
            int index;

            while (numSorted < arrayClone.Length)
            {
                // Use the first unsorted value
                int temp = arrayClone[numSorted];

                for (index = numSorted; index > 0; index--)  // Iterate through each element until they are all sorted
                {
                    if (order == "A")
                    {
                        if (temp < arrayClone[index - 1])
                        {
                            arrayClone[index] = arrayClone[index - 1];
                            count_A++;
                        }
                        else { break; }
                    }
                    else if (order == "D")
                    {
                        if (temp > arrayClone[index - 1])  // > used for sorting in descending order
                        {
                            arrayClone[index] = arrayClone[index - 1];
                            count_D++;
                        }
                        else { break; }
                    }
                }

                arrayClone[index] = temp;  // Re-insert the sorted value

                numSorted++;
                }

            //Console.WriteLine(string.Join(", ", arrayClone));

            return arrayClone;
        }
    }
}
