namespace GeneratorTool.Models
{
    public class TemplateModel
    {
        public GeneratorSettings Settings { get; set; } = new();

        public TableSchema Schema { get; set; } = new();

        public string TableName => Schema.TableName;

        public string ApiUrl => Schema.ApiUrl;

        public List<Column> Columns => Schema.Columns;
    }
}