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

var settings = JsonSerializer.Deserialize<GeneratorSettings>(
    File.ReadAllText(Path.Combine(rootPath, "Configuration", "generator.json")));

string tableName = "category";
var schemaFolder = Path.Combine(rootPath, "Schemas");
var schemaFile = Path.Combine(schemaFolder, $"{tableName}.json");
if (!File.Exists(schemaFile))
{
    Console.WriteLine($"Schema file not found: {schemaFile}");
    return;
}
var json = File.ReadAllText(schemaFile);
var schema = JsonSerializer.Deserialize<TableSchema>(json);

if (schema == null)
{
    Console.WriteLine("Invalid JSON Schema");
    return;
}

var templateModel = new TemplateModel
{
    Settings = settings,
    Schema = schema
};


Console.WriteLine(schema.TableName);

foreach (var col in schema.Columns)
{
    Console.WriteLine($"{col.Name}  {col.IsPrimary}");
}
Console.ReadLine();

string GenerateCode(string templatePath, object model)
{
    var templateText = File.ReadAllText(templatePath);
    var template = Template.Parse(templateText);
    return template.Render(model, member => member.Name.ToLower());
}

var entityCode = GenerateCode(tmplEntityPath, templateModel);
var dtoCode = GenerateCode(tmplDtoPath, templateModel);
var modelCode = GenerateCode(tmplModelPath, templateModel);
var appDbContextCode = GenerateCode(tmplAppDbContextPath, templateModel);

var irepoCode = GenerateCode(tmplIRepoPath, templateModel);
var repoCode = GenerateCode(tmplRepoPath, templateModel);

var iserviceCode = GenerateCode(tmplIServicePath, templateModel);
var serviceCode = GenerateCode(tmplServicePath, templateModel);

var mapperCode = GenerateCode(tmplMapperPath, templateModel);

var controllerCode = GenerateCode(tmplControllerPath, templateModel);
var loginjwtcontrollerCode = GenerateCode(tmplLoginJwtControllerPath, templateModel);

var angularServiceCode = GenerateCode(tmplAngularServicePath, templateModel);

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

