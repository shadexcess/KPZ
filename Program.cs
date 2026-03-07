public class Program
{
    private static void Main(string[] args)
    {
        try
        {
            string filePath = args[0];

            FileReader reader = new FileReader();
            DependencyGraph graph = reader.ReadFile(filePath);

            DependencyResolver resolver = new DependencyResolver(graph);
            List<Package> order = resolver.Resolve();

            foreach (Package package in order)
            {
                Console.WriteLine(package.Name + "-" + package.Version);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
}