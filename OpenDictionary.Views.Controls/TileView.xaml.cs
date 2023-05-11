using System.Windows.Input;

using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace OpenDictionary.Views.Controls;

public partial class TileView : Border
{
    public static readonly BindableProperty TitleProperty = BindableBuilder.Create<TileView, string>(nameof(Title))
        .WithPropertyChanged<TileView, string>(OnTitlePropertyChanged);
    public static readonly BindableProperty SubTitleProperty = BindableBuilder.Create<TileView, string>(nameof(SubTitle))
        .WithPropertyChanged<TileView, string>(OnSubTitlePropertyChanged);

    public static readonly BindableProperty TitleColorProperty = BindableBuilder.Create<TileView, Color>(nameof(TitleColor))
        .WithPropertyChanged<TileView, Color>(OnTitleColorPropertyChanged);
    public static readonly BindableProperty SubTitleColorProperty = BindableBuilder.Create<TileView, Color>(nameof(SubTitleColor))
        .WithPropertyChanged<TileView, Color>(OnSubTitleColorPropertyChanged);

    public static readonly BindableProperty TapProperty = BindableBuilder.Create<TileView, ICommand>(nameof(Tap));
    public static readonly BindableProperty LongPressProperty = BindableBuilder.Create<TileView, ICommand>(nameof(LongPress));

    public static readonly BindableProperty TapParameterProperty = BindableBuilder.Create<TileView, object>(nameof(TapParameter));

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

    public Color TitleColor
    {
        get => (Color)GetValue(TitleColorProperty);
        set => SetValue(TitleColorProperty, value);
    }
    public Color SubTitleColor
    {
        get => (Color)GetValue(SubTitleColorProperty);
        set => SetValue(SubTitleColorProperty, value);
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

    public object TapParameter
    {
        get => GetValue(TapParameterProperty);
        set => SetValue(TapParameterProperty, value);
    }

    public TileView()
    {
        InitializeComponent();
    }

    private static void OnTitlePropertyChanged(TileView view, string old, string current)
    {
        view.title.Text = current;
    }
    private static void OnSubTitlePropertyChanged(TileView view, string old, string current)
    {
        view.sub.Text = current;
    }

    private static void OnTitleColorPropertyChanged(TileView view, Color old, Color current)
    {
        view.title.TextColor = current;
    }
    private static void OnSubTitleColorPropertyChanged(TileView view, Color old, Color current)
    {
        view.sub.TextColor = current;
    }
}