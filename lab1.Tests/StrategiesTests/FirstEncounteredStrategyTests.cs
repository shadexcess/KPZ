namespace lab1.Tests;

using lab1;

public class FirstEncounteredVersionStrategyTests
{
    [Fact]
    public void ResolveConflicts_ReturnsNull_WhenNoVersionConflicts()
    {
        DependencyGraph graph = TestGraphBuilder.BuildGraphNoConflictingVersions();
        IResolutionStrategy strategy = new FirstEncounteredStrategy();
        var result = strategy.ResolveConflicts(graph);

        Assert.Null(result);
    }

    [Fact]
    public void ResolveConflicts_ReturnsFirstEncounteredPackageVersion_WhenVersionConflict()
    {
        DependencyGraph graph = TestGraphBuilder.BuildGraphConflictingVersions();
        IResolutionStrategy strategy = new FirstEncounteredStrategy();
        var result = strategy.ResolveConflicts(graph);

        Assert.Equal("v1.0", result["A"].version);
    }
}