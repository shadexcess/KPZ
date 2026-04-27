namespace lab1.Tests.Strategies;

using lab1;

public class ConfigurationReaderTests
{
    private string CreateTempJson(string text)
    {
        var path = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".json");
        File.WriteAllText(path, text);
        return path;
    }

    [Fact]
    public void GetResolutionPolicy_FileDoesNotExist_ReturnsNull()
    {
        var result = ConfigurationReader.GetResolutionPolicy("non_existing_file.json");
        Assert.Null(result);
    }

    [Fact]
    public void GetResolutionPolicy_InvalidConfiguration_ReturnsNull()
    {
        string? path = null;

        try
        {
            path = CreateTempJson("{ \"incorrect_key\": \"strict\" }");
            var result = ConfigurationReader.GetResolutionPolicy(path);
            Assert.Null(result);
        }
        finally
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }

    [Fact]
    public void GetResolutionPolicy_ValidPolicy_ReturnsValue()
    {
        string path = null;

        try
        {
            path = CreateTempJson("{ \"resolution_policy\": \"strict\" }");
            var result = ConfigurationReader.GetResolutionPolicy(path);
            Assert.Equal("strict", result);
        }
        finally
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}