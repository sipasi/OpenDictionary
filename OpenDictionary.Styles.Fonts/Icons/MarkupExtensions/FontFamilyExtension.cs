
using System;

using Microsoft.Maui.Controls.Xaml;

namespace OpenDictionary.Styles.Fonts.Icons;

public class FontFamilyExtension : IMarkupExtension<string>
{
    public string ProvideValue(IServiceProvider serviceProvider)
    {
        return AppIcons.Asset.FontFamily;
    }

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
    {
        return (this as IMarkupExtension<string>).ProvideValue(serviceProvider);
    }
}