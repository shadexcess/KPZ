// <copyright file="HighestVersionStrategy.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace lab1;

/// <summary>
/// Implements a dependency resolution strategy that selects the highest
/// available version of each package from the dependency graph.
/// </summary>
public class HighestVersionStrategy : IResolutionStrategy
{
    /// <summary>
    /// Resolves dependency conflicts by choosing the highest version of each package.
    /// </summary>
    /// <param name="graph"> The dependency graph containing packages and their relationships.</param>
    /// <returns>
    /// A dictionary where the key is the package name and the value is the highest available version of that package.
    /// </returns>
    public Dictionary<string, Package>? ResolveConflicts(DependencyGraph graph)
    {
        Dictionary<string, List<Package>> packagesVersions = ((IResolutionStrategy)this).GetVersionsForEachPackage(graph);

        bool hasConflict = packagesVersions.Any(x => x.Value.Count > 1);

        if (!hasConflict)
        {
            return null;
        }

        var maxPackages = packagesVersions.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value
                .OrderByDescending(p => new Version(p.version.TrimStart('v')))
                .First());

        return maxPackages;
    }
}