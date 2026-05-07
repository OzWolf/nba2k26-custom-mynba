using Newtonsoft.Json;

namespace NBA2K26CustomMyNBA.CLI.Domain;

[method: JsonConstructor]
internal class League(string id, string name, List<Team> teams)
{
    private readonly string _id = id;
    private readonly string _name = name;
    [JsonProperty("teams")]
    private readonly List<Team> _teams = teams;

    internal League(string name) : this(Guid.NewGuid().ToString(), name, []) { }

    [JsonProperty("id")]
    public string Id => _id;

    [JsonProperty("name")]
    public string Name => _name;

    [JsonIgnore]
    public List<TeamWithTemplate> Teams
    {
        get {
            var templates = AppConfiguration.TeamTemplates;
            return [.. _teams.Select(t =>
            {
                var template = templates.First(tmp => tmp.Id == t.TemplateId);
                return new TeamWithTemplate(t, template);
            })];
        }
    }

    [JsonIgnore]
    public List<TeamTemplate> AvailableTemplates
    {
        get
        {
            var templates = AppConfiguration.TeamTemplates;
            var usedIds = _teams.Select(t => t.TemplateId).Distinct();
            return templates.FindAll(t => !usedIds.Contains(t.Id));
        }
    }

    public bool Exists(Team team)
    {
        return _teams.Any(t => t.Id == team.Id || t.Name.ToUpper() == team.Name.ToUpper());
    }

    public void AddTeam(Team team)
    {
        if (Exists(team))
            throw new InvalidOperationException("Team [ " + team.Name + " ] already exists.");
        _teams.Add(team);
    }

    public void RemoveTeam(Team team)
    {
        _teams.Remove(team);
    }
}
