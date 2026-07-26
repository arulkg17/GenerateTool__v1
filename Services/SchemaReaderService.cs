using GeneratorTool.Models;
using GeneratorTool.Services.Interfaces;
using Serilog;
using System.Text.Json;

namespace GeneratorTool.Services;

public class SchemaReaderService : ISchemaReaderService
{
    public TableSchema Read(string filePath)
    {
        Log.Information(
            "Reading schema file: {FilePath}",
            filePath);


        if (!File.Exists(filePath))
        {
            Log.Error("Schema file does not exist: {FilePath}",filePath);

            throw new FileNotFoundException("Schema file not found",
                filePath);
        }

        var json = File.ReadAllText(filePath);

        Log.Information("Schema JSON loaded successfully.");


        var schema = JsonSerializer.Deserialize<TableSchema>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        if (schema == null)
        {
            Log.Error("Schema deserialization returned null. File: {FilePath}",filePath);

            throw new Exception("Invalid schema");
        }

        Log.Information("Schema deserialized successfully. TableName: {TableName}",
            schema.TableName);
        return schema;
    }
}
