using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace OpenDictionary.Views.Controls;

public class TextInputLine : Entry
{
    public TextInputLine()
    {
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
        {
            if (view is TextInputLine)
            {
#if ANDROID
                handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToPlatform());
#elif WINDOWS
                handler.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
#endif
            }
        });
    }
}