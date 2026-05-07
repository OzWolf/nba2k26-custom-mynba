using Newtonsoft.Json;

namespace NBA2K26CustomMyNBA.CLI.Domain;

[method: JsonConstructor]
class AppConfiguration(List<TeamTemplate> teamTemplates)
{
    private readonly List<TeamTemplate> _teamTemplates = teamTemplates;

    private static readonly AppConfiguration Instance;

    public static List<TeamTemplate> TeamTemplates => Instance._teamTemplates;

    static AppConfiguration()
    {
        using var stream = AppResources.Configuration.GetStream();
        Instance = JsonUtils.DeserializeResource<AppConfiguration>(stream);
    }
}
