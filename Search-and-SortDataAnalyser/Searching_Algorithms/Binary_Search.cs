using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Search_and_SortDataAnalyser.Sorting_Algorithms;

namespace Search_and_SortDataAnalyser.Searching_Algorithms
{
    internal class Binary_Search
    {
        public static int Count = 1;

        private bool _found = false;

        public Binary_Search() { ClearValues(); }

        private void ClearValues()
        {
            Count = 1;

            _found = false;
        }

        public void BinarySearchArray(int[] array, int key)
        {
            //Sort using Bubble sort
            int[] newSortedArray = new int[array.Length];

            void bubble()
            {
                Bubble_Sort bubble_Sort = new Bubble_Sort();

                int j = 0;

                foreach (int item in array)  // Assign each data point to the cloned arrays
                {
                    newSortedArray[j] = item;

                    j++;
                }

                newSortedArray = bubble_Sort.BubbleSortArray(newSortedArray, "A");
            }

            bubble();

            //

            int indexResult = BinarySearch(newSortedArray, key);

            if (indexResult != -1)  // Key was found in the array
            {
                //Console.WriteLine(indexResult);
            }
        }

        private int BinarySearch(int[] array, int key)
        {
            int left = 0;
            int right = array.Length - 1;

            while (left <= right && _found == false)
            {
                int midpoint = (left + right) / 2;

                if (key == array[midpoint])
                {
                    _found = true;

                    return midpoint;
                }
                else if (key > array[midpoint])
                {
                    Count++;

                    left = midpoint + 1;
                }
                else
                {
                    Count++;

                    right = midpoint - 1;
                }
            }

            return -1;
        }
    }
}
