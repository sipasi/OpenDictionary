using System;

using Microsoft.Maui.Controls;

using OpenDictionary.Controls;
using OpenDictionary.Observables.Metadatas;

namespace OpenDictionary.Words.Controls;

public partial class MeaningControl : ContentView
{
    public static readonly BindableProperty MeaningProperty = BindableBuilder.Create<MeaningControl, MeaningObservable>()
        .WithName(nameof(Meaning))
        .WithPropertyChanged(OnMeaningChanged);

    public MeaningObservable Meaning
    {
        get => (MeaningObservable)GetValue(MeaningProperty);
        set => SetValue(MeaningProperty, value);
    }

    public MeaningControl()
    {
        InitializeComponent();
    }

    private static void OnMeaningChanged(MeaningControl owner, MeaningObservable? old, MeaningObservable? current)
    {
        if (current is null)
        {
            return;
        }

        owner.partOfSpeech.Text = current.PartOfSpeech;
        owner.synonyms.Text = current.Synonyms;
        owner.antonyms.Text = current.Antonyms;

        if (current.Definitions?.Count > 0)
        {
            BindableLayout.SetItemsSource(owner.definitionLayout, current.Definitions);
        }
    }
}