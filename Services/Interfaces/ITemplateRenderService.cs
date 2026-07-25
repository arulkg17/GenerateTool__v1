
namespace GeneratorTool.Services.Interfaces;
public interface ITemplateRenderService
{
    string Render(string templatePath, object model);
}
