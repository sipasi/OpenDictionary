using System;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.Input;

using Microsoft.Maui.Controls;

using OpenDictionary.Controls;
using OpenDictionary.Styles.Fonts.Icons;
using OpenDictionary.Words.ViewModels;

namespace OpenDictionary.Words.Controls;

public partial class PhoneticControl : ContentView
{
    public static readonly BindableProperty WordProperty = BindableBuilder.Create<PhoneticControl, string>()
        .WithName(nameof(Word));

    public static readonly BindableProperty PronunciationProperty = BindableBuilder.Create<PhoneticControl, string>()
        .WithName(nameof(Pronunciation))
        .WithPropertyChanged((view, old, current) => view.pronunciation.Text = current);

    public static readonly BindableProperty SourseProperty = BindableBuilder.Create<PhoneticControl, string>()
        .WithName(nameof(Sourse))
        .WithPropertyChanged((view, old, current) => view.audioSource.Text = current);

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

    public string Sourse
    {
        get => (string)GetValue(SourseProperty);
        set => SetValue(SourseProperty, value);
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
        PhoneticViewModel? viewModel = ViewModel;

        if (viewModel is null || Word is null || Sourse is null)
        {
            return;
        }

        await LoadAudioIfNotExists(viewModel);

        await viewModel.PlayAudio(word: Word, source: Sourse);

        UpdateChacheIcon();
    }

    private void UpdateChacheIcon()
    {
        PhoneticViewModel? viewModel = ViewModel;

        if (viewModel is null)
        {
            return;
        }

        bool cached = viewModel.CacheContains(Word, Sourse);

        cacheIcon.FontFamily = AppIcons.Asset.FontFamily;

        cacheIcon.Text = cached switch
        {
            true => IconDictionary.Get(FontIcon.CloudDone),
            false => IconDictionary.Get(FontIcon.Cloud),
        };
    }

    private async Task LoadAudioIfNotExists(PhoneticViewModel viewModel)
    {
        string word = Word;
        string source = Sourse;

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