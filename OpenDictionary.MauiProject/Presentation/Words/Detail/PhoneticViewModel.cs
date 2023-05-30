#nullable enable

using System;
using System.Diagnostics;
using System.Threading.Tasks;

using OpenDictionary.Services.Audio;

namespace OpenDictionary.Words.ViewModels;

public sealed class PhoneticViewModel
{
    private readonly IAudioPlayerServise player;
    private readonly IPhoneticFilesService files;

    public PhoneticViewModel(IAudioPlayerServise player, IPhoneticFilesService files)
    {
        this.player = player;
        this.files = files;
    }

    public bool CacheContains(string? word, string? source)
    {
        if (word is null || source is null)
        {
            return false;
        }

        return files.Contains(word, source);
    }

    public async ValueTask PlayAudio(string? word, string? source)
    {
        if (string.IsNullOrWhiteSpace(word) || string.IsNullOrWhiteSpace(source))
        {
            return;
        }

        try
        {
            string? path = await GetOrLoadFilePath(word, address: source);

            if (path == null)
            {
                return;
            }

            player.Play(path);
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
        }
    }

    private async ValueTask<string?> GetOrLoadFilePath(string word, string address)
    {
        if (files.Contains(word, address) || await files.AddFromWebAsync(word, address))
        {
            return files.GetFilePath(word, address);
        }

        return address;
    }
}