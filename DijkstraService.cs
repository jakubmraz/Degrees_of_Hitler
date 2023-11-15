using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Degrees_of_Hitler
{
    public class DijkstraService
    {
        public double CalculateAverageShortestPathLengthParallel(List<List<int>> adjacencyList, int targetNode)
        {
            int numberOfNodes = adjacencyList.Count;
            double[] totalPathLengths = new double[numberOfNodes];
            int[] reachableNodesCount = new int[numberOfNodes];
            object lockObj = new object();

            Parallel.For(0, numberOfNodes, startNode =>
            {
                if (startNode != targetNode)
                {
                    double pathLength = Dijkstra(adjacencyList, startNode, targetNode);
                    lock (lockObj)
                    {
                        if (pathLength != double.MaxValue)
                        {
                            totalPathLengths[startNode] = pathLength;
                            reachableNodesCount[startNode] = 1;
                        }
                    }

                    // Update progress
                    int progress = (int)(((double)startNode / numberOfNodes) * 100);
                    Console.WriteLine($"Progress: {progress}%");
                }
            });

            double totalPathLength = 0;
            int totalReachableNodes = 0;
            for (int i = 0; i < numberOfNodes; i++)
            {
                totalPathLength += totalPathLengths[i];
                totalReachableNodes += reachableNodesCount[i];
            }

            return totalReachableNodes > 0 ? totalPathLength / totalReachableNodes : 0;
        }

        public double Dijkstra(List<List<int>> adjacencyList, int startNode, int targetNode)
        {
            int numberOfNodes = adjacencyList.Count;
            double[] distances = new double[numberOfNodes];
            bool[] shortestPathTreeSet = new bool[numberOfNodes];

            for (int i = 0; i < numberOfNodes; i++)
            {
                distances[i] = double.MaxValue;
                shortestPathTreeSet[i] = false;
            }

            distances[startNode] = 0;

            for (int count = 0; count < numberOfNodes - 1; count++)
            {
                int u = MinDistance(distances, shortestPathTreeSet);
                shortestPathTreeSet[u] = true;

                for (int v = 0; v < numberOfNodes; v++)
                {
                    if (!shortestPathTreeSet[v] && adjacencyList[u].Contains(v) &&
                        distances[u] != double.MaxValue && distances[u] + 1 < distances[v])
                    {
                        distances[v] = distances[u] + 1;
                    }
                }
            }

            return distances[targetNode];
        }

        private int MinDistance(double[] distances, bool[] sptSet)
        {
            double min = double.MaxValue;
            int minIndex = -1;

            for (int v = 0; v < distances.Length; v++)
            {
                if (sptSet[v] == false && distances[v] <= min)
                {
                    min = distances[v];
                    minIndex = v;
                }
            }

            return minIndex;
        }
    }
}
