using NBA2K26CustomMyNBA.CLI.Domain;
using Spectre.Console;

namespace NBA2K26CustomMyNBA.CLI.GUI;

internal static class TeamTemplateSelector
{
    public static TeamTemplate Render(List<TeamTemplate> templates)
    {
        var leagueTeams = templates.FindAll(t => !t.Expansion).OrderBy(t => t.Name);
        var expansionTeams = templates.FindAll(t => t.Expansion).OrderBy(t => t.Name);

        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select team to replace")
                .PageSize(15)
                .AddChoiceGroup("NBA Teams", leagueTeams.Select(t => t.Name))
                .AddChoiceGroup("Expansion Teams", expansionTeams.Select(t => t.Name))
        );

        return templates.Find(t => t.Name == choice);
    }
}
