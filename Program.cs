// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

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

            FileReader reader = new FileReader();
            DependencyGraph graph = reader.ReadFile(filePath);

            DependencyResolver resolver = new DependencyResolver(graph);
            List<Package> order = resolver.Resolve();

            Console.WriteLine("Package installation order:\n");

            foreach (Package package in order)
            {
                Console.WriteLine(package.name + "-" + package.version);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
}