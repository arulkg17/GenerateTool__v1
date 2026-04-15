using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorTool.Models;

public class DatabaseSchema
{
    public string Namespace { get; set; }
    public List<TableSchema> Tables { get; set; }
}

public class TableSchema
{
    public string TableName { get; set; }
    public List<Column> Columns { get; set; }
}

public class Column
{ 
    public string Name { get; set; }
    public string Type { get; set; }
    public bool IsPrimaryKey { get; set; }
    public bool IsIdentity { get; set; }
    public bool Required { get; set; }
    public int? MaxLength { get; set; }
}
