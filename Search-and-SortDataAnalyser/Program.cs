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
 *  > Sorting: Bubble Sort, Quick Sort, Merge Sort
 *  > Searching: Linear Search, Binary Search
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        

        public static void CLEAR_CONSOLE()
        {
            Console.Clear(); Console.WriteLine("\x1b[3J");  // Clears the current console screen and the scrollback buffer (characters that may be out of view but still there when you scroll up)
        }
    }
}
