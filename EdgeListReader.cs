using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Degrees_of_Hitler
{
    public class EdgeListReader
    {
        public static List<Tuple<int, int>> ReadEdgeListFromFile()
        {
            Console.WriteLine("Please enter the full path of the CSV file:");
            string filePath = Console.ReadLine();

            var edges = new List<Tuple<int, int>>();

            if (File.Exists(filePath))
            {
                using (var reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split(' ');
                        if (parts.Length == 2)
                        {
                            if (int.TryParse(parts[0], out int node1) && int.TryParse(parts[1], out int node2))
                            {
                                edges.Add(new Tuple<int, int>(node1, node2));
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("File not found.");
            }

            return edges;
        }

        public static List<List<int>> ConvertEdgeListToAdjacencyListParallel(List<Tuple<int, int>> edgeList, int numberOfNodes)
        {
            var adjacencyList = new List<List<int>>();
            for (int i = 0; i < numberOfNodes; i++)
            {
                adjacencyList.Add(new List<int>());
            }

            object lockObj = new object();

            Parallel.ForEach(edgeList, (edge, state, index) =>
            {
                lock (lockObj)
                {
                    adjacencyList[edge.Item1].Add(edge.Item2);
                }

                // Update progress
                if (index % 1000 == 0) // Update progress for every 1000 operations (adjust as needed)
                {
                    int progress = (int)((index / (double)edgeList.Count) * 100);
                    Console.WriteLine($"Progress: {progress}%");
                }
            });

            return adjacencyList;
        }

        public static int GetNumberOfNodes(List<Tuple<int, int>> edgeList)
        {
            int maxNode = 0;
            foreach (var edge in edgeList)
            {
                maxNode = Math.Max(maxNode, Math.Max(edge.Item1, edge.Item2));
            }
            return maxNode + 1; // Add 1 because node indexing starts from 0
        }
    }
}
