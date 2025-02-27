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

        static void Main(string[] args)
        {
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            Console.Write(sCurrentDirectory);

            Console.ReadKey();

            string sFile = Path.Combine(sCurrentDirectory, @"..\..\..\Search-and-SortDataAnalyser\Data_256\Share_1_256.txt");
            string sFilePath = Path.GetFullPath(sFile);

            Console.Write("sFile : " + sFile);
            Console.Write("sFilePath : " + sFilePath);
            
            Console.ReadKey();

            string contents = File.ReadAllText(sFilePath);

            Console.Write(contents);
            
            Console.ReadKey();
        }
    }
}
