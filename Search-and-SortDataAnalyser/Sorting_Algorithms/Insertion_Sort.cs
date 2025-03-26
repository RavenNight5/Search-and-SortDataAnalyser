using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search_and_SortDataAnalyser.Sorting_Algorithms
{
    internal class Insertion_Sort
    {
        public int Count_A = 1;
        public int Count_D = 1;

        private int[] _arrayClone;

        public Insertion_Sort() { ClearValues(); }

        private void ClearValues()
        {
            Count_A = 1;
            Count_D = 1;
        }

        public int[] InsertionSortArray(int[] array, string order)
        {
            _arrayClone = array;

            //Console.WriteLine(order + "\n   lscA: " + Count_A + "   lscD: " + Count_D);

            int numSorted = 1; // number of values in place
            int index;

            while (numSorted < _arrayClone.Length)
            {
                // Use the first unsorted value
                int temp = _arrayClone[numSorted];

                for (index = numSorted; index > 0; index--)  // Iterate through each element until they are all sorted
                {
                    if (order == "A")
                    {
                        if (temp < _arrayClone[index - 1])
                        {
                            _arrayClone[index] = _arrayClone[index - 1];
                            Count_A++;
                        }
                        else { break; }
                    }
                    else if (order == "D")
                    {
                        if (temp > _arrayClone[index - 1])  // > used for sorting in descending order
                        {
                            _arrayClone[index] = _arrayClone[index - 1];
                            Count_D++;
                        }
                        else { break; }
                    }
                }

                _arrayClone[index] = temp;  // Re-insert the sorted value

                numSorted++;
                }

            //Console.WriteLine(string.Join(", ", _arrayClone));

            return _arrayClone;
        }
    }
}
