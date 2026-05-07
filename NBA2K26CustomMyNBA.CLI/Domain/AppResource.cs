using Spectre.Console;
using System.Reflection;

namespace NBA2K26CustomMyNBA.CLI.Domain;
internal class AppResource(string name, string resource)
{
    public string Name => name;
    public string Resource => resource;

    public Stream GetStream() => Assembly.GetExecutingAssembly().GetManifestResourceStream(resource) ?? throw new Exception($"Could not open stream for resource {name}");

    public void SaveTo(string path, ProgressContext? onProgress = null)
    {
        using var stream = GetStream();
        using var file = new FileStream(path, FileMode.Create, FileAccess.Write);


        var task = onProgress?.AddTask($"Extracting {name} ", maxValue: stream.Length);

        var buffer = new byte[81920];
        int read;

        while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
        {
            file.Write(buffer, 0, read);
            task?.Increment(read);
        }
    }
}
