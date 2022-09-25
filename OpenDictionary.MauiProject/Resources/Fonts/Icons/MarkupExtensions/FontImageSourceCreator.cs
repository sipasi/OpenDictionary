using Microsoft.Maui.Controls;

namespace OpenDictionary.Fonts.Icons;

public static class FontImageSourceCreator
{
    public static FontImageSource Create(FontIcon icon, double? size = null, object? colorResource = null)
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

        if (colorResource is not null)
        {
            var resource = colorResource as Microsoft.Maui.Controls.Internals.DynamicResource;

            if (resource is not null)
            {
                font.SetDynamicResource(FontImageSource.ColorProperty, resource.Key);
            }
        }

        return font;
    }
}