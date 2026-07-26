using GeneratorTool.Services.Interfaces;
using Serilog;

namespace GeneratorTool.Services;

public class SchemaSelectorService : ISchemaSelectorService
{
    private readonly string _schemaFolder;

    public SchemaSelectorService(string schemaFolder)
    {
        _schemaFolder = schemaFolder;
    }


    public string SelectSchema()
    {
        Log.Information("Starting schema selection.");
        Log.Information("Schema folder: {SchemaFolder}", _schemaFolder);


        var files = Directory.GetFiles(
            _schemaFolder,
            "*.json");


        if (files.Length == 0)
        {
            Log.Error(
                "No schema files found in folder: {SchemaFolder}",
                _schemaFolder);

            throw new Exception(
              "No schema files found");
        }


        Console.WriteLine(
            "Available Schema Files");


        Log.Information(
            "Found {Count} schema files.",
            files.Length);


        for (int i = 0; i < files.Length; i++)
        {
            Console.WriteLine(
              $"{i + 1}. {Path.GetFileName(files[i])}");
        }


        Console.Write(
          "Select schema number: ");


        var input = Console.ReadLine();


        if (!int.TryParse(input, out int index))
        {
            Log.Error(
                "Invalid schema selection input: {Input}",
                input);

            throw new Exception(
              "Invalid selection");
        }


        if (index < 1 || index > files.Length)
        {
            Log.Error(
                "Schema selection index {Index} is invalid. Available count: {Count}",
                index,
                files.Length);

            throw new Exception("Invalid selection");
        }


        var selectedSchema = files[index - 1];

        Log.Information(
            "Schema selected successfully: {SchemaFile}",
            selectedSchema);


        return selectedSchema;
    }
}