

using GeneratorTool.Models;
using System.Text;
namespace GeneratorTool.Services;

public class GenerationReportService
{

    public void CreateReport(string outputFolder, List<GeneratedFile> generatedFiles)
    {
        var reportPath = Path.Combine(outputFolder, "GenerationReport.txt");

        var builder = new StringBuilder();

        builder.AppendLine("================================");
        builder.AppendLine(" Generator Tool Report");
        builder.AppendLine("================================");
        builder.AppendLine();

        builder.AppendLine($"Generated Date : {DateTime.Now}");
        builder.AppendLine($"Total Files    : {generatedFiles.Count}");
        builder.AppendLine();

        foreach (var file in generatedFiles)
        {
            builder.AppendLine($"File Name : {file.FileName}");
            builder.AppendLine($"Path      : {file.FilePath}");
            builder.AppendLine($"Size      : {file.FileSize} bytes");
            builder.AppendLine($"Created   : {file.GeneratedDate}");
            builder.AppendLine("--------------------------------");
        }

        File.WriteAllText(reportPath, builder.ToString());
    }
}
