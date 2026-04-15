// See https://aka.ms/new-console-template for more information
using GeneratorTool.Models;
using Scriban;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Xml.Linq;

var rootPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;

// Output folder
var outputDir = Path.Combine(rootPath, "Output");
Directory.CreateDirectory(outputDir);

// Template path
var tmplModelPath = Path.Combine(rootPath, "Templates", "model.sbn");
var tmplControllerPath = Path.Combine(rootPath, "Templates", "controller.sbn");
var tmplAngularServicePath = Path.Combine(rootPath, "Templates", "angular-service.sbn");

var tmplEntityPath = Path.Combine(rootPath, "Templates","entity.sbn");
var tmplAppDbContextPath = Path.Combine(rootPath, "Templates", "appdbcontext.sbn");
var tmplDtoPath = Path.Combine(rootPath, "Templates", "dto.sbn");

var json = File.ReadAllText(Path.Combine(rootPath,"schema.json"));
var schema = JsonSerializer.Deserialize<TableSchema>(json);


string GenerateCode(string templatePath, object model)
{
    var templateText = File.ReadAllText(templatePath);
    var template = Template.Parse(templateText);
    return template.Render(model, member => member.Name.ToLower());
}

var modelCode = GenerateCode(tmplModelPath, new
{
    table_name = schema.TableName,
    columns = schema.Columns
});

var controllerCode = GenerateCode(tmplControllerPath, new
{
    table_name = schema.TableName,
    columns = schema.Columns
});

var angularServiceCode = GenerateCode(tmplAngularServicePath, new
{
    table_name = schema.TableName,
    columns = schema.Columns
});

var entityCode = GenerateCode(tmplEntityPath, new 
{
    table_name = schema.TableName,
    columns = schema.Columns
});

var appDbContextCode = GenerateCode(tmplAppDbContextPath, new
{
    table_name = schema.TableName,
    columns = schema.Columns
});

var dtoCode = GenerateCode(tmplDtoPath, new
{
    table_name = schema.TableName,
    columns = schema.Columns
});


// Write file
var modelfilePath = Path.Combine(outputDir, $"{schema.TableName}.cs");
File.WriteAllText(modelfilePath, modelCode);

var controllerfilePath = Path.Combine(outputDir, $"{schema.TableName}Controller.cs");
File.WriteAllText(controllerfilePath, controllerCode);

var angularservicefilePath = Path.Combine(outputDir, $"{schema.TableName}Service.cs");
File.WriteAllText(angularservicefilePath, angularServiceCode);

var entityfilePath = Path.Combine(outputDir, $"{schema.TableName}Entity.cs");
File.WriteAllText(entityfilePath, entityCode);

var appdbcontextfilePath = Path.Combine(outputDir, $"AppDbContext.cs");
File.WriteAllText(appdbcontextfilePath, appDbContextCode);

var dtofilePath = Path.Combine(outputDir, $"{schema.TableName}Dto.cs");
File.WriteAllText(dtofilePath, dtoCode);


Console.WriteLine("File generated successfully!");


//var schema = new TableSchema
//{
//    TableName = "Product",
//    Columns = new List<Column> {
//        new Column {Name="Id", Type="int"},
//        new Column { Name="Name", Type="string"},
//        new Column { Name="Price", Type="decimal" }
//    }
//};

//{
//    "namespace": "MyApp",
//  "tables": [
//    {
//        "name": "Product",
//      "columns": [
//        { "name": "Id", "type": "int", "isPrimaryKey": true, "isIdentity": true },
//        { "name": "Name", "type": "string", "required": true, "maxLength": 100 },
//        { "name": "Price", "type": "decimal" }
//      ]
//    }
//  ]
//}

//var templateModel = new
//{
//    namespace = schema.Namespace,
//    tables = schema.Tables
//};

//GenerateFile("appdbcontext.sbn", "AppDbContext.cs");
//GenerateFile("dto.sbn", $"{name}Dto.cs");
//GenerateFile("repository.sbn", $"I{name}Repository.cs");
//GenerateFile("service.sbn", $"{name}Service.cs");
//GenerateFile("controller.sbn", $"{name}Controller.cs");
//GenerateFile("automapper.sbn", $"MappingProfile.cs");
//GenerateFile("angular-service.sbn", $"{name}.service.ts");
//GenerateFile("react-component.sbn", $"{name}.jsx");


