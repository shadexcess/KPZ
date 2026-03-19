// <copyright file="CyclicDependencyException.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace lab1;

using System;

public class CyclicDependencyException : Exception
{
    public CyclicDependencyException()
    {
    }

    public CyclicDependencyException(string message)
        : base(message)
    {
    }

    public CyclicDependencyException(string message,  Exception innerException)
        : base(message, innerException)
    {
    }
}