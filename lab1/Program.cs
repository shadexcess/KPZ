// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace lab1;

/// <summary>
/// Main program class responsible for reading the dependency file and resolving package installation order.
/// </summary>
public class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Reads the file path from command line arguments, builds a dependency graph, and prints the installation order of packages.
    /// </summary>
    /// <param name="args">Array of command-line arguments. args[0] should contain the path to the dependency file.</param>
    private static void Main(string[] args)
    {
        try
        {
            string filePath = args[0];

            var lines = new FileReader().ReadLines(filePath);
            GraphBuilder builder = new GraphBuilder();
            DependencyGraph graph = builder.BuildGraph(lines);

            string? resolutionPolicy = ConfigurationReader.GetResolutionPolicy("config.json");
            IResolutionStrategy strategy = StrategyFactory.SetStrategy(resolutionPolicy);
            Dictionary<string, Package>? chosenVersions = strategy.ResolveConflicts(graph);
            DependencyGraph finalGraph = chosenVersions == null ? graph : builder.CorrectGraph(graph, chosenVersions);

            DependencyResolver resolver = new DependencyResolver(finalGraph);
            List<Package> order = resolver.Resolve();

            Console.WriteLine("Package installation order:\n");

            for (int i = 0; i < order.Count; i++)
            {
                Console.WriteLine(i + 1 + ". " + order[i].name + "-" + order[i].version);
            }
        }
        catch (DependencyException ex)
        {
            Console.WriteLine("An error occurred: " + ex.GetType().Name + " - " + ex.Message);
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine("File not found: " + ex.FileName);
        }
    }
}