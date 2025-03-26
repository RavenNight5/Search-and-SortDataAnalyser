using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search_and_SortDataAnalyser.Sorting_Algorithms
{
    internal class Bubble_Sort
    {
        public int Count_A = 0;
        public int Count_D = 0;

        private int[] _arrayClone;

        public Bubble_Sort() { ClearValues(); }

        private void ClearValues()
        {
            Count_A = 0;
            Count_D = 0;
        }

        public int[] BubbleSortArray(int[] array, string order)
        {
            _arrayClone = array;
            
            //Console.WriteLine(order + "\n   lscA: " + Count_A + "   lscD: " + Count_D);

            for (int i = 0; i < _arrayClone.Length; i++)  // For each element in the array
            {
                for (int j = 0; j < (_arrayClone.Length) - i; j++)  // When j is < array length - current element index
                {
                    if (order == "A" && !(j + 1 > _arrayClone.Length - 1))
                    {
                        if (_arrayClone[j + 1] < _arrayClone[j])  // If the element in front of the element at index j in the array is SMALLER (then swap)
                        {
                            Count_A++;  // Iterate the counter

                            int buffer = _arrayClone[j];

                            _arrayClone[j] = _arrayClone[j + 1];
                            _arrayClone[j + 1] = buffer;
                        }
                    }
                    else if (order == "D" && !(j + 1 > _arrayClone.Length - 1))
                    {
                        if (_arrayClone[j + 1] > _arrayClone[j])  // If the element in front of the element at index j in the array is LARGER (then swap)
                        {
                            Count_D++;  // Iterate the counter

                            int buffer = _arrayClone[j];

                            _arrayClone[j] = _arrayClone[j + 1];
                            _arrayClone[j + 1] = buffer;
                        }
                    }
                }
            }

            return _arrayClone;
        }
    }
}
