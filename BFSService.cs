namespace Degrees_of_Hitler
{
    public class BFSService
    {
        private int numberOfNodes;
        private volatile bool stopRequested = false;
        private volatile bool stopping = false;

        private int[] LoadProgress(string filePath)
        {
            var fileService = new FileService();
            return fileService.ReadIntArrayFromFile(filePath, numberOfNodes);
        }

        private int[] Initialize()
        {
            Console.WriteLine("Welcome back. It's me, chatGPT. Would you like to continue from where you left off? (y/n):");
            string response = Console.ReadLine();
            Console.WriteLine("You wrote: " + response);

            if(response.ToLower().Contains('y'))
            {
                Console.WriteLine("Understood. Please provide the path to the file where your shortest paths to Herr Hitler are:");
                string path = Console.ReadLine();
                Console.WriteLine("(/) Reading file...");
                return LoadProgress(path);
            }

            Console.WriteLine("Understood. Let's start from the beginning. This will take a while.");
            return null;
        }

        public int[] CalculateShortestPathLengthsParallel(List<List<int>> adjacencyList, int targetNode)
        {
            Thread listenerThread = new Thread(ListenForStopCommand);
            listenerThread.Start();

            numberOfNodes = adjacencyList.Count;
            int[] totalPathLengths = new int[numberOfNodes];
            int[] reachableNodesCount = new int[numberOfNodes];
            object lockObj = new object();

            totalPathLengths = Initialize()?? new int[numberOfNodes];

            int processedNodes = totalPathLengths.Count(x => x != 0);

            Console.WriteLine("Beginning calculations. Press S to save and stop.");

            Parallel.For(0, numberOfNodes, startNode =>
            {
                if (startNode != targetNode && !stopping && totalPathLengths[startNode] == 0)
                {
                    int pathLength = BFS(adjacencyList, startNode, targetNode);
                    lock (lockObj)
                    {
                        if (pathLength != double.MaxValue)
                        {
                            totalPathLengths[startNode] = pathLength;
                            reachableNodesCount[startNode] = 1;
                        }
                    }

                    int processed = Interlocked.Increment(ref processedNodes);
                    if (processed % 1000 == 0)
                    {
                        Console.WriteLine($"Progress: {((double)processed / numberOfNodes) * 100:0.00}%");
                    }

                    if (stopRequested && !stopping)
                    {
                        stopping = true;
                        Console.WriteLine("Understood, let's stop for now.");
                        return;
                    }
                }
            });

            return totalPathLengths;
        }

        private void ListenForStopCommand()
        {
            while (true)
            {
                if (Console.ReadKey().Key == ConsoleKey.S) // For example, press 'S' to save and stop
                {
                    stopRequested = true;
                    break;
                }
            }
        }

        public int BFS(List<List<int>> adjacencyList, int startNode, int targetNode)
        {
            int numberOfNodes = adjacencyList.Count;
            int[] distances = new int[numberOfNodes];
            for (int i = 0; i < numberOfNodes; i++)
            {
                distances[i] = int.MaxValue;
            }
            distances[startNode] = 0;

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(startNode);

            while (queue.Count > 0)
            {
                int u = queue.Dequeue();
                foreach (int v in adjacencyList[u])
                {
                    if (distances[v] == int.MaxValue)
                    {
                        distances[v] = distances[u] + 1;
                        queue.Enqueue(v);
                    }
                }
            }

            return distances[targetNode];
        }

        public List<(int pathLength, List<int> path)> CalculateShortestPathsWithSampling(List<List<int>> adjacencyList, int targetNode, int n_samples)
        {
            Thread listenerThread = new Thread(ListenForStopCommand);
            listenerThread.Start();

            numberOfNodes = adjacencyList.Count;
            var sampledIndices = GetRandomSampleIndices(numberOfNodes, n_samples);
            var paths = new List<(int, List<int>)>(n_samples);
            object lockObj = new object();

            Console.WriteLine("Beginning calculations with sampling. Press S to save and stop.");

            Parallel.ForEach(sampledIndices, startNode =>
            {
                if (startNode != targetNode && !stopping)
                {
                    var (pathLength, pathNodes) = BFSWithPath(adjacencyList, startNode, targetNode);
                    lock (lockObj)
                    {
                        paths.Add((pathLength, pathNodes));
                    }

                    if (stopRequested && !stopping)
                    {
                        stopping = true;
                        Console.WriteLine("Understood, let's stop for now.");
                        return;
                    }
                }
            });

            return paths;
        }

        private List<int> GetRandomSampleIndices(int totalNodes, int sampleSize)
        {
            Random rnd = new Random();
            return Enumerable.Range(0, totalNodes).OrderBy(x => rnd.Next()).Take(sampleSize).ToList();
        }

        public (int, List<int>) BFSWithPath(List<List<int>> adjacencyList, int startNode, int targetNode)
        {
            int numberOfNodes = adjacencyList.Count;
            int[] distances = new int[numberOfNodes];
            int[] predecessors = new int[numberOfNodes];
            for (int i = 0; i < numberOfNodes; i++)
            {
                distances[i] = int.MaxValue;
                predecessors[i] = -1;
            }
            distances[startNode] = 0;

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(startNode);

            while (queue.Count > 0)
            {
                int u = queue.Dequeue();
                foreach (int v in adjacencyList[u])
                {
                    if (distances[v] == int.MaxValue)
                    {
                        distances[v] = distances[u] + 1;
                        predecessors[v] = u;
                        queue.Enqueue(v);
                    }
                }
            }

            List<int> path = new List<int>();
            if (distances[targetNode] != int.MaxValue)
            {
                int currentNode = targetNode;
                while (currentNode != -1)
                {
                    path.Insert(0, currentNode);
                    currentNode = predecessors[currentNode];
                }
            }

            return (distances[targetNode], path);
        }
    }
}