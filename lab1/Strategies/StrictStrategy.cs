// <copyright file="StrictStrategy.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace lab1;

/// <summary>
/// Implements a strict dependency resolution strategy that does not allow
/// multiple versions of the same package in the dependency graph.
/// </summary>
public class StrictStrategy : IResolutionStrategy
{
    /// <summary>
    /// Resolves dependencies by validating that no version conflicts exist.
    /// </summary>
    /// <param name="graph">The dependency graph containing packages and their relationships.</param>
    /// <returns>
    /// A dictionary where the key is the package name and the value is the single resolved package version.
    /// </returns>
    /// <exception cref="VersionConflictException">
    /// Thrown when multiple versions of the same package are found in the dependency graph.
    /// </exception>
    public Dictionary<string, Package>? ResolveConflicts(DependencyGraph graph)
    {
        Dictionary<string, HashSet<Package>> packagesVersions = ((IResolutionStrategy)this).GetVersionsForEachPackage(graph);

        foreach (var kvp in packagesVersions)
        {
            if (kvp.Value.Count > 1)
            {
                throw new VersionConflictException("Version conflict detected for package " + kvp.Key);
            }
        }

        return null;
    }
}