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
}
