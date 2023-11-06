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
    }
}
