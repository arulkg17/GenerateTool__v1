using System.Text.RegularExpressions;

public class SolutionUpdater
{
    public void UpdateSolution(string slnPath, List<ProjectModel> allProjects)
    {
        if (!File.Exists(slnPath))
        {
            // First-time creation - render the full NewSolutionBootstrap.sbn template
            RenderFullSolution(slnPath, allProjects);
            return;
        }

        var slnText = File.ReadAllText(slnPath);

        // Step 1: find existing project GUIDs already in the file
        var existingGuids = Regex.Matches(slnText, @"Project\(""\{[^}]+\}""\)\s*=\s*""[^""]+"",\s*""[^""]+"",\s*""(\{[^}]+\})""")
                                  .Select(m => m.Groups[1].Value)
                                  .ToHashSet(StringComparer.OrdinalIgnoreCase);

        // Step 2: filter to only genuinely new projects
        var newProjects = allProjects.Where(p => !existingGuids.Contains(p.ProjectGuid)).ToList();
        if (!newProjects.Count.Equals(0) == false) return; // nothing new to add

        // Step 3: render the 3 fragments via Scriban
        var newBlocks = RenderTemplate("NewProjectBlocks.sbn", newProjects);
        var newConfigLines = RenderTemplate("NewProjectConfigPlatforms.sbn", newProjects);
        var newNestedLines = RenderTemplate("NewNestedProjects.sbn", newProjects);

        // Step 4: splice each fragment into the right place

        // 4a. Insert Project blocks right before the "Global" line
        slnText = slnText.Replace("Global\r\n", newBlocks + "Global\r\n");

        // 4b. Insert config platform lines before the matching EndGlobalSection
        //     (the one that follows "GlobalSection(ProjectConfigurationPlatforms)")
        slnText = InsertBeforeSectionEnd(slnText, "GlobalSection(ProjectConfigurationPlatforms)", newConfigLines);

        // 4c. Insert nested project lines before the matching EndGlobalSection
        //     (the one that follows "GlobalSection(NestedProjects)")
        if (slnText.Contains("GlobalSection(NestedProjects)"))
        {
            slnText = InsertBeforeSectionEnd(slnText, "GlobalSection(NestedProjects)", newNestedLines);
        }
        else if (!string.IsNullOrWhiteSpace(newNestedLines))
        {
            // NestedProjects section doesn't exist yet - need to add the whole section
            var nestedSection = $"\tGlobalSection(NestedProjects) = preSolution\r\n{newNestedLines}\tEndGlobalSection\r\n";
            slnText = slnText.Replace("EndGlobal", nestedSection + "EndGlobal");
        }

        File.WriteAllText(slnPath, slnText);
    }

    private string InsertBeforeSectionEnd(string slnText, string sectionMarker, string newLines)
    {
        if (string.IsNullOrWhiteSpace(newLines)) return slnText;

        int sectionStart = slnText.IndexOf(sectionMarker, StringComparison.Ordinal);
        if (sectionStart < 0) return slnText;

        int sectionEnd = slnText.IndexOf("EndGlobalSection", sectionStart, StringComparison.Ordinal);
        if (sectionEnd < 0) return slnText;

        return slnText.Insert(sectionEnd, newLines);
    }
}