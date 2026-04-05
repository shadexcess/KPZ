namespace lab1.Tests;

using lab1;

public class DependencyGraphTests
{
    [Fact]
    public void AddPackage_AddsPackageToGraph()
    {
        DependencyGraph graph = new DependencyGraph();
        Package packageA = new Package("A", "v1.0");

        graph.AddPackage(packageA);

        Assert.Contains(packageA, graph.GetAllPackages());
    }

    [Fact]
    public void AddPackage_DoesNotDuplicatePackage()
    {
        DependencyGraph graph = new DependencyGraph();
        Package packageA = new Package("A", "v1.0");
        Package packageADuplicate = new Package("A", "v1.0");

        graph.AddPackage(packageA);
        graph.AddPackage(packageADuplicate);

        Assert.Single(graph.GetAllPackages());
    }

    [Fact]
    public void AddDependency_AddsDependencyToGraph()
    {
        DependencyGraph graph = new DependencyGraph();
        Package packageA = new Package("A", "v1.0");
        Package packageB = new Package("B", "v1.0");

        graph.AddPackage(packageA);
        graph.AddPackage(packageB);
        graph.AddDependency(packageA, packageB);

        Assert.Contains(packageB, graph.GetDependencies(packageA));
    }

    [Fact]
    public void AddDependency_DoesNotDuplicateDependencies()
    {
        DependencyGraph graph = new DependencyGraph();
        Package packageA = new Package("A", "v1.0");
        Package packageB = new Package("B", "v1.0");

        graph.AddPackage(packageA);
        graph.AddPackage(packageB);
        graph.AddDependency(packageA, packageB);
        graph.AddDependency(packageA, packageB);

        Assert.Single(graph.GetDependencies(packageA));
    }

    [Fact]
    public void AddDependency_DoesNotAllowSelfDependency()
    {
        DependencyGraph graph = new DependencyGraph();
        Package packageA = new Package("A", "v1.0");

        graph.AddPackage(packageA);
        graph.AddDependency(packageA, packageA);

        Assert.Empty(graph.GetDependencies(packageA));
    }

    [Fact]
    public void GetDependencies_ReturnsDependenciesForPackage()
    {
        DependencyGraph graph = new DependencyGraph();
        Package packageA = new Package("A", "v1.0");
        Package packageB = new Package("B", "v1.0");

        graph.AddPackage(packageA);
        graph.AddPackage(packageB);
        graph.AddDependency(packageA, packageB);

        List<Package> dependencies = graph.GetDependencies(packageA);

        Assert.Single(dependencies, packageB);
    }

    [Fact]
    public void GetDependencies_ReturnsEmptyListForPackageWithoutDependencies()
    {
        DependencyGraph graph = new DependencyGraph();
        Package packageA = new Package("A", "v1.0");

        graph.AddPackage(packageA);
        List<Package> dependencies = graph.GetDependencies(packageA);

        Assert.Empty(dependencies);
    }

    [Fact]
    public void GetDependencies_ReturnsEmptyListForNonExistentPackage()
    {
        DependencyGraph graph = new DependencyGraph();
        Package packageA = new Package("A", "v1.0");

        List<Package> dependencies = graph.GetDependencies(packageA);

        Assert.Empty(dependencies);
    }

    [Fact]
    public void GetAllPackages_ReturnsAllAddedPackages()
    {
        DependencyGraph graph = new DependencyGraph();
        Package packageA = new Package("A", "v1.0");
        Package packageB = new Package("B", "v1.0");

        graph.AddPackage(packageA);
        graph.AddPackage(packageB);
        graph.AddDependency(packageA, packageB);
        IEnumerable<Package> packages = graph.GetAllPackages();

        Assert.Equal(2, packages.Count());
        Assert.Contains(packageA, packages);
        Assert.Contains(packageB, packages);
    }

    [Fact]
    public void GetAllPackages_ReturnsEmptyListForEmptyGraph()
    {
        DependencyGraph graph = new DependencyGraph();
        IEnumerable<Package> packages = graph.GetAllPackages();

        Assert.Empty(packages);
    }
}