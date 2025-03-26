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
        public static int Count = 1;

        private List<int> _keyPositions;
        private int[] _closestKeys = new int[] { -1, -1 };
        private List<List<int>> _closestKeysPositions;

        private int _initialKey = 0;

        private int _newKey;
        private int _newHigherKey;
        private int _newLowerKey;

        private bool _outOfBounds = false;
        private bool _findingNearestVals = false;
        private bool _foundLowerVal = false;
        private bool _foundLargerKey = false;

        private int _countCloseValues = 0;

        private int _smallestVal = 1;
        private int _largestVal = 2;

        public Sequential_Search() { ClearValues(); }

        public void SequentialSearchArray(int[] array, int key)
        {
            _initialKey = key;

            _newKey = key;
            _newHigherKey = key;
            _newLowerKey = key;

            _smallestVal = array[0];  // Set a pivot for _smallestVal in the array
            _largestVal = array[1];  // Set a pivot for _largestVal in the array

            SequentialSearch(array, key);

            //Console.WriteLine(_smallestVal + "_s l_" + _largestVal);
            //Console.WriteLine(string.Join("  ", _closestKeys));
            //Console.WriteLine("Closest index for 0");
            //Console.WriteLine(string.Join("     ", _closestKeysPositions[0]));
            //Console.WriteLine("Closest index for 1");
            //Console.WriteLine(string.Join("     ", _closestKeysPositions[1]));

            // Writes the found positions to the console through program class (so the output message can be easily modified)
            Program.SEARCH_RESULTS(array, key, _outOfBounds, _keyPositions, _newHigherKey, _newLowerKey, _closestKeys, _closestKeysPositions);
        }

        private void ClearValues()
        {
            _keyPositions = new List<int>();
            _closestKeys = new int[] { -2, -2 };
            _closestKeysPositions = new List<List<int>>() { new List<int>(), new List<int>() };

            Count = 1;

            _outOfBounds = false;
            _findingNearestVals = false;
            _foundLowerVal = false;

            _countCloseValues = 0;  // Used when key is in the bounds of the array, but not found - stops recursion causing stack overflow
        }

        private void SequentialSearch(int[] array, int key, bool recursion = false, bool indexOfRecursionVals = false)  // Key is the value being searched for in the array
        {
            _newKey = key;

            int n = array.Length;
            int i = 0;

            while (i != n - 1)  // Increments the index counter i at each element in the array
            {
                if (!recursion && !indexOfRecursionVals)
                {
                    if (array[i] == key ) { _keyPositions.Add(i); }  // If the current element matches the search key, add its position to the list

                    // Determine the smallest and largest values of the array (later used for finding the key's closest value)
                    if (array[i] < _smallestVal) { _smallestVal = array[i]; }
                    else if (array[i] > _largestVal) { _largestVal = array[i]; }

                    i++;
                    Count++;
                }
                else if (recursion && !indexOfRecursionVals)  // For finding the next highest value to the key
                {
                    if (array[i] == _newKey)
                    {
                        if (_foundLargerKey == false)
                        {
                            _foundLargerKey = true;

                            _closestKeys[1] = _newKey;
                            _newHigherKey = _newKey;

                            // Change the _newKey back to the initial one so the search can decrement from that point (rather than going through the whole array),
                            // then the next lowest value can be found
                            _newKey = _initialKey;
                        }
                    }

                    i++;
                    Count++;
                }
                else
                {
                    if (array[i] == _newKey)
                    {
                        if (_findingNearestVals == true)
                        {
                            if (_foundLargerKey && _closestKeys[0] == -2)  // For finding the next lowest value to the key
                            {
                                _newLowerKey = _newKey;
                                _closestKeys[0] = _newKey;
                            }
                            else  // For finding the indexes of the next higher and lower values
                            {
                                if (array[i] == _closestKeys[0])
                                {
                                    if (!_closestKeysPositions[0].Contains(i))
                                    {
                                        _closestKeysPositions[0].Add(i);
                                    }
                                }
                                else if (array[i] == _closestKeys[1])
                                { 
                                    if (!_closestKeysPositions[1].Contains(i))
                                    {
                                        _closestKeysPositions[1].Add(i);
                                    }
                                }
                            }
                        }
                        else  // For assigning their index positions to _closestKeysPositions
                        {
                            if (_foundLargerKey == true)
                            {
                                _closestKeys[0] = _newKey;
                            }
                            else
                            {
                                _closestKeys[1] = _newKey;
                            }
                        }
                    }

                    i++;
                }
            }

            if (_keyPositions.Count < 1 && (_closestKeys[0] == -2 && _closestKeys[1] == -2) && _findingNearestVals == false)  // Key not found: search for the key's closest value instead
            {
                if (_newKey < _largestVal && _newKey > _smallestVal)  // Search key is within the bounds of the array (between the lowest and highest values)
                {
                    if (_newKey < _largestVal && !_foundLargerKey)
                    {
                        SequentialSearch(array, _newKey = _newKey + 1, true);
                    }
                    if (_newKey > _smallestVal)
                    {
                        //Console.WriteLine(_newKey - 1);
                        SequentialSearch(array, _newKey = _newKey - 1, true);
                    }
                }
                else if (_newKey < _smallestVal)  // Search key is smaller than the smallest value in the array
                {
                    _outOfBounds = true;

                    _newLowerKey = -1;

                    _closestKeys[0] = _smallestVal;
                    _closestKeys[1] = -1;

                    SequentialSearch(array, _smallestVal, false);
                }
                else if (_newKey > _largestVal)  // Search key is larger than the largest value in the array
                {
                    _outOfBounds = true;

                    _newHigherKey = -1;

                    _closestKeys[0] = -1;
                    _closestKeys[1] = _largestVal;

                    SequentialSearch(array, _largestVal, false);
                }
            }
            // Search for how many times the closest higher and lower values appear
            else if (_keyPositions.Count < 1 && ((_closestKeys[0] == -2 || _closestKeys[1] == -2) || _countCloseValues <= 3))
            {
                _countCloseValues++;

                _findingNearestVals = true;

                //Console.WriteLine(string.Join(", ", _closestKeys));
                //Console.WriteLine("Count: " + _countCloseValues);

                if (_closestKeys[0] == -2 && _foundLowerVal == false)
                {
                    SequentialSearch(array, _newKey = _newKey - 1, true, true);
                }
                else if (_foundLowerVal == false)
                {
                    _foundLowerVal = true;

                    SequentialSearch(array, _newLowerKey, true, true);
                }

                // Search for the index positions of the new nearer high key (when already completed for the smaller key)
                if (_countCloseValues == 4)
                {
                    SequentialSearch(array, _newHigherKey, true, true);
                }

            }
        }
    }
}
