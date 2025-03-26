/* Program Name: Stock Exchange Volume Analyzer
 * 
 * Author: Isla McLaughlin
 * Date: [26/03/2025]
 * Description: This program uses classes to provide a console-based application that reads, sorts, and searches stock exchange volume data from multiple text files.
 *              Users are able to analyze stock trading activity by the implementation of different searching and sorting algorithms; each showing their efficiency
 *              through a count of steps taken, after sorting the selected data. 
 * 
 * Main Functionality:
 *  > Reads stock exchange volume data from multiple text files, creating separate arrays containing the data points for each file.
 *      > Searching:
 *          > Allows the user to input a value to search for in the currently selected array.
 *          > This is then run through the implemented search algorithms to determine if the value is in the array.
 *          > If the value is present, then provide its respective location(s).
 *              > If not present then provide the next highest and lowest values and their location(s).
 *  
 *      > Sorting:
 *          > Sorts the data in ascending and/or descending order based on user preference.
 *          > Displays how many steps each sorting algorithm took in order to fully sort the array.
 *          > Displays every 10th (for smaller datasets) or 50th (for larger datasets) value after sorting.
 *         
 *      > Merging:
 *          > Combines any two of the available files into a new array.
 *          > This merged array can then be searched or sorted like any other array.
 * 
 * 
 * Input Parameters:
 *  > *Choose file from the number of data points then the specific share file name
 *      > **Search, sort, merge or select a different file:
 *          > ***Search:
 *              > Ask the user for a search key
 *                  > Search again ***
 *                  > Merge current file with a different file *****
 *                  > Select a different file *
 *          > ****Sort:
 *              > Sort in ascending order, descending order or both (program sorts both, so this is just for displaying the preffered order to the user)
 *                  *****
 *                  > Start the sort
 *                      ******
 *                      > Show every 10th (for smaller datasets) or 50th (for larger datasets) of the sorted array(s)
 *                          > Select a different sorting method ****
 *                          > Merge current file with a different file *****
 *                          > Select a different file *
 *                      > Show fully sorted array(s)
 *                          > Back ******
 *                  > Show the unsorted array
 *                      > Back *****
 *          > *****Merge:
 *              > Choose a file to merge with the current file
 *                  ******
 *                  > Continue to search or sort the merged array **
 *                  > Show merged array
 *                      > Back ******
 *                      
 *                      
 * Expected Output:
 *  > Sorted data with corresponding interval values displayed.
 *  > Search results (from a user-defined value) including the locations of the value (by index).
 *  > If the value is not found, then provide the nearest value(s) and their respective locations(s).
 *  
 *  > All of the above with a combination of any two files (merge them into one array then choose to search or sort)
 *  
 *  > Compares different sorting and searching algorithms by counting their execution steps.
 *  > States the best, most effective, algorithm for the process based on the amount of steps.
 * 
 * Implemented Algorithms:
 *  > Sorting: Bubble Sort, Insertion Sort, Merge Sort, Quick Sort
 *  > Searching: Sequential (Linear) Search, Binary Search
 *      Note: the Binary Search implemented only searches for the initial search key - not for any closest values: meaning
 *      the steps count only applies to this initial search.
 *  
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Search_and_SortDataAnalyser.Searching_Algorithms;

namespace Search_and_SortDataAnalyser
{
    internal class Program
    {
        public static string CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;

        public static List<int[]> Files_256 = new List<int[]>();
        public static List<int[]> Files_2048 = new List<int[]>();

        public static List<List<int[]>> AllFiles = new List<List<int[]>>();

        public static string S_1_256_Contents;
        public static string S_2_256_Contents;
        public static string S_3_256_Contents;

        public static string S_1_2048_Contents;
        public static string S_2_2048_Contents;
        public static string S_3_2048_Contents;

        static void Main(string[] args)
        {
            // Assign the contents of each .txt file to a static string that can be directly accessed throughout the program class
            // (as these will be copied and ammended rather than being changed)

            // 256 data points
            string S_1_256_File = Path.Combine(CurrentDirectory, @"..\..\..\Search-and-SortDataAnalyser\Data_256\Share_1_256.txt");
            string S_1_256_FilePath = Path.GetFullPath(S_1_256_File);
            S_1_256_Contents = File.ReadAllText(S_1_256_FilePath);

            string S_2_256_File = Path.Combine(CurrentDirectory, @"..\..\..\Search-and-SortDataAnalyser\Data_256\Share_2_256.txt");
            string S_2_256_FilePath = Path.GetFullPath(S_2_256_File);
            S_2_256_Contents = File.ReadAllText(S_2_256_FilePath);

            string S_3_256_File = Path.Combine(CurrentDirectory, @"..\..\..\Search-and-SortDataAnalyser\Data_256\Share_3_256.txt");
            string S_3_256_FilePath = Path.GetFullPath(S_3_256_File);
            S_3_256_Contents = File.ReadAllText(S_3_256_FilePath);

            // 2048 data points
            string S_1_2048_File = Path.Combine(CurrentDirectory, @"..\..\..\Search-and-SortDataAnalyser\Data_2048\Share_1_2048.txt");
            string S_1_2048_FilePath = Path.GetFullPath(S_1_2048_File);
            S_1_2048_Contents = File.ReadAllText(S_1_2048_FilePath);

            string S_2_2048_File = Path.Combine(CurrentDirectory, @"..\..\..\Search-and-SortDataAnalyser\Data_2048\Share_2_2048.txt");
            string S_2_2048_FilePath = Path.GetFullPath(S_2_2048_File);
            S_2_2048_Contents = File.ReadAllText(S_2_2048_FilePath);

            string S_3_2048_File = Path.Combine(CurrentDirectory, @"..\..\..\Search-and-SortDataAnalyser\Data_2048\Share_3_2048.txt");
            string S_3_2048_FilePath = Path.GetFullPath(S_3_2048_File);
            S_3_2048_Contents = File.ReadAllText(S_3_2048_FilePath);

            //

            AllFiles.Add(Files_256);
            AllFiles.Add(Files_2048);

            Initialise initialise = new Initialise();  // Start the initialise class

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();  // When Initialise and ConfigureArrays is complete, wait for an input so the console does not close
                                // immediately (this should not occur due to options looping, but has been included just incase)
        }
        
        // Writes the results from the search algorithms
        public static void SEARCH_RESULTS(int[] array, int key, bool outOfBounds, List<int> keyPositions, int newHigherKey, int newLowerKey, int[] closestKeys, List<List<int>> closestKeysPositions)
        {
            bool directlyFound = false;

            Console.WriteLine("\n---");

            if (keyPositions.Count >= 1 && outOfBounds == false)  // Key was found
            {
                directlyFound = true;

                if (keyPositions.Count == 1) { Console.WriteLine($"The value {key} appears once at index position {keyPositions[0]} of the selected array."); }
                else if (keyPositions.Count > 1)
                {
                    List<int> KeyPositionsNew = new List<int>();

                    for (int i = 0; i < keyPositions.Count - 1; i++)  // Assign all but the last element to the newly created list
                    {
                        KeyPositionsNew.Add(keyPositions[i]);
                    }

                    Console.WriteLine($"The value {key} appears {keyPositions.Count} times at index positions: {string.Join(", ", KeyPositionsNew)} and {keyPositions[keyPositions.Count - 1]} of the selected array.");
                }
            }
            else  // Closest values to the search key were found instead
            {
                Console.WriteLine($"! Key not found !\n  The value {key} is not present in the selected array.\n");

                if (closestKeys[0] != -1 && closestKeys[1] == -1)  // Search key was smaller than the smalest value
                {
                    List<int> KeyPositionsNew = new List<int>();

                    for (int i = 0; i < keyPositions.Count - 1; i++) { KeyPositionsNew.Add(keyPositions[i]); }

                    string concat = "";

                    if (keyPositions.Count == 1) { concat += $"\n{closestKeys[0]} is the lowest possible value in the array, which appears once at index position: {keyPositions[0].ToString()} of the selected array."; }
                    else { concat += $"\n{closestKeys[0]} is the lowest possible value in the array, which appears {keyPositions.Count} times at index positions: {string.Join(", ", KeyPositionsNew)} and {keyPositions[keyPositions.Count - 1]} of the selected array."; }

                    Console.WriteLine(concat);
                }
                else if (closestKeys[0] == -1 && closestKeys[1] != -1)  // Search key was larger than the largest value
                {
                    List<int> KeyPositionsNew = new List<int>();

                    for (int i = 0; i < keyPositions.Count - 1; i++) { KeyPositionsNew.Add(keyPositions[i]); }

                    string concat = "";

                    if (keyPositions.Count == 1) { concat += $"\n{closestKeys[1]} is the highest possible value in the array, which appears once at index position: {keyPositions[0].ToString()} of the selected array."; }
                    else { concat += $"\n{closestKeys[1]} is the highest possible value in the array, which appears {keyPositions.Count} times at index positions: {string.Join(", ", KeyPositionsNew)} and {keyPositions[keyPositions.Count - 1]} of the selected array."; }

                    Console.WriteLine(concat);
                }
                else  // Key is within the bounds of the array but is not found
                {
                    List<int> KeyLowPositionsNew = new List<int>();
                    List<int> KeyHighPositionsNew = new List<int>();

                    for (int i = 0; i < closestKeysPositions[0].Count - 1; i++) { KeyLowPositionsNew.Add(closestKeysPositions[0][i]); }
                    for (int i = 0; i < closestKeysPositions[1].Count - 1; i++) { KeyHighPositionsNew.Add(closestKeysPositions[1][i]); }

                    string concat = "";

                    if (closestKeysPositions[0].Count == 1) { concat += $"\n{closestKeys[0]} is the next lowest value which appears once at index position: {closestKeysPositions[0][0].ToString()} of the selected array."; }
                    else { concat += $"\n{closestKeys[0]} is the next lowest value which appears {closestKeysPositions[0].Count} times at index positions: {string.Join(", ", KeyLowPositionsNew)} and {closestKeysPositions[0][closestKeysPositions[0].Count - 1]} of the selected array."; }

                    if (closestKeysPositions[1].Count == 1) { concat += $"\n{closestKeys[1]} is the next highest value which appears once at index position: {closestKeysPositions[1][0].ToString()} of the selected array."; }
                    else { concat += $"\n{closestKeys[1]} is the next highest value which appears {closestKeysPositions[1].Count} times at index positions: {string.Join(", ", KeyHighPositionsNew)} and {closestKeysPositions[1][closestKeysPositions[1].Count - 1]} of the selected array."; }

                    Console.WriteLine(concat);
                }
               
            }

            void ContinueSearch()
            {
                // Binary Search (sorted using Bubble sort from the class)
                Binary_Search binary_Search = new Binary_Search();

                binary_Search.BinarySearchArray(array, key);
            }

            ContinueSearch();

            Console.WriteLine("---\n");

            Console.WriteLine($"Searched with Sequential Search in {Sequential_Search.Count} steps.");
            if (directlyFound == true)
            {
                Console.WriteLine($"\nSearched with Binary Search in {Binary_Search.Count} steps.");
            }
            else
            {
                Console.WriteLine($"\nSearched with Binary Search in {Binary_Search.Count} steps. (Based on the search key only)");
            }
            Console.WriteLine("\n---\n");

            // Based on their steps (the count variable) do a comparative evaluation
            if (Binary_Search.Count < Sequential_Search.Count)
            {
                Console.WriteLine($"In this case, Binary Search would be the most effective search algorithm as it takes the least amount of steps.\n" +
                    $"(However, the algorithm must be sorted before it can be searched!)");
            }
            else  // For some reason sequential takes less steps than binary, which would likely never happen, I've implemented this it just incase.
            {
                Console.WriteLine($"In this case, Sequential Search would be the most effective search algorithm as it takes the least amount of steps.");
            }
        }

        public static void CLEAR_CONSOLE()
        {
            Console.Clear(); Console.WriteLine("\x1b[3J");  // Clears the current console screen and the scrollback buffer (characters that
                                                            // may be out of view but still there when you scroll up)
        }
    }
}
