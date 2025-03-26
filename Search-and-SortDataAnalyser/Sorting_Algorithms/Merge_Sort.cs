using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search_and_SortDataAnalyser.Sorting_Algorithms
{
    internal class Merge_Sort
    {
        public int Count_A = 1;
        public int Count_D = 1;

        private string _currentOrder = "A";

        private int[] _arrayClone;

        public Merge_Sort() { ClearValues(); }

        private void ClearValues()
        {
            Count_A = 1;
            Count_D = 1;
        }

        public int[] MergeSortArray(int[] array, string order)
        {
            _currentOrder = order;

            List<int> leftAndRight = new List<int>();

            for (int i = 0; i < array.Length; i++)  // Assign each element of the array to the list (so it can easily be manipulated)
            {
                leftAndRight.Add(array[i]);
            }

            List<int> mergeSortedList_LeftAndRight = Sort(leftAndRight);

            _arrayClone = new int[array.Length];

            for (int i = 0; i < _arrayClone.Length; i++)
            {
                _arrayClone[i] = mergeSortedList_LeftAndRight[i];
            }
            
            //Console.WriteLine(string.Join(", ", _arrayClone));

            return _arrayClone;
        }
        
        private List<int> Sort(List<int> unsorted)
        {
            if (unsorted.Count <= 1) { return unsorted; }
            
            List<int> left = new List<int>();
            List<int> right = new List<int>();

            //Dividing the unsorted list
            int middle = unsorted.Count / 2;

            for (int i = 0; i < middle; i++)
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
                        if (_currentOrder == "A")
                        {
                            if (left.First() <= right.First())  //Comparing First two elements to see which is smaller
                            {
                                result.Add(left.First());
                                left.Remove(left.First());
                            }
                            else
                            {
                                result.Add(right.First());
                                right.Remove(right.First());
                            }

                            Count_A++;
                        }
                        else if (_currentOrder == "D")
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

                            Count_D++;
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