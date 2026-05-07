namespace NBA2K26CustomMyNBA.CLI.Domain;
internal class TeamFiles(string logo, string court, string homeUniform, string awayUniform, string secondaryUniform)
{
    public string Logo => logo;
    public string Court => court;
    public string HomeUniform => homeUniform;
    public string AwayUniform => awayUniform;
    public string SecondaryUniform => secondaryUniform;

    public static TeamFiles SourceImageFiles(Team team) => new(
        $"{team.Folder}/logo.png",
        $"{team.Folder}/court_floor.png",
        $"{team.Folder}/home_uniform.png",
        $"{team.Folder}/away_uniform.png",
        $"{team.Folder}/secondary_uniform.png"
    );

    public static TeamFiles WorkImageFiles(Team team) => new(
        $"{AppDirectories.Work}/{team.Name}_logo.dds",
        $"{AppDirectories.Work}/{team.Name}_court_floor.dds",
        $"{AppDirectories.Work}/{team.Name}_home_uniform.dds",
        $"{AppDirectories.Work}/{team.Name}_away_uniform.dds",
        $"{AppDirectories.Work}/{team.Name}_secondary_uniform.dds"
    );

    public static TeamFiles TargetArchiveFiles(TeamTemplate template) => new(
        $"{AppDirectories.ModsLogos}/{template.Logo}",
        $"{AppDirectories.ModsLevels}/{template.Court}",
        $"{AppDirectories.ModsLogos}/{template.HomeUniform}",
        $"{AppDirectories.ModsLogos}/{template.AwayUniform}",
        $"{AppDirectories.ModsLogos}/{template.SecondaryUniform}"
    );
}
