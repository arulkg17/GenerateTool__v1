using System.Text.Json.Serialization;

namespace GeneratorTool.Models
{
    public class GeneratorSettings
    {
        public string ProjectName { get; set; } = string.Empty;

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
    }
}