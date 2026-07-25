// See https://aka.ms/new-console-template for more information
using GeneratorTool.Models;
using Scriban;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Xml.Linq;

var rootPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;

var outputDir = Path.Combine(rootPath, "Output");

try
{
    if (Directory.Exists(outputDir))
    {
        Directory.Delete(outputDir, true);
    }

    Directory.CreateDirectory(outputDir);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

var templateBackendPathSbn = "Templates\\backend";
var templateAngularParthSbn = "Templates\\angular";
var templateReactPathSbn = "Templates\\react";
var templateDatabasePathSbn = "Templates\\dababase";

var backendTemplatePath = Path.Combine(rootPath, "Templates", "Backend");
var contractsPath = Path.Combine(backendTemplatePath,"Contracts");

// Generate Entities, AppDbContext and Dto files
var tmplEntityPath = Path.Combine(rootPath, templateBackendPathSbn,"Entities", "entity.sbn");
var tmplAppDbContextPath = Path.Combine(rootPath, templateBackendPathSbn,"Data", "appdbcontext.sbn");
var tmplDtoPath = Path.Combine(rootPath, templateBackendPathSbn,"DTOs", "dto.sbn");
var tmplSearchDtoPath = Path.Combine(rootPath, templateBackendPathSbn, "DTOs", "SearchDto.sbn");

var tmplPagedResultDtopath = Path.Combine(rootPath, templateBackendPathSbn,"DTOs", "PagedResultDto.sbn");

var tmplLoginDtoPath = Path.Combine(rootPath, templateBackendPathSbn,"DTOs","loginDto.sbn");

// BaseRepository 
var tmplBaseRepoPath = Path.Combine(rootPath, templateBackendPathSbn, "Repository", "BaseRepository.sbn");

// Repository Interface and implementation 
var tmplIRepoPath = Path.Combine(rootPath, templateBackendPathSbn, "Contracts", "IRepository.sbn");
var tmplRepoPath = Path.Combine(rootPath, templateBackendPathSbn,"Repository", "RepositoryEF.sbn");

// Service Interface and Implementation 
var tmplIServicePath = Path.Combine(rootPath, templateBackendPathSbn, "Contracts", "IService.sbn");
var tmplServicePath = Path.Combine(rootPath, templateBackendPathSbn,"Services", "ServiceEF.sbn");

// Generate Mapper
var tmplAutoMapperPath = Path.Combine(rootPath, templateBackendPathSbn,"Mapper", "AutoMapper.sbn");

// Models 
var tmplApiResponsePath = Path.Combine(rootPath, templateBackendPathSbn,"Models", "ApiResponse.sbn");
var tmplApiErrorPath = Path.Combine(rootPath, templateBackendPathSbn, "Models", "ApiError.sbn");

// Controller 
var tmplControllerPath = Path.Combine(rootPath, templateBackendPathSbn,"Controllers", "controller.sbn");
var tmplLoginJwtControllerPath = Path.Combine(rootPath, templateBackendPathSbn, "Controllers", "loginJwtController.sbn");

// Generate  Db TaBKE AND SP CREATE
var tmplDatabasePath = Path.Combine(rootPath, templateDatabasePathSbn, "TableAndStoredProcedure.sbn");
var tmplDbTestScriptPath = Path.Combine(rootPath, templateDatabasePathSbn,"TestScript.sbn");

// Generate Program.cs, appsettings.json and launchSettings.json
var tmplProgramPath = Path.Combine(rootPath, templateBackendPathSbn, "Program.sbn");
var tmplAppSettingsPath = Path.Combine(rootPath, templateBackendPathSbn, "AppSettings.sbn");
var tmplLaunchSettingsPath = Path.Combine(rootPath, templateBackendPathSbn, "LaunchSettings.sbn");
var tmplProjectPath = Path.Combine(rootPath, templateBackendPathSbn, "Project.sbn");

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

var generatorSchema = JsonSerializer.Deserialize<GeneratorSchema>(json);

if (generatorSchema == null)
{
    Console.WriteLine("Invalid JSON Schema");
    return;
}

var templateModel = new TemplateModel
{
    Settings = generatorSchema.Settings,

    Schema = new TableSchema
    {
        TableName = generatorSchema.TableName,
        DbSetName = generatorSchema.DbSetName,
        ApiUrl = generatorSchema.ApiUrl,
        Columns = generatorSchema.Columns,
        StoredProcedures = generatorSchema.StoredProcedures
    },

    SearchColumns = generatorSchema.Columns
                    .Where(c => c.Searchable)
                    .ToList(),

    StoredProcedures = generatorSchema.StoredProcedures
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
   // Console.WriteLine($"Processing Template : {templatePath}");

    var templateText = File.ReadAllText(templatePath);
    var template = Template.Parse(templateText);
    //if (template.HasErrors)
    //{
    //    Console.WriteLine("================================");
    //    Console.WriteLine("Template Parsing Error");
    //    Console.WriteLine($"File : {templatePath}");
    //    Console.WriteLine("================================");

    //    foreach (var message in template.Messages)
    //    {
    //        Console.WriteLine(message);
    //    }

    //    Console.WriteLine("================================");

    //    throw new Exception("Template parsing failed");
    //}
    Console.Write("Table Name = '" + templateModel.TableName.ToLowerInvariant());
    return template.Render(model, member => member.Name.ToLower());
}

Console.WriteLine(templateModel.StoredProcedures.GetAllPaged);


var tableSpCode = GenerateCode(tmplDatabasePath, templateModel);
var tableTestSpCode = GenerateCode(tmplDbTestScriptPath, templateModel);

var entityCode = GenerateCode(tmplEntityPath, templateModel);
var dtoCode = GenerateCode(tmplDtoPath, templateModel);
var searchDtoCode = GenerateCode(tmplSearchDtoPath, templateModel);
var pagedResultDtoCode = GenerateCode(tmplPagedResultDtopath, templateModel);
//var modelCode = GenerateCode(tmplModelPath, templateModel);
var appDbContextCode = GenerateCode(tmplAppDbContextPath, templateModel);

var baseRepoCode = GenerateCode(tmplBaseRepoPath, templateModel);

var irepoCode = GenerateCode(tmplIRepoPath, templateModel);
var repoCode = GenerateCode(tmplRepoPath, templateModel);

var iserviceCode = GenerateCode(tmplIServicePath, templateModel);
var serviceCode = GenerateCode(tmplServicePath, templateModel);

var autoMapperCode = GenerateCode(tmplAutoMapperPath, templateModel);

var apiResponseCode = GenerateCode(tmplApiResponsePath, templateModel);
var apiErrorCode = GenerateCode(tmplApiErrorPath, templateModel);

var controllerCode = GenerateCode(tmplControllerPath, templateModel);
var loginjwtcontrollerCode = GenerateCode(tmplLoginJwtControllerPath, templateModel);
 
var programCode = GenerateCode(tmplProgramPath, templateModel);
var appSettingsCode = GenerateCode(tmplAppSettingsPath, templateModel);
var launchSettingsCode = GenerateCode(tmplLaunchSettingsPath, templateModel);
var projectCode = GenerateCode(tmplProjectPath,templateModel);

var angularServiceCode = GenerateCode(tmplAngularServicePath, templateModel);

// Write file

var outputDatabase = Path.Combine(outputDir, "Database");
var outputEntities = Path.Combine(outputDir, "Entities");
var outputData = Path.Combine(outputDir, "Data");
var outputDto = Path.Combine(outputDir, "DTOs");
var outputContracts = Path.Combine(outputDir, "Contracts");
var outputRepository = Path.Combine(outputDir, "Repositories");
var outputServices = Path.Combine(outputDir, "Services");
var outputMapper = Path.Combine(outputDir, "Mapper");
var outputModels = Path.Combine(outputDir, "Models");
var outputControllers = Path.Combine(outputDir, "Controllers");

Directory.CreateDirectory(outputDatabase);
Directory.CreateDirectory(outputEntities);
Directory.CreateDirectory(outputData);
Directory.CreateDirectory(outputDto);
Directory.CreateDirectory(outputContracts);
Directory.CreateDirectory(outputRepository);
Directory.CreateDirectory(outputServices);
Directory.CreateDirectory(outputMapper);
Directory.CreateDirectory(outputModels);
Directory.CreateDirectory(outputControllers);

//var modelfilePath = Path.Combine(outputDir, $"{schema.TableName}.cs");
//File.WriteAllText(modelfilePath, modelCode);

var tableSPfilePath = Path.Combine(outputDatabase, $"{schema.TableName}DbScript.sql");
File.WriteAllText(tableSPfilePath, tableSpCode);

var tableTestSpFilePath = Path.Combine(outputDatabase, $"{schema.TableName}TestSpScript.sql");
File.WriteAllText(tableTestSpFilePath, tableTestSpCode);

var entityfilePath = Path.Combine(outputEntities, $"{schema.TableName}Entity.cs");
File.WriteAllText(entityfilePath, entityCode);

var appdbcontextfilePath = Path.Combine(outputData, $"AppDbContext.cs");
File.WriteAllText(appdbcontextfilePath, appDbContextCode);

var dtofilePath = Path.Combine(outputDto, $"{schema.TableName}Dto.cs");
File.WriteAllText(dtofilePath, dtoCode);

var searchDtofilePath = Path.Combine(outputDto, $"{schema.TableName}SearchDto.cs");
File.WriteAllText(searchDtofilePath, searchDtoCode);

var pagedResultDtofilePath = Path.Combine(outputDto, $"PagedResultDto.cs");
File.WriteAllText(pagedResultDtofilePath, pagedResultDtoCode);

var baseRepofilePath = Path.Combine(outputRepository, $"BaseRepository.cs");
File.WriteAllText(baseRepofilePath, baseRepoCode);

var irepofilePath = Path.Combine(outputContracts, $"I{schema.TableName}Repository.cs");
File.WriteAllText(irepofilePath, irepoCode);
var repofilePath = Path.Combine(outputRepository, $"{schema.TableName}RepositoryEF.cs");
File.WriteAllText(repofilePath, repoCode);

var iservicefilePath = Path.Combine(outputContracts, $"I{schema.TableName}Service.cs");
File.WriteAllText(iservicefilePath, iserviceCode);
var servicefilePath = Path.Combine(outputServices, $"{schema.TableName}ServiceEF.cs");
File.WriteAllText(servicefilePath, serviceCode);

var autoMapperfilePath = Path.Combine(outputMapper, $"{schema.TableName}Profile.cs");
File.WriteAllText(autoMapperfilePath, autoMapperCode);

var apiResponsefilePath = Path.Combine(outputModels, $"ApiResponse.cs");
File.WriteAllText(apiResponsefilePath, apiResponseCode);
var apiErrorfilePath = Path.Combine(outputModels, $"ApiError.cs");
File.WriteAllText(apiErrorfilePath, apiErrorCode);

var controllerfilePath = Path.Combine(outputControllers, $"{schema.TableName}Controller.cs");
File.WriteAllText(controllerfilePath, controllerCode);

var loginjwtcontrollerfilePath = Path.Combine(outputControllers, $"LoginJwtController.cs");
File.WriteAllText(loginjwtcontrollerfilePath, loginjwtcontrollerCode);

var programfilePathh = Path.Combine(outputDir,$"Program.cs");
File.WriteAllText(programfilePathh,programCode);
var appSettingsfilePath = Path.Combine(outputDir,$"appsettings.json");
File.WriteAllText(appSettingsfilePath, appSettingsCode);
var launchSettingsfilepath = Path.Combine(outputDir, $"launchSettings.json");
File.WriteAllText(launchSettingsfilepath, launchSettingsCode);
var projectfilePath = Path.Combine(outputDir, $"{generatorSchema.Settings.ProjectName}.csproj");
File.WriteAllText(projectfilePath, projectCode);

//Test angular file
//var angularservicefilePath = Path.Combine(outputDir, $"{schema.TableName}Service.cs");
//File.WriteAllText(angularservicefilePath, angularServiceCode);


Console.WriteLine("File generated successfully!");

