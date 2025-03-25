using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search_and_SortDataAnalyser.Sorting_Algorithms
{
    internal class Merge_Sort
    {
        public int count_A = 1;
        public int count_D = 1;

        private string currentOrder = "A";

        private int[] arrayClone;

        public Merge_Sort() { ClearValues(); }

        private void ClearValues()
        {
            count_A = 1;
            count_D = 1;
        }

        public int[] MergeSortArray(int[] array, string order)
        {
            currentOrder = order;

            List<int> leftAndRight = new List<int>();

            for (int i = 0; i < array.Length; i++)  // Assign the values of the array to array list
            {
                leftAndRight.Add(array[i]);
            }

            List<int> mergeSortedList_LeftAndRight = Sort(leftAndRight);

            arrayClone = new int[array.Length];

            for (int i = 0; i < arrayClone.Length; i++)
            {
                arrayClone[i] = mergeSortedList_LeftAndRight[i];
            }
            
            //Console.WriteLine(string.Join(", ", arrayClone));

            return arrayClone;
        }
        
        private List<int> Sort(List<int> unsorted)
        {
            if (unsorted.Count <= 1) { return unsorted; }
            
            List<int> left = new List<int>();
            List<int> right = new List<int>();

            int middle = unsorted.Count / 2;
            for (int i = 0; i < middle; i++) //Dividing the unsorted list
            {
                left.Add(unsorted[i]);
            }
            for (int i = middle; i < unsorted.Count; i++)
            {
                right.Add(unsorted[i]);
            }

            left = Sort(left);
            right = Sort(right);

            return Merge(left, right);
        }

        private List<int> Merge(List<int> left, List<int> right)
        {
            List<int> result = new List<int>();
            {
                while (left.Count > 0 || right.Count > 0)
                {
                    if (left.Count > 0 && right.Count > 0)
                    {
                        if (currentOrder == "A")
                        {
                            if (left.First() <= right.First()) //Comparing First two elements to see which is smaller
                            {
                                result.Add(left.First());
                                left.Remove(left.First());
                            }
                            else
                            {
                                result.Add(right.First());
                                right.Remove(right.First());
                            }

                            count_A++;
                        }
                        else if (currentOrder == "D")
                        {
                            if (left.First() >= right.First())
                            {
                                result.Add(left.First());
                                left.Remove(left.First());
                            }
                            else
                            {
                                result.Add(right.First());
                                right.Remove(right.First());
                            }

                            count_D++;
                        }
                    }
                    else if (left.Count > 0)
                    {
                        result.Add(left.First());
                        left.Remove(left.First());
                    }
                    else if (right.Count > 0)
                    {
                        result.Add(right.First());
                        right.Remove(right.First());

                    }
                }

                return result;
            }
        }
    }
}