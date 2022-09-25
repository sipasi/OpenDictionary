
using System;

using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace OpenDictionary.Fonts.Icons;

public class FontIconExtension : IMarkupExtension<FontImageSource>
{
    public FontIcon Icon { get; set; }
    public object? Color { get; set; }

    public double? Size { get; set; }

    public FontImageSource ProvideValue(IServiceProvider serviceProvider)
    {
        return FontImageSourceCreator.Create(Icon, Size, Color);
    }

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
    {
        return (this as IMarkupExtension<FontImageSource>).ProvideValue(serviceProvider);
    }
}