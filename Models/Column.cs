using System.Text.Json.Serialization;

namespace GeneratorTool.Models
{
    public class Column
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("required")]
        public bool Required { get; set; }

        [JsonPropertyName("isPrimary")]
        public bool IsPrimary { get; set; }

        public string CSharpType
        {
            get
            {
                return Type.ToLower() switch
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
            }
        }

        public string SqlType
        {
            get
            {
                return Type.ToLower() switch
                {
                    "number" => "INT",
                    "int" => "INT",
                    "long" => "BIGINT",
                    "decimal" => "DECIMAL(18,2)",
                    "double" => "FLOAT",
                    "float" => "REAL",
                    "string" => "NVARCHAR(MAX)",
                    "boolean" => "BIT",
                    "bool" => "BIT",
                    "date" => "DATE",
                    "datetime" => "DATETIME",
                    "guid" => "UNIQUEIDENTIFIER",
                    _ => "NVARCHAR(MAX)"
                };
            }
        }

        public string TypeScriptType
        {
            get
            {
                return Type.ToLower() switch
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
        }

        public string DefaultValue
        {
            get
            {
                return Type.ToLower() switch
                {
                    "number" => "0",
                    "int" => "0",
                    "long" => "0",
                    "decimal" => "0",
                    "double" => "0",
                    "float" => "0",
                    "string" => "\"\"",
                    "boolean" => "false",
                    "bool" => "false",
                    "date" => "DateTime.Now",
                    "datetime" => "DateTime.Now",
                    "guid" => "Guid.NewGuid()",
                    _ => "\"\""
                };
            }
        }

        public bool IsString =>
            CSharpType == "string";

        public bool IsNumber =>
            CSharpType == "int" ||
            CSharpType == "long" ||
            CSharpType == "decimal" ||
            CSharpType == "double" ||
            CSharpType == "float";

        public bool IsDate =>
            CSharpType == "DateTime";

        public bool IsBoolean =>
            CSharpType == "bool";
    }
}
