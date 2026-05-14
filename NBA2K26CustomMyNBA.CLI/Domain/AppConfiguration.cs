using Newtonsoft.Json;

namespace NBA2K26CustomMyNBA.CLI.Domain;

[method: JsonConstructor]
internal class AppConfiguration(List<TeamTemplate> teamTemplates, List<string> uniformSlots)
{
    private readonly List<TeamTemplate> _teamTemplates = teamTemplates;

    private static readonly AppConfiguration Instance;

    public static List<TeamTemplate> TeamTemplates => Instance._teamTemplates;
    
    public static UniformSlots UniformSlotsFor(int teamIndex) => Instance._UniformSlotsFor(teamIndex);

    static AppConfiguration()
    {
        using var stream = AppResources.Configuration.GetStream();
        Instance = JsonUtils.DeserializeResource<AppConfiguration>(stream);
    }

    private UniformSlots _UniformSlotsFor(int teamIndex)
    {
        var startIndex = (teamIndex - 1) * 3;
        return new UniformSlots(
            uniformSlots[startIndex],
            uniformSlots[startIndex + 1],
            uniformSlots[startIndex + 2]
        );
    }
}
