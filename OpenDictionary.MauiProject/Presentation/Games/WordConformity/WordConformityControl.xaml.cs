using System.Linq;

using Microsoft.Maui.Controls;

using OpenDictionary.Controls;
using OpenDictionary.Games.WordConformities.Observables;
using OpenDictionary.Games.WordConformities.ViewModels;

namespace OpenDictionary.Games.WordConformities.Controls;

public partial class WordConformityControl : ContentView
{
    public static readonly BindableProperty WordConformityProperty = BindableBuilder.Create<WordConformityControl, WordConformityViewModel>()
        .WithName(nameof(WordConformity))
        .WithPropertyChanged(WordConformityChanged);

    public WordConformityViewModel WordConformity { get; set; } = null!;

    public WordConformityControl()
    {
        InitializeComponent();
    }

    private static void WordConformityChanged(WordConformityControl control, WordConformityViewModel? old, WordConformityViewModel? current)
    {
        if (current is null)
        {
            return;
        }

        control.WordConformity = current;

        control.BindButtons();
    }

    private void BindButtons()
    {
        var buttons = answerButtons.Children;

        foreach (Button button in buttons.Cast<Button>())
        {
            AnswerButtonObservable answer = new();

            BindButton(button, answer);

            WordConformity.Buttons.Collection.Add(answer);
        }
    }
    private void BindButton(Button button, AnswerButtonObservable answer)
    {
        Binding binding = new(path: nameof(AnswerButtonObservable.Text));

        button.SetBinding(Button.TextProperty, binding);

        button.BindingContext = answer;
        button.Command = WordConformity.Buttons.TappedCommand;
        button.CommandParameter = answer;
    }
}