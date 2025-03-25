using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search_and_SortDataAnalyser
{
    internal class Initialise
    {
        private List<string> contents_256 = new List<string> { Program.S_1_256_Contents, Program.S_2_256_Contents, Program.S_3_256_Contents };
        private List<string> contents_2048 = new List<string> { Program.S_1_2048_Contents, Program.S_2_2048_Contents, Program.S_3_2048_Contents };

        public Initialise()
        {

            for (int i = 0; i < contents_256.Count; i++)  // Iterates 3 times (for each 256 data file)
            {
                int[] newDataArray = GetArray("256", i);

                Program.AllFiles[0].Add(newDataArray);  // Program.AllFiles[0] refers to Files_256 that will contain 3 string arrays
            }

            for (int i = 0; i < contents_2048.Count; i++)  // Iterates 3 times (for each 2048 data file)
            {
                int[] newDataArray = GetArray("2048", i);

                Program.AllFiles[1].Add(newDataArray);  // Program.AllFiles[1] refers to Files_2048 that will contain 3 string arrays
            }

            ConfigureArrays configureArrays = new ConfigureArrays();
        }

        public int[] GetArray(string dataPoints, int index)
        {
            string currrentContents = "";

            if (dataPoints == "256")
            {
                currrentContents = contents_256[index];
            }
            else if (dataPoints == "2048")
            {
                currrentContents = contents_2048[index];
            }

            string[] newDataArray = currrentContents.Split('\n').ToArray();

            int[] newDataArrayInt = new int[newDataArray.Length - 1];  // -1 as an empty string is created as an element in the newDataArray (from the newline at the end of the files)

            for (int i = 0; i < newDataArrayInt.Length; i++)
            {
                newDataArray[i] = newDataArray[i].Trim().Replace("\n", string.Empty);  // Remove the newline characters and any whitespace
                
                if (int.TryParse(newDataArray[i], out int _))  // Checks if the element can be converted to an integer number
                {
                    newDataArrayInt[i] = int.Parse(newDataArray[i]);  // Convert the string number to an integer (so it can be sorted easily)
                }
            }

            return newDataArrayInt;
        }
    }
}
