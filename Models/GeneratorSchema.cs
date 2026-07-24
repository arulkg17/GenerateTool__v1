using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.Marshalling.IIUnknownCacheStrategy;

namespace GeneratorTool.Models;

public class GeneratorSchema
{
    [JsonPropertyName("settings")]
    public GeneratorSettings Settings { get; set; } = new();

    [JsonPropertyName("tables")]
    public List<TableInfo> Tables { get; set; } = new();

    [JsonPropertyName("tablename")]
    public string TableName { get; set; } = string.Empty;

    [JsonPropertyName("entityname")]
    public string EntityName { get; set; } = string.Empty;

    [JsonPropertyName("dbsetname")]
    public string DbSetName { get; set; } = string.Empty;

    [JsonPropertyName("apiUrl")]
    public string ApiUrl { get; set; } = string.Empty;

    [JsonPropertyName("apititle")]
    public string ApiTitle { get; set; } = string.Empty;

    [JsonPropertyName("Columns")]
    public List<Column> Columns { get; set; } = new();

    [JsonPropertyName("storedProcedures")]
    public StoredProcedures StoredProcedures { get; set; } = new();
    [JsonPropertyName("jwtkey")]
    public string JwtKey {  get; set; } = string.Empty;
    [JsonPropertyName("jwtissuer")]
    public string JwtIssuer {  get; set; } = string.Empty;
    [JsonPropertyName("jwtexpiryminutes")]
    public int JwtExpiryminutes { get; set; }
    [JsonPropertyName("adminusername")]
    public string AdminUsername {  get; set; } = string.Empty;
    [JsonPropertyName("aaminpassword")]
    public string AdminPassword {  get; set; } = string.Empty;
    [JsonPropertyName("port")]
    public int Port { get; set; }
    [JsonPropertyName("angularorigin")]
    public string AngularOrigin { get; set; } = string.Empty;

}
