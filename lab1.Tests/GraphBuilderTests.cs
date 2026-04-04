namespace lab1.Tests;

using lab1;

public class GraphBuilderTests
{
    [Fact]
    public void BuildGraph_ThrowsMissingDependencyException()
    {
        List<string> lines = new List<string>
        {
            "package:A,v1.0",
            "requires:B,v1.0"
        };

        GraphBuilder builder = new GraphBuilder();

        Assert.Throws<MissingDependencyException>(() => builder.BuildGraph(lines));
    }

    [Theory]
    [InlineData("package:")]
    [InlineData("package: name, ")]
    [InlineData("package: , version")]
    [InlineData("package: name,1.0")]
    [InlineData("package: name,v")]
    public void BuildGraph_ThrowsPackageFormatException(string line)
    {
        GraphBuilder builder = new GraphBuilder();
        List<string> lines = new List<string> { line };

        Assert.Throws<PackageFormatException>(() => builder.BuildGraph(lines));
    }
}
