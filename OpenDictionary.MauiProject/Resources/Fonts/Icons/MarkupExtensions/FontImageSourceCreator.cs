using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace OpenDictionary.Fonts.Icons;

public static class FontImageSourceCreator
{
    public static FontImageSource Create(FontIcon icon, double? size = null, object? color = null)
    {
        var font = new FontImageSource()
        {
            FontFamily = AppIcons.Asset.FontFamily,
            Glyph = IconDictionary.Get(icon),
        };

        if (size is not null)
        {
            font.Size = size.Value;
        }

        if (color is Color mauiColor)
        {
            font.Color = mauiColor;
        }
        else if (color is Microsoft.Maui.Controls.Internals.DynamicResource resource)
        {
            font.SetDynamicResource(FontImageSource.ColorProperty, resource.Key);
        }

        return font;
    }
}