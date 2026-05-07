using Spectre.Console;
using System.Diagnostics;

namespace NBA2K26CustomMyNBA.CLI;
internal static class ProcessUtils
{
    public static bool Execute(string executable, string arguments)
    {
        try
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = executable,
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = false,
                RedirectStandardError = true,
                CreateNoWindow = true,
            };

            using var process = Process.Start(startInfo);
            if (process == null) return false;
            process.WaitForExit();
            return true;

        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[bold red]{ex.Message}[/]");
            return false;
        }
    }
}
