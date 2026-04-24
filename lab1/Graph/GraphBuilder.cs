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
    /// <exception cref="MissingDependencyException">Thrown if a package declares a dependency that does not exist in the input lines.</exception>
    public DependencyGraph BuildGraph(IEnumerable<string> lines)
    {
        DependencyGraph graph = new DependencyGraph();

        string packagePattern = @"package:";
        string requiresPattern = @"requires:";

        List<string> lineList = lines.ToList();

        FindAllPackages(lineList, packagePattern, out HashSet<Package> allPackages);

        int i = 0;

        for (; i < lineList.Count; i++)
        {
            string line = lineList[i];

            if (!line.StartsWith(packagePattern))
            {
                continue;
            }

            this.FindPackage(line, packagePattern, out Package package);
            if (package == null)
            {
                throw new PackageFormatException("Package line is empty or invalid: " + line);
            }

            graph.AddPackage(package);

            int j = i + 1;

            while (j < lineList.Count && lineList[j].StartsWith(requiresPattern))
            {
                string dependencyLine = lineList[j].Substring(requiresPattern.Length).Trim();
                this.FindPackage(lineList[j], requiresPattern, out Package dependencyPackage);

                if (dependencyPackage != null)
                {
                    if (!allPackages.Contains(dependencyPackage))
                    {
                        throw new MissingDependencyException("Error in line: " + i + ": Package " +
                            package.name + "-" + package.version + " requires missing package.");
                    }

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
    /// Scans the provided lines and collects all package definitions into a <see cref="HashSet{Package}"/>.
    /// </summary>
    /// <param name="lineList">A list of strings representing lines from the dependency file.</param>
    /// <param name="packagePattern">The keyword pattern used to identify package lines (e.g., "package:").</param>
    /// <param name="allPackages"> The resulting <see cref="HashSet{Package}"/> containing all packages found in the file.</param>
    private void FindAllPackages(List<string> lineList, string packagePattern, out HashSet<Package> allPackages)
    {
        allPackages = new HashSet<Package>();

        foreach (string line in lineList)
        {
            if (line.StartsWith(packagePattern))
            {
                this.FindPackage(line, packagePattern, out Package package);
                allPackages.Add(package);
            }
        }
    }

    /// <summary>
    /// Parses a line from the dependency file and extracts a <see cref="Package"/> object if present.
    /// </summary>
    /// <param name="line">The line from the file containing package information.</param>
    /// <param name="pattern">The keyword pattern to remove from the line (e.g., "package:" or "requires:").</param>
    /// <param name="package">The resulting <see cref="Package"/> object, or null if the line is empty after removing the pattern.</param>
    /// <exception cref="PackageFormatException">
    /// Thrown if the package line is incorrectly formatted, the package name is missing, or the package version is invalid.
    /// </exception>
    private void FindPackage(string line, string pattern, out Package? package)
    {
        string result = Regex.Replace(line, pattern, string.Empty, RegexOptions.IgnoreCase);
        if (string.IsNullOrEmpty(result.Trim()))
        {
            package = null;
            return;
        }

        string[] parts = result.Split(',', StringSplitOptions.TrimEntries);

        if (parts.Length < 2)
        {
            throw new PackageFormatException("Package line is invalid (expected 'name,version'): " + line);
        }

        string name = parts[0];
        string version = parts[1];

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new PackageFormatException("Package name is missing in line " + line);
        }

        if (!this.IsVersionValid(version))
        {
            throw new PackageFormatException("Package version is invalid for package " + name + " in line " + line);
        }

        package = new Package(name, version);
    }

    /// <summary>
    /// Determines whether a version string is valid according to the expected format.
    /// </summary>
    /// <param name="version">The version string to validate (e.g., "v1.0.3").</param>
    /// <returns>
    /// <c>true</c> if the version string starts with 'v' followed by numbers separated by dots; otherwise, <c>false</c>.
    /// </returns>
    private bool IsVersionValid(string version)
    {
        string pattern = @"^v\d+(\.\d+)*$";
        return Regex.IsMatch(version, pattern);
    }
}