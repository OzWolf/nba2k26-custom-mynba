namespace NBA2K26CustomMyNBA.CLI.Domain;

internal readonly struct UniformSlots(string homeUniform, string awayUniform, string secondaryUniform)
{
    public string HomeUniform => homeUniform;
    public string AwayUniform => awayUniform;
    public string SecondaryUniform => secondaryUniform;
}