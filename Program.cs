using Degrees_of_Hitler;

const int Adolf = 1159788;

var networkAsEdgeList = EdgeListReader.ReadEdgeListFromFile();

//var degreeDistributionCalculator = new DegreeDistributionCalculator(networkAsEdgeList);
//var resultolini = degreeDistributionCalculator.CalculateDegreeDistribution();
//var averageResultolini = degreeDistributionCalculator.CalculateAverageDegrees(resultolini);

//Console.WriteLine($"{(degreeDistributionCalculator.IsClosedNetwork(resultolini) ? "Closed" : "Open")}");
//Console.WriteLine($"Avg indegree dist: {averageResultolini.averageInDegree}, avg outdegree dist: {averageResultolini.averageOutDegree}");
//Console.WriteLine($"Indegree distribution of Adi: {resultolini[Adolf].Item1}, outdegree distribution of Adi: {resultolini[Adolf].Item2}");

//static double CalculatePercentile(List<int> degrees, int hitlerDegree)
//{
//    int count = degrees.Count;
//    int numberBelow = degrees.Count(d => d < hitlerDegree);
//    int numberEqual = degrees.Count(d => d == hitlerDegree);

//    // Using the formula for percentile rank: P = (C + 0.5F) / N * 100
//    // Where C is the count of values below, F is the frequency of the value, and N is the total count
//    double percentile = (numberBelow + 0.5 * numberEqual) / count * 100;
//    return percentile;
//}

//int hitlerInDegree = resultolini[Adolf].Item1;
//int hitlerOutDegree = resultolini[Adolf].Item2;

//var percentileInDegree = CalculatePercentile(resultolini.Values.Select(x => x.Item1).ToList(), hitlerInDegree);
//var percentileOutDegree = CalculatePercentile(resultolini.Values.Select(x => x.Item2).ToList(), hitlerOutDegree);

//Console.WriteLine($"Hitler's In-Degree Percentile: {percentileInDegree}");
//Console.WriteLine($"Hitler's Out-Degree Percentile: {percentileOutDegree}");

//static List<double> GetFiveNumberSummary(List<int> values)
//{
//    values.Sort();
//    var min = values.First();
//    var max = values.Last();
//    var median = GetMedian(values);
//    var q1 = GetMedian(values.Take(values.Count / 2).ToList());
//    var q3 = GetMedian(values.Skip((values.Count + 1) / 2).ToList());

//    return new List<double> { min, q1, median, q3, max };
//}

//static double GetMedian(List<int> sortedValues)
//{
//    int size = sortedValues.Count;
//    int mid = size / 2;

//    if (size % 2 == 0)
//        return (sortedValues[mid] + sortedValues[mid - 1]) / 2.0;
//    else
//        return sortedValues[mid];
//}

//var inDegrees = resultolini.Values.Select(x => x.Item1).ToList();
//var outDegrees = resultolini.Values.Select(x => x.Item2).ToList();

//var inDegreeSummary = GetFiveNumberSummary(inDegrees);
//var outDegreeSummary = GetFiveNumberSummary(outDegrees);

//Console.WriteLine("In-Degree Summary: " + string.Join(", ", inDegreeSummary));
//Console.WriteLine("Out-Degree Summary: " + string.Join(", ", outDegreeSummary));


//var calculator = new ClusteringCoefficientCalculator(networkAsEdgeList);
//var coefficients = calculator.CalculateClusteringCoefficients();
//var averageCoefficient = calculator.CalculateAverageClusteringCoefficient(coefficients);

//Console.WriteLine($"Average Clustering Coefficient: {averageCoefficient}");
//Console.WriteLine($"Clustering coefficient of Hitler: {coefficients[Adolf]}");

int numberOfNodes = EdgeListReader.GetNumberOfNodes(networkAsEdgeList);
Console.WriteLine("Converting...");
var network = EdgeListReader.ConvertEdgeListToAdjacencyListParallel(networkAsEdgeList, numberOfNodes);

//To clear up memory
networkAsEdgeList = null;

var bfsService = new BFSService();
Console.WriteLine("BFS time...");
var shortestPathsArray = bfsService.CalculateShortestPathsWithSampling(network, Adolf, 100000);

Console.WriteLine("I, chatGPT, will save your progress to a file now.");
Console.WriteLine("(/) Saving...");

var fileService = new FileService();
fileService.SavePathsToFile(shortestPathsArray, @"ShortestPaths.txt");

Console.WriteLine("All done. See you next time.");