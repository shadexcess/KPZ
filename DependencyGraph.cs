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
		throw new NotImplementedException();
	}

	public void AddDependency(Package from, Package to)
	{
        throw new NotImplementedException();
    }

    public List<Package> GetDependencies(Package package)
	{
        throw new NotImplementedException();
    }

    public IEnumerable<Package> GetAllPackages()
	{
        throw new NotImplementedException();
    }
}