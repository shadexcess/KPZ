// <copyright file="DependencyException.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace lab1;

using System;

/// <summary>
/// Represents errors that occur during package dependency processing.
/// Serves as the base class for all dependency-related exceptions.
/// </summary>
public class DependencyException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DependencyException"/> class.
    /// </summary>
    public DependencyException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DependencyException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public DependencyException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DependencyException"/> class
    /// with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that caused the current exception.</param>
    public DependencyException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}