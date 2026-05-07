
using Newtonsoft.Json;

namespace NBA2K26CustomMyNBA.CLI.Domain;

[method:JsonConstructor]
internal struct Team(string id, string name, int templateId)
{
    internal Team(string name, int templateId): this(name.ToUpper(), name, templateId) { }

    [JsonProperty("id")]
    public string Id { get; } = id;

    [JsonProperty("name")]
    public string Name { get; set; } = name;

    [JsonProperty("templateId")]
    public int TemplateId { get; set; } = templateId;

    [JsonIgnore]
    public readonly string Folder => $"{AppDirectories.Teams}/{Name}";

    public override readonly bool Equals(object? obj)
    {
        return obj is Team team &&
               Id == team.Id;
    }

    public override readonly int GetHashCode()
    {
        return HashCode.Combine(Id);
    }
}
