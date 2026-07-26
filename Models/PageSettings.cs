using System.Text.Json.Serialization;

namespace GeneratorTool.Models;

public class PagingSettings
{
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; }

    [JsonPropertyName("pageNumber")]
    public int PageNumber { get; set; }

    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; }

    [JsonPropertyName("searchEnabled")]
    public bool SearchEnabled { get; set; }
}