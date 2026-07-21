using System.Text.Json.Serialization;

namespace GeneratorTool.Models;

public class Column
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("sqlType")]
    public string SqlType { get; set; } = string.Empty;

    [JsonPropertyName("required")]
    public bool Required { get; set; }

    [JsonPropertyName("isPrimary")]
    public bool IsPrimary { get; set; }

    [JsonPropertyName("isIdentity")]
    public bool IsIdentity { get; set; }

    [JsonPropertyName("searchable")]
    public bool Searchable { get; set; }

    [JsonPropertyName("maxlength")]
    public int? MaxLength { get; set; }

    [JsonPropertyName("defaultValue")]
    public object? DefaultValue { get; set; }

    public string CSharpType =>
        Type.ToLower() switch
        {
            "number" => "int",
            "int" => "int",
            "long" => "long",
            "decimal" => "decimal",
            "double" => "double",
            "float" => "float",
            "string" => "string",
            "boolean" => "bool",
            "bool" => "bool",
            "date" => "DateTime",
            "datetime" => "DateTime",
            "guid" => "Guid",
            _ => "string"
        };

    public string TypeScriptType =>
        Type.ToLower() switch
        {
            "number" => "number",
            "int" => "number",
            "long" => "number",
            "decimal" => "number",
            "double" => "number",
            "float" => "number",
            "string" => "string",
            "boolean" => "boolean",
            "bool" => "boolean",
            "date" => "Date",
            "datetime" => "Date",
            "guid" => "string",
            _ => "string"
        };
}