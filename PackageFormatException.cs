// <copyright file="PackageFormatException.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace lab1;

using System;

/// <summary>
/// Represents an error that occurs when a package line does not conform to the expected format.
/// </summary>
public class PackageFormatException : DependencyException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PackageFormatException"/> class.
    /// </summary>
    public PackageFormatException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PackageFormatException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public PackageFormatException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PackageFormatException"/> class
    /// with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that caused the current exception.</param>
    public PackageFormatException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}