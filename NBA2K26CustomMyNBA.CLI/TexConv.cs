using Spectre.Console;

namespace NBA2K26CustomMyNBA.CLI;
internal static class TexConv
{
    public static void Verify()
    {
        if (!ProcessUtils.Execute("texconv.exe", "--help"))
            throw new Exception("Missing texconv.exe tool.  Download from https://github.com/microsoft/DirectXTex/wiki/texconv and install.");
    }

    public static void Convert(string sourcePNG, string targetDDS, string compression)
    {
        var targetDirectory = Path.GetDirectoryName(targetDDS);
        var sourceFileName = Path.GetFileNameWithoutExtension(sourcePNG);
        var targetFile = $"{targetDirectory}/{sourceFileName}.dds";


        if (!ProcessUtils.Execute("texconv.exe", $"-y -nologo -f {compression} -o \"{targetDirectory}\" \"{sourcePNG}\""))
            throw new Exception($"Failed to convert {sourcePNG} to DDS.");

        File.Move(targetFile, targetDDS, true);
    }
}
