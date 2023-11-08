using Degrees_of_Hitler;

const int Adolf = 1159788;

var network = EdgeListReader.ReadEdgeListFromFile();

var degreeDistributionCalculator = new DegreeDistributionCalculator(network);
var resultolini = degreeDistributionCalculator.CalculateDegreeDistribution();
var averageResultolini = degreeDistributionCalculator.CalculateAverageDegrees(resultolini);

Console.WriteLine($"{(degreeDistributionCalculator.IsClosedNetwork(resultolini)?"Closed":"Open")}");
Console.WriteLine($"Avg indegree dist: {averageResultolini.averageInDegree}, avg outdegree dist: {averageResultolini.averageOutDegree}");
Console.WriteLine($"Indegree distribution of Adi: {resultolini[Adolf].Item1}, outdegree distribution of Adi: {resultolini[Adolf].Item2}");

var calculator = new ClusteringCoefficientCalculator(network);
var coefficients = calculator.CalculateClusteringCoefficients();
var averageCoefficient = calculator.CalculateAverageClusteringCoefficient(coefficients);

Console.WriteLine($"Average Clustering Coefficient: {averageCoefficient}");
Console.WriteLine($"Clustering coefficient of Hitler: {coefficients[Adolf]}");