namespace lab1.Tests;

using lab1;

public class StrictStrategyTests
{
    [Fact]
    public void ResolveConflicts_ReturnsNull_WhenNoVersionConflicts()
    {
        DependencyGraph graph = TestGraphBuilder.BuildGraphNoConflictingVersions();
        IResolutionStrategy strategy = new StrictStrategy();
        var result = strategy.ResolveConflicts(graph);

        Assert.Null(result);
    }

    [Fact]
    public void ResolveConflicts_ThrowsVersionConflictException_WhenVersionConflict()   
    {
        DependencyGraph graph = TestGraphBuilder.BuildGraphConflictingVersions();
        IResolutionStrategy strategy = new StrictStrategy();

        Assert.Throws<VersionConflictException>(() => strategy.ResolveConflicts(graph));
    }
}