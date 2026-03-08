// <copyright file="DependencyGraph.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

/// <summary>
/// Represents a dependency graph of packages and stores them and their dependency relationships.
/// </summary>
public class DependencyGraph
{
    private Dictionary<Package, List<Package>> dependencies;

    /// <summary>
    /// Initializes a new instance of the <see cref="DependencyGraph"/> class.
    /// </summary>
    public DependencyGraph()
    {
        this.dependencies = new Dictionary<Package, List<Package>>();
    }

    /// <summary>
    /// Adds a package to the dependency graph. If the package already exists, it will not be added again.
    /// </summary>
    /// <param name="package">The package to add to the graph.</param>
    public void AddPackage(Package package)
    {
        if (!this.dependencies.ContainsKey(package))
        {
            this.dependencies.Add(package, new List<Package>());
        }
    }

    /// <summary>
    /// Adds a dependency from one package to another.
    /// </summary>
    /// <param name="from">The package that depends on another package.</param>
    /// <param name="to">The package being depended upon.</param>
    public void AddDependency(Package from, Package to)
    {
        this.dependencies[from].Add(to);
    }

    /// <summary>
    /// Retrieves the list of packages that the specified package depends on.
    /// </summary>
    /// <param name="package">The package whose dependencies are to be retrieved.</param>
    /// <returns>A list of packages that the specified package depends on. Returns an empty list if there are no dependencies.</returns>
    public List<Package> GetDependencies(Package package)
    {
        if (this.dependencies.ContainsKey(package))
        {
            return this.dependencies[package];
        }
        else
        {
            return new List<Package>();
        }
    }

    /// <summary>
    /// Retrieves all packages in the dependency graph.
    /// </summary>
    /// <returns>An enumerable collection of all packages stored in the graph.</returns>
    public IEnumerable<Package> GetAllPackages()
    {
        return this.dependencies.Keys;
    }
}