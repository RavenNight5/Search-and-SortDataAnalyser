﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Search_and_SortDataAnalyser.Sorting_Algorithms;

namespace Search_and_SortDataAnalyser
{
    internal class ConfigureArrays
    {
        private string[] allFileNames = { "Share_1_256", "Share_2_256", "Share_3_256", "Share_1_2048", "Share_2_2048", "Share_3_2048" };

        public static string currentFileName = "NoFileSelected";
        public string currentFileDataPoints = "NoFileSelected";

        public string sortingOrder = "NoOrderSelected";

        public int dataPointsSelect = -1;
        public int dataFileSelect = -1;
        public int searchSort = -1;

        public static int[] selectedFile;

        public string errMessage1 = "Error occured and an unexpected value was passed.";
        public string errMessage2 = "Error occured: data point count was not recognised.";

        public string[] integerInputs = new string[] { "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9" };

        public string pluralArray = "";

        public ConfigureArrays()
        {
            SelectArray();
        }

        public void SelectArray()
        {
            Program.CLEAR_CONSOLE();

            currentFileName = "";
            currentFileDataPoints = "";

            Console.WriteLine("Select data point count:\n-----------------------\n\n > 256 data points [1]\n > 2048 data points [2]\n");
            
            //
            dataPointsSelect = -1;
            
            void DataPointSelect()  // Repeats until a valid option has been selected
            {
                dataPointsSelect = UserInput(1, 2);
                
                if (dataPointsSelect == -1)
                {
                    DataPointSelect();
                }
            }

            DataPointSelect();
            //
            Debug.Assert(dataPointsSelect != -1, errMessage1);
            Program.CLEAR_CONSOLE();
            //

            if (dataPointsSelect == 1)
            {
                currentFileDataPoints = "256";

                Console.WriteLine("Select array:\n------------\n\n > Share_1_256 [1]\n > Share_2_256 [2]\n > Share_3_256 [3]\n");
            }
            else if (dataPointsSelect == 2)
            {
                currentFileDataPoints = "2048";

                Console.WriteLine("Select array:\n------------\n\n > Share_1_2048 [1]\n > Share_2_2048 [2]\n > Share_3_2048 [3]\n");
            }

            //
            dataFileSelect = -1;
            
            void DataFileSelect()
            {
                dataFileSelect = UserInput(1, 3);

                if (dataFileSelect == -1)
                {
                    DataFileSelect();
                }
            }

            DataFileSelect();
            //
            Debug.Assert(dataFileSelect != -1, errMessage1);
            Program.CLEAR_CONSOLE();
            //

            currentFileName = $"Share_{dataFileSelect.ToString()}_{currentFileDataPoints}";

            selectedFile = Program.AllFiles[dataPointsSelect - 1][dataFileSelect - 1];  // - 1 to get the index from the selected option

            SelectedFile();
        }

        private void SelectedFile()
        {
            string canMerge = "\n > Merge with another file [3]";
            int maxNum = 4;

            if (currentFileName.Contains("&")) 
            {
                canMerge = ""; 
                maxNum = 3; 
            }

            Console.WriteLine($"File Selected: " + currentFileName + "\n-------------");
            Console.WriteLine($"\n > Search [1]\n > Sort [2]\n{canMerge}\n > Select different file [{maxNum}]\n");

            //
            searchSort = -1;

            void SortSelect()
            {
                searchSort = UserInput(1, maxNum);

                if (searchSort == -1)
                {
                    SortSelect();
                }
                else if (searchSort == maxNum)
                {
                    SelectArray();
                }
            }

            SortSelect();
            //
            Debug.Assert(searchSort != -1, errMessage1);
            Program.CLEAR_CONSOLE();
            //

            if (searchSort == 1)
            {
                SearchArrayOptions();
            }
            else if (searchSort == 2)
            {
                SortArrayOptions();
            }
            else if (searchSort == 3 && canMerge != "")
            {
                MergeArrayOptions();
            }
        }

        //
        private void SearchArrayOptions()
        {
            Program.CLEAR_CONSOLE();

        }
        
        private void SortArrayOptions()
        {
            Program.CLEAR_CONSOLE();

            Console.WriteLine("Sort in:\n-------\n\n > Ascending [1]\n > Descending [2]\n > Both [3]\n");

            //
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
            //
            Debug.Assert(sortSelect != -1, errMessage1);
            Program.CLEAR_CONSOLE();
            //

            if (sortSelect == 1)
            {
                sortingOrder = "ascending";
                pluralArray = "";
            }
            else if (sortSelect == 2)
            {
                sortingOrder = "descending";
                pluralArray = "";
            }
            else if (sortSelect == 3)
            {
                sortingOrder = "both ascending & descending";
                pluralArray = "s";
            }

            Console.WriteLine($"File {currentFileName} is to be sorted in {sortingOrder} order.");

            SortArray();
        }

        private void MergeArrayOptions()
        {
            Program.CLEAR_CONSOLE();

            string files = "";
            int key = 1;

            string[] selectableFileNames = new string[allFileNames.Length - 1];

            for (int i = 0; i < allFileNames.Length; i++)
            {
                if (allFileNames[i] != currentFileName)
                {
                    string extraNewLine = "";
                    if (i == 3) { extraNewLine = "\n"; }
                    
                    files += $"\n{extraNewLine} > {allFileNames[i]} [{key}]";
                    selectableFileNames[key - 1] = allFileNames[i];

                    key++;
                }
            }

            Console.WriteLine($"Merge file {currentFileName} with:\n{files}\n");

            //
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

            //
            Debug.Assert(option != -1, errMessage1);
            Program.CLEAR_CONSOLE();
            //
            int newSelectedFileIndex = Array.IndexOf(selectableFileNames, selectableFileNames[option - 1]);  // Get the index of the name of the file chosen from an array of all the names

            int[] newSelectedFile = Program.AllFiles[0][0];

            if (newSelectedFileIndex < 3) { newSelectedFile = Program.AllFiles[0][newSelectedFileIndex]; }  // An index less than 3 means it is a 256 file, otherwise a 2048 file
            else { newSelectedFile = Program.AllFiles[1][newSelectedFileIndex - 3]; }  // To get the correct file - 3 from the newSelectedFileIndex so it refers to one of the 3 possible 2048 files
            
            int[] mergedArray = new int[selectedFile.Length + newSelectedFile.Length];


            int k = 0;
            bool kReset = false;

            for (int j = 0; j < selectedFile.Length + newSelectedFile.Length; j++)  // Assign each data point to the cloned array
            {
                if (k < selectedFile.Length && kReset == false)
                {
                    mergedArray[j] = selectedFile[k];

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
            currentFileName = $"{currentFileName} & {newFileName}";

            selectedFile = mergedArray;

            Console.WriteLine($"Successfully merged files {currentFileName}.\n");
            Console.WriteLine($"\n > Continue [1]\n > Show merged array [2]\n");

            option = -1;

            void MergeOptions()
            {
                option = UserInput(1, 2);

                if (option == -1)
                {
                    MergeOptions();
                }
                else if (option == 2)
                {
                    Program.CLEAR_CONSOLE();

                    Console.WriteLine(string.Join(", ", selectedFile));
                    Console.WriteLine("\n\n > Back [1]\n\n");

                    //
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

                            Console.WriteLine($"Successfully merged files {currentFileName}.\n");
                            Console.WriteLine($"\n > Continue [1]\n > Show merged array [2]\n");

                            MergeOptions();
                        }
                    }

                    InnerOption1();
                    //
                }
            }

            MergeOptions();

            //
            Program.CLEAR_CONSOLE();
            Debug.Assert(option != -1, errMessage1);
            //

            SelectedFile();
        }
        //

        private void SortArray()
        {
            Program.CLEAR_CONSOLE();

            Console.WriteLine($"File {currentFileName} is to be sorted in {sortingOrder} order.");
            Console.WriteLine($"\n\n > Sort array{pluralArray} [1]\n > Show unsorted array [2]\n");

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

                    Console.WriteLine(string.Join(", ", selectedFile));
                    Console.WriteLine("\n\n > Back [1]\n\n");

                    //
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

                            Console.WriteLine($"File {currentFileName} is to be sorted in {sortingOrder} order.");
                            Console.WriteLine($"\n\n > Sort array{pluralArray} [1]\n > Show unsorted array [2]\n");

                            SortArray_Select1();
                        }
                    }

                    InnerOption1();
                    //
                }
            }

            SortArray_Select1();

            //
            Debug.Assert(option != -1, errMessage1);
            Program.CLEAR_CONSOLE();
            //


            ////Sort array with all implemented algorithms (see classes in View > Solution Explorer)
            Console.WriteLine("Sorting...\n");

            string concatenatedResult = "";

            //Bubble sort
            Bubble_Sort bubbleSort = new Bubble_Sort();

            bubbleSort.ClearValues();  // Resets the counters

            int[] bubbleSortedArray_A = new int[selectedFile.Length];
            int[] bubbleSortedArray_D = new int[selectedFile.Length];

            for (int j = 0; j < selectedFile.Length - 1; j++)  // Assign each data point to the cloned arrays
            {
                bubbleSortedArray_A[j] = selectedFile[j];
                bubbleSortedArray_D[j] = selectedFile[j];
            }

            bubbleSortedArray_A = bubbleSort.BubbleSortArray(bubbleSortedArray_A, "A");
            bubbleSortedArray_D = bubbleSort.BubbleSortArray(bubbleSortedArray_D, "D");

            if (sortingOrder == "ascending" || sortingOrder == "descending") { concatenatedResult += $"\n---\nSorted with Bubble Sort in {bubbleSort.lastSortCount} iterations.\n---\n"; }
            else { concatenatedResult += $"\n---\nSorted with Bubble Sort in {bubbleSort.lastSortCount} iterations for ascending order and {bubbleSort.lastSortCountD} iterations for descending order.\n---\n"; }

            //

            Console.WriteLine(concatenatedResult);

            ////
            //////cont...

            Console.WriteLine("\nSorted!");

            int val = -1;

            if (currentFileDataPoints == "256")
            {
                val = 10;
            }
            else if (currentFileDataPoints == "2048")
            {
                val = 50;
            }

            Debug.Assert(val != -1, errMessage2);

            //
            Console.WriteLine($"\n\n > Show every {val.ToString()}th value [1]\n > Show fully sorted array{pluralArray} [2]\n");

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

                    if (sortingOrder == "ascending")
                    {
                        Console.WriteLine("Ascending order:\n");
                        Console.WriteLine(string.Join(", ", bubbleSortedArray_A));
                    }
                    else if (sortingOrder == "descending")
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

                    //
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

                            Console.WriteLine("Sorted!");
                            Console.WriteLine(concatenatedResult);
                            Console.WriteLine($"\n > Show every {val.ToString()}th value [1]\n > Show fully sorted array{pluralArray} [2]\n");

                            SortArray_Select2();
                        }
                    }

                    InnerOption1();
                    //
                }
            }

            SortArray_Select2();

            //
            Debug.Assert(option != -1, errMessage1);
            Program.CLEAR_CONSOLE();
            //
            Console.WriteLine($"Every {val.ToString()}th value of the sorted array{pluralArray} from file {currentFileName}: \n");

            int[] specificValArray_A = new int[selectedFile.Length / val];
            int[] specificValArray_D = new int[selectedFile.Length / val];

            int i = 0;  // Separate counter to iterate through the specificValArray array
            for (int j = val; j < bubbleSortedArray_A.Length; j += val)  // Iterate by val each time
            {
                specificValArray_A[i] = bubbleSortedArray_A[j];
                i++;
            }

            i = 0;
            for (int j = val; j < bubbleSortedArray_D.Length; j += val)
            {
                specificValArray_D[i] = bubbleSortedArray_D[j];
                i++;
            }

            if (sortingOrder == "ascending")
            {
                Console.WriteLine("Ascending order:\n");
                Console.WriteLine(string.Join(", ", specificValArray_A));
            }
            else if (sortingOrder == "descending")
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


            //
            Console.WriteLine("\n > Choose different sorting method [1]\n > Choose different file [2]\n");
            
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

            //
            Debug.Assert(option != -1, errMessage1);
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
        

        private int UserInput(int selectionMin, int selectionMax)
        {
            string keyInfo = Console.ReadKey().Key.ToString();

            if (integerInputs.Contains(keyInfo))
            {
                if (int.TryParse(keyInfo.TrimStart('D'), out int n))  // Input is an integer ("" means string, '' means character)
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
