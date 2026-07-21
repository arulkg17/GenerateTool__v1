using System.Text.Json.Serialization;


namespace GeneratorTool.Models;

public class TableSchema
{
    [JsonPropertyName("tablename")]
    public string TableName { get; set; } = string.Empty;

    [JsonPropertyName("apiUrl")]
    public string ApiUrl { get; set; } = string.Empty;

    [JsonPropertyName("Columns")]
    public List<Column> Columns { get; set; } = new();
}

