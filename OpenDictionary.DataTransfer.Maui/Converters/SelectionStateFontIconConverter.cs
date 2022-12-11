#nullable enable

using System.Globalization;

using OpenDictionary.Styles.Fonts.Icons;
using OpenDictionary.ViewModels;

namespace OpenDictionary.DataTransfer.Converters;

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
