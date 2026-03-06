public class Program
{
    private static void Main(string[] args)
    {
        string filePath = args[0];

        FileReader reader = new FileReader();
        DependencyGraph graph = reader.ReadFile(filePath);

        //DependencyResolver resolver = new DependencyResolver(graph);
        //List<Package> order = resolver.Resolve();

        //foreach (Package package in order)
        //{
        //    Console.WriteLine(package.Name + "-" + package.Version);
        //}



        // to test if it works
        IEnumerable<Package> list = graph.GetAllPackages();
        foreach (var element in list)
        {
            Console.WriteLine(element);
        }
    }
}