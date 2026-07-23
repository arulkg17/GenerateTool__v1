// See https://aka.ms/new-console-template for more information
using GeneratorTool.Models;
using Scriban;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Xml.Linq;

var rootPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;

// Output folder
var outputDir = Path.Combine(rootPath, "Output");

// Delete existing output folder
if (Directory.Exists(outputDir))
{
    Directory.Delete(outputDir, true);
}

// Create fresh output folder
Directory.CreateDirectory(outputDir);


var templateBackendPathSbn = "Templates\\backend";
var templateAngularParthSbn = "Templates\\angular";
var templateReactPathSbn = "Templates\\react";
var templateDatabasePathSbn = "Templates\\dababase";

// Template path for backend
var tmplModelPath = Path.Combine(rootPath, templateBackendPathSbn, "model.sbn");
var tmplControllerPath = Path.Combine(rootPath, templateBackendPathSbn, "controller.sbn");
var tmplLoginJwtControllerPath = Path.Combine(rootPath, templateBackendPathSbn, "loginJwtController.sbn");

// Generate Entities, AppDbContext and Dto files
var tmplEntityPath = Path.Combine(rootPath, templateBackendPathSbn, "entity.sbn");
var tmplAppDbContextPath = Path.Combine(rootPath, templateBackendPathSbn, "appdbcontext.sbn");
var tmplDtoPath = Path.Combine(rootPath, templateBackendPathSbn, "dto.sbn");

// Repository Interface and implementation 
var tmplIRepoPath = Path.Combine(rootPath, templateBackendPathSbn, "IRepository.sbn");
var tmplRepoPath = Path.Combine(rootPath, templateBackendPathSbn, "Repository.sbn");

// Service Interface and Implementation 
var tmplIServicePath = Path.Combine(rootPath, templateBackendPathSbn, "IService.sbn");
var tmplServicePath = Path.Combine(rootPath, templateBackendPathSbn, "Service.sbn");

// Generate Mapper
var tmplMapperPath = Path.Combine(rootPath, templateBackendPathSbn, "mapper.sbn");

// Generate  Db TaBKE AND SP CREATE
var tmplDatabasePath = Path.Combine(rootPath, templateDatabasePathSbn, "TableAndStoredProcedure.sbn");
var tmplDbTestScriptPath = Path.Combine(rootPath, templateDatabasePathSbn, "TestScript.sbn");


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
    Schema = schema,
    SearchColumns = schema.Columns
                    .Where(c => c.Searchable)
                    .ToList(),
    StoredProcedures = schema.StoredProcedures
};


Console.WriteLine($"TestData is null: {schema.TestData == null}");

if (schema.TestData != null)
{
    Console.WriteLine($"Enabled: {schema.TestData.Enabled}");
    Console.WriteLine($"RecordCount: {schema.TestData.RecordCount}");
}


//Console.WriteLine("Table : " + schema.TableName);

//Console.WriteLine(schema.StoredProcedures == null
//    ? "StoredProcedures = NULL"
//    : "StoredProcedures Loaded");



//if (schema.StoredProcedures != null)
//{
//    Console.WriteLine("Insert      : " + schema.StoredProcedures.Insert);
//    Console.WriteLine("Update      : " + schema.StoredProcedures.Update);
//    Console.WriteLine("Delete      : " + schema.StoredProcedures.Delete);
//    Console.WriteLine("GetById     : " + schema.StoredProcedures.GetById);
//    Console.WriteLine("GetAll      : " + schema.StoredProcedures.GetAll);
//    Console.WriteLine("GetAllPaged : " + schema.StoredProcedures.GetAllPaged);
//}
//Console.ReadLine();


string GenerateCode(string templatePath, object model)
{
    Console.WriteLine($"Processing Template : {templatePath}");

    var templateText = File.ReadAllText(templatePath);
    var template = Template.Parse(templateText);
    if (template.HasErrors)
    {
        Console.WriteLine("================================");
        Console.WriteLine("Template Parsing Error");
        Console.WriteLine($"File : {templatePath}");
        Console.WriteLine("================================");

        foreach (var message in template.Messages)
        {
            Console.WriteLine(message);
        }

        Console.WriteLine("================================");

        throw new Exception("Template parsing failed");
    }


    return template.Render(model, member => member.Name.ToLower());
}

Console.WriteLine(templateModel.StoredProcedures.GetAllPaged);


var tableSpCode = GenerateCode(tmplDatabasePath, templateModel);
var tableTestSpCode = GenerateCode(tmplDbTestScriptPath, templateModel);

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

var tableSPfilePath = Path.Combine(outputDir, $"{schema.TableName}DbScript.sql");
File.WriteAllText(tableSPfilePath, tableSpCode);

var tableTestSpFilePath = Path.Combine(outputDir, $"{schema.TableName}TestSpScript.sql");
File.WriteAllText(tableTestSpFilePath, tableTestSpCode);

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

var controllerfilePath = Path.Combine(outputDir, $"{schema.TableName}Controller.cs");
File.WriteAllText(controllerfilePath, controllerCode);

var loginjwtcontrollerfilePath = Path.Combine(outputDir, $"LoginJwtController.cs");
File.WriteAllText(loginjwtcontrollerfilePath, loginjwtcontrollerCode);

var angularservicefilePath = Path.Combine(outputDir, $"{schema.TableName}Service.cs");
File.WriteAllText(angularservicefilePath, angularServiceCode);


Console.WriteLine("File generated successfully!");

