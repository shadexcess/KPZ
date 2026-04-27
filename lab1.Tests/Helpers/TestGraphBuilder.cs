namespace lab1.Tests;

using lab1;

public static class TestGraphBuilder
{
    public static DependencyGraph BuildGraphNoConflictingVersions()
    {
        Package packageA = new Package("A", "v1.0");
        Package packageB = new Package("B", "v1.0");

        DependencyGraph graph = new DependencyGraph();
        graph.AddPackage(packageA);
        graph.AddPackage(packageB);
        graph.AddDependency(packageA, packageB);

        return graph;
    }

    public static DependencyGraph BuildGraphConflictingVersions()
    {
        Package packageA = new Package("A", "v1.0");
        Package packageB = new Package("B", "v1.0");
        Package packageC = new Package("C", "v1.0");
        Package packageADup = new Package("A", "v2.0");

        DependencyGraph graph = new DependencyGraph();
        graph.AddPackage(packageA);
        graph.AddPackage(packageB);
        graph.AddPackage(packageC);
        graph.AddPackage(packageADup);
        graph.AddDependency(packageB, packageA);
        graph.AddDependency(packageC, packageADup);

        return graph;
    }
}