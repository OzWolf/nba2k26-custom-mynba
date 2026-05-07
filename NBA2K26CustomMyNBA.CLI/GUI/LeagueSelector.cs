using NBA2K26CustomMyNBA.CLI.Domain;
using Spectre.Console;

namespace NBA2K26CustomMyNBA.CLI.GUI;

internal static class LeagueSelector
{
    public static League Render(List<League> leagues)
    {
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>().Title("Select league:").AddChoices(leagues.Select(l => l.Name))
        );

        return leagues.First(l => l.Name.Equals(choice, StringComparison.CurrentCultureIgnoreCase));
    }
}
