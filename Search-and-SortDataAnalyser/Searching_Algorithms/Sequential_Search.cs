using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Search_and_SortDataAnalyser.Searching_Algorithms
{
    internal class Sequential_Search
    {
        private List<int> KeyPositions;
        private int[] ClosestKeys = new int[] { -1, -1 };
        private List<List<int>> ClosestKeysPositions;

        public static int count = 1;

        private int initialKey = 0;

        private int newKey;
        private int newHigherKey;
        private int newLowerKey;

        private bool _outOfBounds = false;
        private bool _findingNearestVals = false;
        private bool _foundLowerVal = false;
        private bool foundLargerKey = false;

        private int countCloseValues = 0;

        private int smallestVal = 1;
        private int largestVal = 2;

        public Sequential_Search() { ClearValues(); }

        private void ClearValues()
        {
            KeyPositions = new List<int>();
            ClosestKeys = new int[] { -2, -2 };
            ClosestKeysPositions = new List<List<int>>() { new List<int>(), new List<int>() };

            count = 1;

            _outOfBounds = false;
            _findingNearestVals = false;
            _foundLowerVal = false;

            countCloseValues = 0;  // Max value of two as it will be the index for my created lists and arrays - used when key is in the bounds of the array but not found
        }

        public void SequentialSearchArray(int[] array, int key)
        {
            initialKey = key;

            newKey = key;
            newHigherKey = key;
            newLowerKey = key;

            smallestVal = array[0];  // Set a pivot for smallestVal in the array
            largestVal = array[1];  // Set a pivot for largestVal in the array

            SequentialSearch(array, key);

            //Console.WriteLine(smallestVal + "_s l_" + largestVal);
            //Console.WriteLine(string.Join("  ", ClosestKeys));
            //Console.WriteLine("Closest index for 0");
            //Console.WriteLine(string.Join("     ", ClosestKeysPositions[0]));
            //Console.WriteLine("Closest index for 1");
            //Console.WriteLine(string.Join("     ", ClosestKeysPositions[1]));

            Program.SEARCH_RESULTS(key, _outOfBounds, KeyPositions, newHigherKey, newLowerKey, ClosestKeys, ClosestKeysPositions);  // Writes the found positions to the console through program (so the output message can be easily modified)
        }

        private void SequentialSearch(int[] array, int key, bool recursion = false, bool indexOfRecursionVals = false)  // Key is the value being searched for in the array
        {
            newKey = key;

            int n = array.Length;
            int i = 0;

            while (i != n - 1)  // Increments the index counter i at each element in the array
            {
                if (!recursion && !indexOfRecursionVals)
                {
                    if (array[i] == key ) { KeyPositions.Add(i); }  // If the current element matches the search key, add its position to the list

                    // Determine the smallest and largest values of the array (later used for finding the key's closest value)
                    if (array[i] < smallestVal) { smallestVal = array[i]; }
                    else if (array[i] > largestVal) { largestVal = array[i]; }

                    i++;
                    count++;
                }
                else if (recursion && !indexOfRecursionVals)  // For finding the next highest value to the key
                {
                    if (array[i] == newKey)
                    {
                        if (foundLargerKey == false)
                        {
                            foundLargerKey = true;

                            ClosestKeys[1] = newKey;
                            newHigherKey = newKey;

                            newKey = initialKey;  // Change the newKey back to the initial one so the search can decrement from that point (rather than going through the whole array) - then the next lowest value can be found
                        }
                    }

                    i++;
                    count++;
                }
                else
                {
                    if (array[i] == newKey)
                    {
                        if (_findingNearestVals == true)
                        {
                            if (foundLargerKey && ClosestKeys[0] == -2)  // For finding the next lowest value to the key
                            {
                                newLowerKey = newKey;
                                ClosestKeys[0] = newKey;
                            }
                            else  // For finding the indexes of the next higher and lower values
                            {
                                if (array[i] == ClosestKeys[0])
                                {
                                    if (!ClosestKeysPositions[0].Contains(i))
                                    {
                                        ClosestKeysPositions[0].Add(i);
                                    }
                                }
                                else if (array[i] == ClosestKeys[1])
                                { 
                                    if (!ClosestKeysPositions[1].Contains(i))
                                    {
                                        ClosestKeysPositions[1].Add(i);
                                    }
                                }
                            }
                        }
                        else  // For assigning their index positions to ClosestKeysPositions
                        {
                            if (foundLargerKey == true)
                            {
                                ClosestKeys[0] = newKey;
                                //ClosestKeysPositions[0].Add(i);
                            }
                            else
                            {
                                ClosestKeys[1] = newKey;
                                //ClosestKeysPositions[1].Add(i);
                            }
                        }
                    }

                    i++;
                }
            }

            if (KeyPositions.Count < 1 && (ClosestKeys[0] == -2 && ClosestKeys[1] == -2) && _findingNearestVals == false)  // Key not found: search for the Key's closest value instead
            {
                if (newKey < largestVal && newKey > smallestVal)  // Search key is within the bounds of the array (between the lowest and highest values)
                {
                    if (newKey < largestVal && !foundLargerKey)
                    {
                        SequentialSearch(array, newKey = newKey + 1, true);
                    }
                    if (newKey > smallestVal)
                    {
                        //Console.WriteLine(newKey - 1);
                        SequentialSearch(array, newKey = newKey - 1, true);
                    }
                }
                else if (newKey < smallestVal)  // Search key is smaller than the smallest value in the array
                {
                    _outOfBounds = true;

                    newLowerKey = -1;

                    ClosestKeys[0] = smallestVal;
                    ClosestKeys[1] = -1;

                    SequentialSearch(array, smallestVal, false);
                }
                else if (newKey > largestVal)  // Search key is larger than the largest value in the array
                {
                    _outOfBounds = true;

                    newHigherKey = -1;

                    ClosestKeys[0] = -1;
                    ClosestKeys[1] = largestVal;

                    SequentialSearch(array, largestVal, false);
                }
            }
            else if (KeyPositions.Count < 1 && ((ClosestKeys[0] == -2 || ClosestKeys[1] == -2) || countCloseValues <= 3))  // Search for how many times the closest higher and lower values appear
            {
                countCloseValues++;

                _findingNearestVals = true;

                //Console.WriteLine(string.Join(", ", ClosestKeys));
                //Console.WriteLine("count: " + countCloseValues);

                if (ClosestKeys[0] == -2 && _foundLowerVal == false)
                {
                    SequentialSearch(array, newKey = newKey - 1, true, true);
                }
                else if (_foundLowerVal == false)
                {
                    _foundLowerVal = true;

                    SequentialSearch(array, newLowerKey, true, true);
                }

                // Search for the index positions of the new nearer high key (when already completed for the smaller key)
                if (countCloseValues == 4)
                {
                    SequentialSearch(array, newHigherKey, true, true);
                }

            }
        }
    }
}
