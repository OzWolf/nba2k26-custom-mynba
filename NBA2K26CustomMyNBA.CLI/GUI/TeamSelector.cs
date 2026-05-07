using NBA2K26CustomMyNBA.CLI.Domain;
using Spectre.Console;

namespace NBA2K26CustomMyNBA.CLI.GUI;
internal static class TeamSelector
{
    public static Team? Render(List<TeamWithTemplate> teams)
    {
        var choices = teams.OrderBy(t => t.Team.Name).Select(t => t.Team.Name).Append("Cancel");

        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>().Title("Select team").AddChoices(choices)
        );

        if (choice == "Cancel") return null;

        return teams.First(t => t.Team.Name.Equals(choice, StringComparison.CurrentCultureIgnoreCase)).Team;
    }
}
