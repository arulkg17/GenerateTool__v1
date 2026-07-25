
using GeneratorTool.Services.Interfaces;
using Scriban;

namespace GeneratorTool.Services;

public class TemplateRenderService : ITemplateRenderService
{
    public string Render(string templatePath, object model)
    {
        var text = File.ReadAllText(templatePath);

        var template = Template.Parse(text);

        return template.Render( model, member => member.Name.ToLower());

    }
}
