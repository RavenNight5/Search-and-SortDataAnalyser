/* Program Name: Stock Exchange Volume Analyzer
 * 
 * Author: Isla McLaughlin
 * Date: [Submission Date]////////////////////////////////////////////
 * Description: This program uses classes to provide a console-based application that reads, sorts, and searches stock exchange volume data from multiple text files.
 *              Users are able to analyze stock trading activity by the implementation of different searching and sorting algorithms; each showing their efficiency
 *              after sorting the selected data. 
 * 
 * Main Functionality:
 *  > Reads stock exchange volume data from multiple text files, creating separate arrays containing the data points for each file.
 *      > Searching:
 *  
 *      > Sorting:
 *          > Sorts the data in ascending and/or descending order based on user preference.
 *          > Displays how many times each sorting algorithm was iterated in order to fully sort the array.
 *          > Displays every 10th (for smaller datasets) or 50th (for larger datasets) value after sorting.
 *         
 *      > Merging:
 *          > Combines any two of the available files into a new array.
 *          > This merged array can be searched or sorted like any other array.
 * 
 * 
 * 
 * Input Parameters:
 *  > *Choose file from the number of data points then the specific share file name
 *      > **Search, sort, merge or select a different file:
 *          > ***Search:
 *              > 
 *          > ***Sort:
 *              > Sort in ascending order, descending order or both (program sorts both, so this is just for displaying the preffered order to the user)
 *                  ****
 *                  > Start the sort
 *                      *****
 *                      > Show every 10th (for smaller datasets) or 50th (for larger datasets) of the sorted array(s)
 *                          > Select a different sorting method ***
 *                          > Select a different file *
 *                      > Show fully sorted array(s)
 *                          > Back *****
 *                  > Show the unsorted array
 *                      > Back ****
 *          > Merge:
 *              > Choose a file to merge with the current file
 *                  ****
 *                  > Continue to search or sort the merged array **
 *                  > Show merged array
 *                      > Back ****
 * 
 * 
 * Expected Output:
 *  > Sorted data with selected interval values displayed.
 *  > Search results including value locations or nearest available values.
 * 
 * Implemented Algorithms:
 *  > Sorting: Bubble Sort, Insertion Sort, Quick Sort, Merge Sort
 *  > Searching: Sequential (Linear) Search, Binary Search, Interpolation Search
 * 

-
- Searches for a user-defined value and returns its position(s). 
- If the value is not found, provides the nearest value(s) and their position(s). 
- Merges and processes datasets for advanced analysis. 
- Compares different sorting and searching algorithms by counting their execution steps. 
 
*/


using System;
using System.Collections.Generic;
using System.IO;
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
            // Assign the contents of each .txt file to a static string that can be directly accessed throughout the program (as these will be copied and ammended rather than being changed themselves)

            string S_1_256_File = Path.Combine(CurrentDirectory, @"..\..\..\Search-and-SortDataAnalyser\Data_256\Share_1_256.txt");
            string S_1_256_FilePath = Path.GetFullPath(S_1_256_File);
            S_1_256_Contents = File.ReadAllText(S_1_256_FilePath);

            string S_2_256_File = Path.Combine(CurrentDirectory, @"..\..\..\Search-and-SortDataAnalyser\Data_256\Share_2_256.txt");
            string S_2_256_FilePath = Path.GetFullPath(S_2_256_File);
            S_2_256_Contents = File.ReadAllText(S_2_256_FilePath);

            string S_3_256_File = Path.Combine(CurrentDirectory, @"..\..\..\Search-and-SortDataAnalyser\Data_256\Share_3_256.txt");
            string S_3_256_FilePath = Path.GetFullPath(S_3_256_File);
            S_3_256_Contents = File.ReadAllText(S_3_256_FilePath);

            //

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

            Initialise initialise = new Initialise();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();  // When Initialise and ConfigureArrays is complete, wait for an input so the console does not close immediately
        }
        
        public static void SEARCH_RESULTS(int key, bool outOfBounds, List<int> KeyPositions, int newHigherKey, int newLowerKey, int[] ClosestKeys, List<List<int>> ClosestKeysPositions)
        {
            Console.WriteLine("\n---");

            if (KeyPositions.Count >= 1 && outOfBounds == false)  // Key was found
            {
                if (KeyPositions.Count == 1) { Console.WriteLine($"The value {key} appears once at index position {KeyPositions[0]} of the selected array."); }
                else if (KeyPositions.Count > 1)
                {
                    List<int> KeyPositionsNew = new List<int>();

                    for (int i = 0; i < KeyPositions.Count - 1; i++)  // Assign all but the last element to the newly created list
                    {
                        KeyPositionsNew.Add(KeyPositions[i]);
                    }

                    Console.WriteLine($"The value {key} appears {KeyPositions.Count} times at index positions: {string.Join(", ", KeyPositionsNew)} and {KeyPositions[KeyPositions.Count - 1]} of the selected array.");
                }
            }
            else  // Closest values to the search key were found instead
            {
                Console.WriteLine($"! Key not found !\n  The value {key} is not present in the selected array.\n");

                if (ClosestKeys[0] != -1 && ClosestKeys[1] == -1)  // Search key was smaller than the smalest value
                {
                    List<int> KeyPositionsNew = new List<int>();

                    for (int i = 0; i < KeyPositions.Count - 1; i++) { KeyPositionsNew.Add(KeyPositions[i]); }

                    string concat = "";

                    if (KeyPositions.Count == 1) { concat += $"\n{ClosestKeys[0]} is the lowest possible value in the array, which appears once at index position: {KeyPositions[0].ToString()} of the selected array."; }
                    else { concat += $"\n{ClosestKeys[0]} is the lowest possible value in the array, which appears {KeyPositions.Count} times at index positions: {string.Join(", ", KeyPositionsNew)} and {KeyPositions[KeyPositions.Count - 1]} of the selected array."; }

                    Console.WriteLine(concat);
                }
                else if (ClosestKeys[0] == -1 && ClosestKeys[1] != -1)  // Search key was larger than the largest value
                {
                    List<int> KeyPositionsNew = new List<int>();

                    for (int i = 0; i < KeyPositions.Count - 1; i++) { KeyPositionsNew.Add(KeyPositions[i]); }

                    string concat = "";

                    if (KeyPositions.Count == 1) { concat += $"\n{ClosestKeys[1]} is the highest possible value in the array, which appears once at index position: {KeyPositions[0].ToString()} of the selected array."; }
                    else { concat += $"\n{ClosestKeys[1]} is the highest possible value in the array, which appears {KeyPositions.Count} times at index positions: {string.Join(", ", KeyPositionsNew)} and {KeyPositions[KeyPositions.Count - 1]} of the selected array."; }

                    Console.WriteLine(concat);
                }
                else  // Key is within the bounds of the array but is not found
                {
                    List<int> KeyLowPositionsNew = new List<int>();
                    List<int> KeyHighPositionsNew = new List<int>();

                    for (int i = 0; i < ClosestKeysPositions[0].Count - 1; i++) { KeyLowPositionsNew.Add(ClosestKeysPositions[0][i]); }
                    for (int i = 0; i < ClosestKeysPositions[1].Count - 1; i++) { KeyHighPositionsNew.Add(ClosestKeysPositions[1][i]); }

                    string concat = "";

                    if (ClosestKeysPositions[0].Count == 1) { concat += $"\n{ClosestKeys[0]} is the next lowest value which appears once at index position: {ClosestKeysPositions[0][0].ToString()} of the selected array."; }
                    else { concat += $"\n{ClosestKeys[0]} is the next lowest value which appears {ClosestKeysPositions[0].Count} times at index positions: {string.Join(", ", KeyLowPositionsNew)} and {ClosestKeysPositions[0][ClosestKeysPositions[0].Count - 1]} of the selected array."; }

                    if (ClosestKeysPositions[1].Count == 1) { concat += $"\n{ClosestKeys[1]} is the next highest value which appears once at index position: {ClosestKeysPositions[1][0].ToString()} of the selected array."; }
                    else { concat += $"\n{ClosestKeys[1]} is the next highest value which appears {ClosestKeysPositions[1].Count} times at index positions: {string.Join(", ", KeyHighPositionsNew)} and {ClosestKeysPositions[1][ClosestKeysPositions[1].Count - 1]} of the selected array."; }

                    Console.WriteLine(concat);
                }
               
            }

            Console.WriteLine("---\n");

            Console.WriteLine($"Searched with Sequential Search in {Sequential_Search.count} steps.");
            Console.WriteLine($"\nSearched with Binary Search in xxx steps.");
            Console.WriteLine($"\nSearched with Interpolation Search in xxx steps.");

            Console.WriteLine("\n---\n");
        }

        public static void CLEAR_CONSOLE()
        {
            Console.Clear(); Console.WriteLine("\x1b[3J");  // Clears the current console screen and the scrollback buffer (characters that may be out of view but still there when you scroll up)
        }
    }
}
