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
