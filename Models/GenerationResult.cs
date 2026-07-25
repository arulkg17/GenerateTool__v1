namespace GeneratorTool.Models;

public class GenerationResult
{
    public string OutputFolder { get; set; } = string.Empty;

    public DateTime GeneratedDate { get; set; }

    public List<GeneratedFile> Files { get; set; } = new();
}