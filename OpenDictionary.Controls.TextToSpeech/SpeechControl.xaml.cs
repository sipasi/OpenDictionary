

using CommunityToolkit.Mvvm.Input;

namespace OpenDictionary.Controls.SpeechSynthesis;

public partial class SpeechControl : ContentView
{
    public static readonly BindableProperty TextProperty = BindableBuilder.Create<SpeechControl, string?>()
        .WithName(nameof(Text))
        .WithPropertyChanged(static (view, old, current) => view.text.Text = current);

    public static readonly BindableProperty ImageSourceProperty = BindableBuilder.Create<SpeechControl, ImageSource?>()
        .WithName(nameof(ImageSource))
        .WithPropertyChanged(static (view, old, current) => view.play.Source = current);

    public string? Text
    {
        get => (string?)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public ImageSource? ImageSource
    {
        get => (ImageSource?)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }

    public SpeechControl()
    {
        InitializeComponent();

        play.Command = new AsyncRelayCommand(OnPlay);
    }

    private async Task OnPlay()
    {
        if (TextToSpeech.Default is not ITextToSpeech speech || Text is not string text)
        {
            return;
        }

        await speech.SpeakAsync(text);
    }
}