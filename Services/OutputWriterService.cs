using GeneratorTool.Models;
using GeneratorTool.Services.Interfaces;
using Serilog;

namespace GeneratorTool.Services;
public class OutputWriterService : IOutputWriterService
{
    public void CreateDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            Log.Information(
                "Creating output directory: {Directory}",
                path);

            Directory.CreateDirectory(path);

            Log.Information(
                "Directory created successfully: {Directory}",
                path);
        }
        else
        {
            Log.Information(
                "Directory already exists: {Directory}",
                path);
        }
    }

    public GeneratedFile WriteFile(string filePath, string content)
    {
        try
        {
            Log.Information(
                "Writing generated file: {FilePath}",
                filePath);


            var directory = Path.GetDirectoryName(filePath);


            if (!string.IsNullOrEmpty(directory))
            {
                CreateDirectory(directory);
            }


            File.WriteAllText(filePath, content);


            var fileInfo = new FileInfo(filePath);


            Log.Information(
                "File generated successfully. Name: {FileName}, Size: {FileSize} bytes",
                fileInfo.Name,
                fileInfo.Length);


            return new GeneratedFile
            {
                FileName = fileInfo.Name,
                FilePath = fileInfo.FullName,
                FileSize = fileInfo.Length,
                GeneratedDate = DateTime.Now
            };

        }
        catch (Exception ex)
        {
            Log.Error(
                ex,
                "Failed to write generated file: {FilePath}",
                filePath);

            throw;
        }
    }

}
