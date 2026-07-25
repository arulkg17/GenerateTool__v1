namespace GeneratorTool.Models;

public class GeneratedFile
{
    public string FileName { get; set; } = string.Empty;

    public string FilePath { get; set; } = string.Empty;

    public long FileSize { get; set; }

    public DateTime GeneratedDate { get; set; }
}
