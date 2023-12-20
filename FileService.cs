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

        public void SavePathsToFile(List<(int pathLength, List<int> path)> paths, string filePath)
        {
            using (StreamWriter file = new StreamWriter(filePath))
            {
                foreach (var pathInfo in paths)
                {
                    // Constructing the line: pathLength followed by node IDs
                    string line = pathInfo.pathLength.ToString();
                    foreach (int node in pathInfo.path)
                    {
                        line += " " + node;
                    }

                    // Writing the line to the file
                    file.WriteLine(line);
                }
            }
        }

        public void SaveAdjacencyListToFile(List<List<int>> adjacencyList, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (List<int> neighbors in adjacencyList)
                {
                    string line = string.Join(" ", neighbors);
                    writer.WriteLine(line);
                }
            }
        }
    }
}
