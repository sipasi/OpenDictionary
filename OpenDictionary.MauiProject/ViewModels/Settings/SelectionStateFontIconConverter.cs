#nullable enable

using System;
using System.Collections.Generic;
using System.Globalization;

using Microsoft.Maui.Controls;

using OpenDictionary.Fonts.Icons;

namespace OpenDictionary.ViewModels;

public sealed class SelectionStateFontIconConverter : IValueConverter
{
    private static readonly Dictionary<ISelectionState, FontImageSource> pairs = new()
    {
        [SelectionStates.None] = FontImageSourceCreator.Create(FontIcon.CheckboxBlankCircle),
        [SelectionStates.Indeterminate] = FontImageSourceCreator.Create(FontIcon.CheckboxIndeterminateCircle),
        [SelectionStates.All] = FontImageSourceCreator.Create(FontIcon.CheckboxCircle),
    };

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var state = (value as ISelectionState) ?? SelectionStates.None;

        return pairs[state];
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
