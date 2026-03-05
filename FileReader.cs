using System.Text.RegularExpressions;

public class FileReader
{
    public FileReader()
    {
    }

    public DependencyGraph ReadFile(string filePath)
    {
        DependencyGraph graph = new DependencyGraph();

        string line;
        string packagePattern = @"\bpackage:\b";
        string requiresPattern = @"\brequires:\b";

        StreamReader reader = new StreamReader(filePath);

        while ((line = reader.ReadLine()) != null)
        {
            if (line.Contains("package"))
            {
                FindPackage(line, packagePattern, graph, out Package package1);
                graph.AddPackage(package1);

                while ((line = reader.ReadLine()) != null && line.Contains("requires"))
                {
                    FindPackage(line, requiresPattern, graph, out Package package2);
                    graph.AddPackage(package2);
                    graph.AddDependency(package1, package2);
                }
            }
        }

        return graph;
    }

    private Package FindPackage(string line, string pattern, DependencyGraph graph, out Package package)
    {
        string result = Regex.Replace(line, pattern, string.Empty, RegexOptions.IgnoreCase);
        string[] requiresParts = result.Split(',');
        package = new Package(requiresParts[0], requiresParts[1]);
        return package;
    }
}