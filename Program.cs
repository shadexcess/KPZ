public class Program
{
    private static void Main()
    {
        string filePath = "..\\file.txt";

        FileReader reader = new FileReader();
        DependencyGraph graph = reader.ReadFile(filePath);

        DependencyResolver resolver = new DependencyResolver(graph);
        List<Package> order = resolver.Resolve();

        foreach (Package package in order)
        {
            Console.WriteLine(package.Name + "-" + package.Version);
        }
    }
}