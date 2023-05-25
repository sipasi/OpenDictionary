using System;
using System.Windows.Input;

using Microsoft.Maui.Controls;

namespace OpenDictionary.Controls.Chips;

public partial class Chip : ContentView
{
    private object? value;

    private readonly TapGestureRecognizer tapGesture = new();

    public static readonly BindableProperty TextProperty = BindableBuilder.Create<Chip, string>()
        .WithName(nameof(Text))
        .WithPropertyChanged(static (view, _, current) => view.text.Text = current);

    public static readonly BindableProperty ValueProperty = BindableBuilder.Create<Chip, string>()
        .WithName(nameof(Value))
        .WithPropertyChanged(static (view, _, current) => view.value = current);

    public static readonly BindableProperty LeftIconProperty = BindableBuilder.Create<Chip, ImageSource>()
        .WithName(nameof(LeftIcon))
        .WithPropertyChanged(static (view, _, current) => view.leftIcon.Source = current);

    public static readonly BindableProperty LeftIconVisibleProperty = BindableBuilder.Create<Chip, bool>()
        .WithName(nameof(LeftIconVisible))
        .WithDefaultValue(false)
        .WithPropertyChanged(static (view, _, current) =>
        {
            view.leftIcon.IsVisible = current;
        });

    public static readonly BindableProperty RightIconProperty = BindableBuilder.Create<Chip, ImageSource>()
        .WithName(nameof(RightIcon))
        .WithPropertyChanged(static (view, _, current) => view.rightIcon.Source = current);
    public static readonly BindableProperty RightIconVisibleProperty = BindableBuilder.Create<Chip, bool>()
        .WithName(nameof(RightIconVisible))
        .WithDefaultValue(false)
        .WithPropertyChanged(static (view, _, current) => view.rightIcon.IsVisible = current);

    public static readonly BindableProperty IsSelectedProperty = BindableBuilder.Create<Chip, bool>()
        .WithName(nameof(IsSelected))
        .WithDefaultValue(false)
        .WithPropertyChanged(static (view, _, current) => VisualStateManager.GoToState(view, view.IsSelected ? ChipVisualStates.Selected : ChipVisualStates.Normal));


    public static readonly BindableProperty TapProperty = BindableBuilder.Create<Chip, ICommand>(nameof(Tap));
    public static readonly BindableProperty TapParameterProperty = BindableBuilder.Create<Chip, object>(nameof(TapParameter));

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    public object? Value
    {
        get => value;
        set => this.value = value;
    }

    public ImageSource LeftIcon
    {
        get => (ImageSource)GetValue(LeftIconProperty);
        set => SetValue(LeftIconProperty, value);
    }
    public bool LeftIconVisible
    {
        get => (bool)GetValue(LeftIconVisibleProperty);
        set => SetValue(LeftIconVisibleProperty, value);
    }

    public ImageSource RightIcon
    {
        get => (ImageSource)GetValue(LeftIconProperty);
        set => SetValue(LeftIconProperty, value);
    }
    public bool RightIconVisible
    {
        get => (bool)GetValue(RightIconVisibleProperty);
        set => SetValue(RightIconVisibleProperty, value);
    }

    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }

    public ICommand Tap
    {
        get => (ICommand)GetValue(TapProperty);
        set => SetValue(TapProperty, value);
    }
    public object TapParameter
    {
        get => GetValue(TapParameterProperty);
        set => SetValue(TapParameterProperty, value);
    }

    public Chip()
    {
        InitializeComponent();

        leftIcon.IsVisible = false;
        rightIcon.IsVisible = false;

        Enable();
    }

    private void Tapped(object? sender, TappedEventArgs e)
    {
        IsSelected = !IsSelected;

        Tap?.Execute(TapParameter);
    }

    public void Enable()
    {
        if (root.GestureRecognizers.Count > 0)
        {
            return;
        }

        tapGesture.Tapped += Tapped;

        root.GestureRecognizers.Add(tapGesture);
    }
    public void Disable()
    {
        if (root.GestureRecognizers.Count == 0)
        {
            return;
        }

        tapGesture.Tapped -= Tapped;

        root.GestureRecognizers.Remove(tapGesture);
    }
}