using GeneratorTool.Models;
using GeneratorTool.Services.Interfaces;
using System.Text.Json;

namespace GeneratorTool.Services;

public class SchemaReaderService : ISchemaReaderService
{
    public TableSchema Read(string filePath)
    {
        var json = File.ReadAllText(filePath);

        var schema = JsonSerializer.Deserialize<TableSchema>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        if (schema == null)
        {
            throw new Exception("Invalid schema");
        }
        return schema;
    }
}
