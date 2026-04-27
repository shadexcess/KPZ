namespace lab1.Tests;

using lab1;

public class HighestVersionStrategyTests
{
    [Fact]
    public void ResolveConflicts_ReturnsHighestPackageVersion_WhenVersionConflict()
    {
        DependencyGraph graph = TestGraphBuilder.BuildGraphConflictingVersions();
        IResolutionStrategy strategy = new HighestVersionStrategy();
        var result = strategy.ResolveConflicts(graph);

        Assert.Equal("v2.0", result["A"].version);
    }
}