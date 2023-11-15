using System.Collections.Concurrent;

public class ClusteringCoefficientCalculator
{
    private Dictionary<int, HashSet<int>> _successors = new Dictionary<int, HashSet<int>>();
    private Dictionary<int, HashSet<int>> _predecessors = new Dictionary<int, HashSet<int>>();
    private int _totalNodes;
    private int _processedNodes = 0;

    public ClusteringCoefficientCalculator(List<Tuple<int, int>> edges)
    {
        foreach (var edge in edges)
        {
            int source = edge.Item1;
            int target = edge.Item2;

            if (!_successors.ContainsKey(source))
                _successors[source] = new HashSet<int>();
            if (!_predecessors.ContainsKey(target))
                _predecessors[target] = new HashSet<int>();

            _successors[source].Add(target);
            _predecessors[target].Add(source);
        }

        _totalNodes = _successors.Keys.Union(_predecessors.Keys).Count();
    }

    public ConcurrentDictionary<int, double> CalculateClusteringCoefficients()
    {
        var clusteringCoefficients = new ConcurrentDictionary<int, double>();

        Parallel.ForEach(_successors.Keys, node =>
        {
            HashSet<int> successors = _successors.ContainsKey(node) ? _successors[node] : new HashSet<int>();
            HashSet<int> predecessors = _predecessors.ContainsKey(node) ? _predecessors[node] : new HashSet<int>();
            HashSet<int> neighbors = new HashSet<int>(successors);
            foreach (var pred in predecessors)
            {
                // Add predecessors to neighbors if it's not a successor to avoid counting twice for undirected edges
                if (!successors.Contains(pred))
                {
                    neighbors.Add(pred);
                }
            }

            int triangles = 0;
            int triplets = 0;

            foreach (var succ in successors)
            {
                foreach (var pred in predecessors)
                {
                    // For each pair of successor and predecessor, check if there is a directed edge forming a triangle
                    if (_successors.ContainsKey(pred) && _successors[pred].Contains(succ))
                    {
                        triangles++; // This is a directed triangle
                    }
                    if (pred != succ) // Make sure we don't count the node itself as a triplet
                    {
                        triplets++; // This is a potential triplet (open triplet)
                    }
                }
            }

            // Each triangle is counted once for each direction in the method, so we don't divide by 3 here.
            double clusteringCoefficient = triplets == 0 ? 0 : (double)triangles / triplets;
            clusteringCoefficients[node] = clusteringCoefficient;

            int processed = Interlocked.Increment(ref _processedNodes);
            if (processed % 1000 == 0)
            {
                Console.WriteLine($"Progress: {((double)processed / _totalNodes) * 100:0.00}%");
            }
        });

        return clusteringCoefficients;
    }


    public double CalculateAverageClusteringCoefficient(ConcurrentDictionary<int, double> clusteringCoefficients)
    {
        double sum = clusteringCoefficients.Values.Sum();
        return sum / _totalNodes;
    }
}

