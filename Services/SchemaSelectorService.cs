using GeneratorTool.Services.Interfaces;

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

        var files = Directory.GetFiles(
            _schemaFolder,
            "*.json");


        if (files.Length == 0)
        {
            throw new Exception(
              "No schema files found");
        }


        Console.WriteLine(
            "Available Schema Files");


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
            throw new Exception(
              "Invalid selection");
        }


        if (index < 1 || index > files.Length)
        {
           throw new Exception("Invalid selection");
        }

        return files[index - 1];
    }
}
