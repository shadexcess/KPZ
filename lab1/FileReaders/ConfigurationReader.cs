// <copyright file="ConfigurationReader.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace lab1;

using System.Text.Json;

/// <summary>
/// Provides functionality for reading application configuration from a JSON file.
/// </summary>
public class ConfigurationReader
{
    /// <summary>
    /// Reads the resolution strategy from the configuration file.
    /// </summary>
    /// <param name="filePath">The path to the file containing the resolution strategy.</param>
    /// <returns>
    /// A string representing the resolution strategy if found in the configuration file; otherwise, null.
    /// </returns>
    public static string? GetResolutionPolicy(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return null;
        }

        string json = File.ReadAllText(filePath);
        using JsonDocument document = JsonDocument.Parse(json);

        if (document.RootElement.TryGetProperty("resolution_policy", out JsonElement resolutionPolicy))
        {
            return resolutionPolicy.GetString();
        }

        return null;
    }
}