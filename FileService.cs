using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Degrees_of_Hitler
{
    public class FileService
    {
        public void SaveIntArrayToFile(int[] array, string filePath)
        {
            using (StreamWriter file = new StreamWriter(filePath, false))
            {
                foreach (int number in array)
                {
                    file.WriteLine(number);
                }
            }
        }

        public int[] ReadIntArrayFromFile(string filePath, int arraySize)
        {
            int[] array = new int[arraySize];
            int index = 0;

            using (StreamReader file = new StreamReader(filePath))
            {
                string line;
                while ((line = file.ReadLine()) != null && index < arraySize)
                {
                    array[index] = int.Parse(line);
                    index++;
                }
            }

            return array;
        }

        public int[] ReadIntArrayFromFile(string filePath)
        {
            List<int> list = new List<int>();

            using (StreamReader file = new StreamReader(filePath))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    list.Add(int.Parse(line));
                }
            }

            return list.ToArray();
        }
    }
}
