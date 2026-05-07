using Newtonsoft.Json;

namespace NBA2K26CustomMyNBA.CLI.Domain;

[method: JsonConstructor]
internal readonly struct TeamTemplate(int id, bool expansion, string name, string logo, string court, string homeUniform, string awayUniform, string secondaryUniform)
{
    public int Id { get; } = id;

    public bool Expansion { get; } = expansion;

    public string Name { get; } = name;

    public string Logo { get; } = logo;

    public string Court { get; } = court;

    public string HomeUniform { get; } = homeUniform;

    public string AwayUniform { get; } = awayUniform;

    public string SecondaryUniform { get; } = secondaryUniform;
}
