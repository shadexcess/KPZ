using System;

public class DependencyGraph
{
    private Dictionary<Package, List<Package>> dependencies;

	public DependencyGraph()
	{
        dependencies = new Dictionary<Package, List<Package>>();
	}

	public void AddPackage(Package package)
	{
		if (!dependencies.ContainsKey(package))
		{
            dependencies.Add(package, new List<Package>());
        }
    }

	public void AddDependency(Package from, Package to)
	{
		dependencies[from].Add(to);
    }

    public List<Package> GetDependencies(Package package)
	{
		if (dependencies.ContainsKey(package))
		{
			return dependencies[package];
		}

		else
		{
			return new List<Package>();
		}
    }

    public IEnumerable<Package> GetAllPackages()
	{
		return dependencies.Keys;
    }
}