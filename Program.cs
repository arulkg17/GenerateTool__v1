/* 
"========================================"
"     Maayai GeneratorTool")
"========================================"
"Author  : Arul Kalivarathan"
"Version : 1.0.0"
"License : Commercial and Educational Use")
"========================================"
*/

// See https://aka.ms/new-console-template for more information
using GeneratorTool.Models;
using GeneratorTool.Services;
using GeneratorTool.Services.Interfaces;
using Scriban;
using Serilog;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Xml.Linq;

Console.WriteLine("========================================");
Console.WriteLine("     Maayai GeneratorTool");
Console.WriteLine("========================================");
Console.WriteLine("Author  : Arul Kalivarathan");
Console.WriteLine("Version : 1.0.0");
Console.WriteLine("License : Educational Use");
Console.WriteLine("========================================");
Console.WriteLine();

//var rootPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
var rootPath = AppContext.BaseDirectory; 

//Logs related 
var logFolder = Path.Combine(AppContext.BaseDirectory, "Logs");
Directory.CreateDirectory(logFolder);
Console.WriteLine($"Log folder location: {logFolder}");
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File(
        Path.Combine(logFolder, "generator-.log"),
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30)
    .CreateLogger();
Log.Information("GeneratorTool logging initialized.");
Log.Information("=========================================");
Log.Information("GeneratorTool Started");
Log.Information("Version : 1.0.0");
Log.Information("Started : {DateTime}", DateTime.Now);
Log.Information("=========================================");


var outputDir = Path.Combine(rootPath, "Output");
var generationResult = new GenerationResult
{
    OutputFolder = outputDir,
    GeneratedDate = DateTime.Now,
    Files = new List<GeneratedFile>()
};

var generatedFiles = new List<GeneratedFile>();
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
if (!Directory.Exists(schemaFolder))
{
    Directory.CreateDirectory(schemaFolder);
}

try
{


    //Schema Selection Service

    var selector = new SchemaSelectorService(schemaFolder);
    var schemaFile = selector.SelectSchema();
    Log.Information("Schema File : {SchemaFile}", schemaFile);

    //Schema Reader Service 
    var reader = new SchemaReaderService();
    var schema = reader.Read(schemaFile);

    var json = File.ReadAllText(schemaFile);

    Directory.Delete(outputDir, true);
    outputDir = Path.Combine(
        rootPath,
        "Output",
        $"{schema.TableName}_{DateTime.Now:yyyyMMdd_HHmmss}"
    );

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


    //Console.WriteLine($"TestData is null: {schema.TestData == null}");

    //if (schema.TestData != null)
    //{
    //    Console.WriteLine($"Enabled: {schema.TestData.Enabled}");
    //    Console.WriteLine($"RecordCount: {schema.TestData.RecordCount}");
    //}



    //Console.WriteLine(templateModel.StoredProcedures.GetAllPaged);

    // Template Render Service
    var templateRender = new TemplateRenderService();

    var tableSpCode = templateRender.Render(tmplDatabasePath, templateModel);
    var tableTestSpCode = templateRender.Render(tmplDbTestScriptPath, templateModel);

    var entityCode = templateRender.Render(tmplEntityPath, templateModel);
    var dtoCode = templateRender.Render(tmplDtoPath, templateModel);
    var searchDtoCode = templateRender.Render(tmplSearchDtoPath, templateModel);
    var pagedResultDtoCode = templateRender.Render(tmplPagedResultDtopath, templateModel);
    //var modelCode = GenerateCode(tmplModelPath, templateModel);
    var appDbContextCode = templateRender.Render(tmplAppDbContextPath, templateModel);

    var baseRepoCode = templateRender.Render(tmplBaseRepoPath, templateModel);

    var irepoCode = templateRender.Render(tmplIRepoPath, templateModel);
    var repoCode = templateRender.Render(tmplRepoPath, templateModel);

    var iserviceCode = templateRender.Render(tmplIServicePath, templateModel);
    var serviceCode = templateRender.Render(tmplServicePath, templateModel);

    var autoMapperCode = templateRender.Render(tmplAutoMapperPath, templateModel);

    var apiResponseCode = templateRender.Render(tmplApiResponsePath, templateModel);
    var apiErrorCode = templateRender.Render(tmplApiErrorPath, templateModel);

    var controllerCode = templateRender.Render(tmplControllerPath, templateModel);
    var loginjwtcontrollerCode = templateRender.Render(tmplLoginJwtControllerPath, templateModel);

    var programCode = templateRender.Render(tmplProgramPath, templateModel);
    var appSettingsCode = templateRender.Render(tmplAppSettingsPath, templateModel);
    var launchSettingsCode = templateRender.Render(tmplLaunchSettingsPath, templateModel);
    var projectCode = templateRender.Render(tmplProjectPath, templateModel);

    var angularServiceCode = templateRender.Render(tmplAngularServicePath, templateModel);

    // Write file

    var outputWriter = new OutputWriterService();

    // Database 
    var outputDatabase = Path.Combine(outputDir, "Database");
    var tableSPfilePath = Path.Combine(outputDatabase, $"{schema.TableName}DbScript.sql");
    generationResult.Files.Add(outputWriter.WriteFile(tableSPfilePath, tableSpCode));

    var tableTestSpFilePath = Path.Combine(outputDatabase, $"{schema.TableName}TestSpScript.sql");
    generationResult.Files.Add(outputWriter.WriteFile(tableTestSpFilePath, tableTestSpCode));

    //Entities
    var outputEntities = Path.Combine(outputDir, "Entities");
    var entityfilePath = Path.Combine(outputEntities, $"{schema.TableName}Entity.cs");
    generationResult.Files.Add(outputWriter.WriteFile(entityfilePath, entityCode));

    var outputData = Path.Combine(outputDir, "Data");
    var appdbcontextfilePath = Path.Combine(outputData, $"AppDbContext.cs");
    generationResult.Files.Add(outputWriter.WriteFile(appdbcontextfilePath, appDbContextCode));
    //Dto
    var outputDto = Path.Combine(outputDir, "DTOs");
    var dtofilePath = Path.Combine(outputDto, $"{schema.TableName}Dto.cs");
    generationResult.Files.Add(outputWriter.WriteFile(dtofilePath, dtoCode));

    var searchDtofilePath = Path.Combine(outputDto, $"{schema.TableName}SearchDto.cs");
    generationResult.Files.Add(outputWriter.WriteFile(searchDtofilePath, searchDtoCode));

    var pagedResultDtofilePath = Path.Combine(outputDto, $"PagedResultDto.cs");
    generationResult.Files.Add(outputWriter.WriteFile(pagedResultDtofilePath, pagedResultDtoCode));
    //Contracts
    var outputContracts = Path.Combine(outputDir, "Contracts");
    var irepofilePath = Path.Combine(outputContracts, $"I{schema.TableName}Repository.cs");
    generationResult.Files.Add(outputWriter.WriteFile(irepofilePath, irepoCode));
    var iservicefilePath = Path.Combine(outputContracts, $"I{schema.TableName}Service.cs");
    generationResult.Files.Add(outputWriter.WriteFile(iservicefilePath, iserviceCode));
    //Repository
    var outputRepository = Path.Combine(outputDir, "Repositories");
    var baseRepofilePath = Path.Combine(outputRepository, $"BaseRepository.cs");
    generationResult.Files.Add(outputWriter.WriteFile(baseRepofilePath, baseRepoCode));
    var repofilePath = Path.Combine(outputRepository, $"{schema.TableName}RepositoryEF.cs");
    generationResult.Files.Add(outputWriter.WriteFile(repofilePath, repoCode));
    //Service
    var outputServices = Path.Combine(outputDir, "Services");
    var servicefilePath = Path.Combine(outputServices, $"{schema.TableName}ServiceEF.cs");
    generationResult.Files.Add(outputWriter.WriteFile(servicefilePath, serviceCode));
    //Mapper
    var outputMapper = Path.Combine(outputDir, "Mapper");
    var autoMapperfilePath = Path.Combine(outputMapper, $"{schema.TableName}Profile.cs");
    generationResult.Files.Add(outputWriter.WriteFile(autoMapperfilePath, autoMapperCode));
    //Models
    var outputModels = Path.Combine(outputDir, "Models");
    var apiResponsefilePath = Path.Combine(outputModels, $"ApiResponse.cs");
    generationResult.Files.Add(outputWriter.WriteFile(apiResponsefilePath, apiResponseCode));
    var apiErrorfilePath = Path.Combine(outputModels, $"ApiError.cs");
    generationResult.Files.Add(outputWriter.WriteFile(apiErrorfilePath, apiErrorCode));
    //Controllers
    var outputControllers = Path.Combine(outputDir, "Controllers");
    var controllerfilePath = Path.Combine(outputControllers, $"{schema.TableName}Controller.cs");
    generationResult.Files.Add(outputWriter.WriteFile(controllerfilePath, controllerCode));
    var loginjwtcontrollerfilePath = Path.Combine(outputControllers, $"LoginJwtController.cs");
    generationResult.Files.Add(outputWriter.WriteFile(loginjwtcontrollerfilePath, loginjwtcontrollerCode));
    //Other Root files 
    var programfilePathh = Path.Combine(outputDir, $"Program.cs");
    generationResult.Files.Add(outputWriter.WriteFile(programfilePathh, programCode));
    var appSettingsfilePath = Path.Combine(outputDir, $"appsettings.json");
    generationResult.Files.Add(outputWriter.WriteFile(appSettingsfilePath, appSettingsCode));
    var launchSettingsfilepath = Path.Combine(outputDir, $"launchSettings.json");
    generationResult.Files.Add(outputWriter.WriteFile(launchSettingsfilepath, launchSettingsCode));
    var projectfilePath = Path.Combine(outputDir, $"{generatorSchema.Settings.ProjectName}.csproj");
    generationResult.Files.Add(outputWriter.WriteFile(projectfilePath, projectCode));

    Console.WriteLine("=========================");
    Console.WriteLine("Generation Result");
    Console.WriteLine("=========================");

    Console.WriteLine(
        $"Output Folder : {generationResult.OutputFolder}");

    Console.WriteLine(
        $"Generated Files : {generationResult.Files.Count}");

    foreach (var file in generationResult.Files)
    {
        Console.WriteLine(
            $"{file.FileName} - {file.FileSize} bytes");
    }

    //Report Generation Service
    var reportService = new GenerationReportService();
    reportService.CreateReport(outputDir, generationResult.Files);
}
catch (Exception ex)
{
    Log.Error(ex, "GeneratorTool execution failed.");

    Console.WriteLine("Generation failed.");
    Console.WriteLine(ex.Message);
}
finally
{
    Log.CloseAndFlush();
}

Console.WriteLine("File generated successfully!");

