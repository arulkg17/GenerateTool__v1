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

        // Primary key column
        public Column PrimaryKeyColumn =>
            Columns.FirstOrDefault(x => x.IsPrimary)
            ?? Columns.First(x => x.Name.Equals("Id",
                StringComparison.OrdinalIgnoreCase));


        // Columns used for UPDATE test execution
        // Excludes Audit columns and Primary Key
        public List<Column> UpdateTestColumns =>
            Columns
            .Where(x =>
                !x.IsPrimary &&
                x.IsUpdatable &&
                x.Name != "CreatedBy" &&
                x.Name != "CreatedDate" &&
                x.Name != "UpdatedBy" &&
                x.Name != "UpdatedDate")
            .ToList();


        // Columns used for INSERT test execution
        // Excludes Id and Audit columns
        public List<Column> InsertTestColumns =>
            Columns
            .Where(x =>
                !x.IsPrimary &&
                x.Name != "CreatedBy" &&
                x.Name != "CreatedDate" &&
                x.Name != "UpdatedBy" &&
                x.Name != "UpdatedDate")
            .ToList();

        public List<Column> InsertColumnsWithAudit =>
            Columns
            .Where(x =>
                x.Name != "CreatedBy" &&
                x.Name != "CreatedDate" &&
                x.Name != "UpdatedBy" &&
                x.Name != "UpdatedDate")
            .ToList();

        public Column PrimaryKey =>
            Columns.First(c => c.IsPrimary);

        public IEnumerable<Column> NonIdentityColumns =>
            Columns.Where(c => !c.IsIdentity);


    }
}