// <copyright file="StrategyFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace lab1;

/// <summary>
/// Factory class responsible for creating instances of <see cref="IResolutionStrategy"/> based on a provided configuration string.
/// </summary>
public class StrategyFactory
{
    /// <summary>
    /// Creates a resolution strategy based on the specified input string.
    /// </summary>
    /// <param name="line">
    /// The strategy identifier. Supported values: "strict", "highest", "first". </param>
    /// <returns>
    /// An instance of a class that implements <see cref="IResolutionStrategy"/>.
    /// If the input is null or unrecognized, a <see cref="StrictStrategy"/> is returned by default.
    /// </returns>
    public static IResolutionStrategy SetStrategy(string? line)
    {
        return line switch
        {
            "strict" => new StrictStrategy(),
            "highest" => new HighestVersionStrategy(),
            "first" => new FirstEncounteredStrategy(),
            _ => new StrictStrategy(),
        };
    }
}