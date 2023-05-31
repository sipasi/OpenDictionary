using System;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.Input;

using Microsoft.Maui.Controls;
using Microsoft.Maui.Media;

using OpenDictionary.Controls;
using OpenDictionary.Models;
using OpenDictionary.Styles.Fonts.Icons;
using OpenDictionary.Words.ViewModels;

namespace OpenDictionary.Words.Controls;

public partial class PhoneticControl : ContentView
{
    public static readonly BindableProperty WordProperty = BindableBuilder.Create<PhoneticControl, string>()
        .WithName(nameof(Word));

    public static readonly BindableProperty PronunciationProperty = BindableBuilder.Create<PhoneticControl, string>()
        .WithName(nameof(Pronunciation))
        .WithPropertyChanged(static (view, old, current) => view.pronunciation.Text = current);

    public static readonly BindableProperty SourceProperty = BindableBuilder.Create<PhoneticControl, string>()
        .WithName(nameof(Source))
        .WithPropertyChanged(static (view, old, current) =>
        {
            view.audioSource.Text = string.IsNullOrWhiteSpace(current)
                ? "No audio source, using TextToSpeech instead"
                : current;
        });

    public static readonly BindableProperty ViewModelProperty = BindableBuilder.Create<PhoneticControl, PhoneticViewModel?>()
        .WithName(nameof(ViewModel));

    public string Word
    {
        get => (string)GetValue(WordProperty);
        set => SetValue(WordProperty, value);
    }

    public string Pronunciation
    {
        get => (string)GetValue(PronunciationProperty);
        set => SetValue(PronunciationProperty, value);
    }

    public string Source
    {
        get => (string)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    public required PhoneticViewModel? ViewModel
    {
        get => (PhoneticViewModel?)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    public PhoneticControl()
    {
        InitializeComponent();

        Loaded += OnLoaded;

        play.Command = new AsyncRelayCommand(OnPlay);
    }

    private void OnLoaded(object? sender, EventArgs e) => UpdateChacheIcon();

    private async Task OnPlay()
    {
        if (ViewModel is not PhoneticViewModel viewModel)
        {
            return;
        }

        if (Source is not string source || Word is not string word)
        {
            return;
        }

        await LoadAudioIfNotExists(viewModel, word, source);

        await viewModel.PlayAudio(word: word, source: source);

        UpdateChacheIcon();
    }

    private void UpdateChacheIcon()
    {
        if (ViewModel is not PhoneticViewModel viewModel)
        {
            return;
        }

        bool cached = viewModel.CacheContains(Word, Source);

        cacheIcon.FontFamily = AppIcons.Asset.FontFamily;

        cacheIcon.Text = cached switch
        {
            true => IconDictionary.Get(FontIcon.CloudDone),
            false => IconDictionary.Get(FontIcon.Cloud),
        };
    }

    private async Task LoadAudioIfNotExists(PhoneticViewModel viewModel, string word, string source)
    {
        if (viewModel.CacheContains(word, source))
        {
            return;
        }

        LoadState(running: true);

        await viewModel.LoadAudioIfNotExists(word, source);

        LoadState(running: false);
    }

    private void LoadState(bool running)
    {
        cacheIcon.IsVisible = !running;
        activity.IsRunning = running;
    }
}