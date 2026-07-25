using GeneratorTool.Models;

namespace GeneratorTool.Services.Interfaces;

public interface IGenerationReportService
{
    void CreateReport(
        string outputFolder,
        List<GeneratedFile> generatedFiles);
}
