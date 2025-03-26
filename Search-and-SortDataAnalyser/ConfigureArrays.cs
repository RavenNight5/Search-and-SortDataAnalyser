using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Search_and_SortDataAnalyser.Searching_Algorithms;
using Search_and_SortDataAnalyser.Sorting_Algorithms;

namespace Search_and_SortDataAnalyser
{
    internal class ConfigureArrays
    {
        public static int[] SelectedFile;
        public static string CurrentFileName = "NoFileSelected";
        
        public string CurrentFileDataPoints = "NoFileSelected";
        public string SortingOrder = "NoOrderSelected";

        public string ErrMessage1 = "Error occured and an unexpected value was passed.";
        public string ErrMessage2 = "Error occured: data point count was not recognised.";

        public string[] IntegerInputs = new string[] { "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9" };

        private string[] _allFileNames = { "Share_1_256", "Share_2_256", "Share_3_256", "Share_1_2048", "Share_2_2048", "Share_3_2048" };
        
        private string _pluralArray = "";

        private int _dataPointsSelect = -1;
        private int _dataFileSelect = -1;
        private int _searchSort = -1;

        public ConfigureArrays() { SelectArray(); }

        // Allows the user to select the file they want to search or sort
        public void SelectArray()
        {
            Program.CLEAR_CONSOLE();

            CurrentFileName = "";
            CurrentFileDataPoints = "";

            Console.WriteLine("Select data point count:\n-----------------------\n\n > 256 data points [1]\n > 2048 data points [2]\n");
            
            //Input option
            _dataPointsSelect = -1;
            
            void DataPointSelect()  // Repeats until a valid option has been selected
            {
                _dataPointsSelect = UserInput(1, 2);
                
                if (_dataPointsSelect == -1)
                {
                    DataPointSelect();
                }
            }

            DataPointSelect();

            Debug.Assert(_dataPointsSelect != -1, ErrMessage1);
            Program.CLEAR_CONSOLE();
            //

            if (_dataPointsSelect == 1)
            {
                CurrentFileDataPoints = "256";

                Console.WriteLine("Select array:\n------------\n\n > Share_1_256 [1]\n > Share_2_256 [2]\n > Share_3_256 [3]\n");
            }
            else if (_dataPointsSelect == 2)
            {
                CurrentFileDataPoints = "2048";

                Console.WriteLine("Select array:\n------------\n\n > Share_1_2048 [1]\n > Share_2_2048 [2]\n > Share_3_2048 [3]\n");
            }

            //Input option
            _dataFileSelect = -1;
            
            void DataFileSelect()
            {
                _dataFileSelect = UserInput(1, 3);

                if (_dataFileSelect == -1)
                {
                    DataFileSelect();
                }
            }

            DataFileSelect();

            Debug.Assert(_dataFileSelect != -1, ErrMessage1);
            Program.CLEAR_CONSOLE();
            //

            CurrentFileName = $"Share_{_dataFileSelect.ToString()}_{CurrentFileDataPoints}";

            SelectedFile = Program.AllFiles[_dataPointsSelect - 1][_dataFileSelect - 1];  // - 1 to get the index from the selected option

            SelectedArray();
        }

        // Once the user has selected the array then give them the options to Search, Sort or Merge the file.
        private void SelectedArray()
        {
            string canMerge = "\n > Merge with another file [3]";
            int maxNum = 4;

            if (CurrentFileName.Contains("&")) 
            {
                canMerge = ""; 
                maxNum = 3; 
            }

            Console.WriteLine($"File Selected: " + CurrentFileName + "\n-------------");
            Console.WriteLine($"\n > Search [1]\n > Sort [2]\n{canMerge}\n > Select different file [{maxNum}]\n");

            //Input option - if the user has already merged the file then don't show that option again
            _searchSort = -1;

            void SortSelect()
            {
                _searchSort = UserInput(1, maxNum);

                if (_searchSort == -1)
                {
                    SortSelect();
                }
                else if (_searchSort == maxNum)
                {
                    SelectArray();
                }
            }

            SortSelect();

            Debug.Assert(_searchSort != -1, ErrMessage1);
            Program.CLEAR_CONSOLE();
            //

            if (_searchSort == 1)
            {
                SearchArrayOptions();
            }
            else if (_searchSort == 2)
            {
                SortArrayOptions();
            }
            else if (_searchSort == 3 && canMerge != "")
            {
                MergeArrayOptions();
            }
        }

        // Allows the user to enter their search key
        public void SearchArrayOptions(bool invalidInput = false)
        {
            Program.CLEAR_CONSOLE();

            Console.WriteLine("Input the search key [press Enter to confirm]: ");
            if (invalidInput) { Console.WriteLine("Please enter a positive integer number..."); }

            SearchArray();
        }

        // Gives options the user can choose from when sorting the array, these include:
        // Sort in order of ascending, descending or both
        private void SortArrayOptions()
        {
            Program.CLEAR_CONSOLE();

            Console.WriteLine("Sort in:\n-------\n\n > Ascending [1]\n > Descending [2]\n > Both [3]\n");

            //Input option
            int sortSelect = -1;

            void SortSelect()
            {
                sortSelect = UserInput(1, 3);

                if (sortSelect == -1)
                {
                    SortSelect();
                }
            }

            SortSelect();

            Debug.Assert(sortSelect != -1, ErrMessage1);
            Program.CLEAR_CONSOLE();
            //

            if (sortSelect == 1)
            {
                SortingOrder = "ascending";
                _pluralArray = "";
            }
            else if (sortSelect == 2)
            {
                SortingOrder = "descending";
                _pluralArray = "";
            }
            else if (sortSelect == 3)
            {
                SortingOrder = "both ascending & descending";
                _pluralArray = "s";
            }

            Console.WriteLine($"File {CurrentFileName} is to be sorted in {SortingOrder} order.");

            SortArray();
        }

        // Shows the possible files the user could merge with, allowing them to choose one to merge with their currently selected file
        private void MergeArrayOptions()
        {
            Program.CLEAR_CONSOLE();

            string files = "";
            int key = 1;

            string[] selectableFileNames = new string[_allFileNames.Length - 1];

            for (int i = 0; i < _allFileNames.Length; i++)
            {
                if (_allFileNames[i] != CurrentFileName)
                {
                    string extraNewLine = "";
                    if (i == 3) { extraNewLine = "\n"; }
                    
                    files += $"\n{extraNewLine} > {_allFileNames[i]} [{key}]";
                    selectableFileNames[key - 1] = _allFileNames[i];

                    key++;
                }
            }

            Console.WriteLine($"Merge file {CurrentFileName} with:\n{files}\n");

            //Input option
            int option = -1;

            void MergeSelect()
            {
                option = UserInput(1, 5);

                if (option == -1)
                {
                    MergeSelect();
                }
            }

            MergeSelect();

            Debug.Assert(option != -1, ErrMessage1);
            Program.CLEAR_CONSOLE();
            //

            int newSelectedFileIndex = Array.IndexOf(selectableFileNames, selectableFileNames[option - 1]);  // Get the index of the name of the file chosen from an array of all the names

            int[] newSelectedFile = Program.AllFiles[0][0];

            if (newSelectedFileIndex < 3) { newSelectedFile = Program.AllFiles[0][newSelectedFileIndex]; }  // An index less than 3 means it is a 256 file, otherwise a 2048 file
            else { newSelectedFile = Program.AllFiles[1][newSelectedFileIndex - 3]; }  // To get the correct file - 3 from the newSelectedFileIndex so it refers to one of the 3 possible 2048 files
            
            int[] mergedArray = new int[SelectedFile.Length + newSelectedFile.Length];


            int k = 0;
            bool kReset = false;

            // Merges the chosen file's array with the currently selected array
            for (int j = 0; j < SelectedFile.Length + newSelectedFile.Length; j++)  // Assign each data point to the cloned array
            {
                if (k < SelectedFile.Length && kReset == false)
                {
                    mergedArray[j] = SelectedFile[k];

                    k++;
                }
                else
                {
                    if (kReset == false) { k = 0; kReset = true; }

                    if (k < newSelectedFile.Length)
                    {
                        mergedArray[j] = newSelectedFile[k];

                        k++;
                    }
                }
            }

            string newFileName = selectableFileNames[option - 1];

            CurrentFileName = $"{CurrentFileName} & {newFileName}";

            SelectedFile = mergedArray;

            Console.WriteLine($"Successfully merged files {CurrentFileName}.\n");
            Console.WriteLine($"\n > Continue [1]\n > Show merged array [2]\n");

            //Input option
            option = -1;

            void MergeOptions()
            {
                option = UserInput(1, 2);

                if (option == -1)
                {
                    MergeOptions();
                }
                else if (option == 2)  // Shows the merged array then displays the same set of options (which can now be looped)
                {
                    Program.CLEAR_CONSOLE();

                    Console.WriteLine(string.Join(", ", SelectedFile));
                    Console.WriteLine("\n\n > Back [1]\n\n");

                    //Input option
                    int newOption = -1;

                    void InnerOption1()
                    {
                        newOption = UserInput(1, 1);

                        if (newOption == -1)
                        {
                            InnerOption1();
                        }
                        else if (newOption == 1)
                        {
                            Program.CLEAR_CONSOLE();

                            Console.WriteLine($"Successfully merged files {CurrentFileName}.\n");
                            Console.WriteLine($"\n > Continue [1]\n > Show merged array [2]\n");

                            MergeOptions();
                        }
                    }

                    InnerOption1();
                    //
                }
            }

            MergeOptions();

            Program.CLEAR_CONSOLE();
            Debug.Assert(option != -1, ErrMessage1);
            //

            SelectedArray();
        }


        // Take the user's search key and continue through the search classes
        private void SearchArray()
        {
            int searchKey = UserInput(0, -1);  // Use -1 to tell the input method the max number is infinite

            if (searchKey == -1)  // Search key input was invalid (negative number, string etc.)
            {
                SearchArrayOptions(true);
            }
            else { Search(); }

            void Search()
            {
                //Sequential Search
                Sequential_Search sequential_Search = new Sequential_Search();

                sequential_Search.SequentialSearchArray(SelectedFile, searchKey);

                // Binary Search is handled from the program to continue on from this search - since the program does not return here.
            }

            string canMerge = "\n > Merge with another file [2]";
            int maxNum = 3;

            if (CurrentFileName.Contains("&"))
            {
                canMerge = "";
                maxNum = 2;
            }

            Console.WriteLine($"\n > Search again [1]\n{canMerge}\n > Choose different file [{maxNum}]\n");

            //Input option
            void Options()
            {
                int option = UserInput(1, maxNum);

                if (option == -1)
                {
                    Options();
                }
                else if (option == 1)
                {
                    SearchArrayOptions();
                }
                else if (option == 2 && canMerge != "")
                {
                    MergeArrayOptions();
                }
                else if (option == maxNum)
                {
                    SelectArray();
                }
            }
            
            Options();
            //
        }

        // Handles all of the sorting algorithms that are implemented through their respective classes, displaying their results.
        private void SortArray()
        {
            Program.CLEAR_CONSOLE();

            Console.WriteLine($"File {CurrentFileName} is to be sorted in {SortingOrder} order.");
            Console.WriteLine($"\n\n > Sort array{_pluralArray} [1]\n > Show unsorted array [2]\n");

            //Input option
            int option = -1;

            void SortArray_Select1()
            {
                option = UserInput(1, 2);

                if (option == -1)
                {
                    SortArray_Select1();
                }
                else if (option == 2)
                {
                    Program.CLEAR_CONSOLE();

                    Console.WriteLine(string.Join(", ", SelectedFile));
                    Console.WriteLine("\n\n > Back [1]\n\n");

                    //Input option
                    int newOption = -1;

                    void InnerOption1()
                    {
                        newOption = UserInput(1, 1);

                        if (newOption == -1)
                        {
                            InnerOption1();
                        }
                        else if (newOption == 1)
                        {
                            Program.CLEAR_CONSOLE();

                            Console.WriteLine($"File {CurrentFileName} is to be sorted in {SortingOrder} order.");
                            Console.WriteLine($"\n\n > Sort array{_pluralArray} [1]\n > Show unsorted array [2]\n");

                            SortArray_Select1();
                        }
                    }

                    InnerOption1();
                    //
                }
            }

            SortArray_Select1();

            Debug.Assert(option != -1, ErrMessage1);
            Program.CLEAR_CONSOLE();
            //

            ////Sort array with all implemented algorithms to compare effectiveness (see classes in View > Solution Explorer)
            Console.WriteLine("Sorting...\n");

            string results = "";

            string bestAlgorithm_A = "";
            string bestAlgorithm_D = "";
            int lowestCount_A = 0;
            int lowestCount_D = 0;

            // Called each time a sorting class has completed. This method is used instead of multiple Console.WriteLine()s
            // As it is called after each sort, it also compares the steps each one took: saving the lowest value and the corresponding algorithm name
            void ConcatenateResults(string sortName, int count_A, int count_D)
            {
                // Initialise the variables for finding the most effective algorithm - based on their steps (comparative evaluation)
                if (bestAlgorithm_A == "")
                {
                    bestAlgorithm_A = sortName;
                    bestAlgorithm_D = sortName;
                    lowestCount_A = count_A;
                    lowestCount_D = count_D;
                }

                if (SortingOrder == "ascending") { results += $"Sorted with {sortName} Sort in {count_A} steps.\n---\n"; }
                else if (SortingOrder == "descending") { results += $"Sorted with {sortName} Sort in {count_D} steps.\n---\n"; }
                else { results += $"Sorted with {sortName} in {count_A} steps for ascending order and {count_D} steps for descending order.\n---\n"; }

                // If the new algorithm's results take fewer steps to sort in ascending/descending order then ammend the lowestCount variables
                if (lowestCount_A > count_A)
                {
                    bestAlgorithm_A = sortName;
                    lowestCount_A = count_A;
                }
                if (lowestCount_D > count_D)
                {
                    bestAlgorithm_D = sortName;
                    lowestCount_D = count_D;
                }
            }

            //Bubble sort
            int[] bubbleSortedArray_A = new int[SelectedFile.Length];  // Create a new integer array with the length of the selected file
            int[] bubbleSortedArray_D = new int[SelectedFile.Length];  // (for both ascending and descending)

            void bubble()
            {
                Bubble_Sort bubble_Sort = new Bubble_Sort();  // Create a new object of the bubble_Sort class

                int j = 0;

                foreach (int item in SelectedFile)  // Assign each data point to the cloned arrays (required otherwise the SelectedFile array will be altered)
                {
                    bubbleSortedArray_A[j] = item;
                    bubbleSortedArray_D[j] = item;

                    j++;
                }

                bubbleSortedArray_A = bubble_Sort.BubbleSortArray(bubbleSortedArray_A, "A");  // Call the intital sorting method in the object created from the class Bubble_Sort
                bubbleSortedArray_D = bubble_Sort.BubbleSortArray(bubbleSortedArray_D, "D");  // (for both ascending and descending)

                ConcatenateResults("Bubble", bubble_Sort.Count_A, bubble_Sort.Count_D);  // Call the concatenate method to add the results into a legible string
            }

            bubble();

            //Insertion sort
            void insertion()
            {
                Insertion_Sort insertion_Sort = new Insertion_Sort();

                int[] insertionSortedArray_A = new int[SelectedFile.Length];
                int[] insertionSortedArray_D = new int[SelectedFile.Length];

                int j = 0;

                foreach (int item in SelectedFile)
                {
                    insertionSortedArray_A[j] = item;
                    insertionSortedArray_D[j] = item;

                    j++;
                }

                insertionSortedArray_A = insertion_Sort.InsertionSortArray(insertionSortedArray_A, "A");
                insertionSortedArray_D = insertion_Sort.InsertionSortArray(insertionSortedArray_D, "D");

                ConcatenateResults("Insertion", insertion_Sort.Count_A, insertion_Sort.Count_D);
            }

            insertion();

            //Merge sort
            void merge()
            {
                Merge_Sort merge_Sort = new Merge_Sort();

                int[] mergeSortedArray_A = new int[SelectedFile.Length];
                int[] mergeSortedArray_D = new int[SelectedFile.Length];

                int j = 0;

                foreach (int item in SelectedFile)
                {
                    mergeSortedArray_A[j] = item;
                    mergeSortedArray_D[j] = item;

                    j++;
                }

                mergeSortedArray_A = merge_Sort.MergeSortArray(mergeSortedArray_A, "A");
                mergeSortedArray_D = merge_Sort.MergeSortArray(mergeSortedArray_D, "D");

                ConcatenateResults("Merge", merge_Sort.Count_A, merge_Sort.Count_D);
            }

            merge();

            //Quick sort
            void quick()
            {
                Quick_Sort quick_Sort = new Quick_Sort();

                int[] quickSortedArray_A = new int[SelectedFile.Length];
                int[] quickSortedArray_D = new int[SelectedFile.Length];

                int j = 0;

                foreach (int item in SelectedFile)
                {
                    quickSortedArray_A[j] = item;
                    quickSortedArray_D[j] = item;

                    j++;
                }

                quickSortedArray_A = quick_Sort.QuickSortArray(quickSortedArray_A, 0, quickSortedArray_A.Length, "A");  //0 is the index for the start pointer
                quickSortedArray_D = quick_Sort.QuickSortArray(quickSortedArray_D, 0, quickSortedArray_D.Length, "D");

                ConcatenateResults("Quick", quick_Sort.Count_A, quick_Sort.Count_D);
            }

            quick();

            Console.WriteLine("---\n" + results + "\nSorted!\n\n---\n");

            // Writes the result of the comparative evaluation from the ConcatenateResults method
            if (SortingOrder == "ascending")
            {
                Console.WriteLine($"In this case, {bestAlgorithm_A} Sort would be the most effective at sorting the algorithm in {SortingOrder} order, as it takes the least amount of steps.");
            }
            else if (SortingOrder == "descending")
            {
                Console.WriteLine($"In this case, {bestAlgorithm_D} Sort would be the most effective at sorting the algorithm in {SortingOrder} order, as it takes the least amount of steps.");
            }
            else  // For both ascending and descending order
            {
                if (bestAlgorithm_A == bestAlgorithm_D)  // This is if the count for A and D is the same, only write one message.
                {
                    Console.WriteLine($"In this case, {bestAlgorithm_A} Sort would be the most effective at sorting the algorithm in {SortingOrder} order, as it takes the least amount of steps for both.");
                }
                else  // On the rare case that the A or D count from a different algorithm takes less steps than the usual Quick Sort, display this dual message detailing that.
                {
                    // An example of this b eing shown is by merging file Share_3_2048 with Share_2_2048 (in that order), then choosing to sort in both A and D order.
                    // Or by merging a file with larger numbers with a file containing smaller numbers usually provides the same result.
                    Console.WriteLine($"In this case:\n" +
                        $"{bestAlgorithm_A} Sort would be the most effective at sorting the algorithm in ascending order.\n" +
                        $"Whereas {bestAlgorithm_D} Sort would be the most effective at sorting the algorithm in descending order.\n" +
                        $"\n(Each based on the number of steps taken to sort the algorithm)");
                }

            }

            ////

            int val = -1;

            if (CurrentFileDataPoints == "256")
            {
                val = 10;
            }
            else if (CurrentFileDataPoints == "2048")
            {
                val = 50;
            }

            Debug.Assert(val != -1, ErrMessage2);

            Console.WriteLine($"\n\n > Show every {val.ToString()}th value [1]\n > Show fully sorted array{_pluralArray} [2]\n");

            //Input option: mainly used for showing the fully sorted array(s) if the user wishes to see them
            option = -1;

            void SortArray_Select2()
            {
                option = UserInput(1, 2);

                if (option == -1)
                {
                    SortArray_Select2();
                }
                else if (option == 2)
                {
                    Program.CLEAR_CONSOLE();

                    if (SortingOrder == "ascending")
                    {
                        Console.WriteLine("Ascending order:\n");
                        Console.WriteLine(string.Join(", ", bubbleSortedArray_A));
                    }
                    else if (SortingOrder == "descending")
                    {
                        Console.WriteLine("Descending order:\n");
                        Console.WriteLine(string.Join(", ", bubbleSortedArray_D));
                    }
                    else
                    {
                        Console.WriteLine("Ascending order:\n");
                        Console.WriteLine(string.Join(", ", bubbleSortedArray_A));

                        Console.WriteLine("\nDescending order:\n");
                        Console.WriteLine(string.Join(", ", bubbleSortedArray_D));
                    }

                    Console.WriteLine("\n\n > Back [1]\n\n");

                    //Input option
                    int newOption = -1;

                    void InnerOption1()
                    {
                        newOption = UserInput(1, 1);

                        if (newOption == -1)
                        {
                            InnerOption1();
                        }
                        else if (newOption == 1)
                        {
                            Program.CLEAR_CONSOLE();

                            Console.WriteLine("---\n" + results + "\n\nSorted!");

                            Console.WriteLine($"\n > Show every {val.ToString()}th value [1]\n > Show fully sorted array{_pluralArray} [2]\n");

                            SortArray_Select2();
                        }
                    }

                    InnerOption1();
                    //
                }
            }

            SortArray_Select2();

            Debug.Assert(option != -1, ErrMessage1);
            Program.CLEAR_CONSOLE();
            //

            Console.WriteLine($"Every {val.ToString()}th value of the sorted array{_pluralArray} from file {CurrentFileName}: \n");

            int[] specificValArray_A = new int[SelectedFile.Length / val];  // Create a new integer array with the length of the selected array divided by val (either 10 or 50)
            int[] specificValArray_D = new int[SelectedFile.Length / val];

            int i = 0;  // Separate counter to iterate through the specificValArray array
            for (int j = val; j < bubbleSortedArray_A.Length; j += val)  // Iterate by val each time
            {
                specificValArray_A[i] = bubbleSortedArray_A[j];  // Assign each element in the newly created array to every val (either 10th or 50th) element in the sorted array 
                i++;
            }
            // Do the same as above, but for descending
            i = 0;
            for (int j = val; j < bubbleSortedArray_D.Length; j += val)
            {
                specificValArray_D[i] = bubbleSortedArray_D[j];
                i++;
            }

            if (SortingOrder == "ascending")
            {
                Console.WriteLine("Ascending order:\n");
                Console.WriteLine(string.Join(", ", specificValArray_A));
            }
            else if (SortingOrder == "descending")
            {
                Console.WriteLine("Descending order:\n");
                Console.WriteLine(string.Join(", ", specificValArray_D));
            }
            else
            {
                Console.WriteLine("Ascending order:\n");
                Console.WriteLine(string.Join(", ", specificValArray_A));

                Console.WriteLine("\nDescending order:\n");
                Console.WriteLine(string.Join(", ", specificValArray_D));
            }


            Console.WriteLine("\n > Choose different sorting method [1]\n > Choose different file [2]\n");

            //Input option
            option = -1;

            void SortArray_Select3()
            {
                option = UserInput(1, 2);

                if (option == -1)
                {
                    SortArray_Select3();
                }
            }

            SortArray_Select3();

            Debug.Assert(option != -1, ErrMessage1);
            Program.CLEAR_CONSOLE();
            //

            if (option == 1)
            {
                SortArrayOptions();
            }
            else if (option == 2)
            {
                SelectArray();
            }
        }
        

        // Referenced throughout this class - this method handles the input from the user each time an option is given
        private int UserInput(int selectionMin, int selectionMax)
        {
            if (selectionMax != -1)  // Single key option
            {
                string keyInfo = Console.ReadKey().Key.ToString();

                if (IntegerInputs.Contains(keyInfo))
                {
                    if (int.TryParse(keyInfo.TrimStart('D'), out int _))  // Input is an integer ("" means string, '' means character)
                    {
                        int key = int.Parse(keyInfo.TrimStart('D'));

                        if (key >= selectionMin && key <= selectionMax)
                        {
                            return key;
                        }
                        else
                        {
                            ClearCurrentConsoleLine();

                            Console.WriteLine("Please enter a valid option...");

                            return -1;
                        }
                    }
                    else { ClearCurrentConsoleLine(); Console.WriteLine("Please enter a valid integer option..."); return -1; }
                }
                else
                {
                    ClearCurrentConsoleLine();

                    Console.WriteLine("Please enter a valid option...");

                    return -1;
                }
            }
            else  // Read whole user input
            {
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out int _))  // Try to change the string input to an integer, if successful the input is numerical
                {
                    int numberInput = int.Parse(userInput);

                    if (numberInput >= 0)
                    {
                        return numberInput;
                    }
                    else { return -1; }
                }

                return -1;
            }
            
        }

        // Used to remove the last line after an error message is written to the console (such as "That is not an option")
        public static void ClearCurrentConsoleLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);

            int currentLineCursor = Console.CursorTop;

            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
