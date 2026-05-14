using Newtonsoft.Json;

namespace NBA2K26CustomMyNBA.CLI.Domain;

[method: JsonConstructor]
internal readonly struct TeamTemplate(int id, bool expansion, string name, string logo, string court)
{
    public int Id { get; } = id;

    public bool Expansion { get; } = expansion;

    public string Name { get; } = name;

    public string Logo { get; } = logo;

    public string Court { get; } = court;
}
