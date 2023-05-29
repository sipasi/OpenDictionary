#nullable enable

using System;
using System.Diagnostics;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using OpenDictionary.Services.Audio;
using OpenDictionary.Words.Controls;

namespace OpenDictionary.Words.ViewModels;

public sealed class WordAudioViewModel
{
    private readonly IAudioPlayerServise player;
    private readonly IPhoneticFilesService files;

    public AsyncRelayCommand<PlayAudioInfo> PlayAudioCommand { get; set; }

    public WordAudioViewModel(IAudioPlayerServise player, IPhoneticFilesService files)
    {
        this.player = player;
        this.files = files;

        PlayAudioCommand = new(PlayAudio);
    }

    private async Task PlayAudio(PlayAudioInfo info)
    {
        string? word = info.Word;
        string? source = info.Source;

        if (string.IsNullOrWhiteSpace(word) || string.IsNullOrWhiteSpace(source))
        {
            return;
        }

        try
        {
            string? path = await GetOrLoadFilePath(word, url: source);

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

    private async ValueTask<string?> GetOrLoadFilePath(string word, string url)
    {
        if (files.Contains(url, word) || await files.AddFromWebAsync(url, word))
        {
            return files.GetFilePath(url, word);
        }

        return url;
    }
}