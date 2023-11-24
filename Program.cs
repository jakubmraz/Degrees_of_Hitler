﻿using Degrees_of_Hitler;

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

var bfsService = new BFSService();
Console.WriteLine("BFS time...");
var shortestPathsArray = bfsService.CalculateShortestPathLengthsParallel(network, Adolf);

Console.WriteLine("I, chatGPT, will save your progress to a file now.");
Console.WriteLine("(/) Saving...");

var fileService = new FileService();
fileService.SaveIntArrayToFile(shortestPathsArray, @"ShortestPaths.txt");

Console.WriteLine("All done. See you next time.");