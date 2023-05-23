using System.Windows.Input;

using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace OpenDictionary.Views.Controls;

public partial class TileView : Border
{
    public static readonly BindableProperty TitleProperty = BindableBuilder.Create<TileView, string>()
        .WithName(nameof(Title))
        .WithPropertyChanged(static (view, _, current) => view.title.Text = current);
    public static readonly BindableProperty SubTitleProperty = BindableBuilder.Create<TileView, string>()
        .WithName(nameof(SubTitle))
        .WithPropertyChanged<TileView, string>(static (view, _, current) => view.sub.Text = current);

    public static readonly BindableProperty TitleColorProperty = BindableBuilder.Create<TileView, Color>()
        .WithName(nameof(TitleColor))
        .WithPropertyChanged(static (view, _, current) => view.title.TextColor = current);
    public static readonly BindableProperty SubTitleColorProperty = BindableBuilder.Create<TileView, Color>()
        .WithName(nameof(SubTitleColor))
        .WithPropertyChanged(static (view, _, current) => view.sub.TextColor = current);

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
}