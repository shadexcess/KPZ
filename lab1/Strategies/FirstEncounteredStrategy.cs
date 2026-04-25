// <copyright file="FirstEncounteredStrategy.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace lab1;

public class FirstEncounteredStrategy : IResolutionStrategy
{
    public Dictionary<string, Package> ResolveConflicts(DependencyGraph graph)
    {
        Dictionary<string, HashSet<Package>> packagesVersions = ((IResolutionStrategy)this).GetVersionsForEachPackage(graph);

        Dictionary<string, Package> firstPackages = packagesVersions
            .ToDictionary(x => x.Key, x => x.Value.First());

        return firstPackages;
    }
}