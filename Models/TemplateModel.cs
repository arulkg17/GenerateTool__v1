namespace GeneratorTool.Models
{
    public class TemplateModel
    {
        public GeneratorSettings Settings { get; set; } = new();

        public TableSchema Schema { get; set; } = new();

        public string TableName => Schema.TableName;

        public string EntityName { get; set; }
        public string DbSetName { get; set; }
        public string ApiUrl => Schema.ApiUrl;
        public List<Column> Columns => Schema.Columns;

        public List<Column> SearchColumns { get; set; }

        public StoredProcedures StoredProcedures { get; set; } = new ();

        public List<int> TestRecords
        {
            get
            {
                if (Schema.TestData == null)
                    return new List<int>();

                return Enumerable.Range(1, Schema.TestData.RecordCount).ToList();
            }
        }

        public TestData TestData => Schema.TestData;
        public List<Column> InsertColumns
        {
            get
            {
                var columns = Schema.Columns
                    .Where(c => c.IsInsertable)
                    .ToList();

                for (int i = 0; i < columns.Count; i++)
                {
                    columns[i].Index = i;
                }

                return columns;
            }
        }
        public List<Column> UpdateColumns =>Schema.Columns.Where(c => c.IsUpdatable).ToList();

        public string DatabaseName => Settings.Database;


    }
}