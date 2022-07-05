using System.Windows.Input;

using Microsoft.Maui.Controls;

namespace OpenDictionary.Views.Controls;

public partial class CollectionTile : ContentView
{
    public static readonly BindableProperty TitleProperty = CreateString(nameof(Title));
    public static readonly BindableProperty SubTitleProperty = CreateString(nameof(SubTitle));
    public static readonly BindableProperty TapProperty = CreateCommand(nameof(Tap));
    public static readonly BindableProperty LongPressProperty = CreateCommand(nameof(LongPress));
    public static readonly BindableProperty CommandParameterProperty = CreateObject(nameof(CommandParameter));

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    public string SubTitle
    {
        get => (string)GetValue(SubTitleProperty);
        set => SetValue(SubTitleProperty, value);
    }

    public ICommand Tap
    {
        get => (ICommand)GetValue(TapProperty);
        set => SetValue(TapProperty, value);
    }
    public ICommand LongPress
    {
        get => (ICommand)GetValue(LongPressProperty);
        set => SetValue(LongPressProperty, value);
    }

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public CollectionTile()
    {
        InitializeComponent(); CollectionView s;
    }

    private static BindableProperty CreateObject(string name)
    {
        return BindableProperty.Create(name, typeof(object), typeof(CollectionTile), null);
    }
    private static BindableProperty CreateString(string name)
    {
        return BindableProperty.Create(name, typeof(string), typeof(CollectionTile), string.Empty);
    }
    private static BindableProperty CreateCommand(string name)
    {
        return BindableProperty.Create(name, typeof(ICommand), typeof(CollectionTile), null);
    }
}