using Degrees_of_Hitler;

const int Adolf = 1159788;

var networkAsEdgeList = EdgeListReader.ReadEdgeListFromFile();

//var degreeDistributionCalculator = new DegreeDistributionCalculator(networkAsEdgeList);
//var resultolini = degreeDistributionCalculator.CalculateDegreeDistribution();
//var averageResultolini = degreeDistributionCalculator.CalculateAverageDegrees(resultolini);

//Console.WriteLine($"{(degreeDistributionCalculator.IsClosedNetwork(resultolini)?"Closed":"Open")}");
//Console.WriteLine($"Avg indegree dist: {averageResultolini.averageInDegree}, avg outdegree dist: {averageResultolini.averageOutDegree}");
//Console.WriteLine($"Indegree distribution of Adi: {resultolini[Adolf].Item1}, outdegree distribution of Adi: {resultolini[Adolf].Item2}");

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

var dijkstra = new DijkstraService();
Console.WriteLine("Dijkstra moment...");
var avgShortestPath = dijkstra.CalculateAverageShortestPathLengthParallel(network, Adolf);

Console.WriteLine($"The average shortest path length to Adolf is: {avgShortestPath}");