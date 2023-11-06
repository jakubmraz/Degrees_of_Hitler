using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

public class DegreeDistributionCalculator
{
    private Dictionary<int, List<int>> _inEdges = new Dictionary<int, List<int>>();
    private Dictionary<int, List<int>> _outEdges = new Dictionary<int, List<int>>();
    private int _totalNodes;
    private int _processedNodes = 0;

    public DegreeDistributionCalculator(List<Tuple<int, int>> edges)
    {
        // Initialize dictionaries with all nodes to account for nodes with zero in-degree or out-degree
        var allNodes = new HashSet<int>();

        foreach (var edge in edges)
        {
            int source = edge.Item1;
            int target = edge.Item2;

            allNodes.Add(source);
            allNodes.Add(target);

            if (!_outEdges.ContainsKey(source))
                _outEdges[source] = new List<int>();
            _outEdges[source].Add(target);

            if (!_inEdges.ContainsKey(target))
                _inEdges[target] = new List<int>();
            _inEdges[target].Add(source);
        }

        // Ensure all nodes are represented in both _inEdges and _outEdges
        foreach (var node in allNodes)
        {
            if (!_inEdges.ContainsKey(node))
                _inEdges[node] = new List<int>();
            if (!_outEdges.ContainsKey(node))
                _outEdges[node] = new List<int>();
        }

        _totalNodes = allNodes.Count; // The total number of unique nodes
    }

    public Dictionary<int, Tuple<int, int>> CalculateDegreeDistribution()
    {
        var degreeDistribution = new ConcurrentDictionary<int, Tuple<int, int>>();

        Parallel.ForEach(_inEdges.Keys, node =>
        {
            var inDegree = _inEdges[node].Count;
            var outDegree = _outEdges[node].Count;
            var degreeTuple = new Tuple<int, int>(inDegree, outDegree);

            degreeDistribution[node] = degreeTuple;

            int processed = Interlocked.Increment(ref _processedNodes);
            if (processed % 1000 == 0) // Update progress every 1000 nodes
            {
                Console.WriteLine($"Progress: {((double)processed / _totalNodes) * 100:0.00}%");
            }
        });

        return new Dictionary<int, Tuple<int, int>>(degreeDistribution);
    }

    public (double averageInDegree, double averageOutDegree) CalculateAverageDegrees(Dictionary<int, Tuple<int, int>> degreeDistribution)
    {
        long totalInDegree = 0;
        long totalOutDegree = 0;

        foreach (var kvp in degreeDistribution)
        {
            totalInDegree += kvp.Value.Item1;
            totalOutDegree += kvp.Value.Item2;
        }

        double averageInDegree = (double)totalInDegree / _totalNodes;
        double averageOutDegree = (double)totalOutDegree / _totalNodes;

        return (averageInDegree, averageOutDegree);
    }

    public bool IsClosedNetwork(Dictionary<int, Tuple<int, int>> degreeDistribution)
    {
        foreach (var kvp in degreeDistribution)
        {
            // kvp.Value.Item1 is the in-degree, kvp.Value.Item2 is the out-degree
            if (kvp.Value.Item1 == 0 || kvp.Value.Item2 == 0)
            {
                // If any node has an in-degree or out-degree of zero, it's not a closed network
                return false;
            }
        }
        // If we never find a node with an in-degree or out-degree of zero, it's a closed network
        return true;
    }

}

