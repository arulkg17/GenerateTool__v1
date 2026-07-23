using System.Text.Json.Serialization;

namespace GeneratorTool.Models;

public class TestData
{
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; }

    [JsonPropertyName("recordCount")]
    public int RecordCount { get; set; }
}