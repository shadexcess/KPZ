// <copyright file="DependencyResolver.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;

/// <summary>
/// Resolves the installation order of packages based on their dependencies.
/// Ensures that packages are installed in the correct order and detects dependency cycles.
/// </summary>
public class DependencyResolver
{
    private readonly DependencyGraph graph;

    /// <summary>
    /// Initializes a new instance of the <see cref="DependencyResolver"/> class with a dependency graph.
    /// </summary>
    /// <param name="graph">The <see cref="DependencyGraph"/> containing packages and their dependencies.</param>
    public DependencyResolver(DependencyGraph graph)
    {
        this.graph = graph;
    }

    /// <summary>
    /// Resolves the packages in an order that satisfies all dependencies.
    /// </summary>
    /// <returns>A list of <see cref="Package"/> objects sorted in dependency order.</returns>
    /// <exception cref="Exception">Thrown if a dependency cycle is detected in the graph.</exception>
    public List<Package> Resolve()
    {
        HashSet<Package> visited = new HashSet<Package>();
        Stack<Package> stack = new Stack<Package>();
        IEnumerable<Package> packages = this.graph.GetAllPackages();
        HashSet<Package> currentPath = new HashSet<Package>();

        foreach (Package package in packages)
        {
            if (!visited.Contains(package))
            {
                this.DepthFirstSearch(package, visited, stack, currentPath);
            }
        }

        List<Package> packagesSorted = new List<Package>();
        while (stack.Count != 0)
        {
            packagesSorted.Add(stack.Pop());
        }

        packagesSorted.Reverse();

        return packagesSorted;
    }

    /// <summary>
    /// Performs a depth-first search to visit packages and detect cycles.
    /// </summary>
    /// <param name="currentPackage">The current <see cref="Package"/> being visited.</param>
    /// <param name="visited">A set of packages that have already been visited.</param>
    /// <param name="stack">A stack used to determine the installation order.</param>
    /// <param name="currentPath">A set representing the current path to detect dependency cycles.</param>
    /// <exception cref="Exception">Thrown if a dependency cycle is detected during traversal.</exception>
    private void DepthFirstSearch(Package currentPackage, HashSet<Package> visited, Stack<Package> stack, HashSet<Package> currentPath)
    {
        visited.Add(currentPackage);
        currentPath.Add(currentPackage);

        List<Package> dependencyPackages = this.graph.GetDependencies(currentPackage);

        foreach (Package dependency in dependencyPackages)
        {
            if (currentPath.Contains(dependency))
            {
                throw new Exception("Dependency cycle detected");
            }

            if (!visited.Contains(dependency))
            {
                this.DepthFirstSearch(dependency, visited, stack, currentPath);
            }
        }

        currentPath.Remove(currentPackage);
        stack.Push(currentPackage);
    }
}