namespace lab1.Tests;

using lab1;

public class GraphBuilderTests
{
    [Fact]
    public void BuildGraph_ThrowsMissingDependencyException_WhenDependencyPackageMissing()
    {
        List<string> lines = new List<string>
        {
            "package: A, v1.0",
            "requires: B, v1.0"
        };

        GraphBuilder builder = new GraphBuilder();

        Assert.Throws<MissingDependencyException>(() => builder.BuildGraph(lines));
    }

    [Fact]
    public void BuildGraph_ThrowsMissingDependencyException_WhenVersionMismatch()
    {
        List<string> lines = new List<string>
        {
            "package: A, v1.0",
            "package: B, v1.0",
            "requires: A, v1.0",
            "package: C, v2.0",
            "requires: B, v2.0"
        };

        GraphBuilder builder = new GraphBuilder();

        Assert.Throws<MissingDependencyException>(() => builder.BuildGraph(lines));
    }

    [Theory]
    [InlineData("package: ")]
    [InlineData("package: A")]
    [InlineData("package: A, ")]
    [InlineData("package: , v")]
    [InlineData("package: A, 1.0")]
    [InlineData("package: A, v")]
    public void BuildGraph_ThrowsPackageFormatException(string line)
    {
        GraphBuilder builder = new GraphBuilder();
        List<string> lines = new List<string> { line };

        Assert.Throws<PackageFormatException>(() => builder.BuildGraph(lines));
    }

    [Fact]
    public void BuildGraph_CreatesGraphWithPackagesAndDependencies()
    {
        List<string> lines = new List<string>
        {
            "package: A, v1.0",
            "package: B, v1.0",
            "requires: A, v1.0",
            "package: C, v2.0",
            "requires: B, v1.0"
        };

        GraphBuilder builder = new GraphBuilder();

        DependencyGraph graph = builder.BuildGraph(lines);
        IEnumerable<Package> packages = graph.GetAllPackages();

        Assert.Equal(3, packages.Count());
        Assert.Contains(packages, p => p.name == "A" && p.version == "v1.0");
        Assert.Contains(packages, p => p.name == "B" && p.version == "v1.0");
        Assert.Contains(packages, p => p.name == "C" && p.version == "v2.0");

        List<Package> dependenciesB = graph.GetDependencies(new Package("B", "v1.0"));
        Assert.Single(dependenciesB, new Package("A", "v1.0"));

        List<Package> dependenciesC = graph.GetDependencies(new Package("C", "v2.0"));
        Assert.Single(dependenciesC, new Package("B", "v1.0"));
    }

    [Fact]
    public void BuildGraph_AddsPackageWithoutDependencies()
    {
        List<string> lines = new List<string>
        {
            "package: A, v1.0"
        };

        GraphBuilder builder = new GraphBuilder();
        DependencyGraph graph = builder.BuildGraph(lines);

        Assert.Single(graph.GetAllPackages());
        Assert.Empty(graph.GetDependencies(new Package("A", "v1.0")));
    }

    [Fact]
    public void BuildGraph_IgnoresIncorrectLines()
    {
        List<string> lines = new List<string>
        {
            "package: A, v1.0",
            "incorrect line",
            "package: B, v1.0",
            "requires: A, v1.0",
        };

        GraphBuilder builder = new GraphBuilder();
        DependencyGraph graph = builder.BuildGraph(lines);
        IEnumerable<Package> packages = graph.GetAllPackages();

        Assert.Equal(2, packages.Count());

        List<Package> dependencies = graph.GetDependencies(new Package("B", "v1.0"));
        Assert.Single(dependencies);
        Assert.Contains(dependencies, p => p.name == "A" && p.version == "v1.0");
    }

    [Fact]
    public void BuildGraph_IgnoresRequiresWithoutPackage()
    {
        var lines = new List<string>
        {
            "requires: A, v1.0",
            "package: A, v1.0"
        };

        GraphBuilder builder = new GraphBuilder();
        DependencyGraph graph = builder.BuildGraph(lines);

        Assert.Single(graph.GetAllPackages());
    }

    [Fact]
    public void BuildGraph_IgnoresEmptyRequires()
    {
        List<string> lines = new List<string>
        { 
            "package: A, v1.0",
            "requires: ",
            "package: B, v1.0"
        };

        GraphBuilder builder = new GraphBuilder();
        DependencyGraph graph = builder.BuildGraph(lines);
        IEnumerable<Package> packages = graph.GetAllPackages();

        Assert.Equal(2, packages.Count());
    }

    [Fact]
    public void BuildGraph_EmptyInput_ReturnsEmptyGraph()
    {
        List<string> lines = new List<string>();

        GraphBuilder builder = new GraphBuilder();
        DependencyGraph graph = builder.BuildGraph(lines);

        Assert.Empty(graph.GetAllPackages());
    }
}