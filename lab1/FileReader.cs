// <copyright file="FileReader.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace lab1;

/// <summary>
/// Provides methods for reading files line by line.
/// </summary>
public class FileReader
{
    /// <summary>
    /// Reads all lines from the specified file.
    /// </summary>
    /// <param name="filePath">The path to the file to read.</param>
    /// <returns>An <see cref="IEnumerable{String}"/> containing all lines of the file.</returns>
    public IEnumerable<string> ReadLines(string filePath)
    {
        return File.ReadLines(filePath);
    }
}