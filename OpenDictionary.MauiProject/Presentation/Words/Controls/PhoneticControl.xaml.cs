using System;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Maui.Controls;

using OpenDictionary.Controls;
using OpenDictionary.Styles.Fonts.Icons;

namespace OpenDictionary.Words.Controls;

public readonly record struct PlayAudioInfo(string? Word, string? Source);

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

    public static readonly BindableProperty IsCahceExistsProperty = BindableBuilder.Create<PhoneticControl, bool>()
        .WithName(nameof(IsCahceExists))
        .WithPropertyChanged(static (view, old, current) =>
        {
            view.cacheIcon.FontFamily = AppIcons.Asset.FontFamily;

            view.cacheIcon.Text = current switch
            {
                true => IconDictionary.Get(FontIcon.CloudDone),
                false => IconDictionary.Get(FontIcon.Cloud),
            };
        });

    public static readonly BindableProperty PlayCommandProperty = BindableBuilder.Create<PhoneticControl, IAsyncRelayCommand<PlayAudioInfo>>()
        .WithName(nameof(PlayCommand));

    private async Task OnPlay()
    {
        if (PlayCommand is null)
        {
            return;
        }

        await PlayCommand.ExecuteAsync(new()
        {
            Word = Word,
            Source = Sourse,
        });
    }

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

    public bool IsCahceExists
    {
        get => (bool)GetValue(IsCahceExistsProperty);
        set => SetValue(IsCahceExistsProperty, value);
    }

    public IAsyncRelayCommand<PlayAudioInfo> PlayCommand
    {
        get => (IAsyncRelayCommand<PlayAudioInfo>)GetValue(PlayCommandProperty);
        set => SetValue(PlayCommandProperty, value);
    }

    public PhoneticControl()
    {
        InitializeComponent();

        play.Command = new AsyncRelayCommand(OnPlay);
    }
}