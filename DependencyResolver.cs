using System;

public class DependencyResolver
{
	private readonly DependencyGraph graph;

	public DependencyResolver(DependencyGraph graph)
	{
		this.graph = graph;
	}

	public List<Package> Resolve()
	{
		List<Package> packages = new List<Package>();

		// topological sort

		return packages;
	}
}