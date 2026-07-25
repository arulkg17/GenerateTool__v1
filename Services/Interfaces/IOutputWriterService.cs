
namespace GeneratorTool.Services.Interfaces;

public interface IOutputWriterService
{
    void CreateDirectory(string path);
    void WriteFile(string filePath, string content);
}
