using GeneratorTool.Models;

namespace GeneratorTool.Services.Interfaces;

public interface ISchemaReaderService
{
    TableSchema Read(string filePath);
}
