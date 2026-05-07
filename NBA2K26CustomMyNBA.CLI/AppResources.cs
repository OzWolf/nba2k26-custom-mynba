using NBA2K26CustomMyNBA.CLI.Domain;

namespace NBA2K26CustomMyNBA.CLI;

internal static class AppResources
{
    public static AppResource AnsiRegularFont => new("AnsiRegular.flf", "NBA2K26CustomMyNBA.CLI.Assets.AnsiRegular.flf");
    public static AppResource Configuration => new("Configuration.json", "NBA2K26CustomMyNBA.CLI.Assets.Configuration.json");
    public static AppResource TemplatesZip => new("templates.7z", "NBA2K26CustomMyNBA.CLI.Assets.templates.7z");

    public static AppResource LogoIFF => new("logo.iff", "NBA2K26CustomMyNBA.CLI.Assets.logo.iff");
    public static AppResource ArenaFloorIFF => new("arena_floor.iff", "NBA2K26CustomMyNBA.CLI.Assets.arena_floor.iff");
}
