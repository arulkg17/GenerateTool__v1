
using System.Text.Json.Serialization;

namespace GeneratorTool.Models;

public class StoredProcedures
{
    [JsonPropertyName("insert")]
    public string Insert { get; set; }

    [JsonPropertyName("update")]
    public string Update { get; set; }

    [JsonPropertyName("delete")]
    public string Delete { get; set; }

    [JsonPropertyName("getById")]
    public string GetById { get; set; }

    [JsonPropertyName("getAll")]
    public string GetAll { get; set; }

    [JsonPropertyName("getAllPaged")]
    public string GetAllPaged { get; set; }
}
