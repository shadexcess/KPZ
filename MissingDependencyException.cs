// <copyright file="MissingDependencyException.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace lab1;

using System;

/// <summary>
/// Represents an error that occurs when a package declares a dependency on another package that is missing from the dependency graph.
/// </summary>
public class MissingDependencyException : DependencyException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MissingDependencyException"/> class.
    /// </summary>
    public MissingDependencyException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MissingDependencyException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public MissingDependencyException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MissingDependencyException"/> class
    /// with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that caused the current exception.</param>
    public MissingDependencyException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}