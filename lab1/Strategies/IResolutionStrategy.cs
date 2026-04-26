// <copyright file="IResolutionStrategy.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace lab1;

/// <summary>
/// Defines a strategy for resolving version conflicts between packages in a dependency graph.
/// </summary>
public interface IResolutionStrategy
{
    /// <summary>
    /// Resolves version conflicts between packages in the specified dependency graph.
    /// </summary>
    /// <param name="graph">The dependency graph containing packages and their relationships.</param>
    /// <returns>
    /// A dictionary where the key is the package name and the value is the selected package version after conflict resolution.
    /// </returns>
    Dictionary<string, Package>? ResolveConflicts(DependencyGraph graph);

    /// <summary>
    /// Retrieves all versions of each package present in the dependency graph.
    /// </summary>
    /// <param name="graph">The dependency graph to analyze.</param>
    /// <returns>
    /// A dictionary where the key is the package name and the value is a set of all versions of that package found in the graph.
    /// </returns>
    public Dictionary<string, HashSet<Package>> GetVersionsForEachPackage(DependencyGraph graph)
    {
        var packagesVersions = new Dictionary<string, HashSet<Package>>();

        void Add(Package p)
        {
            if (!packagesVersions.TryGetValue(p.name, out var set))
            {
                set = new HashSet<Package>();
                packagesVersions[p.name] = set;
            }

            set.Add(p);
        }

        foreach (Package package in graph.GetAllPackages())
        {
            Add(package);

            foreach (Package dependency in graph.GetDependencies(package))
            {
                Add(dependency);
            }
        }

        return packagesVersions;
    }
}