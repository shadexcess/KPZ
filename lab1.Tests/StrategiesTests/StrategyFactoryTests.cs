namespace lab1.Tests;

using lab1;

public class StrategyFactoryTests
{
    [Theory]
    [InlineData("strict", typeof(StrictStrategy))]
    [InlineData("first", typeof(FirstEncounteredStrategy))]
    [InlineData("highest", typeof(HighestVersionStrategy))]
    [InlineData(null, typeof(StrictStrategy))]
    public void SetStrategy_CreatesCorrectStrategy(string input, Type expected)
    {
        var result = StrategyFactory.SetStrategy(input);
        Assert.IsType(expected, result);
    }
}