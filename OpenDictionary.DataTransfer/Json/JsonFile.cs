using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace OpenDictionary.DataTransfer.Json;

internal sealed class JsonFile : IFile
{
    private string? fullPath;

    public required string Path { get; init; }
    public required string Name { get; init; }

    public required JsonSerializerOptions Options { get; init; }

    public string FullPath
    {
        get => fullPath is not null
            ? fullPath
            : fullPath = System.IO.Path.Combine(Path, $"{Name}.json");
    }

    public async ValueTask<T?> Read<T>()
    {
        if (File.Exists(FullPath) is false)
        {
            return default;
        }

        string json = await File.ReadAllTextAsync(FullPath);

        try
        {
            return JsonSerializer.Deserialize<T>(json, Options);
        }
        catch (Exception exception)
        {
            string message = GetErrorMessage<T>(exception);

            Debug.WriteLine(message);
        }

        return default;
    }
    public async ValueTask Write<T>(T data)
    {
        await using StreamWriter file = File.CreateText(FullPath);

        try
        {
            string json = JsonSerializer.Serialize(data, Options);

            await file.WriteAsync(json);

            await file.FlushAsync();
        }
        catch (Exception exception)
        {
            string message = GetErrorMessage<T>(exception);

            Debug.WriteLine(message);
        }
    }

    private string GetErrorMessage<T>(Exception exception, [CallerMemberName] string? operation = null)
    {
        StringBuilder builder = new StringBuilder(200);

        builder.Append($"Cant {operation} data[{typeof(T)}] to the path:{FullPath}.");

        builder.Append($" Error: {exception.Message}.");

        if (exception.InnerException is not null)
        {
            builder.Append($" Inner message: {exception.InnerException.Message}");
        }

        return builder.ToString();
    }
}
