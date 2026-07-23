using System.Text.Json.Serialization;

namespace GeneratorTool.Models;

public class Column
{
    public int Index { get; set; }

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

    public string SampleValue
    {
        get
        {
            return Type.ToLower() switch
            {
                "string" => $"'Test {Name}'",

                "number" => "1",

                "int" => "1",

                "long" => "1",

                "boolean" => "1",

                "bool" => "1",

                "datetime" => "GETDATE()",

                "date" => "GETDATE()",

                "decimal" => "10.50",

                "double" => "10.50",

                "float" => "10.50",

                "guid" => "NEWID()",

                _ => "NULL"
            };
        }
    }

    public bool IsAuditColumn
    {
        get
        {
            return Name.Equals("CreatedBy", StringComparison.OrdinalIgnoreCase)
                || Name.Equals("CreatedDate", StringComparison.OrdinalIgnoreCase)
                || Name.Equals("UpdatedBy", StringComparison.OrdinalIgnoreCase)
                || Name.Equals("UpdatedDate", StringComparison.OrdinalIgnoreCase);
        }
    }

    public string CamelName =>
    string.IsNullOrEmpty(Name)
        ? Name
        : char.ToLowerInvariant(Name[0]) + Name.Substring(1);



    public bool IsInsertable
    {
        get
        {
            return !IsIdentity && !IsAuditColumn;
        }
    }


    public bool IsUpdatable
    {
        get
        {
            return !IsPrimary && !IsAuditColumn;
        }
    }



}