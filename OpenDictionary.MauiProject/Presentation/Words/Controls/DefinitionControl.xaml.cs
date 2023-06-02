using Microsoft.Maui.Controls;

using OpenDictionary.Controls;
using OpenDictionary.Observables.Metadatas;

namespace OpenDictionary.Words.Controls;

public partial class DefinitionControl : ContentView
{
    public static readonly BindableProperty DefinitionProperty = BindableBuilder.Create<DefinitionControl, DefinitionObservable>()
        .WithName(nameof(Definition))
        .WithPropertyChanged(OnDefinitionChanged);

    public DefinitionObservable Definition
    {
        get => (DefinitionObservable)GetValue(DefinitionProperty);
        set => SetValue(DefinitionProperty, value);
    }
    public DefinitionControl()
    {
        InitializeComponent();
    }

    private static void OnDefinitionChanged(DefinitionControl owner, DefinitionObservable? old, DefinitionObservable? current)
    {
        if (current is null)
        {
            return;
        }

        owner.definition.Text = current.Value;
        owner.example.Text = current.Example;
    }
}