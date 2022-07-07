#nullable enable

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OpenDictionary.Services.Audio;

public class AudioStorage
{
    private readonly AudioDownloader downloader;

    private readonly DirectoryInfo directory;
    private readonly HashSet<string> extensions;

    public AudioStorage(string path, params string[] extensions)
    {
        directory = new DirectoryInfo(path);

        downloader = new AudioDownloader();

        this.extensions = new HashSet<string>(extensions);

        directory.Create();
    }

    public Task<bool> AddFromWebAsync(string address, string word)
    {
        string extension = Path.GetExtension(address);

        if (!extensions.Contains(extension))
        {
            return Task.FromResult(false);
        }

        return Download(address, word);
    }

    public bool Any(string word)
    {
        string? path = GetSubdirectoryPath(word);

        if (path is null)
        {
            return false;
        }

        int count = Directory.EnumerateFiles(path).Count();

        return count > 0;
    }

    public bool Contains(string address, string word)
    {
        string? path = GetFilePath(address, word);

        if (path is null)
        {
            return false;
        }

        bool exist = File.Exists(path);

        return exist;
    }

    public string? GetFilePath(string address, string word)
    {
        string? subdirectory = GetSubdirectoryPath(word.ToLower());

        if (subdirectory is null)
        {
            return default;
        }

        string name = Path.GetFileName(address);

        return Path.Combine(subdirectory, name);
    }

    private Task<bool> Download(string address, string word)
    {
        DirectoryInfo current = GetSubdirectory(word.ToLower());

        return downloader.Download(current, address);
    }

    private string? GetSubdirectoryPath(string word)
    {
        string path = Path.Combine(directory.FullName, word.ToLower());

        if (Directory.Exists(path) is false)
        {
            return null;
        }

        return path;
    }
    private DirectoryInfo GetSubdirectory(string word)
    {
        return directory.CreateSubdirectory(word.ToLower());
    }
}