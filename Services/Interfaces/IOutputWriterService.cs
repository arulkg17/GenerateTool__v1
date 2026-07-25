
using GeneratorTool.Models;
namespace GeneratorTool.Services.Interfaces;

public interface IOutputWriterService
{
    void CreateDirectory(string path);
    GeneratedFile WriteFile(string filePath, string content);
}
