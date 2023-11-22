namespace Degrees_of_Hitler
{
    public class BFSService
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
                    double pathLength = BFS(adjacencyList, startNode, targetNode);
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

        public double BFS(List<List<int>> adjacencyList, int startNode, int targetNode)
        {
            int numberOfNodes = adjacencyList.Count;
            double[] distances = new double[numberOfNodes];
            for (int i = 0; i < numberOfNodes; i++)
            {
                distances[i] = double.MaxValue;
            }
            distances[startNode] = 0;

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(startNode);

            while (queue.Count > 0)
            {
                int u = queue.Dequeue();
                foreach (int v in adjacencyList[u])
                {
                    if (distances[v] == double.MaxValue)
                    {
                        distances[v] = distances[u] + 1;
                        queue.Enqueue(v);
                    }
                }
            }

            return distances[targetNode];
        }
    }
}