using Newtonsoft.Json;

namespace NBA2K26CustomMyNBA.CLI;

internal static class JsonUtils
{
    public static T DeserializeFile<T>(string path)
    {
        if (!File.Exists(path)) throw new InvalidOperationException("File [ " + path + " ] does not exist.");
        using var stream = File.OpenRead(path);
        using var reader = new StreamReader(stream);
        return Deserialize<T>(reader.ReadToEnd());
    }

    public static T DeserializeResource<T>(Stream stream)
    {
        using var reader = new StreamReader(stream);
        return Deserialize<T>(reader.ReadToEnd());
    }

    public static string Serialize(Object value)
    {
        return JsonConvert.SerializeObject(value);
    }

    private static T Deserialize<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json) ?? throw new InvalidOperationException("JSON deserialization created a NULL value.");
    }
}
