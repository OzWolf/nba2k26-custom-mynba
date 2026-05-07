namespace NBA2K26CustomMyNBA.CLI;

internal static class AppDirectories
{
    public static string Teams = "./teams";
    public static string Work = "./work";
    public static string ModsLogos = "./mods/logos";
    public static string ModsLevels = "./mods/levels";

    public static string AppData
    {
        get
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/NBA2K26CustomMyNBA";
        }
    }

    public static void Initialize()
    {
        if (!Directory.Exists(AppData))
            Directory.CreateDirectory(AppData);
        if(!Directory.Exists(Work))
            Directory.CreateDirectory(Work);
        if (!Directory.Exists(ModsLogos))
            Directory.CreateDirectory(ModsLogos);
        if (!Directory.Exists(ModsLevels))
            Directory.CreateDirectory(ModsLevels);
        if (!Directory.Exists(Teams))
            Directory.CreateDirectory(Teams);
    }
}
