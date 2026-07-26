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
    [JsonPropertyName("storedProcedures")]
    public StoredProcedures StoredProcedures { get; set; } = new();

    [JsonPropertyName("testData")]
    public TestData TestData { get; set; } = new();
    [JsonPropertyName("dbsetname")]
    public string DbSetName { get; set; } = string.Empty;
    [JsonPropertyName("apititle")]
    public string ApiTitle { get; set; } = string.Empty;
    [JsonPropertyName("dbname")]
    public string Dbname { get; set; } = string.Empty;
    [JsonPropertyName("tables")]
    public List<TableSchema> Tables { get; set; } = new();
    [JsonPropertyName("projectname")]
    public string ProjectName { get; set; } = string.Empty;
    [JsonPropertyName("paging")]
    public PagingSettings Paging { get; set; } = new();
}

