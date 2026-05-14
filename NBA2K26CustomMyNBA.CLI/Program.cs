using NBA2K26CustomMyNBA.CLI.GUI;
using NBA2K26CustomMyNBA.CLI.Domain;
using Spectre.Console;
using System.Text;

namespace NBA2K26CustomMyNBA.CLI;

public static class Program
{
    public static void Main(string[] args)
    {
        var currentEncoding = Console.OutputEncoding;
        Console.OutputEncoding = Encoding.Unicode;
        try
        {
            TexConv.Verify();
            Zip7.Verify();
            AppDirectories.Initialize();

            var leagues = LoadLeagues();

            string choice = "";
            while (choice != "Exit")
            {
                AnsiConsole.Clear();
                PrintHeader();
                string[] choices = leagues.Count > 0 ? ["Load League", "Create League", "Delete League", "Export Templates", "Exit"] : ["Create League", "Export Templates", "Exit"];
                choice = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Main Menu").AddChoices(choices));
                try {
                    if (choice == "Create League")
                        leagues = OnCreateLeague(leagues);
                    else if (choice == "Load League")
                        leagues = OnLoadLeague(leagues);
                    else if (choice == "Delete League")
                        leagues = OnDeleteLeague(leagues);
                    else if (choice == "Export Templates")
                        OnExportTemplates();
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine("[bold red]" + ex.Message + "[/]");
                    AnsiConsole.Console.Input.ReadKey(false);
                }
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex);
        }
        finally
        {
            Console.OutputEncoding = currentEncoding;
        }
    }

    private static void PrintHeader()
    {
        var titleFont = FigletFont.Load(AppResources.AnsiRegularFont.GetStream());
        var title = new FigletText(titleFont, "NBA 2K26 Custom MyNBA")
        {
            Color = Color.Gold1,
            Justification = Justify.Center
        };

        var version = new Text("Version 1.1.0", new Style(Color.Grey))
        {
            Justification = Justify.Center
        };

        AnsiConsole.Write(title);
        AnsiConsole.WriteLine();
        AnsiConsole.Write(version);
        AnsiConsole.WriteLine();
    }

    private static List<League> OnCreateLeague(List<League> leagues)
    {
        var name = AnsiConsole.Ask<string>("League Name : ");
        if (leagues.Any(l => l.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)))
            throw new InvalidOperationException("League already exists.");

        var league = new League(name);

        leagues.Add(league);
        SaveLeagues(leagues);

        LeagueManagerScreen.Render(league, onChange);
        return leagues;

        void onChange(League l1)
        {
            var updated = leagues.Select(l2 => l2.Id == l1.Id ? l1 : l2).ToList();
            SaveLeagues(updated);
        }
    }

    private static List<League> OnLoadLeague(List<League> leagues)
    {
        var league = LeagueSelector.Render(leagues);
        LeagueManagerScreen.Render(league, onChange);
        return leagues;

        void onChange(League l1)
        {
            var updated = leagues.Select(l2 => l2.Id == l1.Id ? l1 : l2).ToList();
            SaveLeagues(updated);
        }
    }

    private static List<League> OnDeleteLeague(List<League> leagues)
    {
        var leagueToDelete = LeagueSelector.Render(leagues);
        leagues = leagues.FindAll(l => l.Id != leagueToDelete.Id);
        SaveLeagues(leagues);
        return leagues;
    }

    private static void OnExportTemplates()
    {
        AnsiConsole.Progress().Start(ctx =>
        {
            AppResources.TemplatesZip.SaveTo("./templates.7z", ctx);
        });

        AnsiConsole.MarkupLine("Templates extracted to [bold green]templates.7z[/] file.");
        AnsiConsole.Console.Input.ReadKey(false);
    }

    private static List<League> LoadLeagues()
    {
        var file = AppDirectories.AppData + "/leagues.json";
        if (!File.Exists(file)) return [];

        return JsonUtils.DeserializeFile<List<League>>(file);
    }

    private static void SaveLeagues(List<League> leagues)
    {
        var file = AppDirectories.AppData + "/leagues.json";
        var json = JsonUtils.Serialize(leagues);
        File.WriteAllText(file, json);
    }
}