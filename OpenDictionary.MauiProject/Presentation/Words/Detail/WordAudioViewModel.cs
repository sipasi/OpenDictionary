#nullable enable

using System;
using System.Diagnostics;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using OpenDictionary.Services.Audio;

namespace OpenDictionary.Words.ViewModels;

public sealed partial class WordAudioViewModel : ObservableObject
{
    private readonly IAudioPlayerServise player;
    private readonly IPhoneticFilesService files;

    [ObservableProperty]
    private string? word;
    [ObservableProperty]
    private string? path;

    public AsyncRelayCommand PlayAudioCommand { get; set; }

    public WordAudioViewModel(IAudioPlayerServise player, IPhoneticFilesService files)
    {
        this.player = player;
        this.files = files;

        PlayAudioCommand = new(PlayAudio);
    }

    private async Task PlayAudio()
    {
        string? word = Word;
        string? path = Path;

        if (string.IsNullOrWhiteSpace(word) || string.IsNullOrWhiteSpace(path))
        {
            return;
        }

        try
        {
            string? source = await GetOrLoadFilePath(word, url: path);

            if (source == null)
            {
                return;
            }

            player.Play(source);
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