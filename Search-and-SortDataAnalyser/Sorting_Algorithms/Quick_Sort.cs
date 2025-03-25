using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search_and_SortDataAnalyser.Sorting_Algorithms
{
    internal class Quick_Sort
    {
        public int count_A = 1;
        public int count_D = 1;

        private string currentOrder = "A";

        private int[] arrayClone;

        public Quick_Sort() { ClearValues(); }

        private void ClearValues()
        {
            count_A = 1;
            count_D = 1;
        }

        public int[] QuickSortArray(int[] array, int start, int stop, string order)
        {
            arrayClone = array;
            currentOrder = order;

            QuickSort(array, 0, array.Length - 1);

            //Console.WriteLine(string.Join(", ", arrayClone));

            return arrayClone;
        }

        public void QuickSort(int[] array, int low, int high)
        {
            if (low < high)
            {
                // Get the pivot index by partitioning the array
                int pivotIndex = Partition(array, low, high);

                // Sort the elements before and after the partition using recursion
                QuickSort(array, low, pivotIndex - 1);
                QuickSort(array, pivotIndex + 1, high);
            }
        }

        private int Partition(int[] array, int low, int high)
        {
            int pivot = array[high];  // Set the last element of the array to the pivot
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (currentOrder == "A")
                {
                    if (array[j] <= pivot)  // If the current value is smaller or equal to the pivot
                    {
                        i++;
                        count_A++;

                        SwapElements(array, i, j);
                    }
                }
                else if (currentOrder == "D")
                {
                    if (array[j] >= pivot)  // If the current value is greater or equal to the pivot
                    {
                        i++;
                        count_D++;

                        SwapElements(array, i, j);
                    }
                }
            }

            SwapElements(array, i + 1, high);  // Reposition the pivot to the correct position

            return i + 1;  // Return the index of the pivot
        }

        // Swaps the two elements in the array
        private void SwapElements(int[] array, int i, int j)
        {
            int temp = array[i];

            array[i] = array[j];
            array[j] = temp;
        }
    }
}
