using System.Data.Common;
using System.Text.Json.Serialization;

namespace GeneratorTool.Models
{
    public class GeneratorSettings
    {
        public string ProjectName { get; set; } = string.Empty;
        [JsonPropertyName("namespace")]
        public string Namespace { get; set; } = string.Empty;

        public string Database { get; set; } = string.Empty;

        public string ORM { get; set; } = string.Empty;

        public string Authentication { get; set; } = string.Empty;

        public string UI { get; set; } = string.Empty;

        public string AngularVersion { get; set; } = string.Empty;

        public string ReactVersion { get; set; } = string.Empty;

        public string DotNetVersion { get; set; } = string.Empty;

        public bool UseRepository { get; set; }

        public bool UseService { get; set; }

        public bool UseCQRS { get; set; }

        public bool UseStoredProcedure { get; set; }
        [JsonPropertyName("apititle")]

        public string ApiTitle { get; set; } = string.Empty;
        [JsonPropertyName("dbserver")]
        public string DbServer { get; set; } = string.Empty;
        [JsonPropertyName("dbname")]

        public string DbName { get; set; } = string.Empty;
        [JsonPropertyName("jwtkey")]
        public string JwtKey { get; set; } = string.Empty;

        [JsonPropertyName("jwtissuer")]
        public string JwtIssuer { get; set; } = string.Empty;

        [JsonPropertyName("jwtexpiryminutes")]
        public int JwtExpiryminutes { get; set; }

        [JsonPropertyName("adminusername")]
        public string AdminUsername { get; set; } = string.Empty;

        [JsonPropertyName("adminpassword")]
        public string AdminPassword { get; set; } = string.Empty;

        [JsonPropertyName("port")]
        public int Port { get; set; }

        [JsonPropertyName("angularorigin")]
        public string AngularOrigin { get; set; } = string.Empty;
    
    }
}