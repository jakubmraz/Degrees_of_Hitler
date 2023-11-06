using Degrees_of_Hitler;

var network = EdgeListReader.ReadEdgeListFromFile();

var degreeDistributionCalculator = new DegreeDistributionCalculator(network);
var resultolini = degreeDistributionCalculator.CalculateDegreeDistribution();
var averageResultolini = degreeDistributionCalculator.CalculateAverageDegrees(resultolini);

Console.WriteLine($"{(degreeDistributionCalculator.IsClosedNetwork(resultolini)?"Closed":"Open")}");
Console.WriteLine($"Avg indegree dist: {averageResultolini.averageInDegree}, avg outdegree dist: {averageResultolini.averageOutDegree}");