using System;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;

using OpenDictionary.Controls;
using OpenDictionary.MauiProject;
using OpenDictionary.Services.Audio;
using OpenDictionary.Styles.Fonts.Icons;
using OpenDictionary.Words.ViewModels;

namespace OpenDictionary.Words.Controls;

public partial class PhoneticControl : ContentView
{
    private readonly WordAudioViewModel? viewModel;

    public static readonly BindableProperty WordProperty = BindableBuilder.Create<PhoneticControl, string>()
        .WithName(nameof(Word));

    public static readonly BindableProperty PronunciationProperty = BindableBuilder.Create<PhoneticControl, string>()
        .WithName(nameof(Pronunciation))
        .WithPropertyChanged((view, old, current) => view.pronunciation.Text = current);

    public static readonly BindableProperty SourseProperty = BindableBuilder.Create<PhoneticControl, string>()
        .WithName(nameof(Sourse))
        .WithPropertyChanged((view, old, current) => view.audioSource.Text = current);

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

    public PhoneticControl()
    {
        InitializeComponent();

        Loaded += OnLoaded;

        IServiceProvider? services = Application.Current?.MainPage?.Handler?.MauiContext?.Services;


        if (services is not null)
        {
            var audio = services.GetService<IAudioPlayerServise>();
            var files = services.GetService<IPhoneticFilesService>();

            viewModel = new(audio!, files!);

            play.Command = new AsyncRelayCommand(OnPlay);
        }
    }

    private void OnLoaded(object? sender, EventArgs e) => UpdateChacheIcon();

    private async Task OnPlay()
    {
        if (viewModel is null)
        {
            return;
        }

        await viewModel.PlayAudio(word: Word, source: Sourse);

        UpdateChacheIcon();
    }

    private void UpdateChacheIcon()
    {
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
}