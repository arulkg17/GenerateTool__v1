using GeneratorTool.Models;
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

    public GeneratedFile WriteFile(string filePath, string content)
    {

        var directory = Path.GetDirectoryName(filePath);

        if (!string.IsNullOrEmpty(directory))
        {
            CreateDirectory(directory);
        }

        File.WriteAllText(filePath, content);

        var fileInfo = new FileInfo(filePath);

        return new GeneratedFile
        {
            FileName = fileInfo.Name,
            FilePath = fileInfo.FullName,
            FileSize = fileInfo.Length,
            GeneratedDate = DateTime.Now
        };

    }

}
