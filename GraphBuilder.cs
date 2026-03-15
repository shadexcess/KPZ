// <copyright file="GraphBuilder.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace lab1;

using System.Text.RegularExpressions;

/// <summary>
/// Responsible for building a dependency graph from lines of a dependency file.
/// </summary>
public class GraphBuilder
{
    /// <summary>
    /// Constructs a <see cref="DependencyGraph"/> with packages and their dependencies.
    /// </summary>
    /// <param name="lines">An <see cref="IEnumerable{String}"/> containing the lines of the dependency file.</param>
    /// <returns>A <see cref="DependencyGraph"/> representing all packages and their dependency relationships.</returns>
    public DependencyGraph BuildGraph(IEnumerable<string> lines)
    {
        DependencyGraph graph = new DependencyGraph();

        string packagePattern = @"package:";
        string requiresPattern = @"requires:";

        var lineList = lines.ToList();

        int i = 0;

        for (; i < lineList.Count; i++)
        {
            string line = lineList[i];

            if (!line.Contains(packagePattern))
            {
                continue;
            }

            this.FindPackage(line, packagePattern, out Package package);
            graph.AddPackage(package);

            int j = i + 1;

            while (j < lineList.Count && lineList[j].Contains(requiresPattern))
            {
                this.FindPackage(lineList[j], requiresPattern, out Package dependencyPackage);
                if (dependencyPackage != null)
                {
                    graph.AddPackage(dependencyPackage);
                    graph.AddDependency(package, dependencyPackage);
                }

                j++;
            }

            i = j - 1;
        }

        return graph;
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

        string[] parts = result.Split(',', StringSplitOptions.TrimEntries);
        package = new Package(parts[0], parts[1]);
    }
}