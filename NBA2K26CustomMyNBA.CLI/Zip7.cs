using Spectre.Console;

namespace NBA2K26CustomMyNBA.CLI;
internal static class Zip7
{
    public static void Verify()
    {
        if (!ProcessUtils.Execute("7z.exe", "-?"))
            throw new Exception("Missing 7z.exe tool. Download from https://www.7-zip.org/download.html and install.");
    }

    public static void AddToArchive(string sourceFile, string archiveFile, string archiveFileRename)
    {
        if (!ProcessUtils.Execute("7z.exe", $"a \"{archiveFile}\" \"{sourceFile}\""))
            throw new Exception($"Failed to add {sourceFile} to {archiveFile} archive.");

        var fileName = Path.GetFileName(sourceFile);
        
        if (!ProcessUtils.Execute("7z.exe", $"rn \"{archiveFile}\" \"{fileName}\" \"{archiveFileRename}\""))
            throw new Exception("Failed to rename file in archive correctly.  Mod will not work.");
    }
}
