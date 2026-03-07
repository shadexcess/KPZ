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
        string packagePattern = @"package:";
        string requiresPattern = @"requires:";

        StreamReader reader = new StreamReader(filePath);
        try
        {
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Contains(packagePattern))
                {
                    FindPackage(line, packagePattern, out Package package1);
                    graph.AddPackage(package1);

                    while ((line = reader.ReadLine()) != null && line.Contains(requiresPattern))
                    {
                        FindPackage(line, requiresPattern, out Package package2);

                        if (package2 != null)
                        {
                            graph.AddPackage(package2);
                            graph.AddDependency(package1, package2);
                        }
                    }
                }
            }

            return graph;
        }
        finally
        {
            reader.Dispose();
        }
    }

    private void FindPackage(string line, string pattern, out Package? package)
    {
        string result = Regex.Replace(line, pattern, string.Empty, RegexOptions.IgnoreCase);
        if (string.IsNullOrEmpty(result.Trim()))
        {
            package = null;
            return;
        }

        string[] parts = result.Split(',',  StringSplitOptions.TrimEntries);
        package = new Package(parts[0], parts[1]);
    }
}