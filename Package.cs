// <copyright file="Package.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace lab1;

/// <summary>
/// Represents a package with a name and version.
/// </summary>
/// <param name="name">The name of the package.</param>
/// <param name="version">The version of the package.</param>
public record Package(string name, string version);