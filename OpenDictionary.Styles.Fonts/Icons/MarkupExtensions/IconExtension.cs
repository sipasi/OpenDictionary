
using System;

using Microsoft.Maui.Controls.Xaml;

namespace OpenDictionary.Styles.Fonts.Icons;

public class IconExtension : IMarkupExtension<string>
{
    public FontIcon Icon { get; set; }

    public string ProvideValue(IServiceProvider serviceProvider)
    {
        return IconDictionary.Get(Icon);
    }

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
    {
        return (this as IMarkupExtension<string>).ProvideValue(serviceProvider);
    }
}