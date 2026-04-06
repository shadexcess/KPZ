namespace lab1.Tests;

using lab1;

public class DependencyResolverTests
{
    [Fact]
    public void Resolve_ThrowsCyclicDependencyException()
    {
        Package packageA = new Package("A", "v1.0");
        Package packageB = new Package("B", "v1.0");

        DependencyGraph graph = new DependencyGraph();
        graph.AddPackage(packageA);
        graph.AddPackage(packageB);
        graph.AddDependency(packageA, packageB);
        graph.AddDependency(packageB, packageA);

        DependencyResolver resolver = new DependencyResolver(graph);

        Assert.Throws<CyclicDependencyException>(() => resolver.Resolve());
    }

    [Fact]
    public void Resolve_ReturnsCorrectOrder()
    {
        Package packageA = new Package("A", "v1.0");
        Package packageB = new Package("B", "v1.0");
        Package packageC = new Package("C", "v1.0");

        DependencyGraph graph = new DependencyGraph();
        graph.AddPackage(packageA);
        graph.AddPackage(packageB);
        graph.AddPackage(packageC);
        graph.AddDependency(packageB, packageA);
        graph.AddDependency(packageC, packageB);

        DependencyResolver resolver = new DependencyResolver(graph);

        List<Package> order = resolver.Resolve();

        Assert.True(order.IndexOf(packageA) < order.IndexOf(packageB));
        Assert.True(order.IndexOf(packageB) < order.IndexOf(packageC));
    }

    [Fact]
    public void Resolve_ReturnsCorrectOrderWhenMultipleDependencies()
    {
        Package packageA = new Package("A", "v1.0");
        Package packageB = new Package("B", "v1.0");
        Package packageC = new Package("C", "v1.0");

        DependencyGraph graph = new DependencyGraph();
        graph.AddPackage(packageA);
        graph.AddPackage(packageB);
        graph.AddPackage(packageC);
        graph.AddDependency(packageC, packageA);
        graph.AddDependency(packageC, packageB);

        DependencyResolver resolver = new DependencyResolver(graph);

        List<Package> order = resolver.Resolve();

        Assert.True(order.IndexOf(packageA) < order.IndexOf(packageC));
        Assert.True(order.IndexOf(packageB) < order.IndexOf(packageC));
    }

    [Fact]
    public void Resolve_ReturnsEmptyListIfGraphIsEmpty()
    {
        DependencyGraph graph = new DependencyGraph();
        DependencyResolver resolver = new DependencyResolver(graph);

        List<Package> order = resolver.Resolve();

        Assert.Empty(order);
    }
}