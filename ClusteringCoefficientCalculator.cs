using System;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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

            int triangles = 0;
            int triplets = successors.Count * predecessors.Count;

            foreach (var succ in successors)
            {
                if (_predecessors.ContainsKey(succ))
                {
                    triangles += predecessors.Intersect(_predecessors[succ]).Count();
                }
            }

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

