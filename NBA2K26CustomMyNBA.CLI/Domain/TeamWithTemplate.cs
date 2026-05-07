namespace NBA2K26CustomMyNBA.CLI.Domain;

internal readonly struct TeamWithTemplate(Team team, TeamTemplate template)
{
    public Team Team => team;
    public TeamTemplate Template => template;

    public TeamFiles SourceImageFiles => TeamFiles.SourceImageFiles(Team);

    public TeamFiles TargetArchiveFiles => TeamFiles.TargetArchiveFiles(Template);

    public TeamFiles WorkImageFiles => TeamFiles.WorkImageFiles(Team);
}
