using NBA2K26CustomMyNBA.CLI.Domain;
using Spectre.Console;

namespace NBA2K26CustomMyNBA.CLI.GUI;

internal static class LeagueManagerScreen
{
    private readonly static string LogoDDSName = "logo.2df327256b936ada.dds";
    private readonly static string CourtFloorDDSName = "floor_myteam250ktournament_court_color.e93fea41405ffb99.dds";

    public static void Render(League league, Action<League> onChange)
    {
        string choice = "";
        while (choice != "Exit")
        {
            AnsiConsole.Clear();

            AnsiConsole.MarkupLine("[bold green]League : " + league.Name + "[/]");
            RenderTeamsTable(league.Teams);

            string[] options;
            if (league.Teams.Count >= 36)
                options = ["Delete Team", "Generate Mods", "Exit"];
            else if (league.Teams.Count == 0)
                options = ["Add Team", "Exit"];
            else
                options = ["Add Team", "Delete Team", "Generate Mods", "Exit"];

            choice = AnsiConsole.Prompt(new SelectionPrompt<string>().AddChoices(options));

            try
            {
                if (choice == "Add Team")
                    OnAddTeam(league, onChange);
                else if (choice == "Delete Team")
                    OnDeleteTeam(league, onChange);
                else if (choice == "Generate Mods")
                    OnGenerateMods(league);
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[bold red]{ex.Message}[/]");
                AnsiConsole.Console.Input.ReadKey(false);
            }
        }
    }

    private static void OnAddTeam(League league, Action<League> onChange)
    {
        var name = AnsiConsole.Ask<string>("Team Folder : ");
        if (league.Teams.Any(t => t.Team.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)))
            throw new Exception($"Team {name} already exists in this league.");
        var template = TeamTemplateSelector.Render(league.AvailableTemplates);
        var team = new Team(name, template.Id);

        league.AddTeam(team);
        onChange(league);
    }

    private static void OnDeleteTeam(League league, Action<League> onChange)
    {
        var team = TeamSelector.Render(league.Teams);
        if (team != null)
        {
            league.RemoveTeam((Team)team);
            onChange(league);
        }
    }

    private static void OnGenerateMods(League league)
    {
        AnsiConsole.MarkupLine($"Generating mod files for [bold gold1]{league.Name}[/]...");

        AnsiConsole.Progress()
                .Start(ctx =>
                {
                    var sorted = league.Teams.OrderBy(t => t.Team.Name.ToUpper());
                    var tasks = new Dictionary<string, ProgressTask>();

                    foreach (var t in sorted)
                        tasks.Add(t.Team.Id, ctx.AddTask($"[bold blue]{t.Team.Name.ToUpper()}[/]", maxValue: 5));

                    foreach (var t in sorted)
                    {
                        var task = tasks[t.Team.Id]!;
                        ExportTeam(t, task);
                    }
                });
        AnsiConsole.MarkupLine("[bold green]Export Complete![/]");
        AnsiConsole.Console.Input.ReadKey(false);
    }

    private static void RenderTeamsTable(List<TeamWithTemplate> teams)
    {
        var table = new Table().RoundedBorder().Expand();
        table.AddColumn("Team");
        table.AddColumn("Replacing");
        table.AddColumn("Logo", c => c.Centered());
        table.AddColumn("Court", c => c.Centered());
        table.AddColumn("Home Uni", c => c.Centered());
        table.AddColumn("Away Uni", c => c.Centered());
        table.AddColumn("Secondary Uni", c => c.Centered());

        foreach (var t in teams.OrderBy(t => t.Team.Name))
        {
            var sourceImageFiles = t.SourceImageFiles;

            table.AddRow(
                t.Team.Name.ToUpper(),
                t.Template.Name,
                File.Exists(sourceImageFiles.Logo) ? "[bold green]✓[/]" : "[bold red]✗[/]",
                File.Exists(sourceImageFiles.Court) ? "[bold green]✓[/]" : "[bold red]✗[/]",
                File.Exists(sourceImageFiles.HomeUniform) ? "[bold green]✓[/]" : "[bold red]✗[/]",
                File.Exists(sourceImageFiles.AwayUniform) ? "[bold green]✓[/]" : "[bold red]✗[/]",
                File.Exists(sourceImageFiles.SecondaryUniform) ? "[bold green]✓[/]" : "[bold red]✗[/]"
            );
        }

        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();
    }

    private static void ExportTeam(TeamWithTemplate team, ProgressTask task)
    {
        var sourceImageFiles = team.SourceImageFiles;
        var targetArchiveFiles = team.TargetArchiveFiles;
        var workImageFiles = team.WorkImageFiles;

        //task.Description = "Logo";
        CreateModdedIFF(AppResources.LogoIFF, sourceImageFiles.Logo, workImageFiles.Logo, targetArchiveFiles.Logo, LogoDDSName);
        task.Increment(1);

        //task.Description = "Court floor";
        CreateModdedIFF(AppResources.ArenaFloorIFF, sourceImageFiles.Court, workImageFiles.Court, targetArchiveFiles.Court, CourtFloorDDSName);
        task.Increment(1);

        //task.Description = "Home uniform";
        CreateModdedIFF(AppResources.LogoIFF, sourceImageFiles.HomeUniform, workImageFiles.HomeUniform, targetArchiveFiles.HomeUniform, LogoDDSName);
        task.Increment(1);

        //task.Description = "Away uniform";
        CreateModdedIFF(AppResources.LogoIFF, sourceImageFiles.AwayUniform, workImageFiles.AwayUniform, targetArchiveFiles.AwayUniform, LogoDDSName);
        task.Increment(1);

        //task.Description = "Secondary uniform";
        CreateModdedIFF(AppResources.LogoIFF, sourceImageFiles.SecondaryUniform, workImageFiles.SecondaryUniform, targetArchiveFiles.SecondaryUniform, LogoDDSName);
        task.Increment(1);

        //task.Description = "[bold green]✓[/]";
    }

    public static void CreateModdedIFF(AppResource resource, string sourcePNG, string targetDDS, string targetIFF, string archiveFileName)
    {
        if (!File.Exists(sourcePNG)) return;

        resource.SaveTo(targetIFF);

        TexConv.Convert(sourcePNG, targetDDS, "BC7_UNORM");
        Zip7.AddToArchive(targetDDS, targetIFF, archiveFileName);
    }
}
