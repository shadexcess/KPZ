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
        HashSet<Package> visited = new HashSet<Package>();
        Stack<Package> stack = new Stack<Package>();
        IEnumerable<Package> nodes = graph.GetAllPackages();

        foreach (Package node in nodes)
        {
            if (!visited.Contains(node))
            {
                DFS(node, visited, stack);
			}
		}

        List<Package> packagesSorted = new List<Package>();
		while (stack.Count != 0)
		{
			packagesSorted.Add(stack.Pop());
		}

        return packagesSorted;
	}

	private void DFS(Package node, HashSet<Package> visited, Stack<Package> stack)
	{
		visited.Add(node);
		List<Package> neighbors = graph.GetDependencies(node);

        foreach (Package neighbor in neighbors)
		{
			if (!visited.Contains(neighbor))
			{
                DFS(neighbor, visited, stack);
			}
		}

		stack.Push(node);
	}
}