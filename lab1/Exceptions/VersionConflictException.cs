// <copyright file="VersionConflictException.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace lab1;

using System;

/// <summary>
/// Represents an exception that is thrown when a version conflict is detected 
/// between different versions of the same package in a dependency graph.
/// </summary>
public class VersionConflictException : DependencyException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VersionConflictException"/> class.
    /// </summary>
    public VersionConflictException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="VersionConflictException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public VersionConflictException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="VersionConflictException"/> class
    /// with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that caused the current exception.</param>
    public VersionConflictException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}