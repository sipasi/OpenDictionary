#nullable enable

using System;
using System.Diagnostics;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.Input;

using OpenDictionary.Services.Audio;

namespace OpenDictionary.ViewModels.Words.Commands;

public sealed class WordAudioPlayerCommands
{
    private readonly WordDetailViewModel viewModel;
    private readonly IAudioPlayerServise player;
    private readonly IPhoneticFilesService files;

    public AsyncRelayCommand<string?> PlayAudioCommand { get; set; }

    public WordAudioPlayerCommands(WordDetailViewModel viewModel, IAudioPlayerServise player, IPhoneticFilesService files)
    {
        this.viewModel = viewModel;
        this.player = player;
        this.files = files;

        PlayAudioCommand = new(PlayAudio);
    }

    private async Task PlayAudio(string? path)
    {
        if (string.IsNullOrWhiteSpace(path) || string.IsNullOrWhiteSpace(viewModel.Word.Origin))
        {
            return;
        }

        try
        {
            string? source = await GetOrLoadFilePath(viewModel.Word.Origin, url: path);

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