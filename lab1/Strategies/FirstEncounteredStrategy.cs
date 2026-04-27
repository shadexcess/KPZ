// <copyright file="FirstEncounteredStrategy.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace lab1;

/// <summary>
/// Implements a dependency resolution strategy that selects the first encountered version
/// of each package in the dependency graph without performing version comparison.
/// </summary>
public class FirstEncounteredStrategy : IResolutionStrategy
{
    /// <summary>
    /// Resolves dependency conflicts by selecting the first encountered
    /// package version for each package in the dependency graph.
    /// </summary>
    /// <param name="graph">The dependency graph containing packages and their relationships.</param>
    /// <returns>
    /// A dictionary where the key is the package name and the value is the first encountered version
    /// of that package in the dependency traversal order.
    /// </returns>
    public Dictionary<string, Package>? ResolveConflicts(DependencyGraph graph)
    {
        Dictionary<string, List<Package>> packagesVersions = ((IResolutionStrategy)this).GetVersionsForEachPackage(graph);

        bool hasConflict = packagesVersions.Any(x => x.Value.Count > 1);

        if (!hasConflict)
        {
            return null;
        }

        Dictionary<string, Package> firstPackages = packagesVersions
            .ToDictionary(x => x.Key, x => x.Value.First());

        return firstPackages;
    }
}