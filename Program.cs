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

var templateBackendPathSbn = "Templates\\backend";
var templateAngularParthSbn = "Templates\\angular";
var templateReactPathSbn = "Templates\\react";


// Template path for backend
var tmplModelPath = Path.Combine(rootPath, templateBackendPathSbn, "model.sbn");
var tmplControllerPath = Path.Combine(rootPath, templateBackendPathSbn, "controller.sbn");
var tmplLoginJwtControllerPath = Path.Combine(rootPath, templateBackendPathSbn, "loginJwtController.sbn");


var tmplEntityPath = Path.Combine(rootPath, templateBackendPathSbn, "entity.sbn");
var tmplAppDbContextPath = Path.Combine(rootPath, templateBackendPathSbn, "appdbcontext.sbn");
var tmplDtoPath = Path.Combine(rootPath, templateBackendPathSbn, "dto.sbn");

var tmplIRepoPath = Path.Combine(rootPath, templateBackendPathSbn, "IRepository.sbn");
var tmplRepoPath = Path.Combine(rootPath, templateBackendPathSbn, "Repository.sbn");

var tmplIServicePath = Path.Combine(rootPath, templateBackendPathSbn, "IService.sbn");
var tmplServicePath = Path.Combine(rootPath, templateBackendPathSbn, "Service.sbn");

var tmplMapperPath = Path.Combine(rootPath, templateBackendPathSbn, "mapper.sbn");


//  Template path Angular
var tmplAngularServicePath = Path.Combine(rootPath, "Templates", "angular-service.sbn");

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

var loginjwtcontrollerCode = GenerateCode(tmplLoginJwtControllerPath, new
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

var irepoCode = GenerateCode(tmplIRepoPath, new
{
    table_name = schema.TableName,
    columns = schema.Columns
});

var repoCode = GenerateCode(tmplRepoPath, new
{
    table_name = schema.TableName,
    columns = schema.Columns
});
var iserviceCode = GenerateCode(tmplIServicePath, new
{
    table_name = schema.TableName,
    columns = schema.Columns
});

var serviceCode = GenerateCode(tmplServicePath, new
{
    table_name = schema.TableName,
    columns = schema.Columns
});


var mapperCode = GenerateCode(tmplMapperPath, new
{
    table_name = schema.TableName,
    columns = schema.Columns
});



// Write file
var modelfilePath = Path.Combine(outputDir, $"{schema.TableName}.cs");
File.WriteAllText(modelfilePath, modelCode);

var controllerfilePath = Path.Combine(outputDir, $"{schema.TableName}Controller.cs");
File.WriteAllText(controllerfilePath, controllerCode);

var loginjwtcontrollerfilePath = Path.Combine(outputDir, $"LoginJwtController.cs");
File.WriteAllText(loginjwtcontrollerfilePath, loginjwtcontrollerCode);

var angularservicefilePath = Path.Combine(outputDir, $"{schema.TableName}Service.cs");
File.WriteAllText(angularservicefilePath, angularServiceCode);

var entityfilePath = Path.Combine(outputDir, $"{schema.TableName}Entity.cs");
File.WriteAllText(entityfilePath, entityCode);

var appdbcontextfilePath = Path.Combine(outputDir, $"AppDbContext.cs");
File.WriteAllText(appdbcontextfilePath, appDbContextCode);

var dtofilePath = Path.Combine(outputDir, $"{schema.TableName}Dto.cs");
File.WriteAllText(dtofilePath, dtoCode);

var irepofilePath = Path.Combine(outputDir, $"I{schema.TableName}Repository.cs");
File.WriteAllText(irepofilePath, irepoCode);
var repofilePath = Path.Combine(outputDir, $"{schema.TableName}Repository.cs");
File.WriteAllText(repofilePath, repoCode);

var iservicefilePath = Path.Combine(outputDir, $"I{schema.TableName}Service.cs");
File.WriteAllText(iservicefilePath, iserviceCode);
var servicefilePath = Path.Combine(outputDir, $"{schema.TableName}Service.cs");
File.WriteAllText(servicefilePath, serviceCode);

var mapperfilePath = Path.Combine(outputDir, $"{schema.TableName}Profile.cs");
File.WriteAllText(mapperfilePath, mapperCode);


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


