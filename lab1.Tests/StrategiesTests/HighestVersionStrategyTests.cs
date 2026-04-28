namespace lab1.Tests;

using lab1;

public class HighestVersionStrategyTests
{
    [Fact]
    public void ResolveConflicts_ReturnsNull_WhenNoVersionConflicts()
    {
        DependencyGraph graph = TestGraphBuilder.BuildGraphNoConflictingVersions();
        IResolutionStrategy strategy = new HighestVersionStrategy();
        var result = strategy.ResolveConflicts(graph);

        Assert.Null(result);
    }

    [Fact]
    public void ResolveConflicts_ReturnsHighestPackageVersion_WhenVersionConflict()
    {
        DependencyGraph graph = TestGraphBuilder.BuildGraphConflictingVersions();
        IResolutionStrategy strategy = new HighestVersionStrategy();
        var result = strategy.ResolveConflicts(graph);

        Assert.Equal("v2.0", result["A"].version);
    }
}