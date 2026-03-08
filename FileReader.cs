// <copyright file="FileReader.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Text.RegularExpressions;

/// <summary>
/// Responsible for reading a file and building a dependency graph of packages.
/// </summary>
public class FileReader
{
    /// <summary>
    /// Reads a dependency file and constructs a <see cref="DependencyGraph"/> with packages and their dependencies.
    /// </summary>
    /// <param name="filePath">The path to the file containing package definitions and dependencies.</param>
    /// <returns>A <see cref="DependencyGraph"/> representing all packages and their dependency relationships.</returns>
    public DependencyGraph ReadFile(string filePath)
    {
        DependencyGraph graph = new DependencyGraph();

        string line;
        string packagePattern = @"package:";
        string requiresPattern = @"requires:";

        StreamReader reader = new StreamReader(filePath);
        try
        {
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Contains(packagePattern))
                {
                    this.FindPackage(line, packagePattern, out Package package);
                    graph.AddPackage(package);

                    while ((line = reader.ReadLine()) != null && line.Contains(requiresPattern))
                    {
                        this.FindPackage(line, requiresPattern, out Package dependencyPackage);

                        if (dependencyPackage != null)
                        {
                            graph.AddPackage(dependencyPackage);
                            graph.AddDependency(package, dependencyPackage);
                        }
                    }
                }
            }

            return graph;
        }
        finally
        {
            reader.Dispose();
        }
    }

    /// <summary>
    /// Parses a line from the dependency file and extracts a <see cref="Package"/> object if present.
    /// </summary>
    /// <param name="line">The line from the file containing package information.</param>
    /// <param name="pattern">The keyword pattern to remove from the line (e.g., "package:" or "requires:").</param>
    /// <param name="package">The resulting <see cref="Package"/> object, or null if the line is empty after removing the pattern.</param>
    private void FindPackage(string line, string pattern, out Package? package)
    {
        string result = Regex.Replace(line, pattern, string.Empty, RegexOptions.IgnoreCase);
        if (string.IsNullOrEmpty(result.Trim()))
        {
            package = null;
            return;
        }

        string[] parts = result.Split(',',  StringSplitOptions.TrimEntries);
        package = new Package(parts[0], parts[1]);
    }
}