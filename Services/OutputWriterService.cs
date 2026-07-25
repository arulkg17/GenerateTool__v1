using GeneratorTool.Services.Interfaces;

namespace GeneratorTool.Services;
public class OutputWriterService : IOutputWriterService
{
    public void CreateDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }


    public void WriteFile(string filePath, string content)
    {

        var directory = Path.GetDirectoryName(filePath);

        if (!string.IsNullOrEmpty(directory))
        {
            CreateDirectory(directory);
        }

        File.WriteAllText(filePath, content);
    }
}
